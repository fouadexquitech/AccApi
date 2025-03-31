using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.View_Models;
using Microsoft.EntityFrameworkCore;
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
        private readonly GlobalLists _globalLists;

        public SupplierPackagesRevRepository(AccDbContext context, MasterDbContext masterDbContext, GlobalLists globalLists)
        {
            _masterDbContext = masterDbContext;
            _globalLists = globalLists;
            _context = new AccDbContext(_globalLists.GetAccDbconnectionString());
        }

        public List<SupplierPackagesRevList> GetSupplierPackagesRevList(int PackageSupplierId, string CostConn)
        {
            AccDbContext _context = new AccDbContext(CostConn);

            var curList = (from b in _masterDbContext.TblCurrencies
                           select b).ToList();

            var results = (from cur in curList
                           join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                           where b.PrPackSuppId == PackageSupplierId
                           orderby b.PrRevNo
                           select new SupplierPackagesRevList
                           {
                               PrRevId = b.PrRevId,
                               PrRevNo = b.PrRevNo,
                               PrRevDate = b.PrRevDate,
                               PrTotPrice = b.PrTotPrice,
                               PrCurrency = b.PrCurrency,
                               PrExchRate = b.PrExchRate,
                               Currency = cur.CurCode,
                               PrRevExpDate = b.RevExpiryDate
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
        public SupplierPackagesRevList GetSupplierPackagesRevision(int revisionId, string CostConn)
        {
            AccDbContext _dbcontext = new AccDbContext(CostConn);

            var res = (from b in _dbcontext.TblSupplierPackageRevisions
                           where b.PrRevId == revisionId
                           
                           select new SupplierPackagesRevList
                           {
                               PrRevId = b.PrRevId,
                               PrRevNo = b.PrRevNo,
                               PrRevDate = b.PrRevDate,
                               PrTotPrice = b.PrTotPrice,
                               PrCurrency = b.PrCurrency,
                               PrExchRate = b.PrExchRate,
                               PrRevExpDate=b.RevExpiryDate
                           }).FirstOrDefault();

            var fields = _dbcontext.TblRevisionFields.Where(x => x.RevisionId == revisionId).ToList();
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

        public decimal? AddField(int revId, string lbl, double val, int type, string CostConn)
        {
            AccDbContext _context = new AccDbContext(CostConn);

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

        public bool DeleteField(int fieldId, string CostConn)
        {
            AccDbContext _context = new AccDbContext(CostConn);

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

        public List<RevisionFieldsList> GetFields(int revisionid, string CostConn)
        {
            AccDbContext _dbcontext = new AccDbContext(CostConn);

            var result = (from b in _dbcontext.TblRevisionFields
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
