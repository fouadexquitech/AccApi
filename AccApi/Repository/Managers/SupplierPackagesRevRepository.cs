﻿using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.Managers
{
    public class SupplierPackagesRevRepository: ISupplierPackagesRevRepository
    {
        private readonly AccDbContext _context;

        public SupplierPackagesRevRepository(AccDbContext context)
        {
            _context = context;
        }

        public List<SupplierPackagesRevList> GetSupplierPackagesRevList(int PackageSupplierId)
        {
            var results = (from b in _context.TblSupplierPackageRevisions
                          where b.PrPackSuppId==PackageSupplierId
                          orderby b.PrRevNo
                          select new SupplierPackagesRevList
                          {
                              PrRevId= b.PrRevId,
                              PrRevNo = b.PrRevNo,
                              PrRevDate = b.PrRevDate,
                              PrTotPrice = b.PrTotPrice
                          }).ToList();


            // Check If Fields Exists
            foreach (var item in results)
            {
                var fields = _context.TblRevisionFields.Where(x => x.RevisionId == item.PrRevId).ToList();
                if (fields.Count > 0)
                {
                    foreach (var itemFields in fields)
                    {
                        item.PrTotPrice = item.PrTotPrice - item.PrTotPrice * (itemFields.Value / 100m);
                    }
                }
            }

            return results;
        }

        public decimal? AddField(int revId, string lbl, int val)
        {
            var NewField = new TblRevisionField { RevisionId = revId, Label = lbl, Value = val };
            _context.Add(NewField);
            _context.SaveChanges();

            var revision = _context.TblSupplierPackageRevisions.Where(x => x.PrRevId == revId).FirstOrDefault();

            var revisionFields = (from b in _context.TblRevisionFields
                                  where b.RevisionId == revId
                                  select b).ToList();

            if (revision != null)
            {
                if (revisionFields.Count > 0)
                {
                    foreach (var item in revisionFields)
                    {
                        revision.PrTotPrice = revision.PrTotPrice - revision.PrTotPrice * (item.Value / 100m);
                    }
                }
            }

            return revision.PrTotPrice;
        }
    }
}
