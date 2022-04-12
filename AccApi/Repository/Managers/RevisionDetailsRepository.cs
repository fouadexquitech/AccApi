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
        public RevisionDetailsRepository(AccDbContext dbContext, PolicyDbContext pdbContext)
        {
            _dbContext = dbContext;
            _pdbContext = pdbContext;
        }

        public List<RevisionDetailsList> GetRevisionDetails(int RevisionId, string itemDesc, string resource)
        {
            var supPackRev = _dbContext.TblSupplierPackageRevisions.SingleOrDefault(b => (b.PrRevId == RevisionId));
            int PackageSuppliersID = (int)supPackRev.PrPackSuppId;

            var supPack = _dbContext.TblSupplierPackages.Where(x => x.SpPackSuppId == PackageSuppliersID).FirstOrDefault();
            byte byBoq = (byte)((supPack.SpByBoq == null) ? 0 : supPack.SpByBoq);

            RevisionDetailsList revDtlList = new RevisionDetailsList();
            IEnumerable<RevisionDetailsList> revDtlQry;

            if (byBoq == 1)
            {
                revDtlQry = (from b in _dbContext.TblRevisionDetails
                             join c in _dbContext.TblOriginalBoqs on b.RdBoqItem equals c.ItemO
                             where b.RdRevisionId == RevisionId

                             select new RevisionDetailsList
                             {
                                 RdResourceSeq = b.RdResourceSeq,
                                 RdPrice = b.RdPrice,
                                 RdMissedPrice = b.RdMissedPrice,
                                 RdBoqItem = b.RdBoqItem,
                                 RdItemDescription = c.DescriptionO
                             }).ToList();

                if (itemDesc != null) revDtlQry = revDtlQry.Where(w => w.RdItemDescription.ToUpper().Contains(itemDesc.ToUpper()));
            }
            else
            {
                revDtlQry = (from b in _dbContext.TblRevisionDetails
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
                                 RdItemDescription = e.ResDescription
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
            return true;
        }

        public bool AssignSupplierListRessourceList(int packId, AssignSuppliertRes item)
        {
            foreach (var sup in item.supplierPercentList)
            {
                //List<ressourceItem> itemList = item.supplierResItemList;

                //join d in item.supplierResItemList.AsQueryable() on c.RdResourceSeq equals d.resId
                //(a.SpPackageId == packId && a.SpSupplierId == sup.supID && b.PrRevNo == 0)

                var revisionDetails = (from a in _dbContext.TblSupplierPackages
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

            return true;
        }

        public bool AssignSupplierListBoqList(int packId, AssignSuppliertBoq item)
        {

            foreach (var sup in item.supplierPercentList)
            {
                var revisionDetails = (from a in _dbContext.TblSupplierPackages
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

            return true;
        }

        public bool AssignSupplierGroup(int packId, bool byBoq, List<SupplierGroups> SupplierGroupList)
        {
            if (!byBoq)
            {
                foreach (var sup in SupplierGroupList)
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
            }
            else
            {
                foreach (var sup in SupplierGroupList)
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
            }

            return true;
        }

        public bool AssignSupplierListGroupList(int packId, bool byBoq, AssignSupplierGroup item)
        {
            if (byBoq)
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

            return true;
        }

        public bool AssignSupplierBOQ(int packId, List<SupplierBOQ> SupplierBOQList)
        {
            foreach (var sup in SupplierBOQList)
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

        public bool SendCompToManagement(string parameters, IFormFile attachement)
        {
            string send = "";

            string[] paramArray = parameters.Split(",");
            string[] emails = new string[paramArray.Length - 1];
            if (paramArray.Length > 0)
            {
                int packId = Convert.ToInt32(paramArray[0]);
                for (int i = 1; i < paramArray.Length; i++)
                {
                    emails[i - 1] = paramArray[i];
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

                string MailBody;

                MailBody = "Dear Sir,";
                MailBody += Environment.NewLine;
                MailBody += Environment.NewLine;
                MailBody += "Please find attached Comparison sheet.";
                MailBody += Environment.NewLine;
                MailBody += Environment.NewLine;
                MailBody += Environment.NewLine;
                MailBody += Environment.NewLine;
                MailBody += "Best regards";



                var AttachmentList = new List<string>();

                Mail m = new Mail();
                var res = m.SendMail(mylistTo, mylistCC, Subject, MailBody, AttachmentList, false, attachement);

                send = "sent";
            }

            return (send == "sent");
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





            var querySupp = (from a in _dbContext.TblSupplierPackages
                             join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                             join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                             join sup in _dbContext.TblSuppliers on a.SpSupplierId equals sup.SupCode
                             where (a.SpPackageId == packageId && b.PrRevNo == 0)
                             select new GroupingPackageSupplierPriceModel
                             {
                                 SupplierId = sup.SupCode,
                                 SupplierName = sup.SupName,
                                 LastRevisionDate = b.PrRevDate,
                                 AssignedPercentage = c.RdAssignedPerc,
                                 MissedPrice = c.RdMissedPrice,
                                 OriginalCurrencyPrice = c.RdPriceOrigCurrency,
                                 Qty = c.RdQty,
                                 UnitPrice = c.RdPrice,
                                 TotalPrice = (c.RdQty * c.RdPrice),
                                 BoqItemO = c.RdBoqItem

                             }).ToList();


            foreach (var item in items)
            {

                item.GroupingPackageSuppliersPrices = querySupp.Where(x => x.BoqItemO == item.ItemO).ToList();


            }

            return items;


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


            var querySupp = (from a in _dbContext.TblSupplierPackages
                             join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                             join c in _dbContext.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                             join sup in _dbContext.TblSuppliers on a.SpSupplierId equals sup.SupCode
                             where (a.SpPackageId == packageId && b.PrRevNo == 0)
                             select new GroupingPackageSupplierPriceModel
                             {
                                 SupplierId = sup.SupCode,
                                 SupplierName = sup.SupName,
                                 LastRevisionDate = b.PrRevDate,
                                 AssignedPercentage = c.RdAssignedPerc,
                                 MissedPrice = c.RdMissedPrice,
                                 OriginalCurrencyPrice = c.RdPriceOrigCurrency,
                                 Qty = c.RdQty,
                                 UnitPrice = c.RdPrice,
                                 TotalPrice = (c.RdQty * c.RdPrice),
                                 BoqResourceId = c.RdResourceSeq

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
                    GroupingPackageSuppliersPrices = querySupp.Where(x => x.BoqResourceId == y.BoqSeq).ToList()

                }).ToList();
            }

            return items;

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


            var querySupp = (from a in _dbContext.TblSupplierPackages
                             join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
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
                                 MissedPrice = c.RdMissedPrice,
                                 TotalPrice = (c.RdQty * c.RdPrice),
                                 GroupId = g.Id

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
                        TotalPrice = p.Sum(c => c.TotalPrice)
                    }).ToList();
            }

            return groups;

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


            var querySupp = (from a in _dbContext.TblSupplierPackages
                             join b in _dbContext.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
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
                                 MissedPrice = c.RdMissedPrice,
                                 TotalPrice = (c.RdQty * c.RdPrice),
                                 GroupId = g.Id

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
                        TotalPrice = p.Sum(c => c.TotalPrice)
                    }).ToList();
            }



            return groups;
        }



    }
}
