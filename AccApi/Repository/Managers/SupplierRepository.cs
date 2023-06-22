using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.Models.MasterModels;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AccApi.Repository.Managers
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly MasterDbContext _mdbcontext;
        private readonly GlobalLists _globalLists;
        private readonly AccDbContext _dbcontext;

        public SupplierRepository(MasterDbContext mdbContext, GlobalLists globalLists)
        {
            _globalLists = globalLists;
            _mdbcontext = mdbContext;
            _dbcontext = new AccDbContext(_globalLists.GetAccDbconnectionString());
        }

        public List<Supplier> SupplierList(int packID)
        {
            Models.MasterModels.TblPackage package = (from p in _mdbcontext.TblPackages                        
            where p.PkgeId == packID
            select p).First();

            //var results=from b in _mdbcontext.TblSuppliers
            //           join d in _mdbcontext.TblSupplierDivs
            //           on b.SupCode  equals d.SupCode
            //           where d.SupDiv == package.Division
            //            orderby b.SupName
            //           select new Supplier
            //           {
            //               SupID = b.SupCode,
            //               SupName = b.SupName,
            //               SupEmail=b.SupEmail
            //           };

            var results = from b in _mdbcontext.TblSuppliers
                          orderby b.SupName
                          select new Supplier
                          {
                              SupID = b.SupCode,
                              SupName = b.SupName,
                              SupEmail = b.SupEmail
                          };

            return results.ToList();
        }

        public List<Supplier> GetSuppliers(string filter)
        {
            var result = (from b in _mdbcontext.TblSuppliers
                          orderby b.SupName
                          select new Supplier
                          {
                              SupID = b.SupCode,
                              SupName = b.SupName,
                              SupEmail = b.SupEmail
                          }).ToList();

            //return result.ToList();

            if (filter != null)
            {
                result = result.Where(x => string.Concat(x.SupName.ToUpper()).Contains(filter.ToUpper())).ToList();
            }
            return result.ToList();
        }


        public bool AddSupplier(List<Supplier> sups)
        {
            foreach (var item in sups)
            {
                var result = new Models.MasterModels.TblSupplier { SupName = item.SupName, SupEmail = item.SupEmail };
                _mdbcontext.Add<Models.MasterModels.TblSupplier>(result);
                _mdbcontext.SaveChanges();
            }

            return true;
        }

        public bool UpdateSupplier(Supplier sup)
        {
            var result = _mdbcontext.TblSuppliers.Where(x => x.SupCode == sup.SupID).FirstOrDefault();
            result.SupName = sup.SupName;
            result.SupEmail = sup.SupEmail;

            if (result != null)
            {
                _mdbcontext.TblSuppliers.Update(result);
                _mdbcontext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool DeleteSupplier(int id)
        {
            var supPackage = _dbcontext.TblSupplierPackages.Where(x => x.SpPackSuppId == id).FirstOrDefault();
            if (supPackage == null)
            {
                var result = _mdbcontext.TblSuppliers.Where(x => x.SupCode == id).FirstOrDefault();
                if (result != null)
                {
                    _mdbcontext.TblSuppliers.Remove(result);
                    _mdbcontext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
    }
}
