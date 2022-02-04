using AccApi.Repository.Interfaces;
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
        private readonly MasterDbContext _masterDbContext;

        public SupplierPackagesRevRepository(AccDbContext context, MasterDbContext masterDbContext)
        {
            _context = context;
            _masterDbContext = masterDbContext;
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
                              PrTotPrice = b.PrTotPrice,
                              PrCurrency=b.PrCurrency,
                              PrExchRate=b.PrExchRate
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

        public decimal? AddField(int revId, string lbl, int val, int type)
        {
            var NewField = new TblRevisionField { RevisionId = revId, Label = lbl, Value = val , Type=type };
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

        public bool DeleteField(int fieldId)
        {
            var field = _context.TblRevisionFields.Where(x => x.Id == fieldId).FirstOrDefault();
            if (field != null)
            {          
                _context.TblRevisionFields.Remove(field);
                return true;
            }
            else
                return false;
        }

        public List<CurrencyList> GetCurrencies()
        {
            var result = from b in _masterDbContext.TblCurrencies
                         select new CurrencyList
                         {
                             curId = b.CurId,
                             CurCode = b.CurCode
                         };

            return result.ToList();
        }
    }
}
