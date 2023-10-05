using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.View_Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AccApi.Repository.Managers
{
    public class SearchRepository: ISearchRepository
    {
        private readonly AccDbContext _context;
        private MasterDbContext _mdbContext;
        private readonly GlobalLists _globalLists;

        public SearchRepository(AccDbContext context, MasterDbContext mdbContext, GlobalLists globalLists)
        {
            _globalLists = globalLists;
            _mdbContext = mdbContext;
            _context = new AccDbContext(_globalLists.GetAccDbconnectionString());
        }

        public List<BOQDivList> GetBOQDivList()
        {
            var results = (from b in _context.TblOriginalBoqs
                          group b.SectionO by b.SectionO into g
                          orderby g.Key
                          select new BOQDivList { SectionO = g.Key}).ToList();

            return results;
        }

        public List<BOQLevelList> GetBOQLevel2List()
        {
            var results = (from b in _context.TblOriginalBoqs
                          group b.L2 by b.L2 into g
                          orderby g.Key
                          select new BOQLevelList { Level = g.Key }).ToList();

            return results;
        }

        public List<BOQLevelList> GetBOQLevel3List()
        {
            var results = (from b in _context.TblOriginalBoqs
                          group b.L3 by b.L3 into g
                          orderby g.Key
                          select new BOQLevelList { Level = g.Key }).ToList();

            return results;
        }

        public List<BOQLevelList> GetBOQLevel4List()
        {
            var results = (from b in _context.TblOriginalBoqs
                          group b.L4 by b.L4 into g
                          orderby g.Key
                          select new BOQLevelList { Level = g.Key }).ToList();

            return results;
        }

        public List<RESDivList> RESDivList()
        {
            var results = (from b in _context.TblBoqs
                        group b by b.BoqDiv into g
                        orderby g.Key
                        select new RESDivList { BoqDiv = g.Key }).ToList();

            return results;
        }

        public List<RESTypeList> RESTypeList()
        {
            var results =( from b in _context.TblBoqs
                          group b by b.BoqCtg into g
                          orderby g.Key
                          select new RESTypeList { BoqCtg = g.Key }).ToList();

            return results;
        }

        public List<Package> PackageList()
        {
            var results = (from b in _mdbContext.TblPackages
                           orderby b.PkgeName
                           select new Package
                           {
                               IDPkge = b.PkgeId,
                               PkgeName = b.PkgeName
                           }).ToList();

            return results;
        }

        public List<RESPackageList> RESPackageList()
        {
            var results = (from b in _context.TblBoqs
                          group b by b.BoqPackage into g
                          orderby g.Key
                          select new RESPackageList { BoqPackage = g.Key }).ToList();

            return results;
        }

        public List<SheetDescList> SheetDescList()
        {
            var results = (from b in _context.TblOriginalBoqs
                          group b by b.ObSheetDesc into g
                          orderby g.Key
                          select new SheetDescList { obSheetDesc = g.Key }).ToList();

            return results;
        }

        public List<RessourceList> GetRessourcesList()
        {
            var boqResList = (from b in _context.TblBoqs
                                            group b by b.BoqResSeq into g
                                            orderby g.Key
                                            select new Ressource
                                            { seq= g.Key }).ToList();

            var results = (from b in boqResList 
                           join c in _context.TblResources
                           on b.seq equals c.ResSeq
                           orderby c.ResDescription
                           select new RessourceList
                           {
                               resSeq = c.ResSeq,
                               resDesc = c.ResDescription
                           }).ToList();

            return results;
        }

        public List<BOQLevelList> GetBOQLevel3ListByLevel2(RessourceLevelsFilter filter)
        {
            IQueryable<BOQLevelList> query = null;
            List<BOQLevelList> results = null;

            var Level2 = filter.Level2;
            var Level3 = filter.Level3;
            var Level4 = filter.Level4;

            if (Level2.Count>0)
            {
                query = (from b in _context.TblOriginalBoqs
                         where Level2.Contains(b.L2)
                         group b.L3 by b.L3 into g
                         orderby g.Key
                         select new BOQLevelList { Level = g.Key }).Distinct();
            }
            else
                query = (from b in _context.TblOriginalBoqs                                        
                         group b.L3 by b.L3 into g
                         orderby g.Key
                         select new BOQLevelList { Level = g.Key }).Distinct();

            return query.ToList();
        }

        public List<BOQLevelList> GetBOQLevel4ListByLevel3(RessourceLevelsFilter filter)
        {
            IQueryable<BOQLevelList> query = null;
            List<BOQLevelList> results = null;

            var Level2 = filter.Level2;
            var Level3 = filter.Level3;
            var Level4 = filter.Level4;

            if (Level2.Count > 0)           
                query = (from b in _context.TblOriginalBoqs
                         where Level2.Contains(b.L2)
                         group b.L4 by b.L4 into g
                         orderby g.Key
                         select new BOQLevelList { Level = g.Key }).Distinct();
            
            else if (Level3.Count > 0)
                query = (from b in _context.TblOriginalBoqs
                         where Level3.Contains(b.L3)
                         group b.L4 by b.L4 into g
                         orderby g.Key
                         select new BOQLevelList { Level = g.Key }).Distinct();

            else
                query = (from b in _context.TblOriginalBoqs
                           group b.L4 by b.L4 into g
                           orderby g.Key
                           select new BOQLevelList { Level = g.Key }).Distinct();

            return query.ToList();
        }

        public List<RessourceList> GetRessourcesListByLevels(RessourceLevelsFilter filter)
        {            
            IQueryable<RessourceList> query = null;
            
            
            List<RessourceList> results = null;

            var Level2 = filter.Level2;
            var Level3 = filter.Level3;
            var Level4 = filter.Level4;

            if ((Level2 == null || Level2.Count == 0) && (Level3 == null || Level3.Count == 0) && (Level4 == null || Level4.Count == 0))
            {
                query = (from b in _context.TblBoqs
                         join c in _context.TblResources
                         on b.BoqResSeq equals c.ResSeq
                         join i in _context.TblOriginalBoqs on b.BoqItem equals i.ItemO
                         orderby c.ResDescription
                         select new RessourceList
                         {
                             resSeq = c.ResSeq,
                             resDesc = c.ResDescription
                         }).Distinct();
            }
            else if (Level2 != null && (Level3 == null || Level3.Count == 0) && (Level4 == null || Level4.Count == 0))
            {
                query = (from b in _context.TblBoqs
                         join c in _context.TblResources
                         on b.BoqResSeq equals c.ResSeq
                         join i in _context.TblOriginalBoqs on b.BoqItem equals i.ItemO
                         where Level2.Contains(i.L2)
                         orderby c.ResDescription
                         select new RessourceList
                         {
                             resSeq = c.ResSeq,
                             resDesc = c.ResDescription
                         }).Distinct();
            }
            else if (Level3 != null && (Level4 == null || Level4.Count == 0))
            {
                query = (from b in _context.TblBoqs
                         join c in _context.TblResources
                         on b.BoqResSeq equals c.ResSeq
                         join i in _context.TblOriginalBoqs on b.BoqItem equals i.ItemO
                         where Level3.Contains(i.L3)
                         orderby c.ResDescription
                         select new RessourceList
                         {
                             resSeq = c.ResSeq,
                             resDesc = c.ResDescription
                         }).Distinct();
            }
            else if (Level4 != null)
            {
                query = (from b in _context.TblBoqs
                         join c in _context.TblResources
                         on b.BoqResSeq equals c.ResSeq
                         join i in _context.TblOriginalBoqs on b.BoqItem equals i.ItemO
                         where Level4.Contains(i.L4)
                         orderby c.ResDescription
                         select new RessourceList
                         {
                             resSeq = c.ResSeq,
                             resDesc = c.ResDescription
                         }).Distinct();
            }

            return query.ToList();
        }

    }
}
