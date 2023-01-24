using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AccApi.Repository.Managers
{
    public class PackageRepository : IPackageRepository
    {
        private readonly AccDbContext _context;
        private readonly MasterDbContext _mcontext;
        private readonly IMapper _mapper;

        public PackageRepository(AccDbContext context, MasterDbContext mcontext, IMapper mapper)
        {
            _context = context;
            _mcontext = mcontext;
            _mapper = mapper;
        }

        public List<BoqRessourcesList> GetOriginalBoqList(SearchInput input)
        {
            //var results = from b in _context.TblOriginalBoqs
            //              where (input.BOQDiv != null && b.SectionO == input.BOQDiv)
            //              where (input.SheetDesc != null && b.ObSheetDesc == input.SheetDesc)
            //              where (input.FromRow != null && input.ToRow != null && b.RowNumber <= int.Parse(input.FromRow) && b.RowNumber >= int.Parse(input.ToRow))
            //              orderby b.RowNumber
            //              select b;


            //var results = from b in _context.TblOriginalBoqs
            //              where (input.BOQDiv != null || b.SectionO == input.BOQDiv)
            //                   && (input.SheetDesc != null || b.ObSheetDesc == input.SheetDesc)
            //                   && ((input.FromRow != null && input.ToRow != null) || (b.RowNumber >= int.Parse(input.FromRow) && b.RowNumber <= int.Parse(input.ToRow)))
            //              orderby b.RowNumber
            //              select b;

            var lstPackages = (from p in _context.PackagesNetworks
                               select new packagesList
                               {
                                   id = p.IdPkge,
                                   name = p.PkgeName
                               }).ToList();

            IEnumerable<BoqRessourcesList> condQuery = (from o in _context.TblOriginalBoqs
                                                        join b in _context.TblBoqs on o.ItemO equals b.BoqItem
                                                        join r in _context.TblResources on b.BoqResSeq equals r.ResSeq
                                                        join p in _context.PackagesNetworks on o.Scope equals p.IdPkge into gj
                                                        from pk in gj.DefaultIfEmpty()
                                                        select new BoqRessourcesList
                                                        {
                                                            RowNumber = o.RowNumber,
                                                            SectionO = o.SectionO,
                                                            ItemO = o.ItemO,
                                                            DescriptionO = o.DescriptionO,
                                                            UnitO = o.UnitO,                                                           
                                                            UnitRate = o.UnitRate,
                                                            Scope = o.Scope,
                                                            BoqSeq = b.BoqSeq,
                                                            BoqCtg = b.BoqCtg,
                                                            BoqUnitMesure = b.BoqUnitMesure,
                                                            BoqUprice = b.BoqUprice,
                                                            BoqDiv = b.BoqDiv,
                                                            BoqPackage = b.BoqPackage,
                                                            BoqScope = b.BoqScope,
                                                            ResDescription = r.ResDescription,
                                                            L2 = ((o.L2 == null) ? "" : o.L2),
                                                            L3 = ((o.L3 == null) ? "" : o.L3),
                                                            AssignedPackage = (pk.PkgeName == null) ? "" : pk.PkgeName,
                                                            BillQtyO = o.ObBillQty,
                                                            QtyO = o.QtyO,                                                        
                                                            ScopeQtyO = o.QtyScope,
                                                            BoqBillQty =b.BoqBillQty,
                                                            BoqQty = b.BoqQty,
                                                            BoqScopeQty =b.BoqQtyScope
                                                            //packageName = (o.Scope == null) ? "" : lstPackages.Where(x => x.id == o.Scope).FirstOrDefault().name
                                                            //boqPackageName = (b.BoqScope == null) ? "" : lstPackages.Find(x => x.IdPkge == b.BoqScope).PkgeName
                                                        });

            if (input.BOQDiv.Length > 0) condQuery = condQuery.Where(w => input.BOQDiv.Contains(w.SectionO));
            if (!string.IsNullOrEmpty(input.BOQItem)) condQuery = condQuery.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
            if (!string.IsNullOrEmpty(input.BOQDesc)) condQuery = condQuery.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
            if (!string.IsNullOrEmpty(input.SheetDesc)) condQuery = condQuery.Where(w => w.ObSheetDesc == input.SheetDesc);
            if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) condQuery = condQuery.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.Scope == input.Package);
            if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));
            if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            if (!string.IsNullOrEmpty(input.RESPackage)) condQuery = condQuery.Where(w => w.BoqPackage == input.RESPackage);
            if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.BoqScope == input.Package);
            if (input.boqLevel2.Length > 0) condQuery = condQuery.Where(w => input.boqLevel2.Contains(w.L2));
            if (!string.IsNullOrEmpty(input.boqLevel3)) condQuery = condQuery.Where(w => w.L3.ToLower().Contains(input.boqLevel3.ToLower()));

            var resutl = condQuery
                .GroupBy(x => new { x.RowNumber, x.SectionO, x.ItemO, x.DescriptionO, x.UnitO })
                .Select(p => p.FirstOrDefault())
                .Select(p => new BoqRessourcesList
                {
                    RowNumber = p.RowNumber,
                    SectionO = p.SectionO,
                    ItemO = p.ItemO,
                    DescriptionO = p.DescriptionO,
                    UnitO = p.UnitO,                
                    UnitRate = p.UnitRate,
                    Scope = p.Scope,
                    AssignedPackage = p.AssignedPackage,
                    BillQtyO = p.BillQtyO,
                    QtyO = p.QtyO,
                    ScopeQtyO = p.ScopeQtyO
                })
                .ToList();

            return resutl.OrderBy(w => w.RowNumber).ToList();
            //return _mapper.Map<List<TblOriginalBoq>, List<OriginalBoqModel>>(results);
        }
        private string GetPackageName(int id)
        {
            var result = _context.PackagesNetworks.Where(x => x.IdPkge == id).FirstOrDefault();
            return result.PkgeName;
        }
        public List<BoqModel> GetBoqList(string ItemO, SearchInput input)
        {
            //var results = (from b in _context.TblBoqs
            //               where b.BoqItem == ItemO
            //               join r in _context.TblResources on b.BoqResSeq equals r.ResSeq
            //               select new BoqModel()
            //               {
            //                   BoqSeq = b.BoqSeq,
            //                   BoqCtg = b.BoqCtg,
            //                   BoqUnitMesure = b.BoqUnitMesure,
            //                   BoqQty = b.BoqQty,
            //                   BoqUprice = b.BoqUprice,
            //                   BoqDiv = b.BoqDiv,
            //                   BoqPackage = b.BoqPackage,
            //                   BoqScope = b.BoqScope,
            //                   ResDescription = r.ResDescription
            //               }).ToList();
            //return results;

            IEnumerable<BoqModel> condQuery = (from b in _context.TblBoqs
                                               where b.BoqItem == ItemO
                                               join r in _context.TblResources on b.BoqResSeq equals r.ResSeq
                                               join p in _context.PackagesNetworks on b.BoqScope equals p.IdPkge into gj
                                               from pk in gj.DefaultIfEmpty()

                                               select new BoqModel()
                                               {
                                                   BoqSeq = b.BoqSeq,
                                                   BoqResSeq = b.BoqResSeq,
                                                   BoqCtg = b.BoqCtg,
                                                   BoqUnitMesure = b.BoqUnitMesure,
                                                   BoqQty = b.BoqQty,
                                                   BoqUprice = b.BoqUprice,
                                                   BoqDiv = b.BoqDiv,
                                                   BoqPackage = b.BoqPackage,
                                                   BoqScope = b.BoqScope,
                                                   ResDescription = r.ResDescription,
                                                   BoqItem = b.BoqItem,
                                                   AssignedPackage = (pk.PkgeName == null) ? "" : pk.PkgeName
                                               });

            if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));
            if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            if (!string.IsNullOrEmpty(input.RESPackage)) condQuery = condQuery.Where(w => w.BoqPackage == input.RESPackage);
            if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.BoqScope == input.Package);

            var results = condQuery.ToList();

            return results;
        }
        public List<BoqModel> GetAllBoqList(SearchInput input)
        {
            IEnumerable<BoqModel> condQuery = (from b in _context.TblBoqs
                                               join r in _context.TblResources on b.BoqResSeq equals r.ResSeq
                                               join p in _context.PackagesNetworks on b.BoqScope equals p.IdPkge into gj
                                               from pk in gj.DefaultIfEmpty()
                                               select new BoqModel()
                                               {
                                                   BoqSeq = b.BoqSeq,
                                                   BoqResSeq = b.BoqResSeq,
                                                   BoqItem = b.BoqItem,
                                                   BoqCtg = b.BoqCtg,
                                                   BoqUnitMesure = b.BoqUnitMesure,
                                                   BoqQty = b.BoqQty,
                                                   BoqUprice = b.BoqUprice,
                                                   BoqDiv = b.BoqDiv,
                                                   BoqPackage = b.BoqPackage,
                                                   BoqScope = b.BoqScope,
                                                   ResDescription = r.ResDescription,
                                                   AssignedPackage = (pk.PkgeName == null) ? "" : pk.PkgeName
                                               });

            if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));
            if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            if (!string.IsNullOrEmpty(input.RESPackage)) condQuery = condQuery.Where(w => w.BoqPackage == input.RESPackage);
            if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.BoqScope == input.Package);

            var results = condQuery.ToList();

            return results;
        }
        public PackageDetailsModel GetPackageById(int IdPkge)
        {
            var query = from b in _context.PackagesNetworks
                        where b.IdPkge == IdPkge
                        select new PackageDetailsModel
                        {
                            PackageName = b.PkgeName,
                            FilePath = b.FilePath
                        };
            return query.FirstOrDefault();
        }
        public bool AssignPackages(AssignPackages input)
        {
            if (input.AssignOriginalBoqList != null)
            {
                //foreach (var item in input.AssignOriginalBoqList)
                //{
                //    var data = _context.TblOriginalBoqs.Where(x => x.RowNumber == item.RowNumber).FirstOrDefault();
                //    data.Scope = item.Scope;

                //    _context.TblOriginalBoqs.Update(data);               
                //}
                //_context.SaveChanges();

                var lstBoqo = (from a in input.AssignOriginalBoqList
                               join b in _context.TblOriginalBoqs on a.RowNumber equals b.RowNumber
                               select b).ToList();

                foreach (var item in input.AssignOriginalBoqList)
                {
                    lstBoqo.Where(d => d.RowNumber == item.RowNumber).First().Scope = item.Scope;
                }
                _context.TblOriginalBoqs.UpdateRange(lstBoqo);
                _context.SaveChanges();
            }

            if (input.AssignBoqList != null)
            {
                //foreach (var item in input.AssignBoqList)
                //{
                //    var data = _context.TblBoqs.Where(x => x.BoqSeq == item.BoqSeq).FirstOrDefault();
                //    data.BoqScope = item.BoqScope;

                //    _context.TblBoqs.Update(data);
                //}
                //_context.SaveChanges();

                var lstBoq = (from a in input.AssignBoqList
                              join b in _context.TblBoqs on a.BoqSeq equals b.BoqSeq
                              select b).ToList();

                foreach (var item in input.AssignBoqList)
                {
                    lstBoq.Where(d => d.BoqSeq == item.BoqSeq).First().BoqScope = item.BoqScope;
                }
                _context.TblBoqs.UpdateRange(lstBoq);
                _context.SaveChanges();
            }
            return true;
        }
        public List<PackageSuppliersPrice> GetPackageSuppliersPrice(int pckgID, SearchInput input)
        {
            //get Exchange Rate Now
            var curList = (from b in _mcontext.TblCurrencies
                           select b).ToList();

            var usedCur = from cur in curList
                          join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                          join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                          where (a.SpPackageId == pckgID && b.PrRevNo == 0)
                          group cur by cur.CurCode into g
                          select new LiveExchange
                          {
                              fromCurrency = g.Key
                          };

            var ExchNowList = (from cur in usedCur
                               select new LiveExchange
                               {
                                   fromCurrency = cur.fromCurrency,
                                   ExchRateNow = GetExchange(cur.fromCurrency)
                               }).ToList();

            List<PackageSuppliersPrice> result = new List<PackageSuppliersPrice>();
            List<RevisionDetails> revisionDetails = new List<RevisionDetails>();
            List<FieldList> fieldLists = new List<FieldList>();

            var query = (from a in _context.TblSupplierPackages
                         join sup in _context.TblSuppliers on a.SpSupplierId equals sup.SupCode
                         join rev in _context.TblSupplierPackageRevisions on a.SpPackSuppId equals rev.PrPackSuppId
                         where (a.SpPackageId == pckgID && rev.PrRevNo == 0)
                         select new PackageSuppliersPrice
                         {
                             SupplierId = a.SpSupplierId,
                             SupplierName = sup.SupName,
                             LastRevisionDate = rev.PrRevDate,
                             ByBoq = (byte)((a.SpByBoq == null) ? 0 : a.SpByBoq)
                         }).ToList();

            if (query.Count > 0)
            {
                byte byboq = query.FirstOrDefault().ByBoq;

                query.Add(new PackageSuppliersPrice() { SupplierId = 0, SupplierName = "Ideal", LastRevisionDate = null, ByBoq = byboq });

                if (query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        PackageSuppliersPrice packageSuppliersPrice = new PackageSuppliersPrice();

                        packageSuppliersPrice.SupplierId = item.SupplierId;
                        packageSuppliersPrice.SupplierName = item.SupplierName;
                        packageSuppliersPrice.ByBoq = item.ByBoq;
                        packageSuppliersPrice.LastRevisionDate = item.LastRevisionDate;
                        byboq = item.ByBoq;
                        IEnumerable<RevisionDetails> revDtlQry;
                        IEnumerable<RevisionDetails> revDtlQryIdeal;

                        if (item.SupplierName == "Ideal")

                        {
                            if (byboq == 1)
                            {
                                revDtlQry = (from cur in curList
                                             join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                             join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                             join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                             join o in _context.TblOriginalBoqs on c.RdBoqItem equals o.ItemO
                                             join sup in _context.TblSuppliers on a.SpSupplierId equals sup.SupCode
                                             where (a.SpPackageId == pckgID && b.PrRevNo == 0)

                                             select new RevisionDetails
                                             {
                                                 ItemO = o.ItemO,
                                                 DescriptionO = o.DescriptionO,
                                                 UnitO = o.UnitO,
                                                 QtyO = c.RdQty,
                                                 price = c.RdPrice,
                                                 perc = c.RdAssignedPerc,
                                                 missedPrice = c.RdMissedPrice,
                                                 priceOrigCur = c.RdPriceOrigCurrency,
                                                 Scope = o.Scope,
                                                 BoqDiv = o.SectionO,
                                                 ObSheetDesc = o.ObSheetDesc,
                                                 RowNumber = o.RowNumber,
                                                 AssignedToSupplier = ((c.RdAssignedQty == null || c.RdAssignedQty == 0)) ? false : true,
                                                 OriginalCurrency = cur.CurCode,
                                                 AssignedQty = c.RdAssignedQty,
                                                 Discount = c.RdDiscount,
                                                 UPriceAfterDiscount = Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2)
                                             });

                                if (input.BOQDiv.Length > 0) revDtlQry = revDtlQry.Where(w => input.BOQDiv.Contains(w.BoqDiv));
                                if (!string.IsNullOrEmpty(input.BOQItem)) revDtlQry = revDtlQry.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
                                if (!string.IsNullOrEmpty(input.BOQDesc)) revDtlQry = revDtlQry.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
                                if (!string.IsNullOrEmpty(input.SheetDesc)) revDtlQry = revDtlQry.Where(w => w.ObSheetDesc == input.SheetDesc);
                                if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) revDtlQry = revDtlQry.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
                                if (input.Package > 0) revDtlQry = revDtlQry.Where(w => w.Scope == input.Package);
                                if (input.RESDiv.Length > 0) revDtlQry = revDtlQry.Where(w => input.RESDiv.Contains(w.ResDiv));
                                if (input.RESType.Length > 0) revDtlQry = revDtlQry.Where(w => input.RESType.Contains(w.ResCtg));
                                if (!string.IsNullOrEmpty(input.RESPackage)) revDtlQry = revDtlQry.Where(w => w.BoqPackage == input.RESPackage);
                                if (!string.IsNullOrEmpty(input.RESDesc)) revDtlQry = revDtlQry.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));


                                revDtlQryIdeal = revDtlQry
                                    .GroupBy(x => new { x.ItemO })
                                    .Select(p => new RevisionDetails
                                    {
                                        ItemO = p.First().ItemO,
                                        DescriptionO = p.First().DescriptionO,
                                        UnitO = p.First().UnitO,
                                        QtyO = p.First().QtyO,
                                        priceOrigCur = p.Min(c => c.priceOrigCur),
                                        AssignedQty = p.First().AssignedQty,
                                        OriginalCurrency = p.First().OriginalCurrency,
                                        UPriceAfterDiscount = p.Min(c => c.UPriceAfterDiscount)
                                    }).ToList();
                            }
                            else
                            {
                                revDtlQry = (from cur in curList
                                             join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                             join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                             join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                             join d in _context.TblBoqs on c.RdResourceSeq equals d.BoqSeq
                                             join e in _context.TblResources on d.BoqResSeq equals e.ResSeq
                                             join o in _context.TblOriginalBoqs on d.BoqItem equals o.ItemO
                                             join sup in _context.TblSuppliers on a.SpSupplierId equals sup.SupCode
                                             where (a.SpPackageId == pckgID && b.PrRevNo == 0 && a.SpSupplierId == item.SupplierId)

                                             select new RevisionDetails
                                             {
                                                 resourceID = c.RdResourceSeq,
                                                 ResDescription = e.ResDescription,
                                                 resourceUnit = d.BoqUnitMesure,
                                                 resourceQty = c.RdQty,
                                                 price = c.RdPrice,
                                                 perc = c.RdAssignedPerc,
                                                 missedPrice = c.RdMissedPrice,
                                                 priceOrigCur = c.RdPriceOrigCurrency,
                                                 ItemO = o.ItemO,
                                                 DescriptionO = o.DescriptionO,
                                                 SectionO = o.SectionO,
                                                 Scope = o.Scope,
                                                 BoqDiv = o.SectionO,
                                                 ObSheetDesc = o.ObSheetDesc,
                                                 RowNumber = o.RowNumber,
                                                 BoqPackage = d.BoqPackage,
                                                 BoqScope = d.BoqScope,
                                                 ResDiv = d.BoqDiv,
                                                 ResCtg = d.BoqCtg,
                                                 AssignedToSupplier = ((c.RdAssignedQty == null || c.RdAssignedQty == 0)) ? false : true,
                                                 OriginalCurrency = cur.CurCode,
                                                 AssignedQty = c.RdAssignedQty,
                                                 Discount = c.RdDiscount,
                                                 UPriceAfterDiscount = Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2)
                                             });

                                if (input.BOQDiv.Length > 0) revDtlQry = revDtlQry.Where(w => input.BOQDiv.Contains(w.BoqDiv));
                                if (!string.IsNullOrEmpty(input.BOQItem)) revDtlQry = revDtlQry.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
                                if (!string.IsNullOrEmpty(input.BOQDesc)) revDtlQry = revDtlQry.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
                                if (!string.IsNullOrEmpty(input.SheetDesc)) revDtlQry = revDtlQry.Where(w => w.ObSheetDesc == input.SheetDesc);
                                if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) revDtlQry = revDtlQry.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
                                if (input.Package > 0) revDtlQry = revDtlQry.Where(w => w.Scope == input.Package);
                                if (input.RESDiv.Length > 0) revDtlQry = revDtlQry.Where(w => input.RESDiv.Contains(w.ResDiv));
                                if (input.RESType.Length > 0) revDtlQry = revDtlQry.Where(w => input.RESType.Contains(w.BoqCtg));
                                if (!string.IsNullOrEmpty(input.RESPackage)) revDtlQry = revDtlQry.Where(w => w.BoqPackage == input.RESPackage);
                                if (!string.IsNullOrEmpty(input.RESDesc)) revDtlQry = revDtlQry.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));

                                revDtlQryIdeal = revDtlQry
                                .GroupBy(x => new { x.ItemO })
                                .Select(p => new RevisionDetails
                                {
                                    ItemO = p.First().ItemO,
                                    DescriptionO = p.First().DescriptionO,
                                    UnitO = p.First().UnitO,
                                    QtyO = p.First().QtyO,
                                    priceOrigCur = p.Min(c => c.priceOrigCur),
                                    AssignedQty = p.First().AssignedQty,
                                    OriginalCurrency = p.First().OriginalCurrency,
                                    UPriceAfterDiscount = p.Min(c => c.UPriceAfterDiscount)
                                }).ToList();

                            }

                            packageSuppliersPrice.revisionDetails = revDtlQryIdeal.ToList();

                            List<FieldList> f = new List<FieldList>();
                            f = null;
                            packageSuppliersPrice.fieldLists = f;
                        }

                        else
                        {
                            if (byboq == 1)
                            {
                                revDtlQry = (from cur in curList
                                             join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                             join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                             join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                             join o in _context.TblOriginalBoqs on c.RdBoqItem equals o.ItemO
                                             join sup in _context.TblSuppliers on a.SpSupplierId equals sup.SupCode
                                             where (a.SpPackageId == pckgID && b.PrRevNo == 0 && a.SpSupplierId == item.SupplierId)

                                             select new RevisionDetails
                                             {
                                                 ItemO = o.ItemO,
                                                 DescriptionO = o.DescriptionO,
                                                 UnitO = o.UnitO,
                                                 QtyO = c.RdQty,
                                                 price = c.RdPrice,
                                                 perc = c.RdAssignedPerc,
                                                 missedPrice = c.RdMissedPrice,
                                                 priceOrigCur = c.RdPriceOrigCurrency,
                                                 Scope = o.Scope,
                                                 BoqDiv = o.SectionO,
                                                 ObSheetDesc = o.ObSheetDesc,
                                                 RowNumber = o.RowNumber,
                                                 AssignedToSupplier = ((c.RdAssignedQty == null || c.RdAssignedQty == 0)) ? false : true,
                                                 OriginalCurrency = cur.CurCode,
                                                 AssignedQty = c.RdAssignedQty,
                                                 Discount = c.RdDiscount,
                                                 UPriceAfterDiscount = Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2),
                                                 
                                             });

                                if (input.BOQDiv.Length > 0) revDtlQry = revDtlQry.Where(w => input.BOQDiv.Contains(w.BoqDiv));
                                if (!string.IsNullOrEmpty(input.BOQItem)) revDtlQry = revDtlQry.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
                                if (!string.IsNullOrEmpty(input.BOQDesc)) revDtlQry = revDtlQry.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
                                if (!string.IsNullOrEmpty(input.SheetDesc)) revDtlQry = revDtlQry.Where(w => w.ObSheetDesc == input.SheetDesc);
                                if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) revDtlQry = revDtlQry.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
                                if (input.Package > 0) revDtlQry = revDtlQry.Where(w => w.Scope == input.Package);
                                if (input.RESDiv.Length > 0) revDtlQry = revDtlQry.Where(w => input.RESDiv.Contains(w.ResDiv));
                                if (input.RESType.Length > 0) revDtlQry = revDtlQry.Where(w => input.RESType.Contains(w.ResCtg));
                                if (!string.IsNullOrEmpty(input.RESPackage)) revDtlQry = revDtlQry.Where(w => w.BoqPackage == input.RESPackage);
                                if (!string.IsNullOrEmpty(input.RESDesc)) revDtlQry = revDtlQry.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));
                            }
                            else
                            {
                                revDtlQry = (from cur in curList
                                             join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                             join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                             join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                             join d in _context.TblBoqs on c.RdResourceSeq equals d.BoqSeq
                                             join e in _context.TblResources on d.BoqResSeq equals e.ResSeq
                                             join o in _context.TblOriginalBoqs on d.BoqItem equals o.ItemO
                                             join sup in _context.TblSuppliers on a.SpSupplierId equals sup.SupCode
                                             where (a.SpPackageId == pckgID && b.PrRevNo == 0 && a.SpSupplierId == item.SupplierId)

                                             select new RevisionDetails
                                             {
                                                 resourceID = c.RdResourceSeq,
                                                 ResDescription = e.ResDescription,
                                                 resourceUnit = d.BoqUnitMesure,
                                                 resourceQty = c.RdQty,
                                                 price = c.RdPrice,
                                                 perc = c.RdAssignedPerc,
                                                 missedPrice = c.RdMissedPrice,
                                                 priceOrigCur = c.RdPriceOrigCurrency,
                                                 ItemO = o.ItemO,
                                                 DescriptionO = o.DescriptionO,
                                                 SectionO = o.SectionO,
                                                 Scope = o.Scope,
                                                 BoqDiv = o.SectionO,
                                                 ObSheetDesc = o.ObSheetDesc,
                                                 RowNumber = o.RowNumber,
                                                 BoqPackage = d.BoqPackage,
                                                 BoqScope = d.BoqScope,
                                                 ResDiv = d.BoqDiv,
                                                 ResCtg = d.BoqCtg,
                                                 AssignedToSupplier = ((c.RdAssignedQty == null || c.RdAssignedQty == 0)) ? false : true,
                                                 OriginalCurrency = cur.CurCode,
                                                 AssignedQty = c.RdAssignedQty,
                                                 Discount = c.RdDiscount,
                                                 UPriceAfterDiscount = Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2)
                                             });

                                if (input.BOQDiv.Length > 0) revDtlQry = revDtlQry.Where(w => input.BOQDiv.Contains(w.BoqDiv));
                                if (!string.IsNullOrEmpty(input.BOQItem)) revDtlQry = revDtlQry.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
                                if (!string.IsNullOrEmpty(input.BOQDesc)) revDtlQry = revDtlQry.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
                                if (!string.IsNullOrEmpty(input.SheetDesc)) revDtlQry = revDtlQry.Where(w => w.ObSheetDesc == input.SheetDesc);
                                if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) revDtlQry = revDtlQry.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
                                if (input.Package > 0) revDtlQry = revDtlQry.Where(w => w.Scope == input.Package);
                                if (input.RESDiv.Length > 0) revDtlQry = revDtlQry.Where(w => input.RESDiv.Contains(w.ResDiv));
                                if (input.RESType.Length > 0) revDtlQry = revDtlQry.Where(w => input.RESType.Contains(w.BoqCtg));
                                if (!string.IsNullOrEmpty(input.RESPackage)) revDtlQry = revDtlQry.Where(w => w.BoqPackage == input.RESPackage);
                                if (!string.IsNullOrEmpty(input.RESDesc)) revDtlQry = revDtlQry.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));
                            }

                            fieldLists = (from cur in curList
                                          join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                          join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                          join c in _context.TblRevisionFields on b.PrRevId equals c.RevisionId
                                          where (a.SpPackageId == pckgID && b.PrRevNo == 0 && a.SpSupplierId == item.SupplierId)

                                          select new FieldList
                                          {
                                              Label = c.Label,
                                              Value = c.Type == 1 ? (double)(c.Value * ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow) : c.Value,
                                              Type = c.Type,
                                              OriginalCurrency = cur.CurCode
                                          }).ToList();

                            packageSuppliersPrice.revisionDetails = revDtlQry.ToList();
                            packageSuppliersPrice.fieldLists = fieldLists;

                        }

                        if (packageSuppliersPrice.revisionDetails.Count > 0)
                        {
                            foreach (var itemRevision in packageSuppliersPrice.revisionDetails)
                            {
                                if (packageSuppliersPrice.SupplierName == "Ideal")
                                    packageSuppliersPrice.totalprice += Convert.ToDecimal(itemRevision.QtyO) * Convert.ToDecimal(itemRevision.UPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == itemRevision.OriginalCurrency).ExchRateNow);
                                else
                                    packageSuppliersPrice.totalprice += Convert.ToDecimal(itemRevision.QtyO) * Convert.ToDecimal(itemRevision.UPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == itemRevision.OriginalCurrency).ExchRateNow);
                                //packageSuppliersPrice.totalprice += Convert.ToDecimal(itemRevision.AssignedQty) * Convert.ToDecimal(itemRevision.priceOrigCur) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == itemRevision.OriginalCurrency).ExchRateNow);
                            }
                        }

                        if (packageSuppliersPrice.fieldLists != null)
                        {
                            if (packageSuppliersPrice.fieldLists.Count > 0)
                            {
                                foreach (var itemFields in packageSuppliersPrice.fieldLists)
                                {
                                    if (itemFields.Type == 1)
                                        packageSuppliersPrice.totalAdditionalPrice += (decimal)itemFields.Value;
                                    else
                                        packageSuppliersPrice.totalAdditionalPrice += packageSuppliersPrice.totalprice * ((decimal)itemFields.Value / 100m);
                                }
                            }
                            else
                                packageSuppliersPrice.totalAdditionalPrice = 0;
                        }
                        else
                            packageSuppliersPrice.totalAdditionalPrice = 0;

                        packageSuppliersPrice.totalNetPrice = packageSuppliersPrice.totalprice + packageSuppliersPrice.totalAdditionalPrice;
                        result.Add(packageSuppliersPrice);
                    }
                }
            }
            return result;
            //return result.OrderBy(x => x.SupplierName).ToList();
        }
        private double GetExchange(string foreignCurrency)
        {
            var result = from a in _context.TblParameters
                         join b in _context.TblCurrencies
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
        public string ExportBoqExcel(AssignPackages input)
        {
            //if (input.AssignOriginalBoqList != null)
            //{

            //    var lstBoqo = (from a in input.AssignOriginalBoqList
            //                   join b in _context.TblOriginalBoqs on a.RowNumber equals b.RowNumber
            //                   select b).ToList();

            //    foreach (var item in input.AssignOriginalBoqList)
            //    {
            //        lstBoqo.Where(d => d.RowNumber == item.RowNumber).First().Scope = item.Scope;
            //    }
            //    _context.TblOriginalBoqs.UpdateRange(lstBoqo);
            //    _context.SaveChanges();
            //}

            string excelName = "";

            if (input.AssignBoqList != null)
            {

                var lstBoq = (from a in input.AssignBoqList
                              join b in _context.TblBoqs on a.BoqSeq equals b.BoqSeq
                              join r in _context.TblResources on b.BoqResSeq equals r.ResSeq
                              select new BoqModel()
                              {
                                  BoqSeq = b.BoqSeq,
                                  BoqResSeq = b.BoqResSeq,
                                  BoqCtg = b.BoqCtg,
                                  BoqUnitMesure = b.BoqUnitMesure,
                                  BoqQty = b.BoqQty,
                                  BoqUprice = b.BoqUprice,
                                  BoqDiv = b.BoqDiv,
                                  BoqPackage = b.BoqPackage,
                                  BoqScope = b.BoqScope,
                                  ResDescription = r.ResDescription,
                                  BoqItem = b.BoqItem,
                                  BoqTotalPrice = b.BoqUprice * b.BoqQty
                              }).ToList();

                //foreach (var item in input.AssignBoqList)
                //{
                //    lstBoq.Where(d => d.BoqSeq == item.BoqSeq).First().BoqScope = item.BoqScope;
                //}

                var stream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var xlPackage = new ExcelPackage(stream))
                {
                    var worksheet = xlPackage.Workbook.Worksheets.Add("BOQ");
                    worksheet.Columns.AutoFit();
                    worksheet.Protection.IsProtected = false;

                    int i, j;

                    i = 1;
                    worksheet.Cells[i, 1].Value = "Item";
                    worksheet.Column(1).Width = 20;
                    worksheet.Cells[i, 2].Value = "Description";
                    worksheet.Column(2).Width = 50;
                    worksheet.Columns[2].Style.WrapText = true;
                    worksheet.Columns[2].Style.WrapText = true;
                    worksheet.Column(2).AutoFit();
                    worksheet.Cells[i, 3].Value = "Unit";
                    worksheet.Cells[i, 4].Value = "Type";
                    worksheet.Cells[i, 5].Value = "Qty";
                    worksheet.Cells[i, 6].Value = "Unit Price";
                    worksheet.Cells[i, 7].Value = "Total Price";

                    worksheet.Row(i).Style.Font.Bold = true;

                    i = 4;
                    foreach (var x in lstBoq)
                    {
                        worksheet.Cells[i, 1].Value = (x.BoqItem == null) ? "" : x.BoqItem;
                        worksheet.Cells[i, 2].Value = (x.ResDescription == null) ? "" : x.ResDescription;
                        worksheet.Cells[i, 3].Value = (x.BoqUnitMesure == null) ? "" : x.BoqUnitMesure;
                        worksheet.Cells[i, 4].Value = (x.BoqCtg == null) ? "" : x.BoqCtg;
                        worksheet.Cells[i, 5].Value = (x.BoqQty == null) ? "" : x.BoqQty;
                        worksheet.Cells[i, 6].Value = (x.BoqUprice == null) ? "" : x.BoqUprice;
                        worksheet.Cells[i, 7].Value = (x.BoqTotalPrice == null) ? "" : x.BoqTotalPrice;
                        i++;
                    }

                    xlPackage.Save();
                    stream.Position = 0;
                    excelName = $"BOQ_Ressources.xlsx";

                    if (File.Exists(excelName))
                        File.Delete(excelName);

                    xlPackage.SaveAs(excelName);
                }
            }
            return excelName;
        }


        public bool updateOriginalBoqQty(OriginalBoqModel boq)
        {
            var result = _context.TblOriginalBoqs.Where(x => x.ItemO == boq.ItemO).FirstOrDefault();
            result.QtyScope = boq.ScopeQtyO;
            
            if (result != null)
            {
                _context.TblOriginalBoqs.Update(result);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

    }
}
