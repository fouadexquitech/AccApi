using AccApi.Repository.Interfaces;
using AccApi.Repository.Models.MasterModels;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Common;
using AccApi.Repository.View_Models.Request;
using AccApi.Data_Layer;
using AutoMapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using File = System.IO.File;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;

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

        public async Task<List<BoqRessourcesList>> GetBoqWithRessourcesAsync(SearchInput input, string costDB,int type)
        {
            bool blankInput = true;
            if (input.BOQDiv.Length > 0) blankInput = false;
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

            if (blankInput && type!=3 && type != 5)
            {
                //List<BoqRessourcesList> res = new List<BoqRessourcesList>();
                return null;
            }

            //Div_List
            var dtDiv = new DataTable();
            dtDiv.Columns.Add("Div", typeof(string));
            foreach (var val in input.BOQDiv)
            dtDiv.Rows.Add(val == null ? "" : val.ToString());
            //DivRes_List
            var dtDivRes = new DataTable();
            dtDivRes.Columns.Add("DivRes", typeof(string));
            foreach (var val in input.RESDiv)
            dtDivRes.Rows.Add(val == null ? "" : val.ToString());
            //L2_List
            var dtL2 = new DataTable();
            dtL2.Columns.Add("L2", typeof(string));
            foreach (var val in input.boqLevel2)
            dtL2.Rows.Add( val==null ? "" : val.ToString());
            //L3_List
            var dtL3 = new DataTable();
            dtL3.Columns.Add("L3", typeof(string));
            foreach (var val in input.boqLevel3)
            dtL3.Rows.Add(val == null ? "" : val.ToString());
            //L4_List
            var dtL4 = new DataTable();
            dtL4.Columns.Add("L4", typeof(string));
            foreach (var val in input.boqLevel4)
            dtL4.Rows.Add(val == null ? "" : val.ToString());
            //Resources_List
            var dtRes = new DataTable();
            dtRes.Columns.Add("Resources", typeof(string));
            foreach (var val in input.boqResourceSeq)
            dtRes.Rows.Add(val == null ? "" : val.ToString());
            //ResType_List
            var dtResType = new DataTable();
            dtResType.Columns.Add("ResType", typeof(string));
            foreach (var val in input.RESType)
            dtResType.Rows.Add(val == null ? "" : val.ToString());

            var p0= new SqlParameter("@Type", type);
            var p1 = new SqlParameter("@DB", costDB);
            var p2 = new SqlParameter("@BOQDivList", SqlDbType.Structured); 
            p2.TypeName = "[dbo].[Div_List]"; p2.SqlValue = dtDiv;

            var p3 = new SqlParameter("@ResDivList", SqlDbType.Structured);
            p3.TypeName = "[dbo].[ResDiv_List]"; p3.SqlValue = dtDivRes;

            var p4 = new SqlParameter("@L2_List", SqlDbType.Structured);
            p4.TypeName = "[dbo].[L2_List]"; p4.SqlValue = dtL2;

            var p5 = new SqlParameter("@L3_List", SqlDbType.Structured);
            p5.TypeName = "[dbo].[L3_List]"; p5.SqlValue = dtL3;

            var p6 = new SqlParameter("@L4_List", SqlDbType.Structured);
            p6.TypeName = "[dbo].[L4_List]"; p6.SqlValue = dtL4;

            var p7 = new SqlParameter("@BoqResList", SqlDbType.Structured);
            p7.TypeName = "[dbo].[Resources_List]"; p7.SqlValue = dtRes;

            var p8 = new SqlParameter("@ResTypeList", SqlDbType.Structured);
            p8.TypeName = "[dbo].[ResType_List]"; p8.SqlValue = dtResType;

            var p9 = new SqlParameter("@BOQItem", (input.BOQItem == null) ? "" : input.BOQItem);
            var p10 = new SqlParameter("@BOQDesc", (input.BOQDesc == null) ? "" : input.BOQDesc);
            var p11 = new SqlParameter("@SheetDesc", (input.SheetDesc == null) ? "" : input.SheetDesc);
            var p12 = new SqlParameter("@FromRow", (input.FromRow == null) ? 0 : input.FromRow);
            var p13 = new SqlParameter("@ToRow", (input.ToRow == null) ? 0 : input.ToRow);
            var p14 = new SqlParameter("@Package", (input.Package == null) ? 0 : input.Package);
            var p15 = new SqlParameter("@ResDesc", (input.RESDesc == null) ? "" : input.RESDesc);
            var p16 = new SqlParameter("@isItemsAssigned", (input.isItemsAssigned == null) ? 0 : input.isItemsAssigned);
            var p17 = new SqlParameter("@isRessourcesAssigned", (input.isRessourcesAssigned == null) ? 0 : input.isRessourcesAssigned);
            var p18 = new SqlParameter("@boqStatus", (input.boqStatus == "") ? "" : input.boqStatus);

            //////////////////
            /////methode SP 1
            List<SqlParameter> parameters = new List<SqlParameter>();
            parameters.Add(p0);
            parameters.Add(p1);
            parameters.Add(p2);
            parameters.Add(p3);
            parameters.Add(p4);
            parameters.Add(p5);
            parameters.Add(p6);
            parameters.Add(p7);
            parameters.Add(p8);
            parameters.Add(p9);
            parameters.Add(p10);
            parameters.Add(p11);
            parameters.Add(p12);
            parameters.Add(p13);
            parameters.Add(p14);
            parameters.Add(p15);
            parameters.Add(p16);
            parameters.Add(p17);
            parameters.Add(p18);

            ExecuteRawSP executeRawSP = new ExecuteRawSP();
            List< BoqRessourcesList> result= new List< BoqRessourcesList>();


            switch (type)
            {
                case 1:
                    result = await executeRawSP.ExecuteRawStoredProcedure(_mdbcontext, "sp_GetOriginalBoqList @Type,@DB,@BOQDivList,@ResDivList,@L2_List,@L3_List,@L4_List,@BoqResList,@ResTypeList,@BOQItem,@BOQDesc,@SheetDesc,@FromRow,@ToRow,@Package,@ResDesc,@isItemsAssigned,@isRessourcesAssigned,@boqStatus", parameters,
                        x => new BoqRessourcesList
                        {
                            RowNumber = (int)x["RowNumber"],
                            SectionO = x["SectionO"] == null ? "" : (string)x["SectionO"],
                            ItemO = (string)x["ItemO"],
                            DescriptionO = (string)x["DescriptionO"],
                            UnitO = (string)x["UnitO"],
                            UnitRateO = (double)x["UnitRate"],
                            AssignedPackage = (string)x["AssignedPackage"],
                            BillQtyO = (double)x["BillQtyO"],
                            QtyO = (double)x["QtyO"],
                            ScopeQtyO = (double)x["ScopeQtyO"],
                            //ObTradeDesc = x["ObTradeDesc"] != DBNull.Value ? (string)x["ObTradeDesc"] : null,
                            BoqStatus = x["BoqStatus"] != DBNull.Value ? (string)x["BoqStatus"] : null,
                            L2= (string)x["L2"],
                            L3 = (string)x["L3"],
                            L4 = (string)x["L4"],
                            C1 = (string)x["C1"],
                            C2 = (string)x["C2"],
                            C3 = (string)x["C3"],
                            C4 = (string)x["C4"]
                        });
                    break;

                case 2: case 3: case 4:
                    result = await executeRawSP.ExecuteRawStoredProcedure(_mdbcontext, "sp_GetOriginalBoqList @Type,@DB,@BOQDivList,@ResDivList,@L2_List,@L3_List,@L4_List,@BoqResList,@ResTypeList,@BOQItem,@BOQDesc,@SheetDesc,@FromRow,@ToRow,@Package,@ResDesc,@isItemsAssigned,@isRessourcesAssigned,@boqStatus", parameters,
                      x => new BoqRessourcesList
                      {
                        ItemO = (string)x["ItemO"],
                        DescriptionO =  (string)x["DescriptionO"],
                        UnitO =  (string)x["UnitO"],
                        QtyO = (double)x["QtyO"],
                        UnitRateO = (double)x["UnitRate"],
                        TotalPriceO= (double)x["totalPrice"],
                        BoqSeq = (int)x["BoqSeq"],
                        //BoqResSeq = (string)x["BoqResSeq"],
                        BoqCtg = (string)x["BoqCtg"],
                        BoqUnitMesure = (string)x["BoqUnitMesure"],
                        BoqQty = (double)x["BoqQty"],
                        BoqUprice = (double)x["BoqUprice"],
                        BoqDiv = (string)x["BoqDiv"],
                        ResDescription =(string)x["ResDescription"],
                        BoqTotalPrice = (double)x["BoqTotalPrice"],
                        L1 = x["L1"] != DBNull.Value ? (string)x["L1"] : null,
                        L2 = x["L2"] != DBNull.Value ? (string)x["L2"] : null,
                        L3 = x["L3"] != DBNull.Value ? (string)x["L3"] : null,
                        resCode= (string)x["BoqPackage"]
                      });
                    break;
                case 5:
                    result = await executeRawSP.ExecuteRawStoredProcedure(_mdbcontext, "sp_GetOriginalBoqList @Type,@DB,@BOQDivList,@ResDivList,@L2_List,@L3_List,@L4_List,@BoqResList,@ResTypeList,@BOQItem,@BOQDesc,@SheetDesc,@FromRow,@ToRow,@Package,@ResDesc,@isItemsAssigned,@isRessourcesAssigned,@boqStatus", parameters,
                        x => new BoqRessourcesList
                        {
                            ScopeO = (int)x["ScopeO"]                           
                        });
                    break;
                default:
                    // code block
                    break;
            }

            /////////////////

            //methode SP 2
            //List<TmpBoqRessource> list = _mdbcontext
            //            .TmpBoqRessources
            //            .FromSqlRaw("exec sp_GetOriginalBoqList @Type,@DB,@BOQDivList,@ResDivList,@L2_List,@L3_List,@L4_List,@BoqResList,@ResTypeList,@BOQItem,@BOQDesc,@SheetDesc,@FromRow,@ToRow,@Package,@ResDesc,@isItemsAssigned,@isRessourcesAssigned,@boqStatus",P0, p1, p2,p3,p4,p5,p6,p7,p8,p9,p10,p11,p12,p13,p14,p15,p16,p17,p18)
            //            .ToList();

            //var result = list.Select(x => new BoqRessourcesList
            //{
            //    RowNumber = x.RowNumber,
            //    SectionO = x.SectionO,
            //    ItemO = x.ItemO,
            //    DescriptionO = x.DescriptionO,
            //    UnitO = x.UnitO,
            //    UnitRate = x.UnitRate,
            //    //Scope = p.Scope,
            //    AssignedPackage = x.AssignedPackage,
            //    BillQtyO = x.BillQtyO,
            //    QtyO = x.QtyO,
            //    ScopeQtyO = x.ScopeQtyO,
            //    ObTradeDesc = x.ObTradeDesc
            //}).OrderBy(w => w.RowNumber)
            //.ToList();

            return result;


            //var packList = (from p in _mdbcontext.TblPackages
            //                select new packagesList
            //                {
            //                    PkgeId = (int)p.PkgeId,
            //                    PkgeName = p.PkgeName
            //                }).ToList();

            //IEnumerable<BoqRessourcesList> condQuery = (from o in _context.TblOriginalBoqVds
            //                                            join b in _context.TblBoqVds on o.ItemO equals b.BoqItem
            //                                            join r in _context.TblResources on b.BoqResSeq equals r.ResSeq
            //                                            select new BoqRessourcesList
            //                                            {
            //                                                RowNumber = o.RowNumber,
            //                                                SectionO = o.SectionO,
            //                                                ItemO = o.ItemO,
            //                                                DescriptionO = o.DescriptionO,
            //                                                UnitO = o.UnitO,
            //                                                UnitRate = o.UnitRate,
            //                                                Scope = o.Scope,
            //                                                L2 = ((o.L2 == null) ? "" : o.L2),
            //                                                L3 = ((o.L3 == null) ? "" : o.L3),
            //                                                L4 = ((o.L4 == null) ? "" : o.L4),
            //                                                BillQtyO = o.ObBillQty,
            //                                                QtyO = o.QtyO,
            //                                                ScopeQtyO = o.QtyScope,
            //                                                ObTradeDesc = ((o.ObTradeDesc == null) ? "" : o.ObTradeDesc),
            //                                                ObSheetDesc = ((o.ObSheetDesc == null) ? "" : o.ObSheetDesc),
            //                                                BoqSeq = b.BoqSeq,
            //                                                BoqCtg = b.BoqCtg,
            //                                                BoqUnitMesure = b.BoqUnitMesure,
            //                                                BoqUprice = b.BoqUprice,
            //                                                BoqDiv = b.BoqDiv,
            //                                                BoqPackage = b.BoqPackage,
            //                                                BoqScope = b.BoqScope,
            //                                                ResDescription = r.ResDescription,
            //                                                BoqBillQty = b.BoqBillQty,
            //                                                BoqQty = b.BoqQty,
            //                                                BoqScopeQty = b.BoqQtyScope,
            //                                                AssignedPackage = "",
            //                                                ResSeq = r.ResSeq
            //                                            });

            //if (input.BOQDiv.Length > 0) condQuery = condQuery.Where(w => input.BOQDiv.Contains(w.SectionO));
            //if (!string.IsNullOrEmpty(input.BOQItem)) condQuery = condQuery.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
            //if (!string.IsNullOrEmpty(input.BOQDesc)) condQuery = condQuery.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
            //if (!string.IsNullOrEmpty(input.SheetDesc)) condQuery = condQuery.Where(w => w.ObSheetDesc == input.SheetDesc);
            //if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) condQuery = condQuery.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
            //if (input.Package > 0) condQuery = condQuery.Where(w => w.Scope == input.Package);
            //if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));
            //if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            //if (!string.IsNullOrEmpty(input.RESPackage)) condQuery = condQuery.Where(w => w.BoqPackage == input.RESPackage);
            //if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));
            //if (input.Package > 0) condQuery = condQuery.Where(w => w.BoqScope == input.Package);
            //if (input.boqLevel2.Length > 0) condQuery = condQuery.Where(w => input.boqLevel2.Contains(w.L2));
            //if (input.boqLevel3.Length > 0) condQuery = condQuery.Where(w => input.boqLevel3.Contains(w.L3));
            ////if (!string.IsNullOrEmpty(input.boqLevel3)) condQuery = condQuery.Where(w => w.L3.ToLower().Contains(input.boqLevel3.ToLower()));
            //if (input.boqLevel4.Length > 0) condQuery = condQuery.Where(w => input.boqLevel4.Contains(w.L4));
            //if (!string.IsNullOrEmpty(input.obTradeDesc)) condQuery = condQuery.Where(w => w.ObTradeDesc.ToLower().Contains(input.obTradeDesc.ToLower()));
            //if (input.boqResourceSeq.Length > 0) condQuery = condQuery.Where(w => input.boqResourceSeq.Contains(w.ResSeq));

            //switch (input.isItemsAssigned)
            //{
            //    case 1:
            //        condQuery = condQuery.Where(w => w.Scope > 0);
            //        break;
            //    case 2:
            //        condQuery = condQuery.Where(w => w.Scope == null || w.Scope == 0);
            //        break;
            //    default:
            //        break;
            //}

            //switch (input.isRessourcesAssigned)
            //{
            //    case 1:
            //        condQuery = condQuery.Where(w => w.BoqScope > 0);
            //        break;
            //    case 2:
            //        condQuery = condQuery.Where(w => w.BoqScope == null || w.BoqScope == 0);
            //        break;
            //    default:
            //        break;
            //}

            ////Update Package Name
            //var qry = condQuery.ToList();
            //foreach (var x in qry.Where(i => i.Scope > 0))
            //{
            //    x.AssignedPackage = packList.FirstOrDefault(d => d.PkgeId == x.Scope).PkgeName;
            //}
            //int s = condQuery.Count();

            //var resutl = condQuery
            //    .GroupBy(x => new { x.RowNumber, x.SectionO, x.ItemO, x.DescriptionO, x.UnitO })
            //    .Select(p => p.FirstOrDefault())
            //    .Select(p => new BoqRessourcesList
            //    {
            //        RowNumber = p.RowNumber,
            //        SectionO = p.SectionO,
            //        ItemO = p.ItemO,
            //        DescriptionO = p.DescriptionO,
            //        UnitO = p.UnitO,
            //        UnitRate = p.UnitRate,
            //        //Scope = p.Scope,
            //        AssignedPackage = p.AssignedPackage,
            //        BillQtyO = p.BillQtyO,
            //        QtyO = p.QtyO,
            //        ScopeQtyO = p.ScopeQtyO,
            //        ObTradeDesc = p.ObTradeDesc
            //    }).OrderBy(w => w.RowNumber)
            //    .ToList();


            //int status, oldstatus;
            //List<string> stsList = new List<string>();
            //foreach (var boq in resutl)
            //{
            //    status = oldstatus = 0;
            //    stsList.Clear();
            //    var resList = condQuery.Where(x => x.ItemO == boq.ItemO).ToList();
            //    foreach (var res in resList)
            //    {
            //        if (res.BoqScope > 0)
            //        {
            //            status = (int)res.BoqScope;
            //            stsList.Add("Assigned");
            //        }
            //        else
            //        {
            //            stsList.Add("Not Assigned");
            //        }

            //        if ((oldstatus != status) && (oldstatus > 0) && (status > 0))
            //            stsList.Add("Mix");

            //        if (oldstatus != status) oldstatus = status;
            //    }

            //    if (stsList.Contains("Assigned") && stsList.Contains("Not Assigned"))
            //        boq.BoqStatus = "Missing";
            //    else if (stsList.Contains("Assigned") && (!stsList.Contains("Mix")))
            //        boq.BoqStatus = "Assigned";
            //    else if (stsList.Contains("Mix"))
            //        boq.BoqStatus = "Mix";
            //}           
        }


        public async Task<List<BoqRessourcesList>> GetOriginalBoqList(SearchInput input,string costDB)
        {
            //string connectionString = Configuration.GetConnectionString("DefaultConnection");
            //string costDb = "CiteDefence_CostData";
            //var connection = new SqlConnectionStringBuilder(connectionString);
            //connection.InitialCatalog = costDb;

            //string conName = connection.ConnectionString.ToString();
            //var _costDBbContext = _context.CreateConnectionFromOut(conName);


            //var results = from b in _context.TblOriginalBoqVds
            //              where (input.BOQDiv != null && b.SectionO == input.BOQDiv)
            //              where (input.SheetDesc != null && b.ObSheetDesc == input.SheetDesc)
            //              where (input.FromRow != null && input.ToRow != null && b.RowNumber <= int.Parse(input.FromRow) && b.RowNumber >= int.Parse(input.ToRow))
            //              orderby b.RowNumber
            //              select b;


            //var results = from b in _context.TblOriginalBoqVds
            //              where (input.BOQDiv != null || b.SectionO == input.BOQDiv)
            //                   && (input.SheetDesc != null || b.ObSheetDesc == input.SheetDesc)
            //                   && ((input.FromRow != null && input.ToRow != null) || (b.RowNumber >= int.Parse(input.FromRow) && b.RowNumber <= int.Parse(input.ToRow)))
            //              orderby b.RowNumber
            //              select b;

            var resutl =await GetBoqWithRessourcesAsync(input,  costDB,1);

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
            //var results = (from b in _context.TblBoqVds
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

            IEnumerable<BoqModel> condQuery = (from o in _context.TblOriginalBoqVds
                                               join b in _context.TblBoqVds on o.ItemO equals b.BoqItem
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
                                                   AssignedPackage = "",
                                                   TotalUnitPrice= (b.BoqUprice * b.BoqQty)/ o.UnitRate
                                               });


            if (input.BOQDiv.Length > 0) condQuery = condQuery.Where(w => input.BOQDiv.Contains(w.SectionO));
            if (!string.IsNullOrEmpty(input.BOQItem)) condQuery = condQuery.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
            if (!string.IsNullOrEmpty(input.BOQDesc)) condQuery = condQuery.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
            if (!string.IsNullOrEmpty(input.SheetDesc)) condQuery = condQuery.Where(w => w.ObSheetDesc == input.SheetDesc);
            if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) condQuery = condQuery.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
            if (input.Package > 0) condQuery = condQuery.Where(w => input.Package.ToString().Contains(w.BoqScope.ToString())); //condQuery = condQuery.Where(w => w.Scope == input.Package || w.BoqScope == input.Package);
            if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));
            if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            if (!string.IsNullOrEmpty(input.RESPackage)) condQuery = condQuery.Where(w => w.BoqPackage == input.RESPackage);
            if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));
            //if (input.Package > 0) condQuery = condQuery.Where(w => w.BoqScope == input.Package);
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
                    condQuery = condQuery.Where(w => w.BoqScope == null || w.BoqScope == 0);
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

            //Calculate Discount
            //var lstDiscount = _context.TblBoqDiscounts.Where(x => (x.BoqdDiscountAll + x.Boqddiscount) > 0).ToList();

            //foreach (var d in lstDiscount.Where(d => (d.BoqdDiscountAll + d.Boqddiscount)>0))
            //{
            //    foreach (var boq in results.Where(x=> x.BoqCtg == d.BoqdCtg && x.BoqDiv == d.BoqdDiv))
            //    {
            //        boq.BoqUprice = boq.BoqUprice - (boq.BoqUprice * ((d.BoqdDiscountAll + d.Boqddiscount) / 100));
            //        boq.BoqTotalPrice = boq.BoqTotalPrice - (boq.BoqTotalPrice * ((d.BoqdDiscountAll + d.Boqddiscount) / 100));
            //    }
            //}
            
            return results.OrderBy(x=>x.BoqCtg).ToList();
        }

        public List<BoqModel> GetAllBoqList(SearchInput input)
        {
            var packList = (from b in _mdbcontext.TblPackages
                            select b).ToList();

            IEnumerable<BoqModel> condQuery = (from o in _context.TblOriginalBoqVds
                                               join b in _context.TblBoqVds on o.ItemO equals b.BoqItem
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
            if (input.Package > 0) condQuery = condQuery.Where(w => w.Scope == input.Package || w.BoqScope == input.Package);
            if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));
            if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            if (!string.IsNullOrEmpty(input.RESPackage)) condQuery = condQuery.Where(w => w.BoqPackage == input.RESPackage);
            if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));
            //if (input.Package > 0) condQuery = condQuery.Where(w => w.BoqScope == input.Package);
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
                    condQuery = condQuery.Where(w => w.BoqScope == null || w.BoqScope == 0);
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
                //    var data = _context.TblOriginalBoqVds.Where(x => x.RowNumber == item.RowNumber).FirstOrDefault();
                //    data.Scope = item.Scope;

                //    _context.TblOriginalBoqVds.Update(data);               
                //}
                //_context.SaveChanges();

                //AH06022024
                //var lstBoqo = (from a in input.AssignOriginalBoqList
                //               join b in _context.TblOriginalBoqVds on a.RowNumber equals b.RowNumber
                //               select b).ToList();
                var lstBoqo = (from a in input.AssignOriginalBoqList
                               join b in _context.TblOriginalBoqVds on a.ItemO equals b.ItemO
                               select b).ToList();

                //AH06022024
                if (lstBoqo != null)
                {
                    foreach (var item in input.AssignOriginalBoqList)
                    {
                        lstBoqo.Where(d => d.ItemO == item.ItemO).First().Scope = item.Scope;
                    }
                    _context.TblOriginalBoqVds.UpdateRange(lstBoqo);
                    _context.SaveChanges();
                }
            }

            if (input.AssignBoqList != null)
            {
                //foreach (var item in input.AssignBoqList)
                //{
                //    var data = _context.TblBoqVds.Where(x => x.BoqSeq == item.BoqSeq).FirstOrDefault();
                //    data.BoqScope = item.BoqScope;

                //    _context.TblBoqVds.Update(data);
                //}
                //_context.SaveChanges();

                var lstBoq = (from a in input.AssignBoqList
                              join b in _context.TblBoqVds on a.BoqSeq equals b.BoqSeq
                              select b).ToList();

                foreach (var item in input.AssignBoqList)
                {
                    lstBoq.Where(d => d.BoqSeq == item.BoqSeq).First().BoqScope = item.BoqScope;
                }
                _context.TblBoqVds.UpdateRange(lstBoq);
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

            var supplierPackageRev = (from sup in supList
                         join b in _context.TblSupplierPackages on sup.SupCode equals b.SpSupplierId
                         join rev in _context.TblSupplierPackageRevisions on b.SpPackSuppId equals rev.PrPackSuppId
                         where (b.SpPackageId == pckgID && rev.PrRevNo == 0)
                         select new PackageSuppliersPrice
                         {
                             SupplierId = b.SpSupplierId,
                             SupplierName = sup.SupName,
                             LastRevisionDate = rev.PrRevDate,
                             ByBoq = (byte)((b.SpByBoq == null) ? 0 : b.SpByBoq),
                             RevisionCurrency= curList.Find(x=>x.CurId==rev.PrCurrency).CurCode
                         }).ToList();

            if (supplierPackageRev.Count > 0)
            {
                byte byboq = supplierPackageRev.FirstOrDefault().ByBoq;

                supplierPackageRev.Add(new PackageSuppliersPrice() { SupplierId = 0, SupplierName = "Ideal", LastRevisionDate = null, ByBoq = byboq });

                if (supplierPackageRev.Count > 0)
                {
                    foreach (var item in supplierPackageRev)
                    {
                        PackageSuppliersPrice packageSuppliersPrice = new PackageSuppliersPrice();

                        packageSuppliersPrice.SupplierId = item.SupplierId;
                        packageSuppliersPrice.SupplierName = item.SupplierName;
                        packageSuppliersPrice.ByBoq = item.ByBoq;
                        packageSuppliersPrice.LastRevisionDate = item.LastRevisionDate;
                        packageSuppliersPrice.RevisionCurrency = item.RevisionCurrency;

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
                                             join o in _context.TblOriginalBoqVds on c.RdBoqItem equals o.ItemO
                                             join sup in supList on a.SpSupplierId equals sup.SupCode
                                             where (a.SpPackageId == pckgID && b.PrRevNo == 0 && (c.IsNew == false || c.IsNew == null) 
                                             && (c.IsAlternative == false || c.IsAlternative == null))
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
                                                 Discount = c.RdDiscount == null ? 0 : c.RdDiscount,
                                                 UPriceAfterDiscount = c.UnitPriceAfterDiscount == null ? 0 : Math.Round((double)(c.UnitPriceAfterDiscount), 2),//  Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2)
                                                 totalPriceAfterExchange= c.UnitPriceAfterDiscount == null ? 0 : Convert.ToDecimal(c.RdQty) * Convert.ToDecimal(c.UnitPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow),
                                                 IsExcluded= c.IsExcluded
                                             }).Union(from cur in curList
                                                      join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                                      join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                                      join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                      join itm in _context.NewItems on c.NewItemId equals itm.Id
                                                      join sup in supList on a.SpSupplierId equals sup.SupCode
                                                      where (a.SpPackageId == pckgID && b.PrRevNo == 0 && c.IsNew == true)
                                                      select new RevisionDetails
                                                      {
                                                          ItemO = c.RdBoqItem,
                                                          DescriptionO = c.ItemDescription,
                                                          UnitO = itm.UnitO,
                                                          QtyO = c.RdQty,
                                                          price = c.RdPrice,
                                                          perc = c.RdAssignedPerc,
                                                          missedPrice = c.RdMissedPrice,
                                                          priceOrigCur = c.RdPriceOrigCurrency,
                                                          Scope = pckgID,
                                                          BoqDiv = "",
                                                          ObSheetDesc = "",
                                                          RowNumber = 0,
                                                          AssignedToSupplier =  false,
                                                          OriginalCurrency = cur.CurCode,
                                                          AssignedQty = c.RdAssignedQty,
                                                          Discount = c.RdDiscount == null ? 0 : c.RdDiscount,
                                                          UPriceAfterDiscount = c.UnitPriceAfterDiscount == null ? 0 : Math.Round((double)(c.UnitPriceAfterDiscount), 2),//  Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2)
                                                          totalPriceAfterExchange = Convert.ToDecimal(c.RdQty) * Convert.ToDecimal(c.UnitPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow),
                                                          IsExcluded = c.IsExcluded
                                                      }).Union(from cur in curList
                                                               join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                                               join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                                               join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                               join o in _context.TblOriginalBoqVds on c.ParentItemO equals o.ItemO
                                                               join sup in supList on a.SpSupplierId equals sup.SupCode
                                                               where (a.SpPackageId == pckgID && b.PrRevNo == 0 && c.IsAlternative == true && c.UnitPriceAfterDiscount>0)
                                                               select new RevisionDetails
                                                               {
                                                                   ItemO = c.RdBoqItem,
                                                                   DescriptionO = c.ItemDescription,
                                                                   UnitO = o.UnitO,
                                                                   QtyO = c.RdQty,
                                                                   price = c.RdPrice,
                                                                   perc = c.RdAssignedPerc,
                                                                   missedPrice = c.RdMissedPrice,
                                                                   priceOrigCur = c.RdPriceOrigCurrency,
                                                                   Scope = pckgID,
                                                                   BoqDiv = o.SectionO,
                                                                   ObSheetDesc = o.ObSheetDesc,
                                                                   RowNumber = 0,
                                                                   AssignedToSupplier = false,
                                                                   OriginalCurrency = cur.CurCode,
                                                                   AssignedQty = c.RdAssignedQty,
                                                                   Discount = c.RdDiscount == null ? 0 : c.RdDiscount,
                                                                   UPriceAfterDiscount = c.UnitPriceAfterDiscount == null ? 0 : Math.Round((double)(c.UnitPriceAfterDiscount), 2),//  Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2)
                                                                   totalPriceAfterExchange = c.UnitPriceAfterDiscount == null ? 0 : Convert.ToDecimal(c.RdQty) * Convert.ToDecimal(c.UnitPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow),
                                                                   IsExcluded = c.IsExcluded
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
                            }
                            else
                            {
                                revDtlQry = (from cur in curList
                                             join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                             join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                             join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                             join d in _context.TblBoqVds on c.RdResourceSeq equals d.BoqSeq
                                             join e in _context.TblResources on d.BoqResSeq equals e.ResSeq
                                             join o in _context.TblOriginalBoqVds on d.BoqItem equals o.ItemO
                                             join sup in supList on a.SpSupplierId equals sup.SupCode
                                             where (a.SpPackageId == pckgID && b.PrRevNo == 0 && 
                                             (c.IsNew == false || c.IsNew == null) && (c.IsAlternative == false || c.IsAlternative == null))
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
                                                 UPriceAfterDiscount = c.UnitPriceAfterDiscount == null ? 0 : Math.Round((double)(c.UnitPriceAfterDiscount), 2),// Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2)
                                                 totalPriceAfterExchange = c.UnitPriceAfterDiscount == null ? 0 : Convert.ToDecimal(c.RdQty) * Convert.ToDecimal(c.UnitPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow),
                                                 IsAlternative = false,
                                                 IsNewItem = false,
                                                 NewItemId = c.NewItemId,
                                                 NewItemResourceId = c.NewItemResourceId,
                                                 ParentItemO = c.ParentItemO,
                                                 ParentResourceId = c.ParentResourceId,
                                                 IsExcluded = c.IsExcluded,
                                                 SupplierId = (int)a.SpSupplierId
                                             }).Union(from cur in curList
                                                      join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                                      join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                                      join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                      join i in _context.NewItems on c.NewItemId equals i.Id
                                                      join newr in _context.NewItemResources on c.NewItemResourceId equals newr.Id
                                                      join sup in supList on a.SpSupplierId equals sup.SupCode
                                                      where (a.SpPackageId == pckgID && b.PrRevNo == 0 && c.IsNew == true)
                                                      select new RevisionDetails
                                                      {
                                                          resourceID = c.RdResourceSeq,
                                                          ResDescription = c.ResourceDescription,
                                                          resourceUnit = newr.ResourceUnit,
                                                          resourceQty = c.RdQty,
                                                          price = c.RdPrice,
                                                          perc = c.RdAssignedPerc,
                                                          missedPrice = c.RdMissedPrice,
                                                          priceOrigCur = c.RdPriceOrigCurrency,
                                                          ItemO = Convert.ToString(c.NewItemId),
                                                          DescriptionO = c.ItemDescription,
                                                          SectionO = Convert.ToString(""),
                                                          Scope = pckgID,
                                                          BoqDiv = Convert.ToString(""),
                                                          ObSheetDesc = Convert.ToString(""),
                                                          RowNumber = 0,
                                                          BoqPackage = Convert.ToString(""),
                                                          BoqScope = pckgID,
                                                          ResDiv = Convert.ToString(""),
                                                          ResCtg = newr.ResourceType,
                                                          AssignedToSupplier = ((c.RdAssignedQty == null || c.RdAssignedQty == 0)) ? false : true,
                                                          OriginalCurrency = cur.CurCode,
                                                          AssignedQty = c.RdAssignedQty,
                                                          Discount = c.RdDiscount,
                                                          UPriceAfterDiscount = c.UnitPriceAfterDiscount == null ? 0 : Math.Round((double)(c.UnitPriceAfterDiscount), 2),// Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2)
                                                          totalPriceAfterExchange = c.UnitPriceAfterDiscount == null ? 0 : Convert.ToDecimal(c.RdQty) * Convert.ToDecimal(c.UnitPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow),
                                                          IsAlternative = false,
                                                          IsNewItem = true,
                                                          NewItemId = c.NewItemId,
                                                          NewItemResourceId = c.NewItemResourceId,
                                                          ParentItemO = c.ParentItemO,
                                                          ParentResourceId = c.ParentResourceId,
                                                          IsExcluded = c.IsExcluded,
                                                          SupplierId = (int)a.SpSupplierId
                                                      }).Union(from cur in curList
                                                               join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                                               join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                                               join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                               join d in _context.TblBoqVds on c.ParentResourceId equals d.BoqSeq
                                                               where (a.SpPackageId == pckgID && b.PrRevNo == 0 && c.IsAlternative==true && c.UnitPriceAfterDiscount > 0)
                                                               select new RevisionDetails
                                                               {
                                                                   resourceID = c.RdResourceSeq,
                                                                   ResDescription = c.ResourceDescription,
                                                                   resourceUnit = d.BoqUnitMesure,
                                                                   resourceQty = c.RdQty,
                                                                   price = c.RdPrice,
                                                                   perc = c.RdAssignedPerc,
                                                                   missedPrice = c.RdMissedPrice,
                                                                   priceOrigCur = c.RdPriceOrigCurrency,
                                                                   ItemO = c.RdBoqItem,
                                                                   DescriptionO = c.ItemDescription,
                                                                   SectionO = d.BoqDiv,
                                                                   Scope = pckgID,
                                                                   BoqDiv = d.BoqDiv,
                                                                   ObSheetDesc = "",
                                                                   RowNumber =0,
                                                                   BoqPackage = d.BoqPackage,
                                                                   BoqScope = d.BoqScope,
                                                                   ResDiv = d.BoqDiv,
                                                                   ResCtg = d.BoqCtg,
                                                                   AssignedToSupplier = ((c.RdAssignedQty == null || c.RdAssignedQty == 0)) ? false : true,
                                                                   OriginalCurrency = cur.CurCode,
                                                                   AssignedQty = c.RdAssignedQty,
                                                                   Discount = c.RdDiscount,
                                                                   UPriceAfterDiscount = c.UnitPriceAfterDiscount == null ? 0 : Math.Round((double)(c.UnitPriceAfterDiscount), 2),// Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2)
                                                                   totalPriceAfterExchange = c.UnitPriceAfterDiscount == null ? 0 : Convert.ToDecimal(c.RdQty) * Convert.ToDecimal(c.UnitPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow),
                                                                   IsAlternative = true,
                                                                   IsNewItem = false,
                                                                   NewItemId = c.NewItemId,
                                                                   NewItemResourceId = c.NewItemResourceId,
                                                                   ParentItemO = c.ParentItemO,
                                                                   ParentResourceId = c.ParentResourceId,
                                                                   IsExcluded = c.IsExcluded,
                                                                   SupplierId = (int)a.SpSupplierId
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

                            revDtlQryIdeal = revDtlQry.Where(x => x.totalPriceAfterExchange > 0)
                            .GroupBy(x => new { x.ItemO,x.resourceID, x.IsExcluded, supplier = (x.IsAlternative == true ? x.SupplierId : 0) })
                            .Select(p => new RevisionDetails
                            {
                                ItemO = p.First().ItemO,
                                DescriptionO = p.First().DescriptionO,
                                UnitO = p.First().UnitO,
                                QtyO = p.First().QtyO,
                                priceOrigCur = p.Min(c => (c.IsExcluded == true) ? 0 : c.priceOrigCur),
                                AssignedQty = p.First().AssignedQty,
                                OriginalCurrency = p.First().OriginalCurrency,
                                UPriceAfterDiscount = p.Min(c => (c.IsExcluded == true) ? 0 : c.UPriceAfterDiscount),
                                totalPriceAfterExchange = p.Min(c => (c.IsExcluded == true) ? 0 : c.totalPriceAfterExchange)
                            }).ToList();

                            packageSuppliersPrice.revisionDetails = revDtlQryIdeal.ToList();

                            List<FieldList> f = new List<FieldList>();
                            f = null;
                            packageSuppliersPrice.fieldLists = f;
                        }
                        else  //item.SupplierName <> "Ideal"
                        {
                            if (byboq == 1)
                            {
                                revDtlQry = (from cur in curList
                                             join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                             join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                             join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                             join o in _context.TblOriginalBoqVds on c.RdBoqItem equals o.ItemO
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
                                                 UPriceAfterDiscount = c.UnitPriceAfterDiscount == null ? 0 :  Math.Round((double)(c.UnitPriceAfterDiscount), 2),// Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2),
                                                 totalPriceAfterExchange = c.UnitPriceAfterDiscount == null ? 0 : Convert.ToDecimal(c.RdQty) * Convert.ToDecimal(c.UnitPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow),
                                                 IsExcluded = c.IsExcluded
                                             }).Union(from cur in curList
                                                      join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                                      join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                                      join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                      join itm in _context.NewItems on c.NewItemId equals itm.Id
                                                      join sup in supList on a.SpSupplierId equals sup.SupCode
                                                      where (a.SpPackageId == pckgID && b.PrRevNo == 0 && c.IsNew == true)
                                                      select new RevisionDetails
                                                      {
                                                          ItemO = c.RdBoqItem,
                                                          DescriptionO = c.ItemDescription,
                                                          UnitO = itm.UnitO,
                                                          QtyO = c.RdQty,
                                                          price = c.RdPrice,
                                                          perc = c.RdAssignedPerc,
                                                          missedPrice = c.RdMissedPrice,
                                                          priceOrigCur = c.RdPriceOrigCurrency,
                                                          Scope = pckgID,
                                                          BoqDiv = "",
                                                          ObSheetDesc = "",
                                                          RowNumber = 0,
                                                          AssignedToSupplier = false,
                                                          OriginalCurrency = cur.CurCode,
                                                          AssignedQty = c.RdAssignedQty,
                                                          Discount = c.RdDiscount,
                                                          UPriceAfterDiscount = c.UnitPriceAfterDiscount == null ? 0 : Math.Round((double)(c.UnitPriceAfterDiscount), 2),//  Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2)
                                                          totalPriceAfterExchange = c.UnitPriceAfterDiscount == null ? 0 : Convert.ToDecimal(c.RdQty) * Convert.ToDecimal(c.UnitPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow),
                                                          IsExcluded = c.IsExcluded
                                                      }).Union(from cur in curList
                                                               join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                                               join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                                               join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                               join o in _context.TblOriginalBoqVds on c.ParentItemO equals o.ItemO
                                                               join sup in supList on a.SpSupplierId equals sup.SupCode
                                                               where (a.SpPackageId == pckgID && b.PrRevNo == 0 && c.IsAlternative == true)
                                                               select new RevisionDetails
                                                               {
                                                                   ItemO = c.RdBoqItem,
                                                                   DescriptionO = c.ItemDescription,
                                                                   UnitO = o.UnitO,
                                                                   QtyO = c.RdQty,
                                                                   price = c.RdPrice,
                                                                   perc = c.RdAssignedPerc,
                                                                   missedPrice = c.RdMissedPrice,
                                                                   priceOrigCur = c.RdPriceOrigCurrency,
                                                                   Scope = pckgID,
                                                                   BoqDiv = o.SectionO,
                                                                   ObSheetDesc = o.ObSheetDesc,
                                                                   RowNumber = 0,
                                                                   AssignedToSupplier = false,
                                                                   OriginalCurrency = cur.CurCode,
                                                                   AssignedQty = c.RdAssignedQty,
                                                                   Discount = c.RdDiscount,
                                                                   UPriceAfterDiscount = c.UnitPriceAfterDiscount == null ? 0 : Math.Round((double)(c.UnitPriceAfterDiscount), 2),//  Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2)
                                                                   totalPriceAfterExchange = c.UnitPriceAfterDiscount == null ? 0 :  Convert.ToDecimal(c.RdQty) * Convert.ToDecimal(c.UnitPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow),
                                                                   IsExcluded = c.IsExcluded,
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
                                             join d in _context.TblBoqVds on c.RdResourceSeq equals d.BoqSeq
                                             join o in _context.TblOriginalBoqVds on d.BoqItem equals o.ItemO
                                             join sup in supList on a.SpSupplierId equals sup.SupCode
                                             where (a.SpPackageId == pckgID && b.PrRevNo == 0 && a.SpSupplierId == item.SupplierId 
                                             && (c.IsNew == false || c.IsNew == null) && (c.IsAlternative == false || c.IsAlternative == null))
                                             select new RevisionDetails
                                             {
                                                 resourceID = c.RdResourceSeq,
                                                 ResDescription = c.ResourceDescription == null ? "": c.ResourceDescription,
                                                 resourceUnit = d.BoqUnitMesure,
                                                 resourceQty = c.RdQty,
                                                 price = c.RdPrice,
                                                 perc = c.RdAssignedPerc,
                                                 missedPrice = c.RdMissedPrice,
                                                 priceOrigCur = c.RdPriceOrigCurrency,
                                                 ItemO = o.ItemO,
                                                 DescriptionO =  o.DescriptionO == null ? "" : o.DescriptionO,
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
                                                 UPriceAfterDiscount = c.UnitPriceAfterDiscount == null ? 0 :  Math.Round((double)(c.UnitPriceAfterDiscount), 2),//  Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2)
                                                 totalPriceAfterExchange = c.UnitPriceAfterDiscount ==null ? 0 : Convert.ToDecimal(c.RdQty) * Convert.ToDecimal(c.UnitPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow),
                                                 IsAlternative = false,
                                                 IsNewItem = false,
                                                 NewItemId = c.NewItemId,
                                                 NewItemResourceId = c.NewItemResourceId,
                                                 ParentItemO = c.ParentItemO,
                                                 ParentResourceId = c.ParentResourceId,
                                                 IsExcluded = c.IsExcluded,
                                                 SupplierId = (int)a.SpSupplierId
                                             }).Union(from cur in curList
                                                      join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                                      join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                                      join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                      join i in _context.NewItems on c.NewItemId equals i.Id
                                                      join newr in _context.NewItemResources on c.NewItemResourceId equals newr.Id
                                                      join sup in supList on a.SpSupplierId equals sup.SupCode
                                                      where (a.SpPackageId == pckgID && b.PrRevNo == 0 && c.IsNew == true)
                                                      select new RevisionDetails
                                                      {
                                                          resourceID = c.RdResourceSeq,
                                                          ResDescription = c.ResourceDescription,
                                                          resourceUnit = newr.ResourceUnit,
                                                          resourceQty = c.RdQty,
                                                          price = c.RdPrice,
                                                          perc = c.RdAssignedPerc,
                                                          missedPrice = c.RdMissedPrice,
                                                          priceOrigCur = c.RdPriceOrigCurrency,
                                                          ItemO = Convert.ToString(c.NewItemId),
                                                          DescriptionO = c.ItemDescription,
                                                          SectionO = Convert.ToString(""),
                                                          Scope = pckgID,
                                                          BoqDiv = Convert.ToString(""),
                                                          ObSheetDesc = Convert.ToString(""),
                                                          RowNumber = 0,
                                                          BoqPackage = Convert.ToString(""),
                                                          BoqScope = pckgID,
                                                          ResDiv = Convert.ToString(""),
                                                          ResCtg = newr.ResourceType,
                                                          AssignedToSupplier = ((c.RdAssignedQty == null || c.RdAssignedQty == 0)) ? false : true,
                                                          OriginalCurrency = cur.CurCode,
                                                          AssignedQty = c.RdAssignedQty,
                                                          Discount = c.RdDiscount,
                                                          UPriceAfterDiscount = c.UnitPriceAfterDiscount == null ? 0 : Math.Round((double)(c.UnitPriceAfterDiscount), 2),//  Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2)
                                                          totalPriceAfterExchange = c.UnitPriceAfterDiscount == null ? 0 : Convert.ToDecimal(c.RdQty) * Convert.ToDecimal(c.UnitPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow),
                                                          IsAlternative = false,
                                                          IsNewItem = true,
                                                          NewItemId = c.NewItemId,
                                                          NewItemResourceId = c.NewItemResourceId,
                                                          ParentItemO = c.ParentItemO,
                                                          ParentResourceId = c.ParentResourceId,
                                                          IsExcluded = c.IsExcluded,
                                                          SupplierId = (int)a.SpSupplierId
                                                      }).Union(from cur in curList
                                                               join b in _context.TblSupplierPackageRevisions on cur.CurId equals b.PrCurrency
                                                               join a in _context.TblSupplierPackages on b.PrPackSuppId equals a.SpPackSuppId
                                                               join c in _context.TblRevisionDetails on b.PrRevId equals c.RdRevisionId
                                                               join d in _context.TblBoqVds on c.ParentResourceId equals d.BoqSeq
                                                               join sup in supList on a.SpSupplierId equals sup.SupCode
                                                               where (a.SpPackageId == pckgID && b.PrRevNo == 0 && c.IsAlternative == true)
                                                               select new RevisionDetails
                                                               {
                                                                   resourceID = c.RdResourceSeq,
                                                                   ResDescription = c.ResourceDescription,
                                                                   resourceUnit = d.BoqUnitMesure,
                                                                   resourceQty = c.RdQty,
                                                                   price = c.RdPrice,
                                                                   perc = c.RdAssignedPerc,
                                                                   missedPrice = c.RdMissedPrice,
                                                                   priceOrigCur = c.RdPriceOrigCurrency,
                                                                   ItemO = c.RdBoqItem,
                                                                   DescriptionO = c.ItemDescription,
                                                                   SectionO = d.BoqDiv,
                                                                   Scope = pckgID,
                                                                   BoqDiv = d.BoqDiv,
                                                                   ObSheetDesc = "",
                                                                   RowNumber = 0,
                                                                   BoqPackage = d.BoqPackage,
                                                                   BoqScope = d.BoqScope,
                                                                   ResDiv = d.BoqDiv,
                                                                   ResCtg = d.BoqCtg,
                                                                   AssignedToSupplier = ((c.RdAssignedQty == null || c.RdAssignedQty == 0)) ? false : true,
                                                                   OriginalCurrency = cur.CurCode,
                                                                   AssignedQty = c.RdAssignedQty,
                                                                   Discount = c.RdDiscount,
                                                                   UPriceAfterDiscount = c.UnitPriceAfterDiscount == null ? 0 : Math.Round((double)(c.UnitPriceAfterDiscount), 2),// Math.Round((double)(c.RdPriceOrigCurrency - (c.RdPriceOrigCurrency * ((c.RdDiscount == null) ? 0 : c.RdDiscount) / 100)), 2)
                                                                   totalPriceAfterExchange = c.UnitPriceAfterDiscount == null ? 0 : (item.SupplierId == sup.SupCode) ? Convert.ToDecimal(c.RdQty) * Convert.ToDecimal(c.UnitPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == cur.CurCode).ExchRateNow) : 0,
                                                                   IsAlternative = true,
                                                                   IsNewItem = false,
                                                                   NewItemId = c.NewItemId,
                                                                   NewItemResourceId = c.NewItemResourceId,
                                                                   ParentItemO = c.ParentItemO,
                                                                   ParentResourceId = c.ParentResourceId,
                                                                   IsExcluded = c.IsExcluded,
                                                                   SupplierId = (int)a.SpSupplierId
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
                                //if (packageSuppliersPrice.SupplierName == "Ideal")
                                //    packageSuppliersPrice.totalprice += itemRevision.IsExcluded itemRevision.totalPriceAfterExchange;  // Convert.ToDecimal(itemRevision.QtyO) * Convert.ToDecimal(itemRevision.UPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == itemRevision.OriginalCurrency).ExchRateNow);
                                //else
                                    packageSuppliersPrice.totalprice += (itemRevision.IsExcluded == true) ? 0 : itemRevision.totalPriceAfterExchange; // Convert.ToDecimal(itemRevision.QtyO) * Convert.ToDecimal(itemRevision.UPriceAfterDiscount) * Convert.ToDecimal(ExchNowList.Find(x => x.fromCurrency == itemRevision.OriginalCurrency).ExchRateNow);
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
            var curList = (from b in _mdbcontext.TblCurrencies
                           select b).ToList();

            var result = from c in curList
                         join b in _context.TblParameters
                         on c.CurId equals b.EstimatedCur
                         select new ProjectCurrency
                         {
                             curId = (int)b.EstimatedCur,
                             curCode = c.CurCode
                         };


            string localCurrency = result.FirstOrDefault().curCode;

            CurrencyConverterRepository currencyConverterRepository = new CurrencyConverterRepository();
            return currencyConverterRepository.GetCurrencyExchange(localCurrency, foreignCurrency);
        }

        public async Task<string> ExportBoqExcel(SearchInput input, string costDB)
        {
            var lstBoq =await GetBoqWithRessourcesAsync(input, costDB,2);

            string excelName = "";

            if (lstBoq != null)
            {

                //var lstBoq = (from a in input.AssignBoqList
                //              join b in _context.TblBoqVds on a.BoqSeq equals b.BoqSeq
                //              join r in _context.TblResources on b.BoqResSeq equals r.ResSeq
                //              select new BoqModel()
                //              {
                //                  BoqSeq = b.BoqSeq,
                //                  BoqResSeq = b.BoqResSeq,
                //                  BoqCtg = b.BoqCtg,
                //                  BoqUnitMesure = b.BoqUnitMesure,
                //                  BoqQty = b.BoqQty,
                //                  BoqUprice = b.BoqUprice,
                //                  BoqDiv = b.BoqDiv,
                //                  BoqPackage = b.BoqPackage,
                //                  BoqScope = b.BoqScope,
                //                  ResDescription = r.ResDescription,
                //                  BoqItem = b.BoqItem,
                //                  BoqTotalPrice = b.BoqUprice * b.BoqQty
                //              }).ToList();


                var stream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var xlPackage = new ExcelPackage(stream))
                {
                    var worksheet = xlPackage.Workbook.Worksheets.Add("BOQ");
                    worksheet.Columns.AutoFit();
                    worksheet.Protection.IsProtected = false;

                    int i, j;

                    i = 1;
                    worksheet.Row(i).Style.Font.Bold = true;
                    worksheet.Cells[i, 1].Value = "Item";
                    worksheet.Column(1).Width = 20;
                    worksheet.Cells[i, 2].Value = "Description";
                    worksheet.Column(2).Width = 50;
                    worksheet.Cells[i, 3].Value = "Ressource";
                    worksheet.Column(3).Width = 50;
                    //worksheet.Columns[2].Style.WrapText = true;
                    //worksheet.Column(2).AutoFit();
                    worksheet.Cells[i, 4].Value = "Unit";
                    worksheet.Cells[i, 5].Value = "Type";
                    worksheet.Cells[i, 6].Value = "Qty";
                    worksheet.Cells[i, 7].Value = "Unit Price";
                    worksheet.Cells[i, 8].Value = "Total Price";
                    worksheet.Cells[i, 9].Value = "Div";
                    worksheet.Cells[i, 10].Value = "Level 1";
                    worksheet.Column(10).Width = 30;
                    worksheet.Cells[i, 11].Value = "Level 2";
                    worksheet.Column(11).Width = 30;
                    worksheet.Cells[i, 12].Value = "Level 3";
                    worksheet.Column(12).Width = 30;

                    i = 4;
                    foreach (var x in lstBoq)
                    {
                        worksheet.Cells[i, 1].Value = (x.ItemO == null) ? "" : x.ItemO;
                        worksheet.Cells[i, 2].Value = (x.ItemO == null) ? "" : x.DescriptionO;
                        worksheet.Cells[i, 3].Value = (x.ResDescription == null) ? "" : x.ResDescription;
                        worksheet.Cells[i, 4].Value = (x.BoqUnitMesure == null) ? "" : x.BoqUnitMesure;
                        worksheet.Cells[i, 5].Value = (x.BoqCtg == null) ? "" : x.BoqCtg;
                        worksheet.Cells[i, 6].Value = (x.BoqQty == null) ? "" : x.BoqQty;
                        worksheet.Cells[i, 7].Value = (x.BoqUprice == null) ? "" : x.BoqUprice;
                        worksheet.Cells[i, 7].Style.Numberformat.Format = "#,##0.0";
                        worksheet.Cells[i, 8].Value = (x.BoqTotalPrice == null) ? "" : x.BoqTotalPrice;
                        worksheet.Cells[i, 8].Style.Numberformat.Format = "#,##0.0";
                        worksheet.Cells[i, 9].Value = (x.BoqDiv == null) ? "" : x.BoqDiv;
                        worksheet.Cells[i, 10].Value = (x.L1 == null) ? "" : x.L1;
                        worksheet.Cells[i, 11].Value = (x.L2 == null) ? "" : x.L2;
                        worksheet.Cells[i, 12].Value = (x.L3 == null) ? "" : x.L3;
                        i++;
                    }

                    var p = _context.TblParameters.FirstOrDefault();
                    string ProjectName = p.Project;

                    xlPackage.Save();
                    stream.Position = 0;
                    excelName = $"{ProjectName}-BOQ_Ressources-{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";

                    if (File.Exists(excelName))
                        File.Delete(excelName);

                    excelName = excelName.Replace("/", "-");
                    xlPackage.SaveAs(excelName);
                }
            }
            return excelName;
        }

        public async Task<string> ExportNotAssigned(string costDB)
        {
            string excelName = "";
            int byBoq = 0;
            var arr = new string[] { };
            SearchInput input = new SearchInput()
            {
                Package = 0,
                BOQDiv = arr,
                RESDiv = arr,
                RESType = arr,
                boqLevel2 = arr,
                boqLevel3 = arr,
                boqLevel4 = arr,
                boqResourceSeq = arr,
                BOQItem = "",
                BOQDesc = "",
                RESDesc = "",
                RESPackage = "",
                FromRow = "",
                ToRow = "",
                SheetDesc = "",
                itemO = "",
                obTradeDesc = "",
                isItemsAssigned = 0,
                isRessourcesAssigned = 0,
                boqStatus = ""
            };

            var lstBoq = await GetBoqWithRessourcesAsync(input, costDB, 3);

            if (lstBoq != null)
            {
                var stream = new MemoryStream();
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var xlPackage = new ExcelPackage(stream))
                {
                    var worksheet = xlPackage.Workbook.Worksheets.Add("BOQ");
                    worksheet.Columns.AutoFit();
                    //worksheet.Protection.IsProtected = true;

                    int i, j;
                    string Boq = "", OldBoq = "", C = "", OldC = "", l1 = "", l2 = "", l3 = "", l4 = "", l5 = "", l6 = "", oldl1 = "", oldl2 = "", oldl3 = "", oldl4 = "", oldl5 = "", oldl6 = "";

                    i = 1;
                    worksheet.Cells[i, 1].Value = "Item";
                    worksheet.Column(1).Width = 40;
                    worksheet.Cells[i, 2].Value = "Level";
                    worksheet.Column(3).Width = 50;
                    worksheet.Columns[3].Style.WrapText = true;
                    worksheet.Column(3).AutoFit();
                    worksheet.Cells[i, 3].Value = "Bill Description";
                    worksheet.Cells[i, 4].Value = "Unit";
                    worksheet.Cells[i, 5].Value = "Qty";
                    worksheet.Cells[i, 6].Value = "Unit Price";
                    worksheet.Cells[i, 7].Value = "Total Price";

                    if (byBoq == 1)
                    {
                    }
                    else
                    {
                        worksheet.Cells[i, 8].Value = "Ressouce Type";
                        worksheet.Cells[i, 9].Value = "Ressouce Code";
                        worksheet.Column(10).Width = 50;
                        worksheet.Columns[10].Style.WrapText = true;
                        worksheet.Column(10).AutoFit();
                        worksheet.Cells[i, 10].Value = "Ressouce Description";
                        worksheet.Cells[i, 11].Value = "Ressouce Unit";
                        worksheet.Cells[i, 12].Value = "Ressouce Qty";
                        worksheet.Column(12).AutoFit();
                        worksheet.Cells[i, 13].Value = "Unit Price";
                        worksheet.Column(13).AutoFit();
                        worksheet.Cells[i, 14].Value = "Total Price";
                        worksheet.Column(14).AutoFit();
                        worksheet.Cells[i, 15].Value = "Comments";
                        worksheet.Column(15).Width = 50;
                        worksheet.Columns[15].Style.WrapText = true;
                        //worksheet.Column(12).AutoFit();                   
                    }
                    worksheet.Row(i).Style.Font.Bold = true;

                    i = 4;
                    foreach (var x in lstBoq)
                    {
                        Boq = x.ItemO;
                        C = x.C1;
                        l1 = (x.L1 == null) ? "" : x.L1;
                        l2 = (x.L2 == null) ? "" : x.L2;
                        l3 = (x.L3 == null) ? "" : x.L3;
                        l4 = (x.L4 == null) ? "" : x.L4;
                        l5 = (x.L5 == null) ? "" : x.L5;
                        l6 = (x.L6 == null) ? "" : x.L6;

                        if ((Boq != OldBoq) || (OldBoq == ""))
                        {
                            if ((l1 != "") && (l1 != oldl1))
                            {
                                //worksheet.Cells[i, 1].Value = (x.L1Ref == null) ? "" : x.L1Ref;
                                worksheet.Cells[i, 2].Value = "1";
                                worksheet.Cells[i, 3].Value = (x.L1 == null) ? "" : x.L1;
                                worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                oldl1 = x.L1;
                                i = i + 2;
                            }
                            if ((l2 != "") && (l2 != oldl2))
                            {
                                //worksheet.Cells[i, 1].Value = (x.l2Ref == null) ? "" : x.l2Ref;
                                worksheet.Cells[i, 2].Value = "2";
                                worksheet.Cells[i, 3].Value = (x.L2 == null) ? "" : x.L2;
                                worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                oldl2 = x.L2;
                                i = i + 2;
                            }
                            if ((l3 != "") && (l3 != oldl3))
                            {
                                //worksheet.Cells[i, 1].Value = (x.l3Ref == null) ? "" : x.l3Ref;
                                worksheet.Cells[i, 2].Value = "3";
                                worksheet.Cells[i, 3].Value = (x.L3 == null) ? "" : x.L3;
                                worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                oldl3 = x.L3;
                                i = i + 2;
                            }
                            if ((l4 != "") && (l4 != oldl4))
                            {
                                //worksheet.Cells[i, 1].Value = (x.l4Ref == null) ? "" : x.l4Ref;
                                worksheet.Cells[i, 2].Value = "4";
                                worksheet.Cells[i, 3].Value = (x.L4 == null) ? "" : x.L4;
                                worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                oldl4 = x.L4;
                                i = i + 2;
                            }
                            if ((l5 != "") && (l5 != oldl5))
                            {
                                //worksheet.Cells[i, 1].Value = (x.l5Ref == null) ? "" : x.l5Ref;
                                worksheet.Cells[i, 2].Value = "5";
                                worksheet.Cells[i, 3].Value = (x.L5 == null) ? "" : x.L5;
                                worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                oldl5 = x.L5;
                                i = i + 2;
                            }
                            if ((l6 != "") && (l6 != oldl6))
                            {
                                //worksheet.Cells[i, 1].Value = (x.l6Ref == null) ? "" : x.l6Ref;
                                worksheet.Cells[i, 2].Value = "6";
                                worksheet.Cells[i, 3].Value = (x.L6 == null) ? "" : x.L6;
                                worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                oldl6 = x.L6;
                                i = i + 2;
                            }

                            if ((C != OldC) | (OldC == ""))
                            {
                                if (((x.C1 == null) ? "" : x.C1) != "")
                                {
                                    //worksheet.Cells[i, 1].Value = (x.c1Ref == null) ? "" : x.c1Ref;
                                    worksheet.Cells[i, 2].Value = "C";
                                    worksheet.Cells[i, 3].Value = (x.C1 == null) ? "" : x.C1;
                                    worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                    i = i + 2;
                                    OldC = C;
                                }
                                if (((x.C2 == null) ? "" : x.C2) != "")
                                {
                                    //worksheet.Cells[i, 1].Value = (x.c2Ref == null) ? "" : x.c2Ref;
                                    worksheet.Cells[i, 2].Value = "C";
                                    worksheet.Cells[i, 3].Value = (x.C2 == null) ? "" : x.C2;
                                    worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                    i = i + 2;
                                }
                                if (((x.C3 == null) ? "" : x.C3) != "")
                                {
                                    //worksheet.Cells[i, 1].Value = (x.c3Ref == null) ? "" : x.c3Ref;
                                    worksheet.Cells[i, 2].Value = "C";
                                    worksheet.Cells[i, 3].Value = (x.C3 == null) ? "" : x.C3;
                                    worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                    i = i + 2;
                                }
                                if (((x.C4 == null) ? "" : x.C4) != "")
                                {
                                    //worksheet.Cells[i, 1].Value = (x.c4Ref == null) ? "" : x.c4Ref;
                                    worksheet.Cells[i, 2].Value = "C";
                                    worksheet.Cells[i, 3].Value = (x.C4 == null) ? "" : x.C4;
                                    worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                    i = i + 2;
                                }
                                if (((x.C5 == null) ? "" : x.C5) != "")
                                {
                                    //worksheet.Cells[i, 1].Value = (x.c5Ref == null) ? "" : x.c5Ref;
                                    worksheet.Cells[i, 2].Value = "C";
                                    worksheet.Cells[i, 3].Value = (x.C5 == null) ? "" : x.C5;
                                    worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                    i = i + 2;
                                }
                                if (((x.C6 == null) ? "" : x.C6) != "")
                                {
                                    //worksheet.Cells[i, 1].Value = (x.c6Ref == null) ? "" : x.c6Ref;
                                    worksheet.Cells[i, 2].Value = "C";
                                    worksheet.Cells[i, 3].Value = (x.C5 == null) ? "" : x.C5;
                                    worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                    i = i + 2;
                                }
                            }

                            worksheet.Cells[i, 1].Value = (x.ItemO == null) ? "" : x.ItemO;
                            worksheet.Cells[i, 3].Value = (x.DescriptionO == null) ? "" : x.DescriptionO;
                            worksheet.Cells[i, 4].Value = (x.UnitO == null) ? "" : x.UnitO;
                            worksheet.Cells[i, 5].Value = (x.QtyO == null) ? "" : x.QtyO;
                            worksheet.Cells[i, 5].Style.Numberformat.Format = "#,##0.0";
                            worksheet.Cells[i, 6].Value = (x.UnitRateO == null) ? "" : x.UnitRateO;
                            worksheet.Cells[i, 6].Style.Numberformat.Format = "#,##0.0";
                            worksheet.Cells[i, 7].Value = (x.TotalPriceO == null) ? "" : x.TotalPriceO;
                            worksheet.Cells[i, 7].Style.Numberformat.Format = "#,##0.0";

                            if (byBoq == 1)
                            {
                                worksheet.Cells[i, 8].Formula = "= (F" + i + ") - (F" + i + "*" + "G" + i + "/100)";
                                worksheet.Cells[i, 8].Style.Numberformat.Format = "#,##0.0";
                                worksheet.Cells[i, 9].Formula = "=E" + i + "*" + "H" + i;
                                worksheet.Cells[i, 9].Style.Numberformat.Format = "#,##0.0";
                                worksheet.Cells[i, 6].Style.Locked = false;
                                worksheet.Cells[i, 7].Style.Locked = false;
                                worksheet.Cells[i, 10].Style.Locked = false;
                            }
                            i = i + 1;
                            OldBoq = Boq;
                        }

                        if (byBoq != 1)
                        {
                            worksheet.Cells[i, 8].Value = (x.BoqCtg == null) ? "" : x.BoqCtg;
                            worksheet.Cells[i, 9].Value = (x.resCode == null) ? "" : x.resCode;
                            worksheet.Cells[i, 10].Value = (x.ResDescription == null) ? "" : x.ResDescription;
                            worksheet.Cells[i, 11].Value = (x.BoqUnitMesure == null) ? "" : x.BoqUnitMesure;
                            worksheet.Cells[i, 12].Value = (x.BoqQty == null) ? "" : x.BoqQty;
                            worksheet.Cells[i, 12].Style.Numberformat.Format = "#,##0.0";
                            worksheet.Cells[i, 13].Value = (x.BoqUprice == null) ? "" : x.BoqUprice;
                            worksheet.Cells[i, 13].Style.Numberformat.Format = "#,##0.0";
                            //worksheet.Cells[i, 13].Formula = "= (K" + i + ") - (K" + i + "*" + "L" + i + "/100)";
                            worksheet.Cells[i, 14].Value = (x.BoqTotalPrice == null) ? "" : x.BoqTotalPrice;
                            worksheet.Cells[i, 14].Style.Numberformat.Format = "#,##0.0";
                        }
                        i++;
                    }

                    var p = _context.TblParameters.FirstOrDefault();
                    string ProjectName = p.Project;

                    xlPackage.Save();
                    stream.Position = 0;
                    excelName = $"{ProjectName}-Items Not Assigned-{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";

                    if (File.Exists(excelName))
                        File.Delete(excelName);

                    excelName = excelName.Replace("/", "-");
                    xlPackage.SaveAs(excelName);

                    //excelName = "Package-Aluminum Doors and Windows.xlsx";
                    
                }

            }

            if (excelName!="")
                return excelName;
            else
                return null;
        }

        public bool updateOriginalBoqQty(OriginalBoqModel boq)
        {
            var result = _context.TblOriginalBoqVds.Where(x => x.ItemO == boq.ItemO).FirstOrDefault();
            result.QtyScope = boq.ScopeQtyO;
            
            if (result != null)
            {
                _context.TblOriginalBoqVds.Update(result);
                _context.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool updateBoqResQty(BoqModel res)
        {
            var result = _context.TblBoqVds.Where(x => x.BoqSeq == res.BoqSeq).FirstOrDefault();
            result.BoqQtyScope = res.BoqScopeQty;

            if (result != null)
            {
                _context.TblBoqVds.Update(result);
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
                //    var data = _context.TblOriginalBoqVds.Where(x => x.RowNumber == item.RowNumber).FirstOrDefault();
                //    data.Scope = item.Scope;

                //    _context.TblOriginalBoqVds.Update(data);               
                //}
                //_context.SaveChanges();

                var lstBoqo = (from a in origBoqList
                               join b in _context.TblOriginalBoqVds on a.RowNumber equals b.RowNumber
                               select b).ToList();

                foreach (var item in origBoqList)
                {
                    lstBoqo.Where(d => d.RowNumber == item.RowNumber).First().ObTradeDesc = tradeDesc;
                }
                _context.TblOriginalBoqVds.UpdateRange(lstBoqo);
                _context.SaveChanges();
            }
            return true;
        }

        public async Task<string> ExportExcelPackagesCost(int withBoq,string costDB, SearchInput input)
        {
            string excelName = "";

            var lstBoqs = await GetBoqWithRessourcesAsync(input, costDB, 5);
            var lstAssignedPackage = lstBoqs.Select(x=> x.ScopeO).ToList();


            var packList = (from p in _mdbcontext.TblPackages
                            where (lstAssignedPackage.Count == 0 || lstAssignedPackage.Contains(p.PkgeId))
                            select new packagesList
                            {
                                PkgeId = (int)p.PkgeId,
                                PkgeName = p.PkgeName
                            }).ToList();

            //var pckgesCost = _context.TblBoqVds.Where(x=>x.BoqScope>0)
            //    .GroupBy(x => new { x.BoqScope})
            //    .Select(p => new packagesList
            //    {
            //        PkgeId = p.First().GroupId.HasValue ? p.First().GroupId.Value : 0,
            //        TotalBudget = p.Sum(c => c.BoqQty * c.BoqUprice)
            //    }).ToList();

            var pckgesCost = from e in _context.TblBoqVds.Where(x => x.BoqScope > 0)
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

            var parm = _context.TblParameters.FirstOrDefault();
            int simsomProjID = (parm.SimsomProjId == null) ? 0 : (int)parm.SimsomProjId;  

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("Packages");
                worksheet.Columns.AutoFit();
                worksheet.Protection.IsProtected = false;

                int r = 1;

                worksheet.Cells[r, 1].Value = "Project_ID";
                worksheet.Cells[r, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[r, 2].Value = "Package_ID";
                worksheet.Cells[r, 2].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[r, 3].Value = "Assigned Package";
                worksheet.Column(3).Width = 30;
                worksheet.Columns[3].Style.WrapText = true;
                worksheet.Cells[1, 3].Style.Font.Bold = true;
                worksheet.Cells[1, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                worksheet.Cells[r, 4].Value = "Estimated Dry cost";
                worksheet.Cells[1,4].Style.Font.Bold = true;
                worksheet.Column(4).Width = 20;      
                worksheet.Columns[4].Style.WrapText = true;

                if (withBoq==1)
                {
                    worksheet.Cells[r, 5].Value = "Item";
                    worksheet.Column(5).Width = 22;
                    worksheet.Cells[r, 6].Value = "Bill Description";
                    worksheet.Column(6).Width = 50;
                    worksheet.Columns[6].Style.WrapText = true;
                    worksheet.Cells[r, 7].Value = "Unit";
                    worksheet.Cells[r, 8].Value = "Qty";
                    worksheet.Cells[r, 9].Value = "Unit Price";
                    worksheet.Cells[r, 10].Value = "Total Price";

                    worksheet.Cells[r, 11].Value = "Res Type";
                    worksheet.Cells[r, 12].Value = "Res Code";
                    worksheet.Column(13).Width = 40;
                    worksheet.Columns[13].Style.WrapText = true;
                    worksheet.Column(13).AutoFit();
                    worksheet.Cells[r, 13].Value = "Res Description";
                    worksheet.Cells[r, 14].Value = "Res Unit";
                    worksheet.Cells[r, 15].Value = "Res Qty";
                    worksheet.Column(15).AutoFit();
                    worksheet.Cells[r, 16].Value = "Res U Price";
                    worksheet.Column(16).AutoFit();
                    worksheet.Cells[r, 17].Value = "Res T Price";
                    worksheet.Column(17).AutoFit();
                    worksheet.Cells[r, 18].Value = "Res Div";
                    worksheet.Column(18).AutoFit();
                    worksheet.Cells[r, 19].Value = "Level 2";
                    worksheet.Column(19).Width = 50;
                    worksheet.Columns[19].Style.WrapText = true;
                    worksheet.Cells[r, 20].Value = "Level 3";
                    worksheet.Column(20).Width = 50;
                    worksheet.Columns[20].Style.WrapText = true;
                    worksheet.Cells[r, 21].Value = "Level 4";
                    worksheet.Column(21).Width = 50;
                    worksheet.Columns[21].Style.WrapText = true;
                }

                r = 2;
                foreach (var x in packgesCost)
                {
                    if (withBoq == 0) {
                        worksheet.Cells[r, 1].Value = simsomProjID;
                        worksheet.Cells[r, 2].Value = (x.PkgeName == null) ? "" : x.PkgeId;
                        worksheet.Cells[r, 3].Value = (x.PkgeName == null) ? "" : x.PkgeName;
                        worksheet.Cells[r, 4].Value = (x.TotalBudget == null) ? 0 : x.TotalBudget;
                        worksheet.Cells[r, 4].Style.Numberformat.Format = "#,##0.0";
                        worksheet.SelectedRange[r, 1, r, 4].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                        worksheet.SelectedRange[r, 1, r, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
                    }
                    else
                    {
                        //var arr = new string[] { };
                        //SearchInput input1 = new SearchInput() { Package = (int)x.PkgeId, BOQDiv=arr, RESDiv =arr, RESType = arr, boqLevel2=arr, boqLevel3 = arr,
                        //    boqLevel4 = arr, boqResourceSeq=arr, BOQItem ="",
                        //    BOQDesc="",
                        //    RESDesc="",
                        //    RESPackage="",
                        //    FromRow="",
                        //    ToRow="",
                        //    SheetDesc="",
                        //    itemO="",
                        //    obTradeDesc="",
                        //    isItemsAssigned=0,
                        //    isRessourcesAssigned=0,
                        //    boqStatus=""
                        //};
                        
                        input.Package = (int)x.PkgeId;
                        var lstBoq = await GetBoqWithRessourcesAsync(input, costDB, 4);

                        if (lstBoq != null)
                        {
                            int j;
                            string Boq = "", OldBoq = ""; // C = "", OldC = "", l1 = "", l2 = "", l3 = "", l4 = "", l5 = "", l6 = "", oldl1 = "", oldl2 = "", oldl3 = "", oldl4 = "", oldl5 = "", oldl6 = "";

                            r ++;
                            foreach (var y in lstBoq)
                            {
                                Boq = y.ItemO;
                                //C = y.c1;
                                //l1 = (y.l1 == null) ? "" : y.l1;
                                //l2 = (y.l2 == null) ? "" : y.l2;
                                //l3 = (y.l3 == null) ? "" : y.l3;
                                //l4 = (y.l4 == null) ? "" : y.l4;
                                //l5 = (y.l5 == null) ? "" : y.l5;
                                //l6 = (y.l6 == null) ? "" : y.l6;

                                if ((Boq != OldBoq) || (OldBoq == ""))
                                {
                                    //if ((l1 != "") && (l1 != oldl1))
                                    //{
                                    //    worksheet.Cells[r, 1].Value = (y.l1Ref == null) ? "" : y.l1Ref;
                                    //    worksheet.Cells[r, 2].Value = "1";
                                    //    worksheet.Cells[r, 3].Value = (y.l1 == null) ? "" : y.l1;
                                    //    worksheet.SelectedRange[r, 3].Style.Font.Bold = true;
                                    //    oldl1 = y.l1;
                                    //    r = r + 2;
                                    //}
                                    //if ((l2 != "") && (l2 != oldl2))
                                    //{
                                    //    worksheet.Cells[r, 1].Value = (y.l2Ref == null) ? "" : y.l2Ref;
                                    //    worksheet.Cells[r, 2].Value = "2";
                                    //    worksheet.Cells[r, 3].Value = (y.l2 == null) ? "" : y.l2;
                                    //    worksheet.SelectedRange[r, 3].Style.Font.Bold = true;
                                    //    oldl2 = y.l2;
                                    //    r = r + 2;
                                    //}
                                    //if ((l3 != "") && (l3 != oldl3))
                                    //{
                                    //    worksheet.Cells[r, 1].Value = (y.l3Ref == null) ? "" : y.l3Ref;
                                    //    worksheet.Cells[r, 2].Value = "3";
                                    //    worksheet.Cells[r, 3].Value = (y.l3 == null) ? "" : y.l3;
                                    //    worksheet.SelectedRange[r, 3].Style.Font.Bold = true;
                                    //    oldl3 = y.l3;
                                    //    r = r + 2;
                                    //}
                                    //if ((l4 != "") && (l4 != oldl4))
                                    //{
                                    //    worksheet.Cells[r, 1].Value = (y.l4Ref == null) ? "" : y.l4Ref;
                                    //    worksheet.Cells[r, 2].Value = "4";
                                    //    worksheet.Cells[r, 3].Value = (y.l4 == null) ? "" : y.l4;
                                    //    worksheet.SelectedRange[r, 3].Style.Font.Bold = true;
                                    //    oldl4 = y.l4;
                                    //    r = r + 2;
                                    //}
                                    //if ((l5 != "") && (l5 != oldl5))
                                    //{
                                    //    worksheet.Cells[r, 1].Value = (y.l5Ref == null) ? "" : y.l5Ref;
                                    //    worksheet.Cells[r, 2].Value = "5";
                                    //    worksheet.Cells[r, 3].Value = (y.l5 == null) ? "" : y.l5;
                                    //    worksheet.SelectedRange[r, 3].Style.Font.Bold = true;
                                    //    oldl5 = y.l5;
                                    //    r = r + 2;
                                    //}
                                    //if ((l6 != "") && (l6 != oldl6))
                                    //{
                                    //    worksheet.Cells[r, 1].Value = (y.l6Ref == null) ? "" : y.l6Ref;
                                    //    worksheet.Cells[r, 2].Value = "6";
                                    //    worksheet.Cells[r, 3].Value = (y.l6 == null) ? "" : y.l6;
                                    //    worksheet.SelectedRange[r, 3].Style.Font.Bold = true;
                                    //    oldl6 = y.l6;
                                    //    r = r + 2;
                                    //}

                                    //if ((C != OldC) | (OldC == ""))
                                    //{
                                    //    if (((y.c1 == null) ? "" : y.c1) != "")
                                    //    {
                                    //        worksheet.Cells[r, 1].Value = (y.c1Ref == null) ? "" : y.c1Ref;
                                    //        worksheet.Cells[r, 2].Value = "C";
                                    //        worksheet.Cells[r, 3].Value = (y.c1 == null) ? "" : y.c1;
                                    //        worksheet.SelectedRange[r, 3].Style.Font.Bold = true;
                                    //        r = r + 2;
                                    //        OldC = C;
                                    //    }
                                    //    if (((y.c2 == null) ? "" : y.c2) != "")
                                    //    {
                                    //        worksheet.Cells[r, 1].Value = (y.c2Ref == null) ? "" : y.c2Ref;
                                    //        worksheet.Cells[r, 2].Value = "C";
                                    //        worksheet.Cells[r, 3].Value = (y.c2 == null) ? "" : y.c2;
                                    //        worksheet.SelectedRange[r, 3].Style.Font.Bold = true;
                                    //        r = r + 2;
                                    //    }
                                    //    if (((y.c3 == null) ? "" : y.c3) != "")
                                    //    {
                                    //        worksheet.Cells[r, 1].Value = (y.c3Ref == null) ? "" : y.c3Ref;
                                    //        worksheet.Cells[r, 2].Value = "C";
                                    //        worksheet.Cells[r, 3].Value = (y.c3 == null) ? "" : y.c3;
                                    //        worksheet.SelectedRange[r, 3].Style.Font.Bold = true;
                                    //        r = r + 2;
                                    //    }
                                    //    if (((y.c4 == null) ? "" : y.c4) != "")
                                    //    {
                                    //        worksheet.Cells[r, 1].Value = (y.c4Ref == null) ? "" : y.c4Ref;
                                    //        worksheet.Cells[r, 2].Value = "C";
                                    //        worksheet.Cells[r, 3].Value = (y.c4 == null) ? "" : y.c4;
                                    //        worksheet.SelectedRange[r, 3].Style.Font.Bold = true;
                                    //        r = r + 2;
                                    //    }
                                    //    if (((y.c5 == null) ? "" : y.c5) != "")
                                    //    {
                                    //        worksheet.Cells[r, 1].Value = (y.c5Ref == null) ? "" : y.c5Ref;
                                    //        worksheet.Cells[r, 2].Value = "C";
                                    //        worksheet.Cells[r, 3].Value = (y.c5 == null) ? "" : y.c5;
                                    //        worksheet.SelectedRange[r, 3].Style.Font.Bold = true;
                                    //        r = r + 2;
                                    //    }
                                    //    if (((y.c6 == null) ? "" : y.c6) != "")
                                    //    {
                                    //        worksheet.Cells[r, 1].Value = (y.c6Ref == null) ? "" : y.c6Ref;
                                    //        worksheet.Cells[r, 2].Value = "C";
                                    //        worksheet.Cells[r, 3].Value = (y.c5 == null) ? "" : y.c5;
                                    //        worksheet.SelectedRange[r, 3].Style.Font.Bold = true;
                                    //        r = r + 2;
                                    //    }
                                    //}
                                    worksheet.Cells[r, 1].Value = simsomProjID;
                                    worksheet.Cells[r, 2].Value = (x.PkgeName == null) ? "" : x.PkgeId;
                                    worksheet.Cells[r, 3].Value = (x.PkgeName == null) ? "" : x.PkgeName;
                                    worksheet.Cells[r, 4].Value = (x.TotalBudget == null) ? 0 : x.TotalBudget;
                                    worksheet.Cells[r, 4].Style.Numberformat.Format = "#,##0.0";
                                    worksheet.SelectedRange[r, 1, r, 4].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                    worksheet.SelectedRange[r, 1, r, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                                    worksheet.SelectedRange[r, 5,r, 10].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                    worksheet.SelectedRange[r, 5, r, 10].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                                    worksheet.Cells[r, 5].Value = (y.ItemO == null) ? "" : y.ItemO;
                                    worksheet.Cells[r, 6].Value = (y.DescriptionO == null) ? "" : y.DescriptionO;
                                    worksheet.Cells[r, 7].Value = (y.UnitO == null) ? "" : y.UnitO;
                                    worksheet.Cells[r, 8].Value = (y.QtyO == null) ? "" : y.QtyO;
                                    worksheet.Cells[r, 8].Style.Numberformat.Format = "#,##0.0";
                                    worksheet.Cells[r, 9].Value = (y.UnitRateO == null) ? "" : y.UnitRateO;
                                    worksheet.Cells[r, 9].Style.Numberformat.Format = "#,##0.0";
                                    worksheet.Cells[r, 10].Value = (y.TotalPriceO == null) ? "" : y.TotalPriceO;
                                    worksheet.Cells[r, 10].Style.Numberformat.Format = "#,##0.0";

                                    //if (byBoq == 1)
                                    //{
                                        //worksheet.Cells[r, 8].Formula = "= (F" + r + ") - (F" + r + "*" + "G" + r + "/100)";
                                        //worksheet.Cells[r, 8].Style.Numberformat.Format = "#,##0.0";
                                        //worksheet.Cells[r, 9].Formula = "=E" + r + "*" + "H" + r;
                                        //worksheet.Cells[r, 9].Style.Numberformat.Format = "#,##0.0";
                                    //}
                                    r = r + 1;
                                    OldBoq = Boq;
                                }

                                //if (byBoq != 1)
                                //{

                                //worksheet.SelectedRange[r, 3, r, 8].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                worksheet.Cells[r, 1].Value = simsomProjID;
                                worksheet.Cells[r, 2].Value = (x.PkgeName == null) ? "" : x.PkgeId;
                                worksheet.Cells[r, 3].Value = (x.PkgeName == null) ? "" : x.PkgeName;
                                worksheet.Cells[r, 4].Value = (x.TotalBudget == null) ? 0 : x.TotalBudget;
                                worksheet.Cells[r, 4].Style.Numberformat.Format = "#,##0.0";
                                worksheet.SelectedRange[r, 1, r, 4].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                                worksheet.SelectedRange[r, 1, r, 4].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);

                                worksheet.Cells[r, 5].Value = (y.ItemO == null) ? "" : y.ItemO;
                                worksheet.Cells[r, 6].Value = (y.DescriptionO == null) ? "" : y.DescriptionO;
                                worksheet.Cells[r, 7].Value = (y.UnitO == null) ? "" : y.UnitO;
                                worksheet.Cells[r, 8].Value = (y.QtyO == null) ? "" : y.QtyO;
                                worksheet.Cells[r, 8].Style.Numberformat.Format = "#,##0.0";
                                worksheet.Cells[r, 9].Value = (y.UnitRateO == null) ? "" : y.UnitRateO;
                                worksheet.Cells[r, 9].Style.Numberformat.Format = "#,##0.0";
                                worksheet.Cells[r, 10].Value = (y.TotalPriceO == null) ? "" : y.TotalPriceO;
                                worksheet.Cells[r, 10].Style.Numberformat.Format = "#,##0.0";

                                worksheet.Cells[r, 11].Value = (y.BoqCtg == null) ? "" : y.BoqCtg;
                                worksheet.Cells[r, 12].Value = (y.resCode == null) ? "" : y.resCode;
                                worksheet.Cells[r, 13].Value = (y.ResDescription == null) ? "" : y.ResDescription;
                                worksheet.Cells[r, 14].Value = (y.BoqUnitMesure == null) ? "" : y.BoqUnitMesure;
                                worksheet.Cells[r, 15].Value = (y.BoqQty == null) ? "" : y.BoqQty;
                                worksheet.Cells[r, 15].Style.Numberformat.Format = "#,##0.0";
                                worksheet.Cells[r, 16].Value = (y.BoqUprice == null) ? "" : y.BoqUprice;
                                worksheet.Cells[r, 16].Style.Numberformat.Format = "#,##0.0";
                                //worksheet.Cells[r, 13].Formula = "= (K" + r + ") - (K" + r + "*" + "L" + r + "/100)";
                                worksheet.Cells[r, 17].Value = (y.BoqTotalPrice == null) ? "" : y.BoqTotalPrice;
                                worksheet.Cells[r, 17].Style.Numberformat.Format = "#,##0.0";
                                worksheet.Cells[r, 18].Value = (y.BoqDiv == null) ? "" : y.BoqDiv;
                                worksheet.Cells[r, 19].Value = (y.L2 == null) ? "" : y.L2;
                                worksheet.Cells[r, 20].Value = (y.L3 == null) ? "" : y.L3;
                                worksheet.Cells[r, 21].Value = (y.L4 == null) ? "" : y.L4;

                                r++;
                                //}                               
                            }
                        }
                        //////////////
                    }
                    r++;
                }

                var p = _context.TblParameters.FirstOrDefault();
                string ProjectName = p.Project;

                xlPackage.Save();
                stream.Position = 0;
                if (withBoq == 1)               
                    excelName = $"{ProjectName}-Packages Dry Cost with BOQ-{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";
                else
                    excelName = $"{ProjectName}-Packages Dry Cost-{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";

                if (File.Exists(excelName))
                    File.Delete(excelName);

                excelName = excelName.Replace("/", "-");
                xlPackage.SaveAs(excelName);
            }
            return excelName;
        }

        #region Packages
        public DataTablesResponse<Package> GetPackages(DataTablesRequest dtRequest)
        {
            var sortColumnName = dtRequest.SortCol;
            var sortDirection = dtRequest.SortDirVal;
            var skip = dtRequest.Start;
            var take = dtRequest.Length;

            var result = (from b in _mdbcontext.TblPackages
                         select new Package
                          {
                              IDPkge = b.PkgeId,
                              PkgeName = b.PkgeName,
                              Division = b.Division
                          }).ToList();

            var totalRecords = result.Count;

            if (dtRequest.SearchVal != null)
            {
                result = result.Where(x => string.Concat(x.PkgeName.ToUpper()).Contains(dtRequest.SearchVal.ToUpper())).ToList();
            }

            var list = result.AsQueryable().OrderBy($"{sortColumnName} {sortDirection}").Skip(skip).Take(take);

            return new DataTablesResponse<Package>
            {
                Data = list.ToList(),
                RecordsTotal = totalRecords,
                RecordsFiltered = result.Count
            };
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

        public async Task<ResponseModel<bool>> DeletePackage(int id)
        {
            var packList = await _context.TblSupplierPackages.Where(x => x.SpPackageId == id).ToListAsync();
            var packOriginalBoq = await _context.TblOriginalBoqVds.Where(x => x.Scope == id).ToListAsync();
            var packBoq = await _context.TblBoqVds.Where(x => x.BoqScope == id).ToListAsync();

            if (!packList.Any() && !packOriginalBoq.Any() && !packBoq.Any())
            {
                var result = await _mdbcontext.TblPackages.Where(x => x.PkgeId == id).FirstOrDefaultAsync();
                if (result != null)
                {
                    _mdbcontext.TblPackages.Remove(result);
                    _mdbcontext.SaveChanges();
                    return new ResponseModel<bool>
                    {
                        Message = "Package deleted successfully"
                    };
                }
                else
                {
                    return new ResponseModel<bool>
                    {
                        Success = false,
                        Message = "Package not found"
                    };
                }

            }
            else
            {
                return new ResponseModel<bool>
                {
                    Success = false,
                    Message = "Package is linked to Boq"
                };
            }
                
        }
        #endregion

        public async Task<DataTablesResponse<BoqModel>> GetBoqResourceRecords(DataTablesRequest dtRequest)
        {
            var input = dtRequest.input;
            var condQuery = (from o in _context.TblOriginalBoqVds
                                               join b in _context.TblBoqVds on o.ItemO equals b.BoqItem
                                               join r in _context.TblResources on b.BoqResSeq equals r.ResSeq
                                             where dtRequest.BoqItems.Contains(b.BoqItem)
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
            var total = condQuery.Count();

            double FinalTotalPrice = 0;
            double FinalUnitPrice = 0;

            //if (input.BOQDiv.Length > 0) condQuery = condQuery.Where(w => input.BOQDiv.Contains(w.SectionO));
            //if (!string.IsNullOrEmpty(input.BOQItem)) condQuery = condQuery.Where(w => w.ItemO.ToLower().Contains(input.BOQItem.ToLower()));
            //if (!string.IsNullOrEmpty(input.BOQDesc)) condQuery = condQuery.Where(w => w.DescriptionO.ToLower().Contains(input.BOQDesc.ToLower()));
            //if (!string.IsNullOrEmpty(input.SheetDesc)) condQuery = condQuery.Where(w => w.ObSheetDesc == input.SheetDesc);
            //if (!string.IsNullOrEmpty(input.FromRow) && !string.IsNullOrEmpty(input.ToRow)) condQuery = condQuery.Where(w => w.RowNumber >= int.Parse(input.FromRow) && w.RowNumber <= int.Parse(input.ToRow));
            //if (input.Package > 0) condQuery = condQuery.Where(w => w.Scope == input.Package);
            //if (input.RESDiv.Length > 0) condQuery = condQuery.Where(w => input.RESDiv.Contains(w.BoqDiv));
            //if (input.RESType.Length > 0) condQuery = condQuery.Where(w => input.RESType.Contains(w.BoqCtg));
            //if (!string.IsNullOrEmpty(input.RESPackage)) condQuery = condQuery.Where(w => w.BoqPackage == input.RESPackage);
            //if (!string.IsNullOrEmpty(input.RESDesc)) condQuery = condQuery.Where(w => w.ResDescription.ToLower().Contains(input.RESDesc.ToLower()));
            //if (input.Package > 0) condQuery = condQuery.Where(w => w.BoqScope == input.Package);
            //if (input.boqLevel2.Length > 0) condQuery = condQuery.Where(w => input.boqLevel2.Contains(w.L2));
            //if (input.boqLevel3.Length > 0) condQuery = condQuery.Where(w => input.boqLevel3.Contains(w.L3));
            //if (input.boqLevel4.Length > 0) condQuery = condQuery.Where(w => input.boqLevel4.Contains(w.L4));
            //if (!string.IsNullOrEmpty(input.obTradeDesc)) condQuery = condQuery.Where(w => w.ObTradeDesc.ToLower().Contains(input.obTradeDesc.ToLower()));
            //if (input.boqResourceSeq.Length > 0) condQuery = condQuery.Where(w => input.boqResourceSeq.Contains(w.BoqResSeq));

            switch (input.isRessourcesAssigned)
            {
                case 1:
                    condQuery = condQuery.Where(w => w.BoqScope > 0);
                    break;
                case 2:
                    condQuery = condQuery.Where(w => w.BoqScope == null || w.BoqScope == 0);
                    break;
                default:
                    break;
            }
            var filtered = condQuery.Count();


            var BoqSeqs = condQuery.Where(x => dtRequest.SelectedBoqItems.Contains(x.BoqItem)).Select(x => x.BoqSeq).ToList();

            var list = await condQuery.Skip(dtRequest.Start).Take(dtRequest.Length).OrderBy(x=>x.BoqItem).ToListAsync();


            
            foreach (var item in list)
            {
                item.IsSelected = dtRequest.SelectedBoqItems.Contains(item.BoqItem);

            }

            //Final Totals
            FinalTotalPrice = condQuery.Where(x=> dtRequest.SelectedBoqItems.Contains(x.BoqItem)).Sum(y=>y.BoqScopeQty.Value *  y.BoqUprice.Value);


            var response = new DataTablesResponse<BoqModel>
            {
                Data = list,
                RecordsFiltered = filtered,
                RecordsTotal = total,
                FinalTotalPrice = FinalTotalPrice,
                FinalUnitPrice = FinalUnitPrice,
                BoqSeqs = BoqSeqs
            };

            return response;
        }

    }

    
}
