using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using AutoMapper;
using System;
using System.Collections.Generic;
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

            IEnumerable<BoqRessourcesList> condQuery = (from o in _context.TblOriginalBoqs
                                                        join b in _context.TblBoqs on o.ItemO equals b.BoqItem
                                                        join r in _context.TblResources on b.BoqResSeq equals r.ResSeq
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
                                                            ResDescription = r.ResDescription
                                                        });

            if (input.BOQDiv.Length > 0) condQuery = condQuery.Where(w => input.BOQDiv.Contains(w.SectionO));
            if (!string.IsNullOrEmpty(input.BOQItem)) condQuery = condQuery.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
            if (!string.IsNullOrEmpty(input.BOQDesc)) condQuery = condQuery.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
            if (!string.IsNullOrEmpty(input.SheetDesc)) condQuery = condQuery.Where(w => w.ObSheetDesc==input.SheetDesc);
            if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) condQuery = condQuery.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.Scope == input.Package);
            if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));           
            if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            if (!string.IsNullOrEmpty(input.RESPackage)) condQuery = condQuery.Where(w => w.BoqPackage == input.RESPackage);
            if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.BoqScope == input.Package);


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
                    QtyO = p.QtyO,
                    UnitRate = p.UnitRate,
                    Scope = p.Scope
                })
                .ToList();

            return resutl.OrderBy(w => w.RowNumber).ToList();
            //return _mapper.Map<List<TblOriginalBoq>, List<OriginalBoqModel>>(results);
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
                                                   BoqItem = b.BoqItem
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
                                               select new BoqModel()
                                               {
                                                   BoqSeq = b.BoqSeq,
                                                   BoqCtg = b.BoqCtg,
                                                   BoqUnitMesure = b.BoqUnitMesure,
                                                   BoqQty = b.BoqQty,
                                                   BoqUprice = b.BoqUprice,
                                                   BoqDiv = b.BoqDiv,
                                                   BoqPackage = b.BoqPackage,
                                                   BoqScope = b.BoqScope,
                                                   ResDescription = r.ResDescription
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
                foreach (var item in input.AssignOriginalBoqList)
                {
                    var data = _context.TblOriginalBoqs.Where(x => x.RowNumber == item.RowNumber).FirstOrDefault();
                    data.Scope = item.Scope;

                    _context.TblOriginalBoqs.Update(data);
                    _context.SaveChanges();
                }
            }

            if (input.AssignBoqList != null)
            {
                foreach (var item in input.AssignBoqList)
                {
                    var data = _context.TblBoqs.Where(x => x.BoqSeq == item.BoqSeq).FirstOrDefault();
                    data.BoqScope = item.BoqScope;

                    _context.TblBoqs.Update(data);
                    _context.SaveChanges();
                }
            }
            return true;
        }

        public List<PackageSuppliersPrice> GetPackageSuppliersPrice(int pckgID, SearchInput input)
        {
            //get Exchange Rate Now
            var curList = (from b in _mcontext.TblCurrencies
                           select b).ToList();
           
            var ExchNowList = (from cur in curList
                               join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                               join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                               join sup in _context.TblSuppliers on a.SpSupplierId equals sup.SupCode
                               where (a.SpPackageId == pckgID && b.PrRevNo == 0)
                               select new LiveExchange
                               {
                                   fromCurrency = cur.CurCode,
                                   ExchRateNow = GetExchange(cur.CurCode)
                               }).ToList();


            List<PackageSuppliersPrice> result = new List<PackageSuppliersPrice>();
            List<RevisionDetails> revisionDetails = new List<RevisionDetails>();
            List<FieldList> fieldLists = new List<FieldList>();

            byte byboq=0;

            var query = (from a in _context.TblSupplierPackages
                         join sup in _context.TblSuppliers on a.SpSupplierId equals sup.SupCode
                         join rev in _context.TblSupplierPackageRevisions on a.SpPackSuppId equals rev.PrPackSuppId
                         where (a.SpPackageId == pckgID && rev.PrRevNo == 0)
                         select new
                         {
                             SupplierId = a.SpSupplierId,
                             SupplierName = sup.SupName,
                             LastRevisionDate = rev.PrRevDate,
                             byboq= (byte)((a.SpByBoq == null) ? 0 : a.SpByBoq)
                         }).ToList();

            if (query.Count > 0)
            {               
                foreach (var item in query)
                {
                    PackageSuppliersPrice packageSuppliersPrice = new PackageSuppliersPrice();

                    packageSuppliersPrice.SupplierId = item.SupplierId;
                    packageSuppliersPrice.SupplierName = item.SupplierName;
                    packageSuppliersPrice.ByBoq = item.byboq;
                    packageSuppliersPrice.LastRevisionDate = item.LastRevisionDate;
                    byboq = item.byboq;
                    IEnumerable<RevisionDetails> revDtlQry;

                    if (byboq==1)
                    {
                       revDtlQry = (from cur in curList
                                     join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                     join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                     join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                     join o in _context.TblOriginalBoqs on c.RdBoqItem equals o.ItemO
                                    // join d in _context.TblBoqs on o.ItemO equals d.BoqItem
                                    //join e in _context.TblResources on d.BoqResSeq equals e.ResSeq
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
                                         ObSheetDesc=o.ObSheetDesc,
                                         RowNumber = o.RowNumber,
                                         //BoqPackage = d.BoqPackage,
                                         //BoqScope = d.BoqScope,
                                         //ResDescription = e.ResDescription,
                                         //ResDiv=d.BoqDiv,
                                         //ResCtg=d.BoqCtg
                                         AssignedToSupplier =((c.RdAssignedQty == null || c.RdAssignedQty==0)) ? false : true,
                                         OriginalCurrency = cur.CurCode
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
                                         missedPrice=c.RdMissedPrice,
                                         priceOrigCur=c.RdPriceOrigCurrency,
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
                                         OriginalCurrency = cur.CurCode
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
                                      Value =  c.Type==1 ? (double)(c.Value * ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow)  : c.Value,
                                      Type = c.Type,
                                      OriginalCurrency=cur.CurCode
                                  }).ToList();

                    packageSuppliersPrice.revisionDetails = revDtlQry.ToList();
                    packageSuppliersPrice.fieldLists = fieldLists;


                    if (packageSuppliersPrice.revisionDetails.Count > 0)
                    {
                        foreach (var itemRevision in packageSuppliersPrice.revisionDetails)
                        {
                            if (byboq == 1)
                                packageSuppliersPrice.totalprice += Convert.ToDecimal(itemRevision.QtyO) * Convert.ToDecimal(itemRevision.priceOrigCur) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == itemRevision.OriginalCurrency).ExchRateNow);
                            else
                                packageSuppliersPrice.totalprice += Convert.ToDecimal(itemRevision.resourceQty) * Convert.ToDecimal(itemRevision.priceOrigCur) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == itemRevision.OriginalCurrency).ExchRateNow);
                        }
                    }

                    if (packageSuppliersPrice.fieldLists.Count > 0)
                    {
                        foreach (var itemFields in packageSuppliersPrice.fieldLists)
                        {
                            if (itemFields.Type == 1)
                                packageSuppliersPrice.totalAdditionalPrice += (decimal)itemFields.Value ;
                            else
                                packageSuppliersPrice.totalAdditionalPrice += packageSuppliersPrice.totalprice * ((decimal)itemFields.Value / 100m);
                        }
                    }
                    else 
                        packageSuppliersPrice.totalAdditionalPrice = 0;
                   
                    packageSuppliersPrice.totalNetPrice = packageSuppliersPrice.totalprice + packageSuppliersPrice.totalAdditionalPrice;
                    result.Add(packageSuppliersPrice);
                }
            }
            return result.OrderBy(x=> x.SupplierName).ToList();
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

    }
}
