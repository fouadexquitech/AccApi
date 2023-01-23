using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.View_Models;
using System.Collections.Generic;
using System.Linq;

namespace AccApi.Repository.Managers
{
    public class SearchRepository: ISearchRepository
    {
        private readonly AccDbContext _context;

        public SearchRepository(AccDbContext context)
        {
            _context = context;
        }

        public List<BOQDivList> GetBOQDivList()
        {
            var results = from b in _context.TblOriginalBoqs
                          group b.SectionO by b.SectionO into g
                          orderby g.Key
                          select new BOQDivList { SectionO = g.Key};

            return results.ToList();
        }

        public List<BOQLevelList> GetBOQLevel2List()
        {
            var results = from b in _context.TblOriginalBoqs
                          group b.L2 by b.L2 into g
                          orderby g.Key
                          select new BOQLevelList { Level = g.Key };

            return results.ToList();
        }

        public List<RESDivList> RESDivList()
        {
            var results = from b in _context.TblBoqs
                        group b by b.BoqDiv into g
                        orderby g.Key
                        select new RESDivList { BoqDiv = g.Key };

            return results.ToList();
        }

        public List<RESTypeList> RESTypeList()
        {
            var results = from b in _context.TblBoqs
                          group b by b.BoqCtg into g
                          orderby g.Key
                          select new RESTypeList { BoqCtg = g.Key };

            return results.ToList();
        }

        public List<PackageList> PackageList()
        {
            var results = from b in _context.PackagesNetworks
                          orderby b.PkgeName
                          select new PackageList { 
                              IDPkge = b.IdPkge ,
                              PkgeName = b.PkgeName
                          };

            return results.ToList();
        }

        public List<RESPackageList> RESPackageList()
        {
            var results = from b in _context.TblBoqs
                          group b by b.BoqPackage into g
                          orderby g.Key
                          select new RESPackageList { BoqPackage = g.Key };

            return results.ToList();
        }

        public List<SheetDescList> SheetDescList()
        {
            var results = from b in _context.TblOriginalBoqs
                          group b by b.ObSheetDesc into g
                          orderby g.Key
                          select new SheetDescList { obSheetDesc = g.Key };

            return results.ToList();
        }

    }
}
