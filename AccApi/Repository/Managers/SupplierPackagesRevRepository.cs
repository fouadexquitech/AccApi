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
            foreach (var SupplierPackageRev in results)
            {
                var fields = _context.TblRevisionFields.Where(x => x.RevisionId == SupplierPackageRev.PrRevId).ToList();
                if (fields.Count > 0)
                {
                    foreach (var itemFields in fields)
                    {
                        if (itemFields.Type == 1)
                            SupplierPackageRev.PrTotPrice += (decimal)itemFields.Value;
                        else
                            SupplierPackageRev.PrTotPrice += SupplierPackageRev.PrTotPrice * ((decimal)itemFields.Value / 100m);
                    }
                }
            }
            return results;
        }

        public SupplierPackagesRevList GetSupplierPackagesRevision(int revisionId)
        {
            var res = (from b in _context.TblSupplierPackageRevisions
                           where b.PrRevId == revisionId
                           
                           select new SupplierPackagesRevList
                           {
                               PrRevId = b.PrRevId,
                               PrRevNo = b.PrRevNo,
                               PrRevDate = b.PrRevDate,
                               PrTotPrice = b.PrTotPrice,
                               PrCurrency = b.PrCurrency,
                               PrExchRate = b.PrExchRate
                           }).FirstOrDefault();

            var fields = _context.TblRevisionFields.Where(x => x.RevisionId == revisionId).ToList();
            if (fields.Count > 0)
            {
                foreach (var itemFields in fields)
                {
                    if (itemFields.Type == 1)
                        res.PrTotPrice += (decimal)itemFields.Value;
                    else
                        res.PrTotPrice += res.PrTotPrice * ((decimal)itemFields.Value / 100m);
                }
            }

            return res;
        }



        public decimal? AddField(int revId, string lbl, double val, int type)
        {
            var NewField = new TblRevisionField { RevisionId = revId, Label = lbl, Value = val , Type=type };
            _context.Add(NewField);
            _context.SaveChanges();

            var SupplierPackageRev = _context.TblSupplierPackageRevisions.Where(x => x.PrRevId == revId).FirstOrDefault();

            var revisionFields = (from b in _context.TblRevisionFields
                                  where b.RevisionId == revId
                                  select b).ToList();

            if (SupplierPackageRev != null)
            {
                if (revisionFields.Count > 0)
                {
                    foreach (var itemFields in revisionFields)
                    {
                        if (itemFields.Type == 1)
                            SupplierPackageRev.PrTotPrice += (decimal)itemFields.Value;
                        else
                            SupplierPackageRev.PrTotPrice += SupplierPackageRev.PrTotPrice * ((decimal)itemFields.Value / 100m);
                    }
                }
            }
            return SupplierPackageRev.PrTotPrice;
        }

        public bool DeleteField(int fieldId)
        {
            var field = _context.TblRevisionFields.Where(x => x.Id == fieldId).FirstOrDefault();
            if (field != null)
            {          
                _context.Remove(field);
                _context.SaveChanges();
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

        public List<RevisionFieldsList> GetFields(int revisionid)
        {
            var result = (from b in _context.TblRevisionFields
                          where b.RevisionId == revisionid
                          select new RevisionFieldsList
                          {
                              Id = b.Id,
                              RevisionId=b.RevisionId,
                              Label=b.Label,
                              Type= b.Type,
                              Value = b.Value
                          }); ;

            return result.ToList();
        }
    }
}
