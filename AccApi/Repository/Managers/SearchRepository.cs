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
        private readonly GlobalLists _globalLists;

        public SearchRepository(AccDbContext context, GlobalLists globalLists)
        {
            _globalLists = globalLists;
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
            var results = (from b in _context.PackagesNetworks
                          orderby b.PkgeName
                          select new Package { 
                              IDPkge = b.IdPkge ,
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

    }
}
