using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

namespace AccApi.Repository.Managers
{
    public class PackageRepository : IPackageRepository
    {
        private readonly AccDbContext _context;
        private readonly MasterDbContext _mdbcontext;
        private readonly IMapper _mapper;
        private readonly GlobalLists _globalLists;

        //public IConfiguration Configuration { get; }

        //private readonly AccDbContext _context = new AccDbContext(new DbContextOptionsBuilder<AccDbContext>().UseSqlServer(@"Server=10.10.2.123;Database=CiteDefence_CostData;Persist Security Info=True;User ID=accdb;Password=db@TSs15;Integrated Security=false").Options);

        public PackageRepository(AccDbContext context, MasterDbContext mcontext, IMapper mapper, GlobalLists globalLists)
        {
            _mdbcontext = mcontext;
            _mapper = mapper;
            _globalLists = globalLists;
            _context = new AccDbContext(_globalLists.GetAccDbconnectionString());
            //_context =context;
        }

        public List<BoqRessourcesList> GetOriginalBoqList(SearchInput input)
        {
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");
            //string costDb = "CiteDefence_CostData";
            //var connection = new SqlConnectionStringBuilder(connectionString);
            //connection.InitialCatalog = costDb;

            //string conName = connection.ConnectionString.ToString();
            //var _costDBbContext = _context.CreateConnectionFromOut(conName);


            //var results = from b in _context.TblOriginalBoqs
            //              where (input.BOQDiv != null && b.SectionO == input.BOQDiv)
            //              where (input.SheetDesc != null && b.ObSheetDesc == input.SheetDesc)
            //              where (input.FromRow != null && input.ToRow != null && b.RowNumber <= int.Parse(input.FromRow) && b.RowNumber >= int.Parse(input.ToRow))
            //              orderby b.RowNumber
            //              select b;


            //var results = from b in _context.TblOriginalBoqs
            //              where (input.BOQDiv != null || b.SectionO == input.BOQDiv)
            //                   && (input.SheetDesc != null || b.ObSheetDesc == input.SheetDesc)
            //                   && ((input.FromRow != null && input.ToRow != null) || (b.RowNumber >= int.Parse(input.FromRow) && b.RowNumber <= int.Parse(input.ToRow)))
            //              orderby b.RowNumber
            //              select b;

            bool blankInput=true;  
            if (input.BOQDiv.Length > 0) blankInput=false;
            if (!string.IsNullOrEmpty(input.BOQItem)) blankInput = false;
            if (!string.IsNullOrEmpty(input.BOQDesc)) blankInput = false;
            if (!string.IsNullOrEmpty(input.SheetDesc)) blankInput = false;
            if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) blankInput = false;
            if (input.Package > 0) blankInput = false;
            if (input.RESDiv.Length > 0) blankInput = false;
            if (input.RESType.Length > 0) blankInput = false;
            if (!string.IsNullOrEmpty(input.RESPackage)) blankInput = false;
            if (!string.IsNullOrEmpty(input.RESDesc)) blankInput = false;
            if (input.boqLevel2.Length > 0) blankInput = false;
            if (input.boqLevel3.Length > 0) blankInput = false;
            if (input.boqLevel4.Length > 0) blankInput = false;
            if (!string.IsNullOrEmpty(input.obTradeDesc)) blankInput = false;
            if (input.isItemsAssigned > 0) blankInput = false;
            if (input.boqResourceSeq.Length > 0) blankInput = false;
            if (input.isRessourcesAssigned > 0) blankInput = false;
            
            if (blankInput)
            {
                //List<BoqRessourcesList> res = new List<BoqRessourcesList>();
                return null;
            }

            var packList = (from p in _mdbcontext.TblPackages
                               select new packagesList
                               {
                                   PkgeId = (int)p.PkgeId,
                                   PkgeName = p.PkgeName
                               }).ToList();

            //IEnumerable<BoqRessourcesList> condQuery = (from o in _context.TblOriginalBoqs
            //                 join b in _context.TblBoqs on o.ItemO equals b.BoqItem
            //                 join r in _context.TblResources on b.BoqResSeq equals r.ResSeq
            //                 join p in packList on o.Scope equals p.PkgeId into gj
            //                 from pk in gj.DefaultIfEmpty()

            IEnumerable<BoqRessourcesList> condQuery = (from o in _context.TblOriginalBoqs
                                                        join b in _context.TblBoqs on o.ItemO equals b.BoqItem
                                                        join r in _context.TblResources on b.BoqResSeq equals r.ResSeq
                                                        select new BoqRessourcesList
                                                        {
                                                            RowNumber = o.RowNumber,
                                                            SectionO = o.SectionO,
                                                            ItemO = o.ItemO,
                                                            DescriptionO = o.DescriptionO,
                                                            UnitO = o.UnitO,
                                                            UnitRate = o.UnitRate,
                                                            Scope = o.Scope,
                                                            L2 = ((o.L2 == null) ? "" : o.L2),
                                                            L3 = ((o.L3 == null) ? "" : o.L3),
                                                            L4 = ((o.L4 == null) ? "" : o.L4),
                                                            BillQtyO = o.ObBillQty,
                                                            QtyO = o.QtyO,
                                                            ScopeQtyO = o.QtyScope,
                                                            ObTradeDesc = ((o.ObTradeDesc == null) ? "" : o.ObTradeDesc),
                                                            ObSheetDesc = ((o.ObSheetDesc == null) ? "" : o.ObSheetDesc),
                                                            BoqSeq = b.BoqSeq,
                                                            BoqCtg = b.BoqCtg,
                                                            BoqUnitMesure = b.BoqUnitMesure,
                                                            BoqUprice = b.BoqUprice,
                                                            BoqDiv = b.BoqDiv,
                                                            BoqPackage = b.BoqPackage,
                                                            BoqScope = b.BoqScope,
                                                            ResDescription = r.ResDescription,
                                                            BoqBillQty = b.BoqBillQty,
                                                            BoqQty = b.BoqQty,
                                                            BoqScopeQty = b.BoqQtyScope,
                                                            AssignedPackage = "",
                                                            ResSeq = r.ResSeq
                                                        });

            if (input.BOQDiv.Length > 0) condQuery = condQuery.Where(w => input.BOQDiv.Contains(w.SectionO));
            if (!string.IsNullOrEmpty(input.BOQItem)) condQuery = condQuery.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
            if (!string.IsNullOrEmpty(input.BOQDesc)) condQuery = condQuery.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
            if (!string.IsNullOrEmpty(input.SheetDesc)) condQuery = condQuery.Where(w => w.ObSheetDesc == input.SheetDesc);
            if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) condQuery = condQuery.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.Scope == input.Package);
            if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));
            if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            if (!string.IsNullOrEmpty(input.RESPackage)) condQuery = condQuery.Where(w => w.BoqPackage == input.RESPackage);
            if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.BoqScope == input.Package);
            if (input.boqLevel2.Length > 0) condQuery = condQuery.Where(w => input.boqLevel2.Contains(w.L2));
            if (input.boqLevel3.Length > 0) condQuery = condQuery.Where(w => input.boqLevel3.Contains(w.L3));
            //if (!string.IsNullOrEmpty(input.boqLevel3)) condQuery = condQuery.Where(w => w.L3.ToLower().Contains(input.boqLevel3.ToLower()));
            if (input.boqLevel4.Length > 0) condQuery = condQuery.Where(w => input.boqLevel4.Contains(w.L4));
            if (!string.IsNullOrEmpty(input.obTradeDesc)) condQuery = condQuery.Where(w => w.ObTradeDesc.ToLower().Contains(input.obTradeDesc.ToLower()));
            if (input.boqResourceSeq.Length > 0) condQuery = condQuery.Where(w => input.boqResourceSeq.Contains(w.ResSeq));

            switch (input.isItemsAssigned)
            {
                case 1:
                    condQuery = condQuery.Where(w => w.Scope >0);
                    break;
                case 2:
                    condQuery = condQuery.Where(w => w.Scope == null || w.Scope == 0);
                    break;
                default:
                    break;
            }

            switch (input.isRessourcesAssigned)
            {
                case 1:            
                    condQuery = condQuery.Where(w => w.BoqScope > 0);
                    break;
                case 2:
                    condQuery = condQuery.Where(w => w.BoqScope == null || w.Scope == 0);
                    break;
                default:
                    break;
            }


            //Update Package Name
            var qry = condQuery.ToList();
            foreach (var x in qry.Where(i=>i.Scope>0))
            {
                 x.AssignedPackage = packList.FirstOrDefault(d => d.PkgeId == x.Scope).PkgeName;
            }


            var resutl = qry
                .GroupBy(x => new { x.RowNumber, x.SectionO, x.ItemO, x.DescriptionO, x.UnitO })
                .Select(p => p.FirstOrDefault())
                .Select(p => new BoqRessourcesList
                {
                    RowNumber = p.RowNumber,
                    SectionO = p.SectionO,
                    ItemO = p.ItemO,
                    DescriptionO = p.DescriptionO,
                    UnitO = p.UnitO,                
                    UnitRate = p.UnitRate,
                    //Scope = p.Scope,
                    AssignedPackage = p.AssignedPackage,
                    BillQtyO = p.BillQtyO,
                    QtyO = p.QtyO,
                    ScopeQtyO = p.ScopeQtyO,
                    ObTradeDesc=p.ObTradeDesc
                }).OrderBy(w => w.RowNumber)
                .ToList();

            int status , oldstatus;
            List<string> stsList = new List<string>();
            foreach (var boq in resutl)
            {
               status = oldstatus= 0;
               stsList.Clear();
               var resList = condQuery.Where(x => x.ItemO == boq.ItemO).ToList();
               foreach(var res in resList)
                {
                    if (res.BoqScope>0)
                    {
                        status = (int) res.BoqScope;
                        stsList.Add("Assigned");
                    }
                    else
                    {
                        stsList.Add("Not Assigned");
                    }
                 
                    if ((oldstatus!=status)  && (oldstatus>0) && (status > 0))
                        stsList.Add("Mix");

                    if (oldstatus != status) oldstatus = status;

                }

                if (stsList.Contains("Assigned") && stsList.Contains("Not Assigned"))
                    boq.BoqStatus = "Missing";
                else if (stsList.Contains("Assigned") && (!stsList.Contains("Mix")))
                    boq.BoqStatus = "Assigned";
                else if (stsList.Contains("Mix"))
                    boq.BoqStatus = "Mix";
            }

            return resutl;
            //return _mapper.Map<List<TblOriginalBoq>, List<OriginalBoqModel>>(results);
        }

        private string GetPackageName(int id)
        {
            var result = _mdbcontext.TblPackages.Where(x => x.PkgeId == id).FirstOrDefault();
            return result.PkgeName;
        }

        public List<BoqModel> GetBoqList(string ItemO, SearchInput input)
        {
            //var results = (from b in _context.TblBoqs
            //               where b.BoqItem == ItemO
            //               join r in _context.TblResources on b.BoqResSeq equals r.ResSeq
            //               select new BoqModel()
            //               {
            //                   BoqSeq = b.BoqSeq,
            //                   BoqCtg = b.BoqCtg,
            //                   BoqUnitMesure = b.BoqUnitMesure,
            //                   BoqQty = b.BoqQty,
            //                   BoqUprice = b.BoqUprice,
            //                   BoqDiv = b.BoqDiv,
            //                   BoqPackage = b.BoqPackage,
            //                   BoqScope = b.BoqScope,
            //                   ResDescription = r.ResDescription
            //               }).ToList();
            //return results;

            var packList = (from b in _mdbcontext.TblPackages
                            select b).ToList();

            IEnumerable<BoqModel> condQuery = (from o in _context.TblOriginalBoqs
                                               join b in _context.TblBoqs on o.ItemO equals b.BoqItem
                                               where b.BoqItem == ItemO
                                               join r in _context.TblResources on b.BoqResSeq equals r.ResSeq
                                               //join p in packList on b.BoqScope equals p.PkgeId into gj
                                               //from pk in gj.DefaultIfEmpty()
                                               select new BoqModel()
                                               {
                                                   RowNumber = o.RowNumber,
                                                   SectionO = o.SectionO,
                                                   ItemO = o.ItemO,
                                                   DescriptionO = o.DescriptionO,
                                                   UnitO = o.UnitO,
                                                   UnitRate = o.UnitRate,
                                                   Scope = o.Scope,
                                                   ObTradeDesc = ((o.ObTradeDesc == null) ? "" : o.ObTradeDesc),
                                                   ObSheetDesc = ((o.ObSheetDesc == null) ? "" : o.ObSheetDesc),
                                                   BoqSeq = b.BoqSeq,
                                                   BoqResSeq = b.BoqResSeq,
                                                   BoqCtg = b.BoqCtg,
                                                   BoqUnitMesure = b.BoqUnitMesure,                     
                                                   BoqUprice = b.BoqUprice,
                                                   BoqDiv = b.BoqDiv,
                                                   BoqPackage = b.BoqPackage,
                                                   BoqScope = b.BoqScope,
                                                   ResDescription = r.ResDescription,
                                                   BoqItem = b.BoqItem,                                                  
                                                   BoqBillQty = b.BoqBillQty,
                                                   BoqQty = b.BoqQty,
                                                   BoqScopeQty = b.BoqQtyScope,
                                                   L2 = ((o.L2 == null) ? "" : o.L2),
                                                   L3 = ((o.L3 == null) ? "" : o.L3),
                                                   L4 = ((o.L4 == null) ? "" : o.L4),
                                                   AssignedPackage = ""
                                               });


            if (input.BOQDiv.Length > 0) condQuery = condQuery.Where(w => input.BOQDiv.Contains(w.SectionO));
            if (!string.IsNullOrEmpty(input.BOQItem)) condQuery = condQuery.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
            if (!string.IsNullOrEmpty(input.BOQDesc)) condQuery = condQuery.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
            if (!string.IsNullOrEmpty(input.SheetDesc)) condQuery = condQuery.Where(w => w.ObSheetDesc == input.SheetDesc);
            if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) condQuery = condQuery.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.Scope == input.Package);
            if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));
            if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            if (!string.IsNullOrEmpty(input.RESPackage)) condQuery = condQuery.Where(w => w.BoqPackage == input.RESPackage);
            if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.BoqScope == input.Package);
            if (input.boqLevel2.Length > 0) condQuery = condQuery.Where(w => input.boqLevel2.Contains(w.L2));
            if (input.boqLevel3.Length > 0) condQuery = condQuery.Where(w => input.boqLevel3.Contains(w.L3));
            //if (!string.IsNullOrEmpty(input.boqLevel3)) condQuery = condQuery.Where(w => w.L3.ToLower().Contains(input.boqLevel3.ToLower()));
            if (input.boqLevel4.Length > 0) condQuery = condQuery.Where(w => input.boqLevel4.Contains(w.L4));
            if (!string.IsNullOrEmpty(input.obTradeDesc)) condQuery = condQuery.Where(w => w.ObTradeDesc.ToLower().Contains(input.obTradeDesc.ToLower()));
            if (input.boqResourceSeq.Length > 0) condQuery = condQuery.Where(w => input.boqResourceSeq.Contains(w.BoqResSeq));

            switch (input.isRessourcesAssigned)
            {
                case 1:
                    condQuery = condQuery.Where(w => w.BoqScope > 0);
                    break;
                case 2:
                    condQuery = condQuery.Where(w => w.BoqScope == null || w.Scope == 0);
                    break;
                default:
                    break;
            }

            //Update Package Name
            var results = condQuery.ToList();
            foreach (var x in results.Where(i => i.BoqScope > 0))
            {
                    x.AssignedPackage = packList.FirstOrDefault(d => d.PkgeId == x.BoqScope).PkgeName;
            }

            return results.OrderBy(x=>x.BoqCtg).ToList();
        }

        public List<BoqModel> GetAllBoqList(SearchInput input)
        {
            var packList = (from b in _mdbcontext.TblPackages
                            select b).ToList();

            IEnumerable<BoqModel> condQuery = (from o in _context.TblOriginalBoqs
                                               join b in _context.TblBoqs on o.ItemO equals b.BoqItem
                                               join r in _context.TblResources on b.BoqResSeq equals r.ResSeq
                                               //join p in packList on b.BoqScope equals p.PkgeId into gj
                                               //from pk in gj.DefaultIfEmpty()
                                               select new BoqModel()
                                               {
                                                   RowNumber = o.RowNumber,
                                                   SectionO = o.SectionO,
                                                   ItemO = o.ItemO,
                                                   DescriptionO = o.DescriptionO,
                                                   UnitO = o.UnitO,
                                                   UnitRate = o.UnitRate,
                                                   Scope = o.Scope,
                                                   ObTradeDesc = ((o.ObTradeDesc == null) ? "" : o.ObTradeDesc),
                                                   ObSheetDesc = ((o.ObSheetDesc == null) ? "" : o.ObSheetDesc),
                                                   BoqSeq = b.BoqSeq,
                                                   BoqResSeq = b.BoqResSeq,
                                                   BoqItem = b.BoqItem,
                                                   BoqCtg = b.BoqCtg,
                                                   BoqUnitMesure = b.BoqUnitMesure,                                               
                                                   BoqUprice = b.BoqUprice,
                                                   BoqDiv = b.BoqDiv,
                                                   BoqPackage = b.BoqPackage,
                                                   BoqScope = b.BoqScope,
                                                   ResDescription = r.ResDescription,
                                                   BoqQty = b.BoqQty,
                                                   BoqBillQty = b.BoqBillQty,
                                                   BoqScopeQty=b.BoqQtyScope,
                                                   L2 = ((o.L2 == null) ? "" : o.L2),
                                                   L3 = ((o.L3 == null) ? "" : o.L3),
                                                   L4 = ((o.L4 == null) ? "" : o.L4),
                                                   AssignedPackage = ""
                                               });

            if (input.BOQDiv.Length > 0) condQuery = condQuery.Where(w => input.BOQDiv.Contains(w.SectionO));
            if (!string.IsNullOrEmpty(input.BOQItem)) condQuery = condQuery.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
            if (!string.IsNullOrEmpty(input.BOQDesc)) condQuery = condQuery.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
            if (!string.IsNullOrEmpty(input.SheetDesc)) condQuery = condQuery.Where(w => w.ObSheetDesc == input.SheetDesc);
            if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) condQuery = condQuery.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.Scope == input.Package);
            if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));
            if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            if (!string.IsNullOrEmpty(input.RESPackage)) condQuery = condQuery.Where(w => w.BoqPackage == input.RESPackage);
            if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));
            if (input.Package > 0) condQuery = condQuery.Where(w => w.BoqScope == input.Package);
            if (input.boqLevel2.Length > 0) condQuery = condQuery.Where(w => input.boqLevel2.Contains(w.L2));
            if (input.boqLevel3.Length > 0) condQuery = condQuery.Where(w => input.boqLevel3.Contains(w.L3));
            //if (!string.IsNullOrEmpty(input.boqLevel3)) condQuery = condQuery.Where(w => w.L3.ToLower().Contains(input.boqLevel3.ToLower()));
            if (input.boqLevel4.Length > 0) condQuery = condQuery.Where(w => input.boqLevel4.Contains(w.L4));
            if (!string.IsNullOrEmpty(input.obTradeDesc)) condQuery = condQuery.Where(w => w.ObTradeDesc.ToLower().Contains(input.obTradeDesc.ToLower()));
            if (input.boqResourceSeq.Length > 0) condQuery = condQuery.Where(w => input.boqResourceSeq.Contains(w.BoqResSeq));

            //Update Package Name
            var results = condQuery.ToList();
            foreach (var x in results.Where(i => i.BoqScope > 0))
            {
                    x.AssignedPackage = packList.FirstOrDefault(d => d.PkgeId == x.BoqScope).PkgeName;
            }
            return results;
        }

        public PackageDetailsModel GetPackageById(int IdPkge)
        {
            var query = from b in _mdbcontext.TblPackages
                        where b.PkgeId == IdPkge
                        select new PackageDetailsModel
                        {
                            PackageName = b.PkgeName,
                            FilePath = b.FilePath
                        };
            return query.FirstOrDefault();
        }

        public bool AssignPackages(AssignPackages input)
        {
            if (input.AssignOriginalBoqList != null)
            {
                //foreach (var item in input.AssignOriginalBoqList)
                //{
                //    var data = _context.TblOriginalBoqs.Where(x => x.RowNumber == item.RowNumber).FirstOrDefault();
                //    data.Scope = item.Scope;

                //    _context.TblOriginalBoqs.Update(data);               
                //}
                //_context.SaveChanges();

                var lstBoqo = (from a in input.AssignOriginalBoqList
                               join b in _context.TblOriginalBoqs on a.RowNumber equals b.RowNumber
                               select b).ToList();

                foreach (var item in input.AssignOriginalBoqList)
                {
                    lstBoqo.Where(d => d.RowNumber == item.RowNumber).First().Scope = item.Scope;
                }
                _context.TblOriginalBoqs.UpdateRange(lstBoqo);
                _context.SaveChanges();
            }

            if (input.AssignBoqList != null)
            {
                //foreach (var item in input.AssignBoqList)
                //{
                //    var data = _context.TblBoqs.Where(x => x.BoqSeq == item.BoqSeq).FirstOrDefault();
                //    data.BoqScope = item.BoqScope;

                //    _context.TblBoqs.Update(data);
                //}
                //_context.SaveChanges();

                var lstBoq = (from a in input.AssignBoqList
                              join b in _context.TblBoqs on a.BoqSeq equals b.BoqSeq
                              select b).ToList();

                foreach (var item in input.AssignBoqList)
                {
                    lstBoq.Where(d => d.BoqSeq == item.BoqSeq).First().BoqScope = item.BoqScope;
                }
                _context.TblBoqs.UpdateRange(lstBoq);
                _context.SaveChanges();
            }
            return true;
        }
        public List<PackageSuppliersPrice> GetPackageSuppliersPrice(int pckgID, SearchInput input)
        {
            //get Exchange Rate Now
            var curList = (from b in _mdbcontext.TblCurrencies
                           select b).ToList();

            var usedCur = from cur in curList
                          join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                          join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                          where (a.SpPackageId == pckgID && b.PrRevNo == 0)
                          group cur by cur.CurCode into g
                          select new LiveExchange
                          {
                              fromCurrency = g.Key
                          };

            var ExchNowList = (from cur in usedCur
                               select new LiveExchange
                               {
                                   fromCurrency = cur.fromCurrency,
                                   ExchRateNow = GetExchange(cur.fromCurrency)
                               }).ToList();

            List<PackageSuppliersPrice> result = new List<PackageSuppliersPrice>();
            List<RevisionDetails> revisionDetails = new List<RevisionDetails>();
            List<FieldList> fieldLists = new List<FieldList>();

            var supList = (from b in _mdbcontext.TblSuppliers
                           select b).ToList();

            var query = (from sup in supList
                         join b in _context.TblSupplierPackages on sup.SupCode equals b.SpSupplierId
                         join rev in _context.TblSupplierPackageRevisions on b.SpPackSuppId equals rev.PrPackSuppId
                         where (b.SpPackageId == pckgID && rev.PrRevNo == 0)
                         select new PackageSuppliersPrice
                         {
                             SupplierId = b.SpSupplierId,
                             SupplierName = sup.SupName,
                             LastRevisionDate = rev.PrRevDate,
                             ByBoq = (byte)((b.SpByBoq == null) ? 0 : b.SpByBoq)
                         }).ToList();

            if (query.Count > 0)
            {
                byte byboq = query.FirstOrDefault().ByBoq;

                query.Add(new PackageSuppliersPrice() { SupplierId = 0, SupplierName = "Ideal", LastRevisionDate = null, ByBoq = byboq });

                if (query.Count > 0)
                {
                    foreach (var item in query)
                    {
                        PackageSuppliersPrice packageSuppliersPrice = new PackageSuppliersPrice();

                        packageSuppliersPrice.SupplierId = item.SupplierId;
                        packageSuppliersPrice.SupplierName = item.SupplierName;
                        packageSuppliersPrice.ByBoq = item.ByBoq;
                        packageSuppliersPrice.LastRevisionDate = item.LastRevisionDate;
                        byboq = item.ByBoq;
                        IEnumerable<RevisionDetails> revDtlQry;
                        IEnumerable<RevisionDetails> revDtlQryIdeal;

                        if (item.SupplierName == "Ideal")
                        {
                            if (byboq == 1)
                            {
                                revDtlQry = (from cur in curList
                                             join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                             join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                             join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                             join o in _context.TblOriginalBoqs on c.RdBoqItem equals o.ItemO
                                             join sup in supList on a.SpSupplierId equals sup.SupCode
                                             where (a.SpPackageId == pckgID && b.PrRevNo == 0)
                                             select new RevisionDetails
                                             {
                                                 ItemO = o.ItemO,
                                                 DescriptionO = o.DescriptionO,
                                                 UnitO = o.UnitO,
                                                 QtyO = c.RdQty,
                                                 price = c.RdPrice,
                                                 perc = c.RdAssignedPerc,
                                                 missedPrice = c.RdMissedPrice,
                                                 priceOrigCur = c.RdPriceOrigCurrency,
                                                 Scope = o.Scope,
                                                 BoqDiv = o.SectionO,
                                                 ObSheetDesc = o.ObSheetDesc,
                                                 RowNumber = o.RowNumber,
                                                 AssignedToSupplier = ((c.RdAssignedQty == null || c.RdAssignedQty == 0)) ? false : true,
                                                 OriginalCurrency = cur.CurCode,
                                                 AssignedQty = c.RdAssignedQty,
                                                 Discount = c.RdDiscount,
                                                 UPriceAfterDiscount = Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2)
                                             });

                                if (input.BOQDiv.Length > 0) revDtlQry = revDtlQry.Where(w => input.BOQDiv.Contains(w.BoqDiv));
                                if (!string.IsNullOrEmpty(input.BOQItem)) revDtlQry = revDtlQry.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
                                if (!string.IsNullOrEmpty(input.BOQDesc)) revDtlQry = revDtlQry.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
                                if (!string.IsNullOrEmpty(input.SheetDesc)) revDtlQry = revDtlQry.Where(w => w.ObSheetDesc == input.SheetDesc);
                                if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) revDtlQry = revDtlQry.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
                                if (input.Package > 0) revDtlQry = revDtlQry.Where(w => w.Scope == input.Package);                                if (input.RESDiv.Length > 0) revDtlQry = revDtlQry.Where(w => input.RESDiv.Contains(w.ResDiv));
                                if (input.RESType.Length > 0) revDtlQry = revDtlQry.Where(w => input.RESType.Contains(w.ResCtg));
                                if (!string.IsNullOrEmpty(input.RESPackage)) revDtlQry = revDtlQry.Where(w => w.BoqPackage == input.RESPackage);
                                if (!string.IsNullOrEmpty(input.RESDesc)) revDtlQry = revDtlQry.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));


                                revDtlQryIdeal = revDtlQry
                                    .GroupBy(x => new { x.ItemO })
                                    .Select(p => new RevisionDetails
                                    {
                                        ItemO = p.First().ItemO,
                                        DescriptionO = p.First().DescriptionO,
                                        UnitO = p.First().UnitO,
                                        QtyO = p.First().QtyO,
                                        priceOrigCur = p.Min(c => c.priceOrigCur),
                                        AssignedQty = p.First().AssignedQty,
                                        OriginalCurrency = p.First().OriginalCurrency,
                                        UPriceAfterDiscount = p.Min(c => c.UPriceAfterDiscount)
                                    }).ToList();
                            }
                            else
                            {
                                revDtlQry = (from cur in curList
                                             join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                             join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                             join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                             join d in _context.TblBoqs on c.RdResourceSeq equals d.BoqSeq
                                             join e in _context.TblResources on d.BoqResSeq equals e.ResSeq
                                             join o in _context.TblOriginalBoqs on d.BoqItem equals o.ItemO
                                             join sup in supList on a.SpSupplierId equals sup.SupCode
                                             where (a.SpPackageId == pckgID && b.PrRevNo == 0 && a.SpSupplierId == item.SupplierId)

                                             select new RevisionDetails
                                             {
                                                 resourceID = c.RdResourceSeq,
                                                 ResDescription = e.ResDescription,
                                                 resourceUnit = d.BoqUnitMesure,
                                                 resourceQty = c.RdQty,
                                                 price = c.RdPrice,
                                                 perc = c.RdAssignedPerc,
                                                 missedPrice = c.RdMissedPrice,
                                                 priceOrigCur = c.RdPriceOrigCurrency,
                                                 ItemO = o.ItemO,
                                                 DescriptionO = o.DescriptionO,
                                                 SectionO = o.SectionO,
                                                 Scope = o.Scope,
                                                 BoqDiv = o.SectionO,
                                                 ObSheetDesc = o.ObSheetDesc,
                                                 RowNumber = o.RowNumber,
                                                 BoqPackage = d.BoqPackage,
                                                 BoqScope = d.BoqScope,
                                                 ResDiv = d.BoqDiv,
                                                 ResCtg = d.BoqCtg,
                                                 AssignedToSupplier = ((c.RdAssignedQty == null || c.RdAssignedQty == 0)) ? false : true,
                                                 OriginalCurrency = cur.CurCode,
                                                 AssignedQty = c.RdAssignedQty,
                                                 Discount = c.RdDiscount,
                                                 UPriceAfterDiscount = Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2)
                                             });

                                if (input.BOQDiv.Length > 0) revDtlQry = revDtlQry.Where(w => input.BOQDiv.Contains(w.BoqDiv));
                                if (!string.IsNullOrEmpty(input.BOQItem)) revDtlQry = revDtlQry.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
                                if (!string.IsNullOrEmpty(input.BOQDesc)) revDtlQry = revDtlQry.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
                                if (!string.IsNullOrEmpty(input.SheetDesc)) revDtlQry = revDtlQry.Where(w => w.ObSheetDesc == input.SheetDesc);
                                if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) revDtlQry = revDtlQry.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
                                if (input.Package > 0) revDtlQry = revDtlQry.Where(w => w.Scope == input.Package);
                                if (input.RESDiv.Length > 0) revDtlQry = revDtlQry.Where(w => input.RESDiv.Contains(w.ResDiv));
                                if (input.RESType.Length > 0) revDtlQry = revDtlQry.Where(w => input.RESType.Contains(w.BoqCtg));
                                if (!string.IsNullOrEmpty(input.RESPackage)) revDtlQry = revDtlQry.Where(w => w.BoqPackage == input.RESPackage);
                                if (!string.IsNullOrEmpty(input.RESDesc)) revDtlQry = revDtlQry.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));

                                revDtlQryIdeal = revDtlQry
                                .GroupBy(x => new { x.ItemO })
                                .Select(p => new RevisionDetails
                                {
                                    ItemO = p.First().ItemO,
                                    DescriptionO = p.First().DescriptionO,
                                    UnitO = p.First().UnitO,
                                    QtyO = p.First().QtyO,
                                    priceOrigCur = p.Min(c => c.priceOrigCur),
                                    AssignedQty = p.First().AssignedQty,
                                    OriginalCurrency = p.First().OriginalCurrency,
                                    UPriceAfterDiscount = p.Min(c => c.UPriceAfterDiscount)
                                }).ToList();

                            }

                            packageSuppliersPrice.revisionDetails = revDtlQryIdeal.ToList();

                            List<FieldList> f = new List<FieldList>();
                            f = null;
                            packageSuppliersPrice.fieldLists = f;
                        }

                        else
                        {
                            if (byboq == 1)
                            {
                                revDtlQry = (from cur in curList
                                             join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                             join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                             join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                             join o in _context.TblOriginalBoqs on c.RdBoqItem equals o.ItemO
                                             join sup in supList on a.SpSupplierId equals sup.SupCode
                                             where (a.SpPackageId == pckgID && b.PrRevNo == 0 && a.SpSupplierId == item.SupplierId)

                                             select new RevisionDetails
                                             {
                                                 ItemO = o.ItemO,
                                                 DescriptionO = o.DescriptionO,
                                                 UnitO = o.UnitO,
                                                 QtyO = c.RdQty,
                                                 price = c.RdPrice,
                                                 perc = c.RdAssignedPerc,
                                                 missedPrice = c.RdMissedPrice,
                                                 priceOrigCur = c.RdPriceOrigCurrency,
                                                 Scope = o.Scope,
                                                 BoqDiv = o.SectionO,
                                                 ObSheetDesc = o.ObSheetDesc,
                                                 RowNumber = o.RowNumber,
                                                 AssignedToSupplier = ((c.RdAssignedQty == null || c.RdAssignedQty == 0)) ? false : true,
                                                 OriginalCurrency = cur.CurCode,
                                                 AssignedQty = c.RdAssignedQty,
                                                 Discount = c.RdDiscount,
                                                 UPriceAfterDiscount = Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2),
                                                 
                                             });

                                if (input.BOQDiv.Length > 0) revDtlQry = revDtlQry.Where(w => input.BOQDiv.Contains(w.BoqDiv));
                                if (!string.IsNullOrEmpty(input.BOQItem)) revDtlQry = revDtlQry.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
                                if (!string.IsNullOrEmpty(input.BOQDesc)) revDtlQry = revDtlQry.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
                                if (!string.IsNullOrEmpty(input.SheetDesc)) revDtlQry = revDtlQry.Where(w => w.ObSheetDesc == input.SheetDesc);
                                if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) revDtlQry = revDtlQry.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
                                if (input.Package > 0) revDtlQry = revDtlQry.Where(w => w.Scope == input.Package);
                                if (input.RESDiv.Length > 0) revDtlQry = revDtlQry.Where(w => input.RESDiv.Contains(w.ResDiv));
                                if (input.RESType.Length > 0) revDtlQry = revDtlQry.Where(w => input.RESType.Contains(w.ResCtg));
                                if (!string.IsNullOrEmpty(input.RESPackage)) revDtlQry = revDtlQry.Where(w => w.BoqPackage == input.RESPackage);
                                if (!string.IsNullOrEmpty(input.RESDesc)) revDtlQry = revDtlQry.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));
                            }
                            else
                            {
                                revDtlQry = (from cur in curList
                                             join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                             join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                             join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                             join d in _context.TblBoqs on c.RdResourceSeq equals d.BoqSeq
                                             join e in _context.TblResources on d.BoqResSeq equals e.ResSeq
                                             join o in _context.TblOriginalBoqs on d.BoqItem equals o.ItemO
                                             join sup in supList on a.SpSupplierId equals sup.SupCode
                                             where (a.SpPackageId == pckgID && b.PrRevNo == 0 && a.SpSupplierId == item.SupplierId)

                                             select new RevisionDetails
                                             {
                                                 resourceID = c.RdResourceSeq,
                                                 ResDescription = e.ResDescription,
                                                 resourceUnit = d.BoqUnitMesure,
                                                 resourceQty = c.RdQty,
                                                 price = c.RdPrice,
                                                 perc = c.RdAssignedPerc,
                                                 missedPrice = c.RdMissedPrice,
                                                 priceOrigCur = c.RdPriceOrigCurrency,
                                                 ItemO = o.ItemO,
                                                 DescriptionO = o.DescriptionO,
                                                 SectionO = o.SectionO,
                                                 Scope = o.Scope,
                                                 BoqDiv = o.SectionO,
                                                 ObSheetDesc = o.ObSheetDesc,
                                                 RowNumber = o.RowNumber,
                                                 BoqPackage = d.BoqPackage,
                                                 BoqScope = d.BoqScope,
                                                 ResDiv = d.BoqDiv,
                                                 ResCtg = d.BoqCtg,
                                                 AssignedToSupplier = ((c.RdAssignedQty == null || c.RdAssignedQty == 0)) ? false : true,
                                                 OriginalCurrency = cur.CurCode,
                                                 AssignedQty = c.RdAssignedQty,
                                                 Discount = c.RdDiscount,
                                                 UPriceAfterDiscount = Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2)
                                             });

                                if (input.BOQDiv.Length > 0) revDtlQry = revDtlQry.Where(w => input.BOQDiv.Contains(w.BoqDiv));
                                if (!string.IsNullOrEmpty(input.BOQItem)) revDtlQry = revDtlQry.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
                                if (!string.IsNullOrEmpty(input.BOQDesc)) revDtlQry = revDtlQry.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
                                if (!string.IsNullOrEmpty(input.SheetDesc)) revDtlQry = revDtlQry.Where(w => w.ObSheetDesc == input.SheetDesc);
                                if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) revDtlQry = revDtlQry.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
                                if (input.Package > 0) revDtlQry = revDtlQry.Where(w => w.Scope == input.Package);
                                if (input.RESDiv.Length > 0) revDtlQry = revDtlQry.Where(w => input.RESDiv.Contains(w.ResDiv));
                                if (input.RESType.Length > 0) revDtlQry = revDtlQry.Where(w => input.RESType.Contains(w.BoqCtg));
                                if (!string.IsNullOrEmpty(input.RESPackage)) revDtlQry = revDtlQry.Where(w => w.BoqPackage == input.RESPackage);
                                if (!string.IsNullOrEmpty(input.RESDesc)) revDtlQry = revDtlQry.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));
                            }

                            fieldLists = (from cur in curList
                                          join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                          join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                          join c in _context.TblRevisionFields on b.PrRevId equals c.RevisionId
                                          where (a.SpPackageId == pckgID && b.PrRevNo == 0 && a.SpSupplierId == item.SupplierId)

                                          select new FieldList
                                          {
                                              Label = c.Label,
                                              Value = c.Type == 1 ? (double)(c.Value * ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow) : c.Value,
                                              Type = c.Type,
                                              OriginalCurrency = cur.CurCode
                                          }).ToList();

                            packageSuppliersPrice.revisionDetails = revDtlQry.ToList();
                            packageSuppliersPrice.fieldLists = fieldLists;

                        }

                        if (packageSuppliersPrice.revisionDetails.Count > 0)
                        {
                            foreach (var itemRevision in packageSuppliersPrice.revisionDetails)
                            {
                                if (packageSuppliersPrice.SupplierName == "Ideal")
                                    packageSuppliersPrice.totalprice += Convert.ToDecimal(itemRevision.QtyO) * Convert.ToDecimal(itemRevision.UPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == itemRevision.OriginalCurrency).ExchRateNow);
                                else
                                    packageSuppliersPrice.totalprice += Convert.ToDecimal(itemRevision.QtyO) * Convert.ToDecimal(itemRevision.UPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == itemRevision.OriginalCurrency).ExchRateNow);
                                //packageSuppliersPrice.totalprice += Convert.ToDecimal(itemRevision.AssignedQty) * Convert.ToDecimal(itemRevision.priceOrigCur) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == itemRevision.OriginalCurrency).ExchRateNow);
                            }
                        }

                        if (packageSuppliersPrice.fieldLists != null)
                        {
                            if (packageSuppliersPrice.fieldLists.Count > 0)
                            {
                                foreach (var itemFields in packageSuppliersPrice.fieldLists)
                                {
                                    if (itemFields.Type == 1)
                                        packageSuppliersPrice.totalAdditionalPrice += (decimal)itemFields.Value;
                                    else
                                        packageSuppliersPrice.totalAdditionalPrice += packageSuppliersPrice.totalprice * ((decimal)itemFields.Value / 100m);
                                }
                            }
                            else
                                packageSuppliersPrice.totalAdditionalPrice = 0;
                        }
                        else
                            packageSuppliersPrice.totalAdditionalPrice = 0;

                        packageSuppliersPrice.totalNetPrice = packageSuppliersPrice.totalprice + packageSuppliersPrice.totalAdditionalPrice;
                        result.Add(packageSuppliersPrice);
                    }
                }
            }
            return result;
            //return result.OrderBy(x => x.SupplierName).ToList();
        }
        private double GetExchange(string foreignCurrency)
        {
            var result = from a in _context.TblParameters
                         join b in _context.TblCurrencies
                         on a.EstimatedCur equals b.CurId
                         select new ProjectCurrency
                         {
                             curId = (int)a.EstimatedCur,
                             curCode = b.CudCode
                         };


            string localCurrency = result.FirstOrDefault().curCode;

            CurrencyConverterRepository currencyConverterRepository = new CurrencyConverterRepository();
            return currencyConverterRepository.GetCurrencyExchange(localCurrency, foreignCurrency);
        }
        public string ExportBoqExcel(AssignPackages input)
        {
            //if (input.AssignOriginalBoqList != null)
            //{

            //    var lstBoqo = (from a in input.AssignOriginalBoqList
            //                   join b in _context.TblOriginalBoqs on a.RowNumber equals b.RowNumber
            //                   select b).ToList();

            //    foreach (var item in input.AssignOriginalBoqList)
            //    {
            //        lstBoqo.Where(d => d.RowNumber == item.RowNumber).First().Scope = item.Scope;
            //    }
            //    _context.TblOriginalBoqs.UpdateRange(lstBoqo);
            //    _context.SaveChanges();
            //}

            string excelName = "";

            if (input.AssignBoqList != null)
            {

                var lstBoq = (from a in input.AssignBoqList
                              join b in _context.TblBoqs on a.BoqSeq equals b.BoqSeq
                              join r in _context.TblResources on b.BoqResSeq equals r.ResSeq
                              select new BoqModel()
                              {
                                  BoqSeq = b.BoqSeq,
                                  BoqResSeq = b.BoqResSeq,
                                  BoqCtg = b.BoqCtg,
                                  BoqUnitMesure = b.BoqUnitMesure,
                                  BoqQty = b.BoqQty,
                                  BoqUprice = b.BoqUprice,
                                  BoqDiv = b.BoqDiv,
                                  BoqPackage = b.BoqPackage,
                                  BoqScope = b.BoqScope,
                                  ResDescription = r.ResDescription,
                                  BoqItem = b.BoqItem,
                                  BoqTotalPrice = b.BoqUprice * b.BoqQty
                              }).ToList();

                //foreach (var item in input.AssignBoqList)
                //{
                //    lstBoq.Where(d => d.BoqSeq == item.BoqSeq).First().BoqScope = item.BoqScope;
                //}

                var stream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var xlPackage = new ExcelPackage(stream))
                {
                    var worksheet = xlPackage.Workbook.Worksheets.Add("BOQ");
                    worksheet.Columns.AutoFit();
                    worksheet.Protection.IsProtected = false;

                    int i, j;

                    i = 1;
                    worksheet.Cells[i, 1].Value = "Item";
                    worksheet.Column(1).Width = 20;
                    worksheet.Cells[i, 2].Value = "Description";
                    worksheet.Column(2).Width = 50;
                    worksheet.Columns[2].Style.WrapText = true;
                    worksheet.Columns[2].Style.WrapText = true;
                    worksheet.Column(2).AutoFit();
                    worksheet.Cells[i, 3].Value = "Unit";
                    worksheet.Cells[i, 4].Value = "Type";
                    worksheet.Cells[i, 5].Value = "Qty";
                    worksheet.Cells[i, 6].Value = "Unit Price";
                    worksheet.Cells[i, 7].Value = "Total Price";

                    worksheet.Row(i).Style.Font.Bold = true;

                    i = 4;
                    foreach (var x in lstBoq)
                    {
                        worksheet.Cells[i, 1].Value = (x.BoqItem == null) ? "" : x.BoqItem;
                        worksheet.Cells[i, 2].Value = (x.ResDescription == null) ? "" : x.ResDescription;
                        worksheet.Cells[i, 3].Value = (x.BoqUnitMesure == null) ? "" : x.BoqUnitMesure;
                        worksheet.Cells[i, 4].Value = (x.BoqCtg == null) ? "" : x.BoqCtg;
                        worksheet.Cells[i, 5].Value = (x.BoqQty == null) ? "" : x.BoqQty;
                        worksheet.Cells[i, 6].Value = (x.BoqUprice == null) ? "" : x.BoqUprice;
                        worksheet.Cells[i, 7].Value = (x.BoqTotalPrice == null) ? "" : x.BoqTotalPrice;
                        i++;
                    }

                    xlPackage.Save();
                    stream.Position = 0;
                    excelName = $"BOQ_Ressources.xlsx";

                    if (File.Exists(excelName))
                        File.Delete(excelName);

                    xlPackage.SaveAs(excelName);
                }
            }
            return excelName;
        }
        public bool updateOriginalBoqQty(OriginalBoqModel boq)
        {
            var result = _context.TblOriginalBoqs.Where(x => x.ItemO == boq.ItemO).FirstOrDefault();
            result.QtyScope = boq.ScopeQtyO;
            
            if (result != null)
            {
                _context.TblOriginalBoqs.Update(result);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool updateBoqResQty(BoqModel res)
        {
            var result = _context.TblBoqs.Where(x => x.BoqSeq == res.BoqSeq).FirstOrDefault();
            result.BoqQtyScope = res.BoqScopeQty;

            if (result != null)
            {
                _context.TblBoqs.Update(result);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool updateBoqTradeDesc(string tradeDesc,List<OriginalBoqModel> origBoqList)
        {
            if (origBoqList != null)
            {
                //foreach (var item in input.AssignOriginalBoqList)
                //{
                //    var data = _context.TblOriginalBoqs.Where(x => x.RowNumber == item.RowNumber).FirstOrDefault();
                //    data.Scope = item.Scope;

                //    _context.TblOriginalBoqs.Update(data);               
                //}
                //_context.SaveChanges();

                var lstBoqo = (from a in origBoqList
                               join b in _context.TblOriginalBoqs on a.RowNumber equals b.RowNumber
                               select b).ToList();

                foreach (var item in origBoqList)
                {
                    lstBoqo.Where(d => d.RowNumber == item.RowNumber).First().ObTradeDesc = tradeDesc;
                }
                _context.TblOriginalBoqs.UpdateRange(lstBoqo);
                _context.SaveChanges();
            }
            return true;
        }

        public string ExportExcelPackagesCost()
        {
            string excelName = "";

            var packList = (from p in _mdbcontext.TblPackages
                            select new packagesList
                            {
                                PkgeId = (int)p.PkgeId,
                                PkgeName = p.PkgeName
                            }).ToList();

            //var pckgesCost = _context.TblBoqs.Where(x=>x.BoqScope>0)
            //    .GroupBy(x => new { x.BoqScope})
            //    .Select(p => new packagesList
            //    {
            //        PkgeId = p.First().GroupId.HasValue ? p.First().GroupId.Value : 0,
            //        TotalBudget = p.Sum(c => c.BoqQty * c.BoqUprice)
            //    }).ToList();


            var pckgesCost = from e in _context.TblBoqs.Where(x => x.BoqScope > 0)
                         group e by e.BoqScope into g
            select new packagesList
            {
                PkgeId = g.Key,
                TotalBudget = g.Sum(x => x.BoqQty * x.BoqUprice)
            };

            var packgesCost = (from a in packList
                               join b in pckgesCost on a.PkgeId equals b.PkgeId
                               select new packagesList
                               {
                                   PkgeId = (int)a.PkgeId,
                                   PkgeName = a.PkgeName,
                                   TotalBudget = b.TotalBudget
                               }).ToList();


            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Packages");
                worksheet.Columns.AutoFit();
                worksheet.Protection.IsProtected = false;

                int i, j;

                i = 1;
                worksheet.Cells[i, 1].Value = "Assigned Package";
                worksheet.Column(1).Width = 100;
                worksheet.Columns[1].Style.WrapText = true;
                worksheet.Cells[1, 1].Style.Font.Bold = true;
                worksheet.Cells[1, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[i, 2].Value = "Estimated Dry cost";
                worksheet.Cells[1,2].Style.Font.Bold = true;
                worksheet.Column(2).Width = 20;      
                worksheet.Columns[2].Style.WrapText = true;

                i = 2;
                foreach (var x in packgesCost)
                {
                    worksheet.Cells[i, 1].Value = (x.PkgeName == null) ? "" : x.PkgeName;
                    worksheet.Cells[i, 2].Value = (x.TotalBudget == null) ? 0 : x.TotalBudget;
                    worksheet.Cells[i,2].Style.Numberformat.Format = "#,##0.0";
                    i++;
                }

                xlPackage.Save();
                stream.Position = 0;
                excelName = $"Packages Dry Cost.xlsx";

                if (File.Exists(excelName))
                    File.Delete(excelName);

                xlPackage.SaveAs(excelName);
            }

            return excelName;
        }

        #region Packages
        public List<Package> GetPackages(string filter)
        {
            var result = (from b in _mdbcontext.TblPackages
                          orderby b.PkgeName
                          select new Package
                          {
                              IDPkge = b.PkgeId,
                              PkgeName = b.PkgeName,
                              Division = b.Division
                          }).ToList();

            //return result.ToList();

            if (filter != null)
            {
                result = result.Where(x => string.Concat(x.PkgeName.ToUpper()).Contains(filter.ToUpper())).ToList();
            }
            return result.ToList();
        }


        public bool AddPackage(List<Package> packs)
        {
            foreach (var item in packs)
            {
                var result = new Models.MasterModels.TblPackage { PkgeName = item.PkgeName, Division = item.Division };
                _mdbcontext.Add<Models.MasterModels.TblPackage>(result);
                _mdbcontext.SaveChanges();
            }

            return true;
        }

        public bool UpdatePackage(Package pack)
        {
            var result = _mdbcontext.TblPackages.Where(x => x.PkgeId == pack.IDPkge).FirstOrDefault();
            result.PkgeName = pack.PkgeName;
            result.Division = pack.Division;

            if (result != null)
            {
                _mdbcontext.TblPackages.Update(result);
                _mdbcontext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool DeletePackage(int id)
        {
            var pack = _context.TblSupplierPackages.Where(x => x.SpPackageId == id).FirstOrDefault();
            if (pack == null)
            {
                var result = _mdbcontext.TblPackages.Where(x => x.PkgeId == id).FirstOrDefault();
                if (result != null)
                {
                    _mdbcontext.TblPackages.Remove(result);
                    _mdbcontext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
        }
        #endregion

    }
}
