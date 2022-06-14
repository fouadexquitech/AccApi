using AccApi.Data_Layer;
using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AccApi.Repository.Managers
{
    public class RevisionDetailsRepository : IRevisionDetailsRepository
    {
        private AccDbContext _dbContext;
        private PolicyDbContext _pdbContext;
        private MasterDbContext _mdbContext;

        public RevisionDetailsRepository(AccDbContext dbContext, PolicyDbContext pdbContext, MasterDbContext mdbContext)
        {
            _dbContext = dbContext;
            _pdbContext = pdbContext;
            _mdbContext = mdbContext;
        }

        public List<RevisionDetailsList> GetRevisionDetails(int RevisionId, string itemDesc, string resource)
        {
            var supPackRev = _dbContext.TblSupplierPackageRevisions.SingleOrDefault(b => (b.PrRevId == RevisionId));
            int PackageSuppliersID = (int)supPackRev.PrPackSuppId;

            var supPack = _dbContext.TblSupplierPackages.Where(x => x.SpPackSuppId == PackageSuppliersID).FirstOrDefault();
            byte byBoq = (byte)((supPack.SpByBoq == null) ? 0 : supPack.SpByBoq);

            RevisionDetailsList revDtlList = new RevisionDetailsList();
            IEnumerable<RevisionDetailsList> revDtlQry;

            var curList = (from b in _mdbContext.TblCurrencies
                           select b).ToList();

            if (byBoq == 1)
            {
                revDtlQry = (from cur in curList
                             join bb in _dbContext.TblSupplierPackageRevisions on cur.CurId equals bb.PrCurrency
                             join a in _dbContext.TblSupplierPackages on bb.PrPackSuppId equals a.SpPackSuppId
                             join b in _dbContext.TblRevisionDetails on bb.PrRevId equals b.RdRevisionId
                             join c in _dbContext.TblOriginalBoqs on b.RdBoqItem equals c.ItemO
                             where b.RdRevisionId == RevisionId

                             select new RevisionDetailsList
                             {
                                 RdResourceSeq = b.RdResourceSeq,
                                 RdPrice = b.RdPrice,
                                 RdMissedPrice = b.RdMissedPrice,
                                 RdBoqItem = b.RdBoqItem,
                                 RdItemDescription = c.DescriptionO,
                                 RdQty=b.RdQty,
                                 RdUnitRate=c.UnitRate,
                                 RdTotalBudget=c.Submitted,
                                 ExchangeRate=bb.PrExchRate,
                                 RdOriginalPrice=b.RdPriceOrigCurrency,
                                 TotalSupplierPrice=b.RdAssignedPrice,
                                 currency=cur.CurCode
                             }).ToList();

                if (itemDesc != null) revDtlQry = revDtlQry.Where(w => w.RdItemDescription.ToUpper().Contains(itemDesc.ToUpper()));
            }
            else
            {
                revDtlQry = (from cur in curList
                             join bb in _dbContext.TblSupplierPackageRevisions on cur.CurId equals bb.PrCurrency
                             join a in _dbContext.TblSupplierPackages on bb.PrPackSuppId equals a.SpPackSuppId
                             join b in _dbContext.TblRevisionDetails on bb.PrRevId equals b.RdRevisionId
                             join c in _dbContext.TblBoqs on b.RdResourceSeq equals c.BoqSeq
                             join i in _dbContext.TblOriginalBoqs on c.BoqItem equals i.ItemO
                             join e in _dbContext.TblResources on c.BoqResSeq equals e.ResSeq
                             where b.RdRevisionId == RevisionId

                             select new RevisionDetailsList
                             {
                                 RdResourceSeq = b.RdResourceSeq,
                                 RdPrice = b.RdPrice,
                                 RdMissedPrice = b.RdMissedPrice,
                                 RdBoqItem = i.ItemO,
                                 RdBoqItemDescription = i.DescriptionO,
                                 RdItemDescription = e.ResDescription,
                                 RdQty = b.RdQty,
                                 RdUnitRate = c.BoqUprice,
                                 RdTotalBudget = (b.RdQty) * (c.BoqUprice),
                                 ExchangeRate = bb.PrExchRate,
                                 RdOriginalPrice = b.RdPriceOrigCurrency,
                                 TotalSupplierPrice = b.RdAssignedPrice,
                                 currency = cur.CurCode
                             }).ToList();

                if (itemDesc != null) revDtlQry = revDtlQry.Where(w => w.RdBoqItemDescription.ToUpper().Contains(itemDesc.ToUpper()));
                if (resource != null) revDtlQry = revDtlQry.Where(w => w.RdItemDescription.ToUpper().Contains(resource.ToUpper()));
            }
            return revDtlQry.ToList();
        }

        public bool AddRevision(int PackageSupplierId, DateTime PackSuppDate, IFormFile ExcelFile, int curId, double ExchRate)
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

            var packageSupp = _dbContext.TblSupplierPackages.Where(x => x.SpPackSuppId == PackageSupplierId).FirstOrDefault();
            byte byBoq = (byte)((packageSupp.SpByBoq == null) ? 0 : packageSupp.SpByBoq);

            if (!InsertRevisionDetail(revId, ExcelFile, byBoq, ExchRate))
                return false;
            else
            {
                UpdateTotalPrice(revId);
                return true;
            }
        }

        public void UpdateTotalPrice(int revId)
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

        private bool InsertRevisionDetail(int revId, IFormFile ExcelFile, byte byBoq, double ExchRate)
        {
            Boolean ret = true;

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
                        //_dbContext.TblMissingPrices.RemoveRange(_dbContext.TblMissingPrices.Where(c => c.RevisionId == revId));
                        //_dbContext.SaveChanges();
                        string resComment, resCode = "", oldBoqRef = "";
                        double resQty, resPrice;

                        for (var row = 2; row <= rowCount; row++)
                        {
                            try
                            {
                                string boqRef = worksheet.Cells[row, 1].Value == null ? "" : worksheet.Cells[row, 1].Value.ToString();
                                string boqDesc = worksheet.Cells[row, 3].Value == null ? "" : worksheet.Cells[row, 3].Value.ToString();
                                double boqQty = worksheet.Cells[row, 5].Value == null ? 0 : (double)worksheet.Cells[row, 5].Value;

                                if (byBoq == 1)
                                {
                                    if (((boqRef != "") && (boqDesc != "") && (boqQty != 0)))
                                    {
                                        resPrice = (worksheet.Cells[row, 6].Value == null) ? 0 : (double)worksheet.Cells[row, 6].Value;
                                        resComment = worksheet.Cells[row, 7].Value == null ? "" : worksheet.Cells[row, 7].Value.ToString();
                                        resQty = boqQty;

                                        byte missPrice = 0;
                                        if (resPrice <= 0 && boqRef != "")
                                        {
                                            missPrice = 1;
                                        }

                                        if ((boqRef != "") && (resQty > 0) && (resPrice >= 0))
                                        {
                                            var revdtl = new TblRevisionDetail()
                                            {
                                                RdRevisionId = revId,
                                                RdResourceSeq = 0,
                                                RdBoqItem = boqRef,
                                                RdPrice = resPrice * (ExchRate > 0 ? ExchRate : 1),
                                                RdQty = resQty,
                                                RdComment = resComment,
                                                RdPriceOrigCurrency = resPrice,
                                                RdMissedPrice = missPrice
                                            };
                                            LstRevDetails.Add(revdtl);
                                        }
                                        oldBoqRef = boqRef;
                                    }
                                }
                                else
                                {

                                    if (((boqRef != "") && (boqDesc != "") && (boqQty != 0)) || ((oldBoqRef != "") && ((worksheet.Cells[row, 6].Value == null ? "" : worksheet.Cells[row, 6].Value.ToString()) != "")))
                                    {
                                        resQty = worksheet.Cells[row, 10].Value == null ? 0 : (double)worksheet.Cells[row, 10].Value;
                                        resPrice = (worksheet.Cells[row, 11].Value == null) ? 0 : (double)worksheet.Cells[row, 11].Value;
                                        resComment = worksheet.Cells[row, 12].Value == null ? "" : worksheet.Cells[row, 12].Value.ToString();

                                        int resSeq = 0;
                                        string boqItem = boqRef == "" ? oldBoqRef : boqRef;

                                        resCode = worksheet.Cells[row, 7].Value == null ? "" : worksheet.Cells[row, 7].Value.ToString();

                                        var result = _dbContext.TblBoqs.SingleOrDefault(b => b.BoqItem == boqItem && b.BoqPackage == resCode);
                                        if (result != null)
                                            resSeq = result.BoqSeq;

                                        byte missPrice = 0;
                                        //Insert missing prices
                                        if (resPrice <= 0 && resSeq != 0)
                                        {
                                            //var missPrice = new TblMissingPrice()
                                            //{
                                            //    RevisionId = revId,
                                            //    BoqResourceSeq = resSeq
                                            //};
                                            //LstMissingPrice.Add(missPrice);
                                            missPrice = 1;
                                        }

                                        if ((resCode != "") && (resQty > 0) && (resPrice >= 0))
                                        {
                                            var revdtl = new TblRevisionDetail()
                                            {
                                                RdRevisionId = revId,
                                                RdResourceSeq = resSeq,
                                                RdPrice = resPrice * (ExchRate > 0 ? ExchRate : 1),
                                                RdQty = resQty,
                                                RdComment = resComment,
                                                RdPriceOrigCurrency = resPrice,
                                                RdMissedPrice = missPrice
                                            };
                                            LstRevDetails.Add(revdtl);
                                        }
                                        oldBoqRef = boqRef != "" ? boqRef : oldBoqRef;
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }

                    //if (LstMissingPrice.Count()>0 )
                    //{ 
                    //    _dbContext.AddRange(LstMissingPrice);
                    //    ret = false;
                    //}
                    //else
                    //{ 
                    _dbContext.AddRange(LstRevDetails);
                    ret = true;
                    //}

                    _dbContext.SaveChanges();
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            return ret;
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

        public bool AssignSupplierRessource(int packId, List<SupplierResrouces> supplierResList, bool isPercent)
        {
           List<AssignRevisionDetails> revisionDetails = new List<AssignRevisionDetails>();

            foreach (var sup in supplierResList)
            {
                if (isPercent)
                {
                    foreach (var supPerc in sup.supplierPercents)
                    {
                         revisionDetails = (from a in _dbContext.TblSupplierPackages
                                               join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                               join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                               where (a.SpPackageId == packId && a.SpSupplierId == supPerc.supID && b.PrRevNo == 0 && c.RdResourceSeq == sup.resourceID)

                                               select new AssignRevisionDetails
                                               {
                                                   resourceID = c.RdResourceSeq,
                                                   resourceQty = c.RdQty,
                                                   price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                   assignpercent = supPerc.percent,
                                                   assignQty = c.RdQty * (supPerc.percent / 100),
                                                   assignPrice = c.RdPrice * c.RdQty * (supPerc.percent / 100),
                                                   revisionId = b.PrRevId,
                                                   priceOrigCurrency = c.RdPriceOrigCurrency
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
                else
                {
                    foreach (var supQty in sup.supplierQtys)
                    {
                        revisionDetails = (from a in _dbContext.TblSupplierPackages
                                           join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                           join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                           where (a.SpPackageId == packId && a.SpSupplierId == supQty.supID && b.PrRevNo == 0 && c.RdResourceSeq == sup.resourceID)

                                           select new AssignRevisionDetails
                                           {
                                               resourceID = c.RdResourceSeq,
                                               resourceQty = c.RdQty,
                                               price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                               assignpercent = (supQty.qty/ c.RdQty ) *100,
                                               assignQty = supQty.qty,
                                               assignPrice = c.RdPrice * supQty.qty,
                                               revisionId = b.PrRevId,
                                               priceOrigCurrency = c.RdPriceOrigCurrency
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
            }
            return true;
        }

        public bool AssignSupplierListRessourceList(int packId, AssignSuppliertRes item, bool isPercent)
        {
            List<AssignRevisionDetails> revisionDetails = new List<AssignRevisionDetails>();

            if (isPercent)
            {
                foreach (var sup in item.supplierPercentList)
                {
                    revisionDetails = (from a in _dbContext.TblSupplierPackages
                                       join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                       join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                       where (a.SpPackageId == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                       select new AssignRevisionDetails
                                       {
                                           resourceID = c.RdResourceSeq,
                                           resourceQty = c.RdQty,
                                           price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                           assignpercent = sup.percent,
                                           assignQty = c.RdQty * (sup.percent / 100),
                                           assignPrice = c.RdPrice * c.RdQty * (sup.percent / 100),
                                           revisionId = b.PrRevId,
                                           priceOrigCurrency = c.RdPriceOrigCurrency
                                       }).ToList();

                    var filtered = revisionDetails.Where(x => item.supplierResItemList.Contains(item.supplierResItemList.Where(y => y.resId == x.resourceID).FirstOrDefault()));

                    foreach (var revDtl in filtered)
                    {
                        UpdateRevDtlAssignedQty(revDtl.revisionId, revDtl.resourceID, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                    }
                }
            }
            else
            {
                foreach (var sup in item.supplierQtyList)
                {
                    revisionDetails = (from a in _dbContext.TblSupplierPackages
                                       join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                       join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                       where (a.SpPackageId == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                       select new AssignRevisionDetails
                                       {
                                           resourceID = c.RdResourceSeq,
                                           resourceQty = c.RdQty,
                                           price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                           assignpercent = (sup.qty/ c.RdQty ) *100,
                                           assignQty =  sup.qty,
                                           assignPrice = c.RdPrice * sup.qty,
                                           revisionId = b.PrRevId,
                                           priceOrigCurrency = c.RdPriceOrigCurrency
                                       }).ToList();

                    var filtered = revisionDetails.Where(x => item.supplierResItemList.Contains(item.supplierResItemList.Where(y => y.resId == x.resourceID).FirstOrDefault()));

                    foreach (var revDtl in filtered)
                    {
                        UpdateRevDtlAssignedQty(revDtl.revisionId, revDtl.resourceID, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                    }
                }
            }
                
            return true;
        }

        public bool AssignSupplierListBoqList(int packId, AssignSuppliertBoq item, bool isPercent)
        {
            List<AssignRevisionDetails> revisionDetails = new List<AssignRevisionDetails>();

            if (isPercent)
            {
                foreach (var sup in item.supplierPercentList)
                {
                    revisionDetails = (from a in _dbContext.TblSupplierPackages
                                           join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                           join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                           where (a.SpPackageId == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                           select new AssignRevisionDetails
                                           {
                                               boqItem = c.RdBoqItem,
                                               boqQty = c.RdQty,
                                               price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                               assignpercent = sup.percent,
                                               assignQty = c.RdQty * (sup.percent / 100),
                                               assignPrice = c.RdPrice * c.RdQty * (sup.percent / 100),
                                               revisionId = b.PrRevId,
                                               priceOrigCurrency = c.RdPriceOrigCurrency
                                           }).ToList();

                    var filtered = revisionDetails.Where(x => item.supplierBoqItemList.Contains(item.supplierBoqItemList.Where(y => y.BoqItemID == x.boqItem).FirstOrDefault()));

                    foreach (var revDtl in filtered)
                    {
                        UpdateRevDtlAssignedQtyBOQ(revDtl.revisionId, revDtl.boqItem, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                    }
                }
            }
            else
            {
                foreach (var sup in item.supplierQtyList)
                {
                    revisionDetails = (from a in _dbContext.TblSupplierPackages
                                           join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                           join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                           where (a.SpPackageId == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                           select new AssignRevisionDetails
                                           {
                                               boqItem = c.RdBoqItem,
                                               boqQty = c.RdQty,
                                               price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                               assignpercent =( sup.qty / c.RdQty ) *100,
                                               assignQty =sup.qty,
                                               assignPrice = c.RdPrice * sup.qty,
                                               revisionId = b.PrRevId,
                                               priceOrigCurrency = c.RdPriceOrigCurrency
                                           }).ToList();

                    var filtered = revisionDetails.Where(x => item.supplierBoqItemList.Contains(item.supplierBoqItemList.Where(y => y.BoqItemID == x.boqItem).FirstOrDefault()));

                    foreach (var revDtl in filtered)
                    {
                        UpdateRevDtlAssignedQtyBOQ(revDtl.revisionId, revDtl.boqItem, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                    }
                }
            }
        
            return true;
        }

        public bool AssignSupplierGroup(int packId, bool byBoq, List<SupplierGroups> SupplierGroupList, bool isPercent)
        {
                if (!byBoq)
            {
                foreach (var sup in SupplierGroupList)
                {
                    if (isPercent)
                    {
                        foreach (var supPerc in sup.supplierPercents)
                        {
                            var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                                   join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                                   join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                   join boq in _dbContext.TblBoqs on c.RdResourceSeq equals boq.BoqSeq
                                                   where (a.SpPackageId == packId && boq.BoqScope == packId && a.SpSupplierId == supPerc.supID && b.PrRevNo == 0 && boq.GroupId == sup.GroupId)

                                                   select new AssignRevisionDetails
                                                   {
                                                       resourceID = c.RdResourceSeq,
                                                       resourceQty = c.RdQty,
                                                       price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                       assignpercent = supPerc.percent,
                                                       assignQty = c.RdQty * (supPerc.percent / 100),
                                                       assignPrice = c.RdPrice * c.RdQty * (supPerc.percent / 100),
                                                       revisionId = b.PrRevId,
                                                       priceOrigCurrency = c.RdPriceOrigCurrency
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
                    else
                    {
                        foreach (var supPerc in sup.supplierQtys)
                        {
                            var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                                   join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                                   join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                   join boq in _dbContext.TblBoqs on c.RdResourceSeq equals boq.BoqSeq
                                                   where (a.SpPackageId == packId && boq.BoqScope == packId && a.SpSupplierId == supPerc.supID && b.PrRevNo == 0 && boq.GroupId == sup.GroupId)

                                                   select new AssignRevisionDetails
                                                   {
                                                       resourceID = c.RdResourceSeq,
                                                       resourceQty = c.RdQty,
                                                       price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                       assignpercent = (supPerc.qty/ c.RdQty) * 100,
                                                       assignQty = supPerc.qty,
                                                       assignPrice = c.RdPrice * supPerc.qty,
                                                       revisionId = b.PrRevId,
                                                       priceOrigCurrency = c.RdPriceOrigCurrency
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

                }
            }
            else
            {
                foreach (var sup in SupplierGroupList)
                {
                    if (isPercent)
                    {
                        foreach (var supPerc in sup.supplierPercents)
                        {
                            var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                                   join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                                   join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                   join boq in _dbContext.TblOriginalBoqs on c.RdBoqItem equals boq.ItemO
                                                   where (a.SpPackageId == packId && boq.Scope == packId && a.SpSupplierId == supPerc.supID && b.PrRevNo == 0 && boq.GroupId == sup.GroupId)

                                                   select new AssignRevisionDetails
                                                   {
                                                       boqItem = c.RdBoqItem,
                                                       boqQty = c.RdQty,
                                                       price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                       assignpercent = supPerc.percent,
                                                       assignQty = c.RdQty * (supPerc.percent / 100),
                                                       assignPrice = c.RdPrice * c.RdQty * (supPerc.percent / 100),
                                                       revisionId = b.PrRevId,
                                                       priceOrigCurrency = c.RdPriceOrigCurrency
                                                   }).ToList();

                            if (revisionDetails.Count > 0)
                            {
                                foreach (var revDtl in revisionDetails)
                                {
                                    UpdateRevDtlAssignedQtyBOQ(revDtl.revisionId, revDtl.boqItem, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                                }
                            }
                        }
                    }
                else
                {
                    foreach (var supPerc in sup.supplierQtys)
                        {
                            var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                                   join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                                   join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                   join boq in _dbContext.TblOriginalBoqs on c.RdBoqItem equals boq.ItemO
                                                   where (a.SpPackageId == packId && boq.Scope == packId && a.SpSupplierId == supPerc.supID && b.PrRevNo == 0 && boq.GroupId == sup.GroupId)

                                                   select new AssignRevisionDetails
                                                   {
                                                       boqItem = c.RdBoqItem,
                                                       boqQty = c.RdQty,
                                                       price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                       assignpercent = (supPerc.qty/ c.RdQty) * 100,
                                                       assignQty = supPerc.qty,
                                                       assignPrice = c.RdPrice * supPerc.qty,
                                                       revisionId = b.PrRevId,
                                                       priceOrigCurrency = c.RdPriceOrigCurrency
                                                   }).ToList();

                            if (revisionDetails.Count > 0)
                            {
                                foreach (var revDtl in revisionDetails)
                                {
                                    UpdateRevDtlAssignedQtyBOQ(revDtl.revisionId, revDtl.boqItem, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                                }
                            }
                        }
                    }
                }
            }

            return true;
        }

        public bool AssignSupplierListGroupList(int packId, bool byBoq, AssignSupplierGroup item, bool isPercent)
        {
            if (byBoq)
            {
                if (isPercent)
                {
                    foreach (var sup in item.supplierPercentList)
                    {
                        var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                               join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                               join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                               join boq in _dbContext.TblOriginalBoqs on c.RdBoqItem equals boq.ItemO
                                               where (a.SpPackageId == packId && boq.Scope == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                               select new AssignRevisionDetails
                                               {
                                                   boqItem = c.RdBoqItem,
                                                   boqQty = c.RdQty,
                                                   price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                   assignpercent = sup.percent,
                                                   assignQty = c.RdQty * (sup.percent / 100),
                                                   assignPrice = c.RdPrice * c.RdQty * (sup.percent / 100),
                                                   revisionId = b.PrRevId,
                                                   priceOrigCurrency = c.RdPriceOrigCurrency,
                                                   GroupId = boq.GroupId
                                               }).ToList();

                        var filtered = revisionDetails.Where(x => item.supplierGroupList.Contains(item.supplierGroupList.Where(y => y.Id == x.GroupId).FirstOrDefault()));

                        foreach (var revDtl in filtered)
                        {
                            UpdateRevDtlAssignedQtyBOQ(revDtl.revisionId, revDtl.boqItem, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                        }

                    }
                }
                else
                {
                    foreach (var sup in item.supplierQtyList)
                    {
                        var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                               join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                               join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                               join boq in _dbContext.TblOriginalBoqs on c.RdBoqItem equals boq.ItemO
                                               where (a.SpPackageId == packId && boq.Scope == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                               select new AssignRevisionDetails
                                               {
                                                   boqItem = c.RdBoqItem,
                                                   boqQty = c.RdQty,
                                                   price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                   assignpercent = (sup.qty/ c.RdQty)*100,
                                                   assignQty = sup.qty ,
                                                   assignPrice = c.RdPrice * sup.qty,
                                                   revisionId = b.PrRevId,
                                                   priceOrigCurrency = c.RdPriceOrigCurrency,
                                                   GroupId = boq.GroupId
                                               }).ToList();

                        var filtered = revisionDetails.Where(x => item.supplierGroupList.Contains(item.supplierGroupList.Where(y => y.Id == x.GroupId).FirstOrDefault()));

                        foreach (var revDtl in filtered)
                        {
                            UpdateRevDtlAssignedQtyBOQ(revDtl.revisionId, revDtl.boqItem, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                        }

                    }
                }
            }
            else
            {
                if (isPercent)
                {
                    foreach (var sup in item.supplierPercentList)
                    {
                        var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                               join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                               join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                               join boq in _dbContext.TblBoqs on c.RdResourceSeq equals boq.BoqSeq
                                               where (a.SpPackageId == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                               select new AssignRevisionDetails
                                               {
                                                   resourceID = c.RdResourceSeq,
                                                   resourceQty = c.RdQty,
                                                   price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                   assignpercent = sup.percent,
                                                   assignQty = c.RdQty * (sup.percent / 100),
                                                   assignPrice = c.RdPrice * c.RdQty * (sup.percent / 100),
                                                   revisionId = b.PrRevId,
                                                   priceOrigCurrency = c.RdPriceOrigCurrency,
                                                   GroupId = boq.GroupId
                                               }).ToList();

                        var filtered = revisionDetails.Where(x => item.supplierGroupList.Contains(item.supplierGroupList.Where(y => y.Id == x.GroupId).FirstOrDefault()));

                        foreach (var revDtl in filtered)
                        {
                            UpdateRevDtlAssignedQty(revDtl.revisionId, revDtl.resourceID, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                        }
                    }
                }
                else
                {
                    foreach (var sup in item.supplierQtyList)
                    {
                        var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                               join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                               join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                               join boq in _dbContext.TblBoqs on c.RdResourceSeq equals boq.BoqSeq
                                               where (a.SpPackageId == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                                               select new AssignRevisionDetails
                                               {
                                                   resourceID = c.RdResourceSeq,
                                                   resourceQty = c.RdQty,
                                                   price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                   assignpercent = (sup.qty/ c.RdQty) * 100,
                                                   assignQty = sup.qty ,
                                                   assignPrice = c.RdPrice *  sup.qty  ,
                                                   revisionId = b.PrRevId,
                                                   priceOrigCurrency = c.RdPriceOrigCurrency,
                                                   GroupId = boq.GroupId
                                               }).ToList();

                        var filtered = revisionDetails.Where(x => item.supplierGroupList.Contains(item.supplierGroupList.Where(y => y.Id == x.GroupId).FirstOrDefault()));

                        foreach (var revDtl in filtered)
                        {
                            UpdateRevDtlAssignedQty(revDtl.revisionId, revDtl.resourceID, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                        }
                    }
                }
            }

            return true;
        }

        public bool AssignSupplierBOQ(int packId, List<SupplierBOQ> SupplierBOQList, bool isPercent)
        {
            foreach (var sup in SupplierBOQList)
            {
                if (isPercent)
                {
                    foreach (var supPerc in sup.supplierPercents)
                    {
                        var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                               join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                               join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                               where (a.SpPackageId == packId && a.SpSupplierId == supPerc.supID && b.PrRevNo == 0 && c.RdBoqItem == sup.BoqItemID)

                                               select new AssignRevisionDetails
                                               {
                                                   boqItem = c.RdBoqItem,
                                                   boqQty = c.RdQty,
                                                   price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                   assignpercent = supPerc.percent,
                                                   assignQty = c.RdQty * (supPerc.percent / 100),
                                                   assignPrice = c.RdPrice * c.RdQty * (supPerc.percent / 100),
                                                   revisionId = b.PrRevId,
                                                   priceOrigCurrency = c.RdPriceOrigCurrency
                                               }).ToList();

                        if (revisionDetails.Count > 0)
                        {
                            foreach (var revDtl in revisionDetails)
                            {
                                UpdateRevDtlAssignedQtyBOQ(revDtl.revisionId, revDtl.boqItem, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                            }
                        }
                    }
                }
                else
                {
                    foreach (var supPerc in sup.supplierQtys)
                    {
                        var revisionDetails = (from a in _dbContext.TblSupplierPackages
                                               join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                               join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                               where (a.SpPackageId == packId && a.SpSupplierId == supPerc.supID && b.PrRevNo == 0 && c.RdBoqItem == sup.BoqItemID)

                                               select new AssignRevisionDetails
                                               {
                                                   boqItem = c.RdBoqItem,
                                                   boqQty = c.RdQty,
                                                   price = c.RdPrice * (b.PrExchRate > 0 ? b.PrExchRate : 1),
                                                   assignpercent = (supPerc.qty/ c.RdQty)*100,
                                                   assignQty =  supPerc.qty ,
                                                   assignPrice = c.RdPrice *supPerc.qty ,
                                                   revisionId = b.PrRevId,
                                                   priceOrigCurrency = c.RdPriceOrigCurrency
                                               }).ToList();

                        if (revisionDetails.Count > 0)
                        {
                            foreach (var revDtl in revisionDetails)
                            {
                                UpdateRevDtlAssignedQtyBOQ(revDtl.revisionId, revDtl.boqItem, (double)revDtl.assignpercent, (double)revDtl.assignQty, (double)revDtl.assignPrice);
                            }
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

        public void UpdateRevDtlAssignedQtyBOQ(int revisionId, string boqItem, double assignpercent, double assignQty, double assignPrice)
        {
            var result = _dbContext.TblRevisionDetails.SingleOrDefault(b => b.RdRevisionId == revisionId && b.RdBoqItem == boqItem);
            if (result != null)
            {
                result.RdAssignedPerc = assignpercent;
                result.RdAssignedQty = assignQty;
                result.RdAssignedPrice = assignPrice;
                _dbContext.SaveChanges();
            }
        }

        public bool UpdateRevisionDetailsPriceByBoq(List<RevisionDetailsList> revisionDetailsList)
        {
            foreach (var item in revisionDetailsList)
            {
                var result = _dbContext.TblRevisionDetails.SingleOrDefault(b => b.RdRevisionId == item.RdRevisionId && b.RdBoqItem == item.RdBoqItem);
                if (result != null)
                {
                    result.RdPrice = item.RdPrice;
                }
            }
            _dbContext.SaveChanges();
            return true;
        }

        public bool UpdateRevisionDetailsPrice(List<RevisionDetailsList> revisionDetailsList)
        {
            foreach (var item in revisionDetailsList)
            {
                var result = _dbContext.TblRevisionDetails.SingleOrDefault(b => b.RdRevisionId == item.RdRevisionId && b.RdResourceSeq == item.RdResourceSeq);
                if (result != null)
                {
                    result.RdPrice = item.RdPrice;
                }
            }
            _dbContext.SaveChanges();
            return true;
        }

        public bool SendCompToManagement(TopManagementTemplateModel topManagementTemplate, IFormFile attachement)
        {
            string send = "";

            
            string[] emails = new string[topManagementTemplate.TopManagements.Count];
            if (topManagementTemplate.TopManagements.Count > 0)
            {
                int packId = topManagementTemplate.PackageId;
                for (int i = 0; i < topManagementTemplate.TopManagements.Count; i++)
                {
                    emails[i] = topManagementTemplate.TopManagements[i].Mail;
                }
                var package = _dbContext.PackagesNetworks.Where(x => x.IdPkge == packId).FirstOrDefault();
                string PackageName = package.PkgeName;

                var p = _dbContext.TblParameters.FirstOrDefault();
                var proj = _pdbContext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefault();
                string ProjectName = proj.PrjName;

                //string excelName = $"{PackageName}-{ProjectName}.xlsx";

                //string path = @"C:\App\";

                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //string FullPath = path + ExcelComparisonSheet;

                //if (File.Exists(FullPath))
                //    File.Delete(FullPath);

                //xlPackage.SaveAs(FullPath);


                //Send Email
                List<General> mylistTo = new List<General>();
                foreach (var email in emails)
                {
                    General g = new General();
                    g.mail = email == null ? "" : email;
                    mylistTo.Add(g);
                }

                List<General> mylistCC = new List<General>();
                mylistCC = null;

                string Subject = "Project: " + ProjectName + ", Package: " + PackageName;

                /*string MailBody;

                MailBody = "Dear Sir,";
                MailBody += Environment.NewLine;
                MailBody += Environment.NewLine;
                MailBody += "Please find attached Comparison sheet.";
                MailBody += Environment.NewLine;
                MailBody += Environment.NewLine;
                MailBody += Environment.NewLine;
                MailBody += Environment.NewLine;
                MailBody += "Best regards";*/



                var AttachmentList = new List<string>();

                Mail m = new Mail();
                var res = m.SendMail(mylistTo, mylistCC, Subject, topManagementTemplate.Template, AttachmentList, false, attachement);

                send = "sent";
            }

            return (send == "sent");
        }
     
        private string getCurDesc(int id)
        {
            var cur = _mdbContext.TblCurrencies.Where(x=> x.CurId==id).FirstOrDefault();

            if (cur != null)
                return cur.CurDesc;
            else
                return "";
        }

        public List<GroupingBoqModel> GetComparisonSheet(int packageId, SearchInput input)
        {
            IEnumerable<BoqRessourcesList> condQuery = (from o in _dbContext.TblOriginalBoqs
                                                        join b in _dbContext.TblBoqs on o.ItemO equals b.BoqItem
                                                        join r in _dbContext.TblResources on b.BoqResSeq equals r.ResSeq
                                                        where o.Scope == packageId
                                                        select new BoqRessourcesList
                                                        {
                                                            RowNumber = o.RowNumber,
                                                            SectionO = o.SectionO,
                                                            ItemO = o.ItemO,
                                                            DescriptionO = o.DescriptionO,
                                                            UnitO = o.UnitO,
                                                            QtyO = o.QtyO,
                                                            UnitRate = o.UnitRate,
                                                            Scope = o.Scope,
                                                            BoqSeq = b.BoqSeq,
                                                            BoqCtg = b.BoqCtg,
                                                            BoqUnitMesure = b.BoqUnitMesure,
                                                            BoqQty = b.BoqQty,
                                                            BoqUprice = b.BoqUprice,
                                                            BoqDiv = b.BoqDiv,
                                                            BoqPackage = b.BoqPackage,
                                                            BoqScope = b.BoqScope,
                                                            ResSeq = r.ResSeq,
                                                            ResDescription = r.ResDescription
                                                        });

            if (input.BOQDiv.Length > 0) condQuery = condQuery.Where(w => input.BOQDiv.Contains(w.SectionO));
            if (!string.IsNullOrEmpty(input.BOQItem)) condQuery = condQuery.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
            if (!string.IsNullOrEmpty(input.BOQDesc)) condQuery = condQuery.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
            if (!string.IsNullOrEmpty(input.SheetDesc)) condQuery = condQuery.Where(w => w.ObSheetDesc == input.SheetDesc);
            if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) condQuery = condQuery.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.Scope == input.Package);
            if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));
            if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));

            var items = condQuery
                .GroupBy(x => new { x.RowNumber, x.SectionO, x.ItemO, x.DescriptionO, x.UnitO })
                .Select(p => p.FirstOrDefault())
                .Select(p => new GroupingBoqModel
                {
                    ItemO = p.ItemO,
                    DescriptionO = p.DescriptionO,
                    IsSelected = false,
                    RowNumber = p.RowNumber.Value
                }).ToList();

            var curList = (from b in _mdbContext.TblCurrencies
                           select b).ToList();

            var ExchNowList = (from cur in curList
                               join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                               join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                               join sup in _dbContext.TblSuppliers on a.SpSupplierId equals sup.SupCode
                               where (a.SpPackageId == packageId && b.PrRevNo == 0)
                               select new LiveExchange
                               {
                                   fromCurrency = cur.CurCode,
                                   ExchRateNow = GetExchange(cur.CurCode)
                               }).ToList();

            var querySupp = (from cur in curList
                             join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                             join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                             join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                             join sup in _dbContext.TblSuppliers on a.SpSupplierId equals sup.SupCode
                             where (a.SpPackageId == packageId && b.PrRevNo == 0)
                             select new GroupingPackageSupplierPriceModel
                             {
                                 SupplierId = sup.SupCode,
                                 SupplierName = sup.SupName,
                                 LastRevisionDate = b.PrRevDate,
                                 AssignedPercentage = c.RdAssignedPerc,
                                 AssignedQty = c.RdAssignedQty,
                                 MissedPrice = c.RdMissedPrice,
                                 OriginalCurrencyPrice = c.RdPriceOrigCurrency,
                                 Qty = c.RdQty,
                                 UnitPrice = c.RdPrice,
                                 TotalPrice = (c.RdQty * c.RdPrice),
                                 BoqResourceId = c.RdResourceSeq,
                                 OriginalCurrency = cur.CurCode,
                                 ExchRate = b.PrExchRate,
                                 ExchRateNow = ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow
                             }).ToList();

            foreach (var item in items)
            {
                item.GroupingResources = condQuery.Where(x => x.ItemO == item.ItemO).Select(y => new GroupingResourceModel
                {
                    BoqSeq = y.BoqSeq,
                    ResourceSeq = y.ResSeq,
                    ResourceDescription = y.ResDescription,
                    Unit = y.BoqUnitMesure,
                    Qty = y.BoqQty,
                    UnitPrice = y.BoqUprice,
                    TotalPrice = (y.BoqQty * y.BoqUprice),
                    ValidPerc = true,
                    IsSelected = false,
                    GroupingPackageSuppliersPrices = querySupp.Where(x => x.BoqResourceId == y.BoqSeq).OrderBy(x=>x.SupplierName).ToList()

                }).ToList();
            }
            return items;
        }

        public List<GroupingBoqModel> GetComparisonSheetByBoq(int packageId, SearchInput input)
        {
            IEnumerable<BoqRessourcesList> condQuery = (from o in _dbContext.TblOriginalBoqs
                                                        join b in _dbContext.TblBoqs on o.ItemO equals b.BoqItem
                                                        join r in _dbContext.TblResources on b.BoqResSeq equals r.ResSeq

                                                        where o.Scope == packageId
                                                        select new BoqRessourcesList
                                                        {
                                                            RowNumber = o.RowNumber,
                                                            SectionO = o.SectionO,
                                                            ItemO = o.ItemO,
                                                            DescriptionO = o.DescriptionO,
                                                            UnitO = o.UnitO,
                                                            QtyO = o.QtyO,
                                                            UnitRate = o.UnitRate,
                                                            Scope = o.Scope,
                                                            BoqSeq = b.BoqSeq,
                                                            BoqCtg = b.BoqCtg,
                                                            BoqUnitMesure = b.BoqUnitMesure,
                                                            BoqQty = b.BoqQty,
                                                            BoqUprice = o.UnitRate,
                                                            BoqDiv = b.BoqDiv,
                                                            BoqPackage = b.BoqPackage,
                                                            BoqScope = b.BoqScope,
                                                            ResSeq = r.ResSeq,
                                                            ResDescription = r.ResDescription
                                                        });

            if (input.BOQDiv.Length > 0) condQuery = condQuery.Where(w => input.BOQDiv.Contains(w.SectionO));
            if (!string.IsNullOrEmpty(input.BOQItem)) condQuery = condQuery.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
            if (!string.IsNullOrEmpty(input.BOQDesc)) condQuery = condQuery.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
            if (!string.IsNullOrEmpty(input.SheetDesc)) condQuery = condQuery.Where(w => w.ObSheetDesc == input.SheetDesc);
            if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) condQuery = condQuery.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.Scope == input.Package);
            if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));
            if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));

            var items = condQuery
                .GroupBy(x => new { x.RowNumber, x.SectionO, x.ItemO, x.DescriptionO, x.UnitO, x.QtyO, x.BoqUprice, x.BoqUnitMesure })
                .Select(p => p.FirstOrDefault()).ToList()
                .Select(p => new GroupingBoqModel
                {
                    ItemO = p.ItemO,
                    DescriptionO = p.DescriptionO,
                    IsSelected = false,
                    ValidPerc = true,
                    RowNumber = p.RowNumber.Value,
                    Qty = p.QtyO,
                    UnitPrice = p.UnitRate,
                    Unit = p.UnitO,
                    TotalPrice = (p.QtyO * p.UnitRate)

                }).ToList();

            var curList = (from b in _mdbContext.TblCurrencies
                            select b).ToList();


            var ExchNowList = (from cur in curList
                             join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                             join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                             join sup in _dbContext.TblSuppliers on a.SpSupplierId equals sup.SupCode
                             where (a.SpPackageId == packageId && b.PrRevNo == 0)
                             select new LiveExchange
                             {                              
                                 fromCurrency = cur.CurCode,
                                 ExchRateNow = GetExchange(cur.CurCode)
                             }).ToList();

            var querySupp = (from cur in curList
                             join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                             join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                             join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                             join sup in _dbContext.TblSuppliers on a.SpSupplierId equals sup.SupCode                            
                             where (a.SpPackageId == packageId && b.PrRevNo == 0)
                             select new GroupingPackageSupplierPriceModel
                             {
                                 SupplierId = sup.SupCode,
                                 SupplierName = sup.SupName,
                                 LastRevisionDate = b.PrRevDate,
                                 AssignedPercentage = c.RdAssignedPerc,
                                 AssignedQty = c.RdAssignedQty,
                                 MissedPrice = c.RdMissedPrice,
                                 OriginalCurrencyPrice = c.RdPriceOrigCurrency,
                                 Qty = c.RdQty,
                                 UnitPrice = c.RdPrice,
                                 TotalPrice = (c.RdQty * c.RdPrice),
                                 BoqItemO = c.RdBoqItem,                               
                                 OriginalCurrency =cur.CurCode,
                                 ExchRate = b.PrExchRate,
                                 ExchRateNow = ExchNowList.Find(x=> x.fromCurrency ==cur.CurCode).ExchRateNow
                             }).ToList();

            foreach (var item in items)
            {
                item.GroupingPackageSuppliersPrices = querySupp.Where(x => x.BoqItemO == item.ItemO).OrderBy(x => x.SupplierName).ToList();
            }

            return items;
        }

        public List<GroupingBoqGroupModel> GetComparisonSheetBoqByGroup(int packageId, SearchInput input)
        {
            IEnumerable<BoqRessourcesList> condQuery = (from o in _dbContext.TblOriginalBoqs
                                                        join b in _dbContext.TblBoqs on o.ItemO equals b.BoqItem
                                                        join r in _dbContext.TblResources on b.BoqResSeq equals r.ResSeq
                                                        join g in _dbContext.ComparisonPackageGroups on o.GroupId equals g.Id
                                                        where o.Scope == packageId
                                                        select new BoqRessourcesList
                                                        {
                                                            RowNumber = o.RowNumber,
                                                            SectionO = o.SectionO,
                                                            ItemO = o.ItemO,
                                                            DescriptionO = o.DescriptionO,
                                                            UnitO = o.UnitO,
                                                            QtyO = o.QtyO,
                                                            UnitRate = o.UnitRate,
                                                            Scope = o.Scope,
                                                            BoqSeq = b.BoqSeq,
                                                            BoqCtg = b.BoqCtg,
                                                            BoqUnitMesure = b.BoqUnitMesure,
                                                            BoqQty = b.BoqQty,
                                                            BoqUprice = o.UnitRate,
                                                            BoqDiv = b.BoqDiv,
                                                            BoqPackage = b.BoqPackage,
                                                            BoqScope = b.BoqScope,
                                                            ResSeq = r.ResSeq,
                                                            ResDescription = r.ResDescription,
                                                            GroupName = g.Name,
                                                            GroupId = g.Id
                                                        });

            if (input.BOQDiv.Length > 0) condQuery = condQuery.Where(w => input.BOQDiv.Contains(w.SectionO));
            if (!string.IsNullOrEmpty(input.BOQItem)) condQuery = condQuery.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
            if (!string.IsNullOrEmpty(input.BOQDesc)) condQuery = condQuery.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
            if (!string.IsNullOrEmpty(input.SheetDesc)) condQuery = condQuery.Where(w => w.ObSheetDesc == input.SheetDesc);
            if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) condQuery = condQuery.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.Scope == input.Package);
            if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));
            if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));


            var groups = condQuery
                .GroupBy(x => new { x.GroupId, x.GroupName })
                //.Select(p => p.Gr)
                .Select(p => new GroupingBoqGroupModel
                {
                    Id = p.First().GroupId.HasValue ? p.First().GroupId.Value : 0,
                    Name = p.First().GroupName,
                    ValidPerc = true,
                    TotalPrice = p.Sum(c => c.QtyO * c.UnitRate)
                }).ToList();

            var curList = (from b in _mdbContext.TblCurrencies
                           select b).ToList();

            var ExchNowList = (from cur in curList
                               join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                               join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                               join sup in _dbContext.TblSuppliers on a.SpSupplierId equals sup.SupCode
                               where (a.SpPackageId == packageId && b.PrRevNo == 0)
                               select new LiveExchange
                               {
                                   fromCurrency = cur.CurCode,
                                   ExchRateNow = GetExchange(cur.CurCode)
                               }).ToList();

            var querySupp = (from cur in curList
                             join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                             join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                             join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                             join sup in _dbContext.TblSuppliers on a.SpSupplierId equals sup.SupCode
                             join boq in _dbContext.TblOriginalBoqs on c.RdBoqItem equals boq.ItemO
                             join g in _dbContext.ComparisonPackageGroups on boq.GroupId equals g.Id
                             where (a.SpPackageId == packageId && boq.Scope == packageId && b.PrRevNo == 0)
                             select new GroupingPackageSupplierPriceModel
                             {
                                 SupplierId = sup.SupCode,
                                 SupplierName = sup.SupName,
                                 LastRevisionDate = b.PrRevDate,
                                 AssignedPercentage = c.RdAssignedPerc,
                                 AssignedQty = c.RdAssignedQty,
                                 MissedPrice = c.RdMissedPrice,
                                 TotalPrice = (c.RdQty * c.RdPriceOrigCurrency),
                                 GroupId = g.Id,
                                 OriginalCurrency = cur.CurCode,
                                 ExchRate = b.PrExchRate,
                                 ExchRateNow = ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow
                             }).ToList();

            foreach (var group in groups)
            {
                var lsst = querySupp.Where(x => x.GroupId == group.Id).ToList();
                group.GroupingPackageSuppliersPrices = lsst
                    .GroupBy(x => new {
                        x.SupplierId,
                        x.SupplierName
                    })
                    .Select(p => new GroupingPackageSupplierPriceModel
                    {
                        SupplierId = p.First().SupplierId,
                        SupplierName = p.First().SupplierName,
                        LastRevisionDate = p.First().LastRevisionDate,
                        AssignedPercentage = p.First().AssignedPercentage,
                        MissedPrice = p.First().MissedPrice,
                        TotalPrice = p.Sum(c => c.TotalPrice) * p.First().ExchRateNow,
                        OriginalCurrency = p.First().OriginalCurrency,
                        ExchRate = p.First().ExchRate,
                        ExchRateNow = p.First().ExchRateNow
                    }).OrderBy(x => x.SupplierName).ToList();
            }
            return groups;
        }

        public List<GroupingBoqGroupModel> GetComparisonSheetResourcesByGroup(int packageId, SearchInput input)
        {
            IEnumerable<BoqRessourcesList> condQuery = (from o in _dbContext.TblOriginalBoqs
                                                        join b in _dbContext.TblBoqs on o.ItemO equals b.BoqItem
                                                        join r in _dbContext.TblResources on b.BoqResSeq equals r.ResSeq
                                                        join g in _dbContext.ComparisonPackageGroups on b.GroupId equals g.Id
                                                        where o.Scope == packageId
                                                        select new BoqRessourcesList
                                                        {
                                                            RowNumber = o.RowNumber,
                                                            SectionO = o.SectionO,
                                                            ItemO = o.ItemO,
                                                            DescriptionO = o.DescriptionO,
                                                            UnitO = o.UnitO,
                                                            QtyO = o.QtyO,
                                                            UnitRate = o.UnitRate,
                                                            Scope = o.Scope,
                                                            BoqSeq = b.BoqSeq,
                                                            BoqCtg = b.BoqCtg,
                                                            BoqUnitMesure = b.BoqUnitMesure,
                                                            BoqQty = b.BoqQty,
                                                            BoqUprice = b.BoqUprice,
                                                            BoqDiv = b.BoqDiv,
                                                            BoqPackage = b.BoqPackage,
                                                            BoqScope = b.BoqScope,
                                                            ResSeq = r.ResSeq,
                                                            ResDescription = r.ResDescription,
                                                            GroupName = g.Name,
                                                            GroupId = g.Id
                                                        });

            if (input.BOQDiv.Length > 0) condQuery = condQuery.Where(w => input.BOQDiv.Contains(w.SectionO));
            if (!string.IsNullOrEmpty(input.BOQItem)) condQuery = condQuery.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
            if (!string.IsNullOrEmpty(input.BOQDesc)) condQuery = condQuery.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
            if (!string.IsNullOrEmpty(input.SheetDesc)) condQuery = condQuery.Where(w => w.ObSheetDesc == input.SheetDesc);
            if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) condQuery = condQuery.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.Scope == input.Package);
            if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));
            if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));


            var groups = condQuery
                .GroupBy(x => new { x.GroupId, x.GroupName })
                //.Select(p => p.Gr)
                .Select(p => new GroupingBoqGroupModel
                {
                    Id = p.First().GroupId.HasValue ? p.First().GroupId.Value : 0,
                    Name = p.First().GroupName,
                    ValidPerc = true,
                    TotalPrice = p.Sum(c => c.BoqQty * c.BoqUprice)
                }).ToList();

            var curList = (from b in _mdbContext.TblCurrencies
                           select b).ToList();

            var ExchNowList = (from cur in curList
                               join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                               join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                               join sup in _dbContext.TblSuppliers on a.SpSupplierId equals sup.SupCode
                               where (a.SpPackageId == packageId && b.PrRevNo == 0)
                               select new LiveExchange
                               {
                                   fromCurrency = cur.CurCode,
                                   ExchRateNow = GetExchange(cur.CurCode)
                               }).ToList();

            var querySupp = (from cur in curList
                             join b in _dbContext.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                             join a in _dbContext.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                             join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                             join sup in _dbContext.TblSuppliers on a.SpSupplierId equals sup.SupCode
                             join boq in _dbContext.TblBoqs on c.RdResourceSeq equals boq.BoqSeq
                             join g in _dbContext.ComparisonPackageGroups on boq.GroupId equals g.Id

                             where (a.SpPackageId == packageId && boq.BoqScope == packageId && b.PrRevNo == 0)
                             select new GroupingPackageSupplierPriceModel
                             {
                                 SupplierId = sup.SupCode,
                                 SupplierName = sup.SupName,
                                 LastRevisionDate = b.PrRevDate,
                                 AssignedPercentage = c.RdAssignedPerc,
                                 AssignedQty = c.RdAssignedQty,
                                 MissedPrice = c.RdMissedPrice,
                                 TotalPrice = (c.RdQty * c.RdPriceOrigCurrency),
                                 GroupId = g.Id,
                                 OriginalCurrency = cur.CurCode,
                                 ExchRate = b.PrExchRate,
                                 ExchRateNow = ExchNowList.Find(x=> x.fromCurrency ==cur.CurCode).ExchRateNow
                             }).ToList();

            foreach (var group in groups)
            {
                var lsst = querySupp.Where(x => x.GroupId == group.Id).ToList();
                group.GroupingPackageSuppliersPrices = lsst
                    .GroupBy(x => new { x.SupplierId, 
                        x.SupplierName
                        
                        })
                    .Select(p => new GroupingPackageSupplierPriceModel
                    {
                        SupplierId = p.First().SupplierId,
                        SupplierName = p.First().SupplierName,
                        LastRevisionDate = p.First().LastRevisionDate,
                        AssignedPercentage = p.First().AssignedPercentage,
                        MissedPrice = p.First().MissedPrice,
                        TotalPrice = p.Sum(c => c.TotalPrice) * p.First().ExchRateNow,
                        OriginalCurrency = p.First().OriginalCurrency,
                        ExchRate = p.First().ExchRate,
                        ExchRateNow =p.First().ExchRateNow
                    }).OrderBy(x => x.SupplierName).ToList();
            }
            return groups;
        }


        public string GetComparisonSheet_Excel(int packageId, SearchInput input, List<boqPackageList> boqPackageList, List<TmpConditionsReply> comcondRepLst, List<TmpConditionsReply> techcondRepLst)
        {
            List<GroupingBoqModel> items = GetComparisonSheet(packageId, input);

            var package = _dbContext.PackagesNetworks.Where(x => x.IdPkge == packageId).FirstOrDefault();
            string PackageName = package.PkgeName;

            var p = _dbContext.TblParameters.FirstOrDefault();
            var proj = _pdbContext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefault();
            string ProjectName = proj.PrjName;

            List<string> suppliers = new List<string>();

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("BOQ Comparison");
                worksheet.Columns.AutoFit();
                worksheet.Protection.IsProtected = false;

                int row, j, c;

                worksheet.Cells["A1:C1"].Merge = true;
                worksheet.Cells["A2:C2"].Merge = true;
                worksheet.Cells["A3:C3"].Merge = true;
                worksheet.Cells["A4:C4"].Merge = true;
                worksheet.Cells["A5:C5"].Merge = true;

                worksheet.Cells[2, 1].Value = "Résumé des Offres/Feuille de Comparaison";
                worksheet.Cells[2, 1].Style.Font.Bold = true;
                worksheet.Cells[2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[3, 1].Value = "Project:" + ProjectName;
                worksheet.Cells[3, 1].Style.Font.Bold = true;
                worksheet.Cells[3, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[4, 1].Value = "Department :";
                worksheet.Cells[4, 1].Style.Font.Bold = true;
                worksheet.Cells[4, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[5, 1].Value = PackageName;
                worksheet.Cells[5, 1].Style.Font.Bold = true;
                worksheet.Cells[5, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.SelectedRange[5, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[5, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.SelectedRange[7, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[7, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells["E6:F6"].Merge = true;
                worksheet.Cells[6, 5].Value = "ACC budget";
                worksheet.Cells[6, 5].Style.Font.Bold = true;
                worksheet.Cells[6, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Column(5).Width = 20;

                if (items.Count > 0)
                {
                    GroupingBoqModel item1 = items.First();
                    GroupingResourceModel sup = item1.GroupingResources.First();
                    string boq = item1.ItemO;

                    //var lst = item1.GroupingPackageSuppliersPrices.Where(x => x.BoqItemO == boq).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                    //var lst1 = lst.OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                    
                    var SupList = sup.GroupingPackageSuppliersPrices.OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();

                    int col = 7;
                    int m = 7;
                    foreach (var l in SupList)
                    {
                        worksheet.Cells[6, m].Value = l.SupplierName + " " + DateTime.Parse(l.LastRevisionDate.ToString()).ToString("dd/MM/yyyy");
                        worksheet.Cells[6, m].Style.Font.Bold = true;
                        worksheet.Columns[m].Style.WrapText = true;
                        worksheet.Column(m).AutoFit();
                        worksheet.Cells[6, m].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[6, m, 6, m + 1].Merge = true;
                        m = m + 2;
                        if (!suppliers.Contains(l.SupplierName))
                            suppliers.Add(l.SupplierName.ToString());

                        col++;
                    }
                }

                row = 7;
                worksheet.Cells[row, 1].Value = "No";
                worksheet.Cells[row, 2].Value = "Description";
                worksheet.Column(2).Width = 70;
                worksheet.Columns[2].Style.WrapText = true;
                worksheet.Column(2).AutoFit();
                worksheet.Cells[row, 3].Value = "U.";
                worksheet.Cells[row, 4].Value = "Qty Total";
                worksheet.Cells[row, 5].Value = "P.U.";
                worksheet.Cells[row, 6].Value = "P.T.";

                worksheet.Cells[row, 1].EntireRow.Style.Font.Bold = true;
                worksheet.Cells[row, 1].EntireRow.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                row = 9;
                j = 0;
                foreach (var item in items)
                {
                    //var lst = item.GroupingPackageSuppliersPrices.OrderByDescending(s => s.GroupId).OrderByDescending(s => s.BoqItemO).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                    //foreach (var sup in item.GroupingPackageSuppliersPrices)
                    //{
                        worksheet.Cells[row, 1].Value = j++;
                        worksheet.Column(2).Width = 70;
                        worksheet.Cells[row, 1].Value = (item.ItemO) == null ? "" : item.ItemO;
                        worksheet.Cells[row, 2].Value = (item.DescriptionO) == null ? "" : item.DescriptionO;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        worksheet.Columns[2].Style.WrapText = true;

                    row++;

                        foreach (var res in item.GroupingResources)
                        {
                            worksheet.Cells[row, 2].Value = (res.ResourceDescription) == null ? "" : res.ResourceDescription;
                            worksheet.Cells[row, 3].Value = (res.Unit) == null ? "" : res.Unit;
                            worksheet.Cells[row, 4].Value = (res.Qty) == null ? "" : res.Qty;
                            worksheet.Cells[row, 5].Value = (res.UnitPrice) == null ? "" : res.UnitPrice;
                            worksheet.Cells[row, 6].Value = (res.TotalPrice) == null ? "" : res.TotalPrice;

                            int col = 0;
                            foreach (var suplier in suppliers)
                            {
                                var v = worksheet.Cells[7, 7 + col].Value;
                                if (v == null)
                                {
                                    worksheet.Cells[7, 7 + col].Value = "P.U.";
                                    worksheet.Cells[7, 8 + col].Value = "P.T.";
                                }

                                var supReply = res.GroupingPackageSuppliersPrices.Where(x => x.BoqResourceId == res.BoqSeq && x.SupplierName == suplier.ToString()).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).FirstOrDefault();
                                if (supReply != null)
                                {
                                    worksheet.Cells[row, 7 + col].Value = (supReply.UnitPrice) == null ? "" : supReply.UnitPrice;
                                    worksheet.Cells[row, 8 + col].Value = (supReply.TotalPrice) == null ? "" : supReply.TotalPrice;
                                }
                                col = col + 2;
                            }
                            row++;
                        }
                    //}
                    row++;
                }

                row++;

                //Commercial Conditions
                var comcondRep = comcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var replies = comcondRep.GroupBy(x => new { x.CondDesc })
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc
                })
                .ToList();

                if (replies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Commercial Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in replies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = comcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 2;
                        }
                        row++;
                    }
                }

                row++;

                //Technical Conditions
                var techcondRep = techcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var treplies = techcondRep.GroupBy(x => new { x.CondDesc })
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc
                })
                .ToList();

                if (treplies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Technical Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in treplies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = techcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 2;
                        }
                        row++;
                    }
                }


                xlPackage.Save();
                stream.Position = 0;
                string excelName = $"{PackageName}-Comparison.xlsx";

                //string path = @"C:\App\";

                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //string FullPath = path + excelName;

                if (File.Exists(excelName))
                    File.Delete(excelName);

                xlPackage.SaveAs(excelName);

                return excelName;
            }
        }

        public string GetComparisonSheetByBoq_Excel(int packageId, SearchInput input, List<boqPackageList> boqPackageList, List<TmpConditionsReply> comcondRepLst, List<TmpConditionsReply> techcondRepLst)
        {
            List<GroupingBoqModel> items = GetComparisonSheetByBoq(packageId, input);

            var package = _dbContext.PackagesNetworks.Where(x => x.IdPkge == packageId).FirstOrDefault();
            string PackageName = package.PkgeName;

            var p = _dbContext.TblParameters.FirstOrDefault();
            var proj = _pdbContext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefault();
            string ProjectName = proj.PrjName;

            List<string> suppliers = new List<string>(); 

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("BOQ Comparison");
                worksheet.Columns.AutoFit();
                worksheet.Protection.IsProtected = false;

                int row, j, c;

                worksheet.Cells["A1:C1"].Merge = true;
                worksheet.Cells["A2:C2"].Merge = true;
                worksheet.Cells["A3:C3"].Merge = true;
                worksheet.Cells["A4:C4"].Merge = true;
                worksheet.Cells["A5:C5"].Merge = true;

                worksheet.Cells[2, 1].Value = "Résumé des Offres/Feuille de Comparaison";
                worksheet.Cells[2, 1].Style.Font.Bold = true;
                worksheet.Cells[2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[3, 1].Value = "Project:" + ProjectName;
                worksheet.Cells[3, 1].Style.Font.Bold = true;
                worksheet.Cells[3, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[4, 1].Value = "Department :";
                worksheet.Cells[4, 1].Style.Font.Bold = true;
                worksheet.Cells[4, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[5, 1].Value = PackageName;
                worksheet.Cells[5, 1].Style.Font.Bold = true;
                worksheet.Cells[5, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.SelectedRange[5, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[5, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.SelectedRange[7, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[7, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells["E6:F6"].Merge = true;
                worksheet.Cells[6, 5].Value = "ACC budget";
                worksheet.Cells[6, 5].Style.Font.Bold = true;
                worksheet.Cells[6, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Column(5).Width = 20;

                if (items.Count > 0)
                {
                    GroupingBoqModel item1 = items.First();
                    GroupingPackageSupplierPriceModel supItem = item1.GroupingPackageSuppliersPrices.First();
                    //string boq = sup.BoqItemO;

                    var SupList = item1.GroupingPackageSuppliersPrices.OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                    //var SupList = lst.OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();

                    int col = 7;
                    int m = 7;
                    foreach (var l in SupList)
                    {
                        worksheet.Cells[6, m].Value = l.SupplierName + " " + DateTime.Parse(l.LastRevisionDate.ToString()).ToString("dd/MM/yyyy");
                        worksheet.Cells[6, m].Style.Font.Bold = true;
                        worksheet.Columns[m].Style.WrapText = true;
                        worksheet.Column(m).AutoFit();
                        worksheet.Cells[6, m].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[6, m,6,m+1].Merge = true;
                        m = m + 2;
                        if (!suppliers.Contains(l.SupplierName))
                            suppliers.Add(l.SupplierName.ToString());

                        col++;
                    }
                }

                row = 7;
                worksheet.Cells[row, 1].Value = "No";
                worksheet.Cells[row, 2].Value = "Description";
                worksheet.Column(2).Width = 70;
                worksheet.Columns[2].Style.WrapText = true;
                worksheet.Column(2).AutoFit();
                worksheet.Cells[row, 3].Value = "U.";
                worksheet.Cells[row, 4].Value = "Qty Total";
                worksheet.Cells[row, 5].Value = "P.U.";
                worksheet.Cells[row, 6].Value = "P.T.";

                worksheet.Cells[row, 1].EntireRow.Style.Font.Bold = true;
                worksheet.Cells[row, 1].EntireRow.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                row = 9;
                j = 0;
                foreach (var item in items)
                {
                    var lst = item.GroupingPackageSuppliersPrices.OrderByDescending(s => s.GroupId).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                    foreach (var sup in item.GroupingPackageSuppliersPrices)
                    {
                        worksheet.Cells[row, 1].Value = j++;
                        worksheet.Column(2).Width = 70;
                        worksheet.Cells[row, 1].Value = (item.ItemO) == null ? "" : item.ItemO;
                        worksheet.Cells[row, 2].Value = (item.DescriptionO) == null ? "" : item.DescriptionO;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        worksheet.Columns[2].Style.WrapText = true;
                        worksheet.Cells[row, 3].Value = (item.Unit) == null ? "" : item.Unit;
                        worksheet.Cells[row, 4].Value = (item.Qty) == null ? "" : item.Qty;
                        worksheet.Cells[row, 5].Value = (item.UnitPrice) == null ? "" : item.UnitPrice;
                        worksheet.Cells[row, 6].Value = (item.TotalPrice) == null ? "" : item.TotalPrice;

                        int col = 0;
                        foreach (var suplier in suppliers)
                        {
                            var v = worksheet.Cells[7, 7 + col].Value;
                            if (v == null)
                            {
                                worksheet.Cells[7, 7 + col].Value = "P.U.";
                                worksheet.Cells[7, 8 + col].Value = "P.T.";
                            }

                            var supReply = item.GroupingPackageSuppliersPrices.Where(x => x.BoqItemO == sup.BoqItemO && x.SupplierName==suplier.ToString()).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).FirstOrDefault();                                                                           
                            if (supReply != null)
                            {
                                worksheet.Cells[row, 7+col].Value = (supReply.UnitPrice) == null ? "" : supReply.UnitPrice;
                                worksheet.Cells[row, 8+col].Value = (supReply.TotalPrice) == null ? "" : supReply.TotalPrice;
                            }                               
                            col=col+2;
                        }
                    }
                    row++;
                }

                row++;

                //Commercial Conditions
                var comcondRep = comcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var replies = comcondRep.GroupBy(x => new { x.CondDesc })
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc
                })
                .ToList();

                if (replies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Commercial Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in replies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = comcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup=colsup+2;
                        }
                        row++;
                    }
                }

                row++;

                //Technical Conditions
                var techcondRep = techcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var treplies = techcondRep.GroupBy(x => new { x.CondDesc })
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc
                })
                .ToList();

                if (treplies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Technical Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in treplies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = techcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 2;
                        }
                        row++;
                    }
                }


                xlPackage.Save();
                stream.Position = 0;
                string excelName = $"{PackageName}-Comparison.xlsx";

                //string path = @"C:\App\";

                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //string FullPath = path + excelName;

                if (File.Exists(excelName))
                    File.Delete(excelName);

                xlPackage.SaveAs(excelName);

                return excelName;
            }
        }
         
        public string GetComparisonSheetResourcesByGroup_Excel(int packageId, SearchInput input, List<TmpConditionsReply> comcondRepLst, List<TmpConditionsReply> techcondRepLst)
        {
            List<GroupingBoqGroupModel> items = GetComparisonSheetBoqByGroup(packageId, input);

            var package = _dbContext.PackagesNetworks.Where(x => x.IdPkge == packageId).FirstOrDefault();
            string PackageName = package.PkgeName;

            var p = _dbContext.TblParameters.FirstOrDefault();
            var proj = _pdbContext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefault();
            string ProjectName = proj.PrjName;

            List<string> suppliers = new List<string>(); ;

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Recap");
                worksheet.Columns.AutoFit();
                worksheet.Protection.IsProtected = false;

                int row, j, c;

                worksheet.Cells["A1:C1"].Merge = true;
                worksheet.Cells["A2:C2"].Merge = true;
                worksheet.Cells["A3:C3"].Merge = true;
                worksheet.Cells["A4:C4"].Merge = true;
                worksheet.Cells["A5:C5"].Merge = true;

                worksheet.Cells[2, 1].Value = "Résumé des Offres/Feuille de Comparaison";
                worksheet.Cells[2, 1].Style.Font.Bold = true;
                worksheet.Cells[2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[3, 1].Value = "Project:" + ProjectName;
                worksheet.Cells[3, 1].Style.Font.Bold = true;
                worksheet.Cells[3, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[4, 1].Value = "Department :";
                worksheet.Cells[4, 1].Style.Font.Bold = true;
                worksheet.Cells[4, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[5, 1].Value = PackageName;
                worksheet.Cells[5, 1].Style.Font.Bold = true;
                worksheet.Cells[5, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.SelectedRange[6, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[6, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.SelectedRange[7, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[7, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells[6, 4].Value = "ACC budget";
                worksheet.Cells[6, 4].Style.Font.Bold = true;
                worksheet.Cells[6, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells["E6:F6"].Merge = true;
                worksheet.Cells[6, 5].Value = "ACC budget breakdown";
                worksheet.Cells[6, 5].Style.Font.Bold = true;
                worksheet.Cells[6, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Column(5).Width = 20;

                if (items.Count > 0)
                {
                    GroupingBoqGroupModel item1 = items.First();
                    GroupingPackageSupplierPriceModel sup = item1.GroupingPackageSuppliersPrices.First();
                    string boq = sup.BoqItemO;

                    var lst = item1.GroupingPackageSuppliersPrices.Where(x => x.BoqItemO == boq).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                    var lst1 = lst.OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();

                    int col = 7;
                    foreach (var l in lst1)
                    {
                        worksheet.Cells[6, col].Value = l.SupplierName + " " + DateTime.Parse(l.LastRevisionDate.ToString()).ToString("dd/MM/yyyy");
                        worksheet.Cells[6, col].Style.Font.Bold = true;
                        worksheet.Columns[col].Style.WrapText = true;
                        worksheet.Column(col).AutoFit();

                        if (!suppliers.Contains(l.SupplierName))
                            suppliers.Add(l.SupplierName.ToString());

                        col++;
                    }
                }

                row = 7;
                worksheet.Cells[row, 1].Value = "No";
                worksheet.Cells[row, 2].Value = "Description";
                worksheet.Column(2).Width = 70;
                worksheet.Columns[2].Style.WrapText = true;
                worksheet.Column(2).AutoFit();
                worksheet.Cells[row, 3].Value = "Qty";
                worksheet.Cells[row, 4].Value = "Total";
                worksheet.Cells[row, 5].Value = "Material";
                worksheet.Cells[row, 6].Value = "Workmanship";

                worksheet.Cells[row, 1].EntireRow.Style.Font.Bold = true;
                worksheet.Cells[row, 1].EntireRow.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                row = 8;
                j = 0;
                foreach (var item in items)
                {
                    var lst = item.GroupingPackageSuppliersPrices.OrderByDescending(s => s.GroupId).OrderByDescending(s => s.BoqItemO).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                    foreach (var sup in item.GroupingPackageSuppliersPrices)
                    {
                        worksheet.Cells[row, 1].Value = j++;
                        worksheet.Column(2).Width = 70;
                        worksheet.Cells[row, 2].Value = (item.Name) == null ? "" : item.Name;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        worksheet.Columns[2].Style.WrapText = true;
                        worksheet.Cells[row, 3].Value = (sup.Qty) == null ? "" : sup.Qty;
                        worksheet.Cells[row, 4].Value = (item.TotalPrice) == null ? "" : item.TotalPrice;

                        var supList = item.GroupingPackageSuppliersPrices.Where(x => x.BoqItemO == sup.BoqItemO).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                        c = 0;
                        foreach (var s in supList)
                        {
                            worksheet.Cells[row, 7 + c++].Value = (s.TotalPrice) == null ? "" : s.TotalPrice;
                        }
                    }
                    row++;
                }

                row++;

                //Commercial Conditions
                var comcondRep = comcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var replies = comcondRep.GroupBy(x => new { x.CondDesc })
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc
                })
                .ToList();

                if (replies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Commercial Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in replies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = comcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 2;
                        }
                        row++;
                    }
                }

                row++;

                //Technical Conditions
                var techcondRep = techcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var treplies = techcondRep.GroupBy(x => new { x.CondDesc })
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc
                })
                .ToList();

                if (treplies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Technical Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in treplies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = techcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 2;
                        }
                        row++;
                    }
                }


                xlPackage.Save();
                stream.Position = 0;
                string excelName = $"{PackageName}-Comparison.xlsx";

                //string path = @"C:\App\";

                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //string FullPath = path + excelName;

                if (File.Exists(excelName))
                    File.Delete(excelName);

                xlPackage.SaveAs(excelName);

                return excelName;
            }
        }
  
        public string GetComparisonSheetBoqByGroup_Excel(int packageId, SearchInput input, List<boqPackageList> boqPackageList, List<TmpConditionsReply> comcondRepLst, List<TmpConditionsReply> techcondRepLst)
        {
            List<GroupingBoqGroupModel> items = GetComparisonSheetBoqByGroup(packageId, input);

            var package = _dbContext.PackagesNetworks.Where(x => x.IdPkge == packageId).FirstOrDefault();
            string PackageName = package.PkgeName;

            var p = _dbContext.TblParameters.FirstOrDefault();
            var proj = _pdbContext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefault();
            string ProjectName = proj.PrjName;

            List<string> suppliers = new List<string>(); ;

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Recap");
                worksheet.Columns.AutoFit();
                worksheet.Protection.IsProtected = false;

                int row, j,c;
   
                worksheet.Cells["A1:C1"].Merge = true;
                worksheet.Cells["A2:C2"].Merge = true;
                worksheet.Cells["A3:C3"].Merge = true;
                worksheet.Cells["A4:C4"].Merge = true;
                worksheet.Cells["A5:C5"].Merge = true;

                worksheet.Cells[2, 1].Value = "Résumé des Offres/Feuille de Comparaison";
                worksheet.Cells[2, 1].Style.Font.Bold = true;
                worksheet.Cells[2, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[3, 1].Value = "Project:" + ProjectName;
                worksheet.Cells[3, 1].Style.Font.Bold = true;
                worksheet.Cells[3, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[4, 1].Value = "Department :";
                worksheet.Cells[4, 1].Style.Font.Bold = true;
                worksheet.Cells[4, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[5, 1].Value = PackageName;
                worksheet.Cells[5, 1].Style.Font.Bold = true;
                worksheet.Cells[5, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.SelectedRange[6, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[6, 50].Style.HorizontalAlignment =OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.SelectedRange[7, 50].Style.Font.Bold = true;
                worksheet.SelectedRange[7, 50].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells[6, 4].Value = "ACC budget";
                worksheet.Cells[6, 4].Style.Font.Bold = true;
                worksheet.Cells[6, 4].Style.HorizontalAlignment=OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells["E6:F6"].Merge = true;
                worksheet.Cells[6, 5].Value = "ACC budget breakdown";
                worksheet.Cells[6, 5].Style.Font.Bold = true;
                worksheet.Cells[6, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Column(5).Width = 20;

                if (items.Count > 0)
                {
                    GroupingBoqGroupModel item1 = items.First();                  
                    GroupingPackageSupplierPriceModel sup = item1.GroupingPackageSuppliersPrices.First();
                    //string boq = sup.BoqItemO;

                    var Suplist = item1.GroupingPackageSuppliersPrices.OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                    //var lst1 =lst.OrderByDescending(s => s.SupplierName).OrderByDescending(s=>s.LastRevisionDate).ToList();

                    int col = 7;
                    foreach (var l in Suplist)
                    {
                        worksheet.Cells[6, col].Value = l.SupplierName + " " + DateTime.Parse(l.LastRevisionDate.ToString()).ToString("dd/MM/yyyy") ; 
                        worksheet.Cells[6, col].Style.Font.Bold = true;
                        worksheet.Columns[col].Style.WrapText = true;
                        worksheet.Column(col).AutoFit();

                        if (!suppliers.Contains(l.SupplierName))    
                          suppliers.Add(l.SupplierName.ToString());

                        col++;
                    }
                }

                row = 7;
                worksheet.Cells[row, 1].Value = "No";
                worksheet.Cells[row, 2].Value = "Description";
                worksheet.Column(2).Width = 70;
                worksheet.Columns[2].Style.WrapText = true;
                worksheet.Column(2).AutoFit();
                worksheet.Cells[row, 3].Value = "Qty";
                worksheet.Cells[row, 4].Value = "Total";
                worksheet.Cells[row, 5].Value = "Material";
                worksheet.Cells[row, 6].Value = "Workmanship";

                worksheet.Cells[row, 1].EntireRow.Style.Font.Bold = true;
                worksheet.Cells[row, 1].EntireRow.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                row = 8;
                j = 1;
                foreach(var item in items)
                {
                    worksheet.Cells[row, 1].Value = j++;
                    worksheet.Cells[row, 2].Value = (item.Name) == null ? "" : item.Name;
                    worksheet.Column(2).Width = 70;
                    worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Columns[2].Style.WrapText = true;

                    var lst = item.GroupingPackageSuppliersPrices.OrderByDescending(s => s.GroupId).OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                    foreach (var sup in item.GroupingPackageSuppliersPrices)
                    {                          
                       //worksheet.Cells[row, 3].Value = (sup.Qty) == null ? "" : sup.Qty;
                       worksheet.Cells[row, 4].Value = (sup.TotalPrice) == null ? "" : sup.TotalPrice;

                        var supList = item.GroupingPackageSuppliersPrices.OrderByDescending(s => s.SupplierName).OrderByDescending(s => s.LastRevisionDate).ToList();
                        c = 0;
                        foreach (var s in supList)
                        {
                            worksheet.Cells[row, 7+c++].Value = (s.TotalPrice) == null ? "" : s.TotalPrice;
                        }                     
                    }
                    row++;
                }

                row++;

                //Commercial Conditions
                var comcondRep = comcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var replies = comcondRep.GroupBy(x =>new {x.CondDesc})
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpConditionsReply
                {
                    CondId=p.CondId,
                    CondDesc = p.CondDesc             
                })
                .ToList();

                if (replies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Commercial Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in replies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = comcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId==reply.CondId).FirstOrDefault();
                            if (supReply!= null) 
                              worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 2;
                        }
                        row++;
                    }
                }

                row++;

                //Technical Conditions
                var techcondRep = techcondRepLst.OrderBy(r => r.CondDesc).ToList();

                var treplies = techcondRep.GroupBy(x => new { x.CondDesc })
                .Select(p => p.FirstOrDefault())
                .Select(p => new TmpConditionsReply
                {
                    CondId = p.CondId,
                    CondDesc = p.CondDesc
                })
                .ToList();

                if (treplies.Count > 0)
                {
                    worksheet.SelectedRange[row, 3].Merge = true;
                    worksheet.Cells[row, 1].Value = "Technical Conditions";
                    worksheet.Cells[row, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    worksheet.Cells[row, 1].Style.Font.Bold = true;

                    row++;
                    foreach (var reply in treplies)
                    {
                        worksheet.Cells[row, 2].Value = reply.CondDesc;
                        worksheet.Cells[row, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        int colsup = 7;
                        foreach (var sup in suppliers)
                        {
                            var supReply = techcondRepLst.Where(x => x.SupName == sup.ToString() && x.CondId == reply.CondId).FirstOrDefault();
                            if (supReply != null)
                                worksheet.Cells[row, colsup].Value = (supReply.CondReply) == null ? "" : supReply.CondReply;

                            colsup = colsup + 2;
                        }
                        row++;
                    }
                }


                xlPackage.Save();
                stream.Position = 0;
                string excelName = $"{PackageName}-Comparison.xlsx";

                //string path = @"C:\App\";

                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //string FullPath = path + excelName;

                if (File.Exists(excelName))
                    File.Delete(excelName);

                xlPackage.SaveAs(excelName);

                return excelName;
            }
        }

        private double GetExchange(string foreignCurrency)
        {
            var result = from a in _dbContext.TblParameters
                         join b in _dbContext.TblCurrencies
                         on a.EstimatedCur equals b.CurId
                         select new ProjectCurrency
                         {
                             curId = (int)a.EstimatedCur,
                             curCode = b.CudCode
                         };


            string localCurrency = result.FirstOrDefault().curCode;

            CurrencyConverterRepository currencyConverterRepository = new CurrencyConverterRepository();
            return currencyConverterRepository.GetCurrencyExchange(localCurrency, foreignCurrency);
        }

    }
}
