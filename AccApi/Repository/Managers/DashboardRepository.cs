using AccApi.Repository.Interfaces;
using AccApi.Repository.View_Models;
using System.Collections.Generic;
using System.Linq;

namespace AccApi.Repository.Managers
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly MasterDbContext _mdbContext;

        public DashboardRepository(MasterDbContext mdbContext)
        {
            _mdbContext = mdbContext;
        }

        public double GetProjectTotalBudget(string costConn)
        {
            var db = new AccDbContext(costConn);
            return (from b in db.TblBoqVds
                    join o in db.TblOriginalBoqVds on b.BoqItem equals o.ItemO
                    select (double?)((b.BoqQty ?? 0) * (b.BoqUprice ?? 0)))
                   .Sum() ?? 0;
        }

        public List<PackageBudgetItem> GetPackagesBudget(string costConn)
        {
            var db = new AccDbContext(costConn);

            var boqData = (from b in db.TblBoqVds
                           join o in db.TblOriginalBoqVds on b.BoqItem equals o.ItemO
                           where b.BoqScope != null && b.BoqScope != 0
                           select new { b.BoqScope, Budget = (b.BoqQty ?? 0) * (b.BoqUprice ?? 0) })
                          .ToList();

            var packages = _mdbContext.TblPackages
                           .Select(p => new { p.PkgeId, p.PkgeName })
                           .ToList();

            return (from b in boqData
                    join p in packages on b.BoqScope equals p.PkgeId
                    group b by p.PkgeName into g
                    select new PackageBudgetItem
                    {
                        PkgeName = g.Key,
                        TotalBudget = g.Sum(x => x.Budget)
                    })
                   .OrderByDescending(x => x.TotalBudget)
                   .ToList();
        }

        public List<DivisionMissingItem> GetMissingByDivision(string costConn)
        {
            var db = new AccDbContext(costConn);
            return (from b in db.TblBoqVds
                    join o in db.TblOriginalBoqVds on b.BoqItem equals o.ItemO
                    where b.BoqScope == null || b.BoqScope == 0
                    group new { b } by b.BoqDiv into g
                    select new DivisionMissingItem
                    {
                        BoqDiv = g.Key,
                        TotalBudget = g.Sum(x => (x.b.BoqQty ?? 0) * (x.b.BoqUprice ?? 0)),
                        MissingCount = g.Count()
                    })
                   .OrderByDescending(x => x.TotalBudget)
                   .ToList();
        }

        public List<DivisionResourceItem> GetMissingResourcesForDivision(string costConn, string division)
        {
            var db = new AccDbContext(costConn);
            return (from b in db.TblBoqVds
                    join o in db.TblOriginalBoqVds on b.BoqItem equals o.ItemO
                    join r in db.TblResources on b.BoqResSeq equals r.ResSeq
                    where (b.BoqScope == null || b.BoqScope == 0) && b.BoqDiv == division
                    group new { b, r } by new
                    {
                        r.ResDescription,
                        b.BoqCtg,
                        b.BoqUnitMesure,
                        b.BoqUprice
                    } into g
                    select new DivisionResourceItem
                    {
                        ResDescription = g.Key.ResDescription,
                        BoqCtg = g.Key.BoqCtg,
                        BoqUnitMesure = g.Key.BoqUnitMesure,
                        BoqUPrice = g.Key.BoqUprice ?? 0,
                        TotalQty = g.Sum(x => x.b.BoqQty ?? 0),
                        TotalBudget = g.Sum(x => (x.b.BoqQty ?? 0) * (x.b.BoqUprice ?? 0))
                    })
                   .OrderByDescending(x => x.TotalBudget)
                   .ToList();
        }

        public List<DivisionBudgetItem> GetBudgetByDivision(string costConn)
        {
            var db = new AccDbContext(costConn);
            return (from b in db.TblBoqVds
                    join o in db.TblOriginalBoqVds on b.BoqItem equals o.ItemO
                    group new { b } by b.BoqDiv into g
                    select new DivisionBudgetItem
                    {
                        BoqDiv = g.Key,
                        TotalBudget = g.Sum(x => (x.b.BoqQty ?? 0) * (x.b.BoqUprice ?? 0))
                    })
                   .OrderByDescending(x => x.TotalBudget)
                   .ToList();
        }

        public List<QuotationSupplierItem> GetQuotationBudget(string costConn)
        {
            var db = new AccDbContext(costConn);

            var costData = (from sp in db.TblSupplierPackages
                            join rev in db.TblSupplierPackageRevisions on sp.SpPackSuppId equals rev.PrPackSuppId
                            join rd in db.TblRevisionDetails on rev.PrRevId equals rd.RdRevisionId
                            where rev.PrRevNo == 0
                            group new { sp, rd } by sp.SpSupplierId into g
                            select new { SupplierId = g.Key, TotalBudget = g.Sum(x => (x.rd.RdQty ?? 0) * (x.rd.RdPrice ?? 0)) })
                           .ToList();

            var suppliers = _mdbContext.TblSuppliers
                           .Select(s => new { s.SupCode, s.SupName })
                           .ToList();

            return (from c in costData
                    join s in suppliers on c.SupplierId equals s.SupCode
                    select new QuotationSupplierItem
                    {
                        SupName = s.SupName,
                        TotalBudget = c.TotalBudget
                    })
                   .OrderByDescending(x => x.TotalBudget)
                   .ToList();
        }
    }
}
