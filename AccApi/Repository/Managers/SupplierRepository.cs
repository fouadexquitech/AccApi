using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
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
        private readonly AccDbContext _context;
        private readonly GlobalLists _globalLists;


        public SupplierRepository(AccDbContext context, GlobalLists globalLists)
        {
            _globalLists = globalLists;
            _context = _context = new AccDbContext(new DbContextOptionsBuilder<AccDbContext>().UseSqlServer(_globalLists.GetAccDbconnectionString()).Options);
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
