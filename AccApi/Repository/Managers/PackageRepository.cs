﻿using AccApi.Repository.Interfaces;
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
using System.Data;
using System.Linq.Dynamic.Core;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

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

            if (blankInput)
            {
                //List<BoqRessourcesList> res = new List<BoqRessourcesList>();
                return null;
            }

            //Div_List
            var dtDiv = new DataTable();
            dtDiv.Columns.Add("Div", typeof(string));
            foreach (var val in input.BOQDiv)
            dtDiv.Rows.Add(val.ToString());
            //DivRes_List
            var dtDivRes = new DataTable();
            dtDivRes.Columns.Add("DivRes", typeof(string));
            foreach (var val in input.RESDiv)
            dtDivRes.Rows.Add(val.ToString());
            //L2_List
            var dtL2 = new DataTable();
            dtL2.Columns.Add("L2", typeof(string));
            foreach (var val in input.boqLevel2)
            dtL2.Rows.Add(val.ToString());
            //L3_List
            var dtL3 = new DataTable();
            dtL3.Columns.Add("L3", typeof(string));
            foreach (var val in input.boqLevel3)
            dtL3.Rows.Add(val.ToString());
            //L4_List
            var dtL4 = new DataTable();
            dtL4.Columns.Add("L4", typeof(string));
            foreach (var val in input.boqLevel4)
            dtL4.Rows.Add(val.ToString());
            //Resources_List
            var dtRes = new DataTable();
            dtRes.Columns.Add("Resources", typeof(string));
            foreach (var val in input.boqResourceSeq)
            dtRes.Rows.Add(val.ToString());
            //ResType_List
            var dtResType = new DataTable();
            dtResType.Columns.Add("ResType", typeof(string));
            foreach (var val in input.RESDiv)
            dtResType.Rows.Add(val.ToString());

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
                            BoqStatus = x["BoqStatus"] != DBNull.Value ? (string)x["BoqStatus"] : null
                        });
                    break;

                case 2: case 3:
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
                        l1 = x["L1"] != DBNull.Value ? (string)x["L1"] : null,
                        l2 = x["L2"] != DBNull.Value ? (string)x["L2"] : null,
                        l3 = x["L3"] != DBNull.Value ? (string)x["L3"] : null,
                        resCode= (string)x["BoqPackage"]
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

            //IEnumerable<BoqRessourcesList> condQuery = (from o in _context.TblOriginalBoqs
            //                                            join b in _context.TblBoqs on o.ItemO equals b.BoqItem
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
                                                   AssignedPackage = "",
                                                   TotalUnitPrice= (b.BoqUprice * b.BoqQty)/ o.UnitRate
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
        public async Task<string> ExportBoqExcel(SearchInput input, string costDB)
        {
            
            var lstBoq =await GetBoqWithRessourcesAsync(input, costDB,2);

            string excelName = "";

            if (lstBoq != null)
            {

                //var lstBoq = (from a in input.AssignBoqList
                //              join b in _context.TblBoqs on a.BoqSeq equals b.BoqSeq
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
                    //worksheet.Columns[2].Style.WrapText = true;
                    //worksheet.Column(2).AutoFit();
                    worksheet.Cells[i, 3].Value = "Unit";
                    worksheet.Cells[i, 4].Value = "Type";
                    worksheet.Cells[i, 5].Value = "Qty";
                    worksheet.Cells[i, 6].Value = "Unit Price";
                    worksheet.Cells[i, 7].Value = "Total Price";
                    worksheet.Cells[i, 8].Value = "Div";
                    worksheet.Cells[i, 9].Value = "Level 1";
                    worksheet.Column(9).Width = 30;
                    worksheet.Cells[i, 10].Value = "Level 2";
                    worksheet.Column(10).Width = 30;
                    worksheet.Cells[i, 11].Value = "Level 3";
                    worksheet.Column(11).Width = 30;

                    i = 4;
                    foreach (var x in lstBoq)
                    {
                        worksheet.Cells[i, 1].Value = (x.ItemO == null) ? "" : x.ItemO;
                        worksheet.Cells[i, 2].Value = (x.ResDescription == null) ? "" : x.ResDescription;
                        worksheet.Cells[i, 3].Value = (x.BoqUnitMesure == null) ? "" : x.BoqUnitMesure;
                        worksheet.Cells[i, 4].Value = (x.BoqCtg == null) ? "" : x.BoqCtg;
                        worksheet.Cells[i, 5].Value = (x.BoqQty == null) ? "" : x.BoqQty;
                        worksheet.Cells[i, 6].Value = (x.BoqUprice == null) ? "" : x.BoqUprice;
                        worksheet.Cells[i, 6].Style.Numberformat.Format = "#,##0.0";
                        worksheet.Cells[i, 7].Value = (x.BoqTotalPrice == null) ? "" : x.BoqTotalPrice;
                        worksheet.Cells[i, 7].Style.Numberformat.Format = "#,##0.0";
                        worksheet.Cells[i, 8].Value = (x.BoqDiv == null) ? "" : x.BoqDiv;
                        worksheet.Cells[i, 9].Value = (x.l1 == null) ? "" : x.l1;
                        worksheet.Cells[i, 10].Value = (x.l2 == null) ? "" : x.l2;
                        worksheet.Cells[i, 11].Value = (x.l3 == null) ? "" : x.l3;
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

        public async Task<string> ExportNotAssigned(SearchInput input, string costDB)
        {
            string excelName = "";
            int byBoq = 0;

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
                        C = x.c1;
                        l1 = (x.l1 == null) ? "" : x.l1;
                        l2 = (x.l2 == null) ? "" : x.l2;
                        l3 = (x.l3 == null) ? "" : x.l3;
                        l4 = (x.l4 == null) ? "" : x.l4;
                        l5 = (x.l5 == null) ? "" : x.l5;
                        l6 = (x.l6 == null) ? "" : x.l6;

                        if ((Boq != OldBoq) || (OldBoq == ""))
                        {
                            if ((l1 != "") && (l1 != oldl1))
                            {
                                worksheet.Cells[i, 1].Value = (x.l1Ref == null) ? "" : x.l1Ref;
                                worksheet.Cells[i, 2].Value = "1";
                                worksheet.Cells[i, 3].Value = (x.l1 == null) ? "" : x.l1;
                                worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                oldl1 = x.l1;
                                i = i + 2;
                            }
                            if ((l2 != "") && (l2 != oldl2))
                            {
                                worksheet.Cells[i, 1].Value = (x.l2Ref == null) ? "" : x.l2Ref;
                                worksheet.Cells[i, 2].Value = "2";
                                worksheet.Cells[i, 3].Value = (x.l2 == null) ? "" : x.l2;
                                worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                oldl2 = x.l2;
                                i = i + 2;
                            }
                            if ((l3 != "") && (l3 != oldl3))
                            {
                                worksheet.Cells[i, 1].Value = (x.l3Ref == null) ? "" : x.l3Ref;
                                worksheet.Cells[i, 2].Value = "3";
                                worksheet.Cells[i, 3].Value = (x.l3 == null) ? "" : x.l3;
                                worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                oldl3 = x.l3;
                                i = i + 2;
                            }
                            if ((l4 != "") && (l4 != oldl4))
                            {
                                worksheet.Cells[i, 1].Value = (x.l4Ref == null) ? "" : x.l4Ref;
                                worksheet.Cells[i, 2].Value = "4";
                                worksheet.Cells[i, 3].Value = (x.l4 == null) ? "" : x.l4;
                                worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                oldl4 = x.l4;
                                i = i + 2;
                            }
                            if ((l5 != "") && (l5 != oldl5))
                            {
                                worksheet.Cells[i, 1].Value = (x.l5Ref == null) ? "" : x.l5Ref;
                                worksheet.Cells[i, 2].Value = "5";
                                worksheet.Cells[i, 3].Value = (x.l5 == null) ? "" : x.l5;
                                worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                oldl5 = x.l5;
                                i = i + 2;
                            }
                            if ((l6 != "") && (l6 != oldl6))
                            {
                                worksheet.Cells[i, 1].Value = (x.l6Ref == null) ? "" : x.l6Ref;
                                worksheet.Cells[i, 2].Value = "6";
                                worksheet.Cells[i, 3].Value = (x.l6 == null) ? "" : x.l6;
                                worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                oldl6 = x.l6;
                                i = i + 2;
                            }

                            if ((C != OldC) | (OldC == ""))
                            {
                                if (((x.c1 == null) ? "" : x.c1) != "")
                                {
                                    worksheet.Cells[i, 1].Value = (x.c1Ref == null) ? "" : x.c1Ref;
                                    worksheet.Cells[i, 2].Value = "C";
                                    worksheet.Cells[i, 3].Value = (x.c1 == null) ? "" : x.c1;
                                    worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                    i = i + 2;
                                    OldC = C;
                                }
                                if (((x.c2 == null) ? "" : x.c2) != "")
                                {
                                    worksheet.Cells[i, 1].Value = (x.c2Ref == null) ? "" : x.c2Ref;
                                    worksheet.Cells[i, 2].Value = "C";
                                    worksheet.Cells[i, 3].Value = (x.c2 == null) ? "" : x.c2;
                                    worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                    i = i + 2;
                                }
                                if (((x.c3 == null) ? "" : x.c3) != "")
                                {
                                    worksheet.Cells[i, 1].Value = (x.c3Ref == null) ? "" : x.c3Ref;
                                    worksheet.Cells[i, 2].Value = "C";
                                    worksheet.Cells[i, 3].Value = (x.c3 == null) ? "" : x.c3;
                                    worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                    i = i + 2;
                                }
                                if (((x.c4 == null) ? "" : x.c4) != "")
                                {
                                    worksheet.Cells[i, 1].Value = (x.c4Ref == null) ? "" : x.c4Ref;
                                    worksheet.Cells[i, 2].Value = "C";
                                    worksheet.Cells[i, 3].Value = (x.c4 == null) ? "" : x.c4;
                                    worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                    i = i + 2;
                                }
                                if (((x.c5 == null) ? "" : x.c5) != "")
                                {
                                    worksheet.Cells[i, 1].Value = (x.c5Ref == null) ? "" : x.c5Ref;
                                    worksheet.Cells[i, 2].Value = "C";
                                    worksheet.Cells[i, 3].Value = (x.c5 == null) ? "" : x.c5;
                                    worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                    i = i + 2;
                                }
                                if (((x.c6 == null) ? "" : x.c6) != "")
                                {
                                    worksheet.Cells[i, 1].Value = (x.c6Ref == null) ? "" : x.c6Ref;
                                    worksheet.Cells[i, 2].Value = "C";
                                    worksheet.Cells[i, 3].Value = (x.c5 == null) ? "" : x.c5;
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

                    xlPackage.Save();
                    stream.Position = 0;
                    excelName = $"Items Not Assigned.xlsx";

                    if (File.Exists(excelName))
                        File.Delete(excelName);

                    xlPackage.SaveAs(excelName);

                    //excelName = "Package-Aluminum Doors and Windows.xlsx";
                    
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

        public string ExportExcelPackagesCost(int withBoq)
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

        public async Task<DataTablesResponse<BoqModel>> GetBoqResourceRecords(DataTablesRequest dtRequest)
        {
            var input = dtRequest.input;
            var condQuery = (from o in _context.TblOriginalBoqs
                                               join b in _context.TblBoqs on o.ItemO equals b.BoqItem
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
