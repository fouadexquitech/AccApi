﻿using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.Models.MasterModels;
using AccApi.Repository.View_Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;

namespace AccApi.Repository.Managers
{
    public class SearchRepository : ISearchRepository
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

        public List<BOQDivList> GetBOQDivList(RessourceLevelsFilter filter, string CostConn)
        {
            List<BOQDivList> results = null;

            var Level2 = filter.Level2;
            var Level3 = filter.Level3;
            var Level4 = filter.Level4;
            var resType = filter.resType;

            AccDbContext _costDbcontext = new AccDbContext(CostConn);
            //AccDbContext _costDbcontext = new AccDbContext(_globalLists.GetAccDbconnectionString());

            if ((Level2 == null || Level2.Count == 0) && (Level3 == null || Level3.Count == 0) && (Level4 == null || Level4.Count == 0))
                results = (from o in _costDbcontext.TblOriginalBoqVds                         
                           group o.SectionO by o.SectionO into g
                           orderby g.Key
                           select new BOQDivList { SectionO = g.Key }).ToList();
            else
                results = (from o in _costDbcontext.TblOriginalBoqVds
                       join b in _costDbcontext.TblBoqVds on o.ItemO equals b.BoqItem
                       where (Level2.Count == 0 || Level2.Contains(o.L2)) &&
                             (Level3.Count == 0 || Level3.Contains(o.L3)) &&
                             (Level4.Count == 0 || Level4.Contains(o.L4)) &&
                             (resType.Count == 0 || resType.Contains(b.BoqCtg))
                       group o.SectionO by o.SectionO into g
                           orderby g.Key
                           select new BOQDivList { SectionO = g.Key }).ToList();

            return results;
        }

        public List<BOQLevelList> GetBOQLevel2List(RessourceLevelsFilter filter, string CostConn)
        {
            List<BOQLevelList> results = null;

            var Level2 = filter.Level2;
            var Level3 = filter.Level3;
            var Level4 = filter.Level4;
            var resType = filter.resType;
            var divO = filter.boqDiv;

            //List<resourcesType> resTypeList = new List<resourcesType>();
            //foreach (var item in resType)
            //{
            //    resTypeList.Add(new resourcesType() { resourceType = item.ToString() });
            //}

            //if (resTypeList.Count > 0)
            //{
            AccDbContext _costDbcontext = new AccDbContext(CostConn);

            if (resType == null || resType.Count == 0)
                results = (from o in _costDbcontext.TblOriginalBoqVds 
                           where (Level2.Count == 0 || Level2.Contains(o.L2)) &&
                         (Level3.Count == 0 || Level3.Contains(o.L3)) &&
                         (Level4.Count == 0 || Level4.Contains(o.L4)) &&                        
                         (divO.Count == 0 || divO.Contains(o.SectionO))
                           group o.L2 by o.L2 into g
                           orderby g.Key
                           select new BOQLevelList
                           {
                               Level = g.Key
                           }).ToList();

            else
                results = (from b in _costDbcontext.TblBoqVds 
                           join o in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals o.ItemO
                           where (Level2.Count == 0 || Level2.Contains(o.L2)) &&
                         (Level3.Count == 0 || Level3.Contains(o.L3)) &&
                         (Level4.Count == 0 || Level4.Contains(o.L4)) &&
                         (resType.Count == 0 || resType.Contains(b.BoqCtg)) &&
                         (divO.Count == 0 || divO.Contains(o.SectionO))
                           group o.L2 by o.L2 into g
                           orderby g.Key
                           select new BOQLevelList
                           {
                             Level = g.Key
                           }).ToList();
            //}
            //else
            //    results = (from b in _costDbcontext.TblOriginalBoqVds
            //               where (divO.Count == 0 || divO.Contains(b.SectionO))
            //               group b.L2 by b.L2 into g
            //               orderby g.Key
            //               select new BOQLevelList
            //               {
            //                 Level = g.Key
            //               }).ToList();

            return results;
        }

        //public List<BOQLevelList> GetBOQLevel2List()
        //{
        //  var  results = (from b in _costDbcontext.TblOriginalBoqVds
        //                   group b.L2 by b.L2 into g
        //                   orderby g.Key
        //                   select new BOQLevelList
        //                   {
        //                       Level = g.Key
        //                   }).ToList();

        //    return results;
        //}

        public List<BOQLevelList> GetBOQLevel3List(RessourceLevelsFilter filter, string CostConn)
        {
            List<BOQLevelList> results = null;

            var Level2 = filter.Level2;
            var Level3 = filter.Level3;
            var Level4 = filter.Level4;
            var resType = filter.resType;
            var divO = filter.boqDiv;

            //List<resourcesType> resTypeList = new List<resourcesType>();
            //foreach (var item in resType)
            //{
            //    resTypeList.Add(new resourcesType() { resourceType = item.ToString() });
            //}
            AccDbContext _costDbcontext = new AccDbContext(CostConn);

            if (resType == null || resType.Count == 0)
            { 
                results = (from o in _costDbcontext.TblOriginalBoqVds 
                           where (Level2.Count == 0 || Level2.Contains(o.L2)) &&
                              (Level3.Count == 0 || Level3.Contains(o.L3)) &&
                              (Level4.Count == 0 || Level4.Contains(o.L4)) &&
                              (divO.Count == 0 || divO.Contains(o.SectionO))
                           group o.L3 by o.L3 into g
                           orderby g.Key
                           select new BOQLevelList
                           {
                               Level = g.Key
                           }).ToList();
            }
            else
                results = (from b in _costDbcontext.TblBoqVds 
                           join o in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals o.ItemO
                           where (Level2.Count == 0 || Level2.Contains(o.L2)) &&
                              (Level3.Count == 0 || Level3.Contains(o.L3)) &&
                              (Level4.Count == 0 || Level4.Contains(o.L4)) &&
                              (resType.Count == 0 || resType.Contains(b.BoqCtg)) &&
                              (divO.Count == 0 || divO.Contains(o.SectionO))
                           group o.L3 by o.L3 into g
                           orderby g.Key
                           select new BOQLevelList
                           {
                               Level = g.Key
                           }).ToList();

            //}
            //else
            //    results = (from b in _costDbcontext.TblOriginalBoqVds
            //               where (divO.Count == 0 || divO.Contains(b.SectionO))
            //               group b.L3 by b.L3 into g
            //               orderby g.Key
            //               select new BOQLevelList
            //               { Level = g.Key }).ToList();


            return results;
        }

        public List<BOQLevelList> GetBOQLevel4List(RessourceLevelsFilter filter, string CostConn)
        {
            List<BOQLevelList> results = null;

            var Level2 = filter.Level2;
            var Level3 = filter.Level3;
            var Level4 = filter.Level4;
            var resType = filter.resType;
            var divO = filter.boqDiv;

            //List<resourcesType> resTypeList = new List<resourcesType>();
            //foreach (var item in resType)
            //{
            //    resTypeList.Add(new resourcesType() { resourceType = item.ToString() });
            //}

            AccDbContext _costDbcontext = new AccDbContext(CostConn);

            if (resType == null || resType.Count == 0)
                results = (from o in _costDbcontext.TblOriginalBoqVds
                           where (Level2.Count == 0 || Level2.Contains(o.L2)) &&
                              (Level3.Count == 0 || Level3.Contains(o.L3)) &&
                              (Level4.Count == 0 || Level4.Contains(o.L4)) &&
                              (divO.Count == 0 || divO.Contains(o.SectionO))
                           group o.L4 by o.L4 into g
                           orderby g.Key
                           select new BOQLevelList
                           {
                               Level = g.Key
                           }).ToList();
            else
                results = (from b in _costDbcontext.TblBoqVds 
                           join o in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals o.ItemO
                           where (Level2.Count == 0 || Level2.Contains(o.L2)) &&
                              (Level3.Count == 0 || Level3.Contains(o.L3)) &&
                              (Level4.Count == 0 || Level4.Contains(o.L4)) &&
                              (resType.Count == 0 || resType.Contains(b.BoqCtg)) &&
                              (divO.Count == 0 || divO.Contains(o.SectionO))
                           group o.L4 by o.L4 into g
                           orderby g.Key
                           select new BOQLevelList
                           {
                               Level = g.Key
                           }).ToList();
            //}
            //else
            //    results = (from b in _costDbcontext.TblOriginalBoqVds
            //               where (divO.Count == 0 || divO.Contains(b.SectionO))
            //               group b.L4 by b.L4 into g
            //               orderby g.Key
            //               select new BOQLevelList { Level = g.Key }).ToList();


            return results;
        }

        public List<RESTypeList> GetResTypeList(RessourceLevelsFilter filter, string CostConn)
        {
            List<RESTypeList> results = null;

            var Level2 = filter.Level2;
            var Level3 = filter.Level3;
            var Level4 = filter.Level4;
            var resType = filter.resType;
            var divO = filter.boqDiv;

            AccDbContext _costDbcontext = new AccDbContext(CostConn);

            results = (from b in _costDbcontext.TblBoqVds
                       join i in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals i.ItemO
                       where    (Level2.Count==0 || Level2.Contains(i.L2)) &&
                                (Level3.Count == 0 || Level3.Contains(i.L3)) &&
                                (Level4.Count == 0 || Level4.Contains(i.L4)) &&
                                (divO.Count == 0 || divO.Contains(i.SectionO))
                       group b by b.BoqCtg into g
                       orderby g.Key
                       select new RESTypeList { BoqCtg = g.Key }).ToList();

            //if (Level4.Count > 0)
            //    if (divO.Count > 0)
            //        results = (from b in _costDbcontext.TblBoqVds
            //                   join i in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals i.ItemO
            //                   where Level4.Contains(i.L4) && divO.Contains(i.SectionO)
            //                   group b by b.BoqCtg into g
            //                   orderby g.Key
            //                   select new RESTypeList { BoqCtg = g.Key }).ToList();
            //    else
            //        results = (from b in _costDbcontext.TblBoqVds
            //                   join i in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals i.ItemO
            //                   where Level4.Contains(i.L4)
            //                   group b by b.BoqCtg into g
            //                   orderby g.Key
            //                   select new RESTypeList { BoqCtg = g.Key }).ToList();

            //else if (Level3.Count > 0)
            //    if (divO.Count > 0)
            //        results = (from b in _costDbcontext.TblBoqVds
            //                   join i in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals i.ItemO
            //                   where Level3.Contains(i.L3) && divO.Contains(i.SectionO)
            //                   group b by b.BoqCtg into g
            //                   orderby g.Key
            //                   select new RESTypeList { BoqCtg = g.Key }).ToList();
            //    else
            //        results = (from b in _costDbcontext.TblBoqVds
            //                   join i in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals i.ItemO
            //                   where Level3.Contains(i.L3)
            //                   group b by b.BoqCtg into g
            //                   orderby g.Key
            //                   select new RESTypeList { BoqCtg = g.Key }).ToList();

            //else if (Level2.Count > 0)
            //    if (divO.Count > 0)
            //        results = (from b in _costDbcontext.TblBoqVds
            //                   join i in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals i.ItemO
            //                   where Level2.Contains(i.L2) && divO.Contains(i.SectionO)
            //                   group b by b.BoqCtg into g
            //                   orderby g.Key
            //                   select new RESTypeList { BoqCtg = g.Key }).ToList();
            //    else
            //        results = (from b in _costDbcontext.TblBoqVds
            //                   join i in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals i.ItemO
            //                   where Level2.Contains(i.L2)
            //                   group b by b.BoqCtg into g
            //                   orderby g.Key
            //                   select new RESTypeList { BoqCtg = g.Key }).ToList();

            //else
            //   if (divO.Count > 0)
            //      results = (from b in _costDbcontext.TblBoqVds
            //                 join i in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals i.ItemO
            //                 where divO.Contains(i.SectionO)
            //                 group b by b.BoqCtg into g
            //                 orderby g.Key
            //                 select new RESTypeList { BoqCtg = g.Key }).ToList();
            //   else
            //      results = (from b in _costDbcontext.TblBoqVds
            //               group b by b.BoqCtg into g
            //               orderby g.Key
            //               select new RESTypeList { BoqCtg = g.Key }).ToList();


            return results;
        }

        public List<RESDivList> RESDivList(string CostConn)
        {
            AccDbContext _costDbcontext = new AccDbContext(CostConn);

            var results = (from b in _costDbcontext.TblBoqVds
                           group b by b.BoqDiv into g
                           orderby g.Key
                           select new RESDivList { BoqDiv = g.Key }).ToList();

            return results;
        }


        public List<Package> GetPackagesList(bool usedPackages, string CostConn)
        {
            List<Package> results = null;

            if (usedPackages)
            {
                AccDbContext _costDbcontext = new AccDbContext(CostConn);

                var usedPackO = _costDbcontext.TblOriginalBoqVds.Where(x => x.Scope > 0).Select(p => p.Scope).Distinct().ToList();
                var usedPackB = _costDbcontext.TblBoqVds.Where(x => x.BoqScope > 0).Select(p => p.BoqScope).Distinct().ToList();
                var usedPack = usedPackO.Union(usedPackB).ToList();

                 results = (from b in _mdbContext.TblPackages
                               where usedPack.Contains(b.PkgeId)
                               orderby b.PkgeName
                               select new Package
                               {
                                   IDPkge = b.PkgeId,
                                   PkgeName = b.PkgeName
                               }).ToList();
            }
            else
            {
                 results = (from b in _mdbContext.TblPackages
                               orderby b.PkgeName
                               select new Package
                               {
                                   IDPkge = b.PkgeId,
                                   PkgeName = b.PkgeName
                               }).ToList();
            }

            var l= results.OrderBy(p=>p.PkgeName).ToList();
            return l;
        }

        public List<RESPackageList> RESPackageList(string CostConn)
        {
            AccDbContext _costDbcontext = new AccDbContext(CostConn);

            var results = (from b in _costDbcontext.TblBoqVds
                           group b by b.BoqPackage into g
                           orderby g.Key
                           select new RESPackageList { BoqPackage = g.Key }).ToList();

            return results;
        }

        public List<SheetDescList> SheetDescList( string CostConn)
        {
            AccDbContext _costDbcontext = new AccDbContext(CostConn);

            var results = (from b in _costDbcontext.TblOriginalBoqVds
                           group b by b.ObSheetDesc into g
                           orderby g.Key
                           select new SheetDescList { obSheetDesc = g.Key }).ToList();

            return results;
        }

        public List<RessourceList> GetRessourcesList(RessourceLevelsFilter filter, string CostConn)
        {
            AccDbContext _costDbcontext = new AccDbContext(CostConn);

            List<Ressource> resList = null;
            List<Ressource> boqResList = null;
            List<RessourceList> results = null;

            var Level2 = filter.Level2;
            var Level3 = filter.Level3;
            var Level4 = filter.Level4;
            var resType = filter.resType;
            var divO = filter.boqDiv;

            List<resourcesType> resTypeList = new List<resourcesType>();
            foreach (var item in resType)
            {
                resTypeList.Add(new resourcesType() { resourceType = item.ToString() });
            }

            if (resTypeList.Count > 0)
                resList = (from l in resTypeList
                           join b in _costDbcontext.TblBoqVds on l.resourceType equals b.BoqCtg
                           group b by b.BoqResSeq into g
                           orderby g.Key
                           select new Ressource
                           {
                               ResId = g.Key
                           }).ToList();


            //if (Level4.Count > 0)
                boqResList = (from b in _costDbcontext.TblBoqVds
                              join i in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals i.ItemO
                              where (Level2.Count == 0 || Level2.Contains(i.L2)) &&
                              (Level3.Count == 0 || Level3.Contains(i.L3)) &&
                              (Level4.Count == 0 || Level4.Contains(i.L4)) &&
                              (divO.Count == 0 || divO.Contains(i.SectionO))
                              group b by b.BoqResSeq into g
                              orderby g.Key
                              select new Ressource
                              { ResId = g.Key }).ToList();

            //else if (Level3.Count > 0)
            //    boqResList = (from b in _costDbcontext.TblBoqVds
            //                  join i in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals i.ItemO
            //                  where Level3.Contains(i.L3)
            //                  group b by b.BoqResSeq into g
            //                  orderby g.Key
            //                  select new Ressource
            //                  { ResId = g.Key }).ToList();

            //else if (Level2.Count > 0)
            //    boqResList = (from b in _costDbcontext.TblBoqVds
            //                  join i in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals i.ItemO
            //                  where Level2.Contains(i.L2)
            //                  group b by b.BoqResSeq into g
            //                  orderby g.Key
            //                  select new Ressource
            //                  { ResId = g.Key }).ToList();

            //else
            //    boqResList = (from b in _costDbcontext.TblBoqVds
            //                  group b by b.BoqResSeq into g
            //                  orderby g.Key
            //                  select new Ressource
            //                  { ResId = g.Key }).ToList();

            if (resList != null)
                results = (from r in resList
                           join b in boqResList on r.ResId equals b.ResId
                           join c in _costDbcontext.TblResources on b.ResId equals c.ResSeq
                           orderby c.ResDescription
                           select new RessourceList
                           {
                               resSeq = c.ResSeq,
                               resDesc = c.ResDescription
                           }).ToList();
            else
                results = (from b in boqResList
                           join c in _costDbcontext.TblResources
                           on b.ResId equals c.ResSeq
                           orderby c.ResDescription
                           select new RessourceList
                           {
                               resSeq = c.ResSeq,
                               resDesc = c.ResDescription
                           }).ToList();

            return results;
        }

        public List<BOQLevelList> GetBOQLevel3ListByLevel2(RessourceLevelsFilter filter, string CostConn)
        {
            IQueryable<BOQLevelList> query = null;

            var Level2 = filter.Level2;
            var Level3 = filter.Level3;
            var Level4 = filter.Level4;
            var divO = filter.boqDiv;

            //if (Level2.Count > 0)
            //{
            AccDbContext _costDbcontext = new AccDbContext(CostConn);

            query = (from b in _costDbcontext.TblOriginalBoqVds
                         where (Level2.Count == 0 || Level2.Contains(b.L2)) &&
                               (divO.Count == 0 || divO.Contains(b.SectionO))
                         group b.L3 by b.L3 into g
                         orderby g.Key
                         select new BOQLevelList { Level = g.Key }).Distinct();
            //}
            //else
            //    query = (from b in _costDbcontext.TblOriginalBoqVds
            //             group b.L3 by b.L3 into g
            //             orderby g.Key
            //             select new BOQLevelList { Level = g.Key }).Distinct();

            return query.ToList();
        }

        public List<BOQLevelList> GetBOQLevel4ListByLevel3(RessourceLevelsFilter filter, string CostConn)
        {
            IQueryable<BOQLevelList> query = null;

            var Level2 = filter.Level2;
            var Level3 = filter.Level3;
            var Level4 = filter.Level4;
            var divO = filter.boqDiv;

            //if (Level2.Count > 0)
            AccDbContext _costDbcontext = new AccDbContext(CostConn);

            query = (from i in _costDbcontext.TblOriginalBoqVds
                         where (Level2.Count == 0 || Level2.Contains(i.L2)) &&
                               (Level3.Count == 0 || Level3.Contains(i.L3)) &&
                               (divO.Count == 0 || divO.Contains(i.SectionO))
                         group i.L4 by i.L4 into g
                         orderby g.Key
                         select new BOQLevelList { Level = g.Key }).Distinct();

            //else if (Level3.Count > 0)
            //    query = (from b in _costDbcontext.TblOriginalBoqVds
            //             where Level3.Contains(b.L3)
            //             group b.L4 by b.L4 into g
            //             orderby g.Key
            //             select new BOQLevelList { Level = g.Key }).Distinct();

            //else
            //    query = (from b in _costDbcontext.TblOriginalBoqVds
            //             group b.L4 by b.L4 into g
            //             orderby g.Key
            //             select new BOQLevelList { Level = g.Key }).Distinct();

            return query.ToList();
        }

        public List<RessourceList> GetRessourcesListByLevels(RessourceLevelsFilter filter, string CostConn)
        {
            IQueryable<RessourceList> query = null;

            var Level2 = filter.Level2;
            var Level3 = filter.Level3;
            var Level4 = filter.Level4;
            var resType = filter.resType;
            var divO = filter.boqDiv;

            //if ((Level2 == null || Level2.Count == 0) && (Level3 == null || Level3.Count == 0) && (Level4 == null || Level4.Count == 0))
            //{
            //    query = (from b in _costDbcontext.TblBoqVds
            //             join c in _costDbcontext.TblResources
            //             on b.BoqResSeq equals c.ResSeq
            //             join i in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals i.ItemO
            //             orderby c.ResDescription
            //             select new RessourceList
            //             {
            //                 resSeq = c.ResSeq,
            //                 resDesc = c.ResDescription,
            //                 resType = b.BoqCtg
            //             }).Distinct();
            //}
            //else if (Level2 != null && (Level3 == null || Level3.Count == 0) && (Level4 == null || Level4.Count == 0))
            //{
            //    query = (from b in _costDbcontext.TblBoqVds
            //             join c in _costDbcontext.TblResources
            //             on b.BoqResSeq equals c.ResSeq
            //             join i in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals i.ItemO
            //             where Level2.Contains(i.L2)
            //             orderby c.ResDescription
            //             select new RessourceList
            //             {
            //                 resSeq = c.ResSeq,
            //                 resDesc = c.ResDescription,
            //                 resType = b.BoqCtg
            //             }).Distinct();
            //}
            //else if (Level3 != null && (Level4 == null || Level4.Count == 0))
            //{
            //    query = (from b in _costDbcontext.TblBoqVds
            //             join c in _costDbcontext.TblResources
            //             on b.BoqResSeq equals c.ResSeq
            //             join i in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals i.ItemO
            //             where Level3.Contains(i.L3)
            //             orderby c.ResDescription
            //             select new RessourceList
            //             {
            //                 resSeq = c.ResSeq,
            //                 resDesc = c.ResDescription,
            //                 resType = b.BoqCtg
            //             }).Distinct();
            //}
            //else if (Level4 != null)
            //{
            AccDbContext _costDbcontext = new AccDbContext(CostConn );

            query = (from b in _costDbcontext.TblBoqVds
                         join c in _costDbcontext.TblResources
                         on b.BoqResSeq equals c.ResSeq
                         join i in _costDbcontext.TblOriginalBoqVds on b.BoqItem equals i.ItemO
                         where (Level2.Count == 0 || Level2.Contains(i.L2)) &&
                               (Level3.Count == 0 || Level3.Contains(i.L3)) &&
                               (Level4.Count == 0 || Level4.Contains(i.L4)) &&
                               (resType.Count == 0 || resType.Contains(b.BoqCtg)) &&
                               (divO.Count == 0 || divO.Contains(i.SectionO))
                               orderby c.ResDescription

                         select new RessourceList
                         {
                             resSeq = c.ResSeq,
                             resDesc = c.ResDescription,
                             resType = b.BoqCtg
                         }).Distinct();

            //if (resType!=null)
            //    List<RessourceList> customers = query.Where(x => filter.resType.Contains(x.resType)).ToList();
            //if (resType.Count > 0)
            //{
            //    query = from item in query
            //            where resType.Contains(item.resType)
            //            select item;
            //}

            return query.ToList();
        }

    }
}
