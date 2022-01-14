using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace AccApi.Repository.Managers
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AccDbContext _context;

        public SupplierRepository(AccDbContext context)
        {
            _context = context;
        }

        public List<SupplierList> SupplierList(int packID)
        {
            PackagesNetwork Package = (from p in _context.PackagesNetworks                        
                          where p.IdPkge == packID
                         select p).First();

            var results=from b in _context.TblSuppliers
                       join d in _context.TblSupplierDivs
                       on b.SupCode  equals d.SupCode
                       where d.SupDiv == Package.Division
                        orderby b.SupName
                       select new SupplierList
                       {
                           SupID = b.SupCode,
                           SupName = b.SupName,
                           SupEmail=b.SupEmail
                       };
            return results.ToList();
        }
    }
}
