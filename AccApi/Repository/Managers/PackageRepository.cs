﻿using AccApi.Repository.Interfaces;
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
        private readonly IMapper _mapper;

        public PackageRepository(AccDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<OriginalBoqModel> GetOriginalBoqList(SearchInput input)
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


            IEnumerable<TblOriginalBoq> condQuery = from b in _context.TblOriginalBoqs select b;
            if (!string.IsNullOrEmpty(input.BOQDiv)) condQuery = condQuery.Where(w => w.SectionO == input.BOQDiv);
            if (!string.IsNullOrEmpty(input.BOQItem)) condQuery = condQuery.Where(w => w.ItemO == input.BOQItem);
            if (!string.IsNullOrEmpty(input.BOQDesc)) condQuery = condQuery.Where(w => w.DescriptionO == input.BOQDesc);
            if (!string.IsNullOrEmpty(input.SheetDesc)) condQuery = condQuery.Where(w => w.ObSheetDesc == input.SheetDesc);
            if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) condQuery = condQuery.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.Scope == input.Package);

            var results = condQuery.OrderBy(w => w.RowNumber).ToList();

            return _mapper.Map<List<TblOriginalBoq>, List<OriginalBoqModel>>(results);

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
                                                   BoqCtg = b.BoqCtg,
                                                   BoqUnitMesure = b.BoqUnitMesure,
                                                   BoqQty = b.BoqQty,
                                                   BoqUprice = b.BoqUprice,
                                                   BoqDiv = b.BoqDiv,
                                                   BoqPackage = b.BoqPackage,
                                                   BoqScope = b.BoqScope,
                                                   ResDescription = r.ResDescription
                                               });

            if (!string.IsNullOrEmpty(input.RESDiv)) condQuery = condQuery.Where(w => w.BoqDiv == input.RESDiv);
            if (!string.IsNullOrEmpty(input.RESType)) condQuery = condQuery.Where(w => w.BoqCtg == input.RESType);
            if (!string.IsNullOrEmpty(input.RESPackage)) condQuery = condQuery.Where(w => w.BoqPackage == input.RESPackage);
            if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription == input.RESDesc);
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
                            PackageName = b.PkgeName
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

        public List<PackageSuppliersPrice> GetPackageSuppliersPrice(int pckgID)
        {
            List<PackageSuppliersPrice> result = new List<PackageSuppliersPrice>();

            List<RevisionDetails> revisionDetails = new List<RevisionDetails>();

            List<FieldList> fieldLists = new List<FieldList>();

            var query = (from a in _context.TblSupplierPackages
                         join sup in _context.TblSuppliers on a.SpSupplierId equals sup.SupCode
                         where (a.SpPackageId == pckgID)
                         select new
                         {
                             SupplierId = a.SpSupplierId,
                             SupplierName = sup.SupName,
                         }).ToList();

            if (query.Count > 0)
            {
                foreach (var item in query)
                {
                    PackageSuppliersPrice packageSuppliersPrice = new PackageSuppliersPrice();

                    packageSuppliersPrice.SupplierId = item.SupplierId;
                    packageSuppliersPrice.SupplierName = item.SupplierName;

                    revisionDetails = (from a in _context.TblSupplierPackages
                                       join b in _context.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                       join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                       join d in _context.TblBoqs on c.RdResourceSeq equals d.BoqSeq
                                       join e in _context.TblResources on d.BoqResSeq equals e.ResSeq
                                       join sup in _context.TblSuppliers on a.SpSupplierId equals sup.SupCode
                                       where (a.SpPackageId == pckgID && b.PrRevNo == 0 && a.SpSupplierId == item.SupplierId)

                                       select new RevisionDetails
                                       {
                                           resourceID = c.RdResourceSeq,
                                           resourceName = e.ResDescription,
                                           resourceUnit = d.BoqUnitMesure,
                                           resourceQty = c.RdQty,
                                           price = c.RdPrice
                                       }).ToList();


                    fieldLists = (from a in _context.TblSupplierPackages
                                  join b in _context.TblSupplierPackageRevisions on a.SpPackSuppId equals b.PrPackSuppId
                                  join c in _context.TblRevisionFields on b.PrRevId equals c.RevisionId
                                  where (a.SpPackageId == pckgID && b.PrRevNo == 0 && a.SpSupplierId == item.SupplierId)

                                  select new FieldList
                                  {
                                      Label = c.Label,
                                      Value = c.Value
                                  }).ToList();

                    packageSuppliersPrice.revisionDetails = revisionDetails;

                    packageSuppliersPrice.fieldLists = fieldLists;

                    if (revisionDetails.Count > 0)
                    {
                        foreach (var itemRevision in revisionDetails)
                        {
                            packageSuppliersPrice.totalprice += Convert.ToDecimal(itemRevision.resourceQty) *  Convert.ToDecimal(itemRevision.price);
                        }
                    }

                    if (fieldLists.Count > 0)
                    {
                        foreach (var itemFields in fieldLists)
                        {
                            packageSuppliersPrice.totalprice = packageSuppliersPrice.totalprice - packageSuppliersPrice.totalprice * (itemFields.Value / 100m);
                        }
                    }
                    result.Add(packageSuppliersPrice);
                }
            }

            return result;
        }
    }
}
