using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.View_Models;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.Managers
{
    public class RevisionDetailsRepository : IRevisionDetailsRepository
    {
        private AccDbContext _dbContext;

        public RevisionDetailsRepository(AccDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<RevisionDetailsList> GetRevisionDetails(int RevisionId)
        {
            var results = from b in _dbContext.TblRevisionDetails
                          where b.RdRevisionId == RevisionId

                          select new RevisionDetailsList
                          {
                              RdResourceSeq = b.RdResourceSeq,
                              RdPrice = b.RdPrice
                          };
            return results.ToList();
        }

        public bool AddRevision(int PackageSupplierId, DateTime PackSuppDate, IFormFile ExcelFile,int curId, double ExchRate)
        {
            int LastRevNo = GetMaxRevisionNumber(PackageSupplierId);

            if (LastRevNo != -1)
            {
                int i = LastRevNo;
                do
                {
                    var res = _dbContext.TblSupplierPackageRevisions.SingleOrDefault(b => b.PrRevNo == i && b.PrPackSuppId == PackageSupplierId);
                    if (res != null)
                    {
                        res.PrRevNo = i + 1;
                        _dbContext.SaveChanges();
                    }
                    i--;
                }
                while (i >= 0);
            }

            var result = new TblSupplierPackageRevision { PrRevNo = 0, PrPackSuppId = PackageSupplierId, PrTotPrice = 0, PrRevDate = PackSuppDate, PrCurrency = curId, PrExchRate = ExchRate };
            _dbContext.Add<TblSupplierPackageRevision>(result);
            _dbContext.SaveChanges();

            //Get inserted Revison ID
            result = _dbContext.TblSupplierPackageRevisions.SingleOrDefault(b => (b.PrPackSuppId == PackageSupplierId) && (b.PrRevNo == 0));
            int revId = result.PrRevId;

            InsertRevisionDetail(revId, ExcelFile);
            UpdateTotalPrice(revId);

            return true;
        }

        private void UpdateTotalPrice(int revId)
        {
            var result = _dbContext.TblSupplierPackageRevisions.SingleOrDefault(b => b.PrRevNo == 0 && b.PrRevId == revId);
            if (result != null)
            {
                var TotalPrice = (from b in _dbContext.TblRevisionDetails
                                  where b.RdRevisionId == revId
                                  select b).Sum(e => (e.RdPrice * e.RdQty));

                result.PrTotPrice = (decimal)TotalPrice;
                _dbContext.SaveChanges();
            }
        }

        private void InsertRevisionDetail(int revId, IFormFile ExcelFile)
        {
            if (ExcelFile?.Length > 0)
            {
                var stream = ExcelFile.OpenReadStream();
                List<TblRevisionDetail> LstRevDetails = new List<TblRevisionDetail>();
                List<TblMissingPrice> LstMissingPrice = new List<TblMissingPrice>();

                try
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.First();
                        var rowCount = worksheet.Dimension.Rows;

                        //RemoveExistingMissing
                        _dbContext.TblMissingPrices.RemoveRange(_dbContext.TblMissingPrices.Where(c => c.RevisionId == revId));
                        _dbContext.SaveChanges();

                        string oldBoqRef = "";

                        for (var row = 2; row <= rowCount; row++)
                        {
                            try
                            {
                                string boqRef = worksheet.Cells[row, 1].Value == null ? "" : worksheet.Cells[row, 1].Value.ToString();
                                string boqDesc = worksheet.Cells[row, 3].Value == null ? "" : worksheet.Cells[row, 3].Value.ToString();
                                string boqQty = worksheet.Cells[row, 5].Value == null ? "" : worksheet.Cells[row, 5].Value.ToString();

                                if (((boqRef != "") && (boqDesc != "") && (boqQty != "")) || ((oldBoqRef != "") && ((worksheet.Cells[row, 6].Value == null ? "" : worksheet.Cells[row, 6].Value.ToString()) != "")))
                                {

                                    string resCode = worksheet.Cells[row, 7].Value == null ? "" : worksheet.Cells[row, 7].Value.ToString();
                                    double resQty = worksheet.Cells[row, 10].Value == null ? 0 : (double)worksheet.Cells[row, 10].Value;
                                    double resPrice = (worksheet.Cells[row, 11].Value == null) ? 0 : (double)worksheet.Cells[row, 11].Value;
                                    string resComment = worksheet.Cells[row, 12].Value == null ? "" : worksheet.Cells[row, 12].Value.ToString();
                                    int resSeq = 0;

                                    string boqItem = boqRef == "" ? oldBoqRef : boqRef;

                                    var result = _dbContext.TblBoqs.SingleOrDefault(b => b.BoqItem == boqItem && b.BoqPackage == resCode);
                                    if (result != null)
                                        resSeq = result.BoqSeq;

                                    //Insert missing prices
                                    if (resPrice <= 0 && resSeq != 0)
                                    {
                                        var missPrice = new TblMissingPrice()
                                        {
                                            RevisionId = revId,
                                            BoqResourceSeq = resSeq
                                        };
                                        LstMissingPrice.Add(missPrice);
                                    }

                                    if ((resCode != "") && (resQty > 0) && (resPrice > 0))
                                    {
                                        var revdtl = new TblRevisionDetail()
                                        {
                                            RdRevisionId = revId,
                                            RdResourceSeq = resSeq,
                                            RdPrice = resPrice,
                                            RdQty = resQty,
                                            RdComment = resComment
                                        };
                                        LstRevDetails.Add(revdtl);
                                    }
                                    oldBoqRef = boqRef != "" ? boqRef : oldBoqRef;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    _dbContext.AddRange(LstRevDetails);
                    _dbContext.AddRange(LstMissingPrice);
                    _dbContext.SaveChanges();
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public int GetMaxRevisionNumber(int PackageSupplierId)
        {
            var query = _dbContext.TblSupplierPackageRevisions.Where(x => x.PrPackSuppId == PackageSupplierId);
            var MaxRevisionNumber = query.Any() ? query.Max(x => x.PrRevNo) : -1;
            return (int)MaxRevisionNumber;
        }

        public bool AssignSupplierPackage(int packId, List<SupplierPercent> SupPercentList)
        {
            foreach (var sup in SupPercentList)
            {
                var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                       join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                       join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                       where (a.SpPackageId == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                       select new AssignRevisionDetails
                                       {
                                           resourceID = c.RdResourceSeq,
                                           resourceQty = c.RdQty,
                                           price = c.RdPrice,
                                           assignpercent = sup.percent,
                                           assignQty = c.RdQty * (sup.percent / 100),
                                           assignPrice = c.RdPrice * c.RdQty * (sup.percent / 100),
                                           revisionId = b.PrRevId
                                       }).ToList();

                if (revisionDetails.Count > 0)
                {
                    foreach (var revDtl in revisionDetails)
                    {
                        UpdateRevDtlAssignedQty(revDtl.revisionId, revDtl.resourceID, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                    }
                }
            }
            return true;
        }

        public bool AssignSupplierRessource(int packId, List<SupplierResrouces> supplierResList)
        {
            foreach (var sup in supplierResList)
            {
                foreach (var supPerc in sup.supplierPercents)
                {
                    var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                           join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                           join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                           where (a.SpPackageId == packId && a.SpSupplierId == supPerc.supID && b.PrRevNo == 0 && c.RdResourceSeq == sup.resourceID)

                                           select new AssignRevisionDetails
                                           {
                                               resourceID = c.RdResourceSeq,
                                               resourceQty = c.RdQty,
                                               price = c.RdPrice * ( b.PrExchRate >0? b.PrExchRate : 1),
                                               assignpercent = supPerc.percent,
                                               assignQty = c.RdQty * (supPerc.percent / 100),
                                               assignPrice = c.RdPrice * c.RdQty * (supPerc.percent / 100),
                                               revisionId = b.PrRevId,
                                               priceOrigCurrency=c.RdPriceOrigCurrency
                                           }).ToList();

                    if (revisionDetails.Count > 0)
                    {
                        foreach (var revDtl in revisionDetails)
                        {
                            UpdateRevDtlAssignedQty(revDtl.revisionId, revDtl.resourceID, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                        }
                    }
                }
            }
            return true;
        }
        public void UpdateRevDtlAssignedQty(int revisionId, int resourceID, double assignpercent, double assignQty, double assignPrice)
        {
            var result = _dbContext.TblRevisionDetails.SingleOrDefault(b => b.RdRevisionId == revisionId && b.RdResourceSeq == resourceID);
            if (result != null)
            {
                result.RdAssignedPerc = assignpercent;
                result.RdAssignedQty = assignQty;
                result.RdAssignedPrice = assignPrice;
                _dbContext.SaveChanges();
            }
        }
    }
}
