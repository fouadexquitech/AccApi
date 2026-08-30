using AccApi.Data_Layer;
using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.View_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Drawing;

namespace AccApi.Repository.Managers
{
    public class SupplierPackagesRepository : ISupplierPackagesRepository
    {
        private readonly AccDbContext _CostDbContext;
        private readonly PolicyDbContext _pdbcontext;
        private MasterDbContext _mdbContext;
        private readonly IlogonRepository _logonRepository;
        private readonly GlobalLists _globalLists;
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration { get; }

        public SupplierPackagesRepository(AccDbContext Context, PolicyDbContext pdbcontext, MasterDbContext mdbContext,
            IlogonRepository logonRepository, GlobalLists globalLists, HttpClient httpClient, IConfiguration configuration)
        {
            /*_dbcontext = Context;*/
            _mdbContext = mdbContext;
            _logonRepository = logonRepository;
            _globalLists = globalLists;
            _CostDbContext = new AccDbContext(_globalLists.GetAccDbconnectionString());
            _pdbcontext = new PolicyDbContext(_globalLists.GetTimeSheetDbconnectionString());
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public SupplierPackagesList GetSupplierPackage(int spId, string CostConn)
        {
            AccDbContext _dbcontext = new AccDbContext(CostConn);

            var supList = (from b in _mdbContext.TblSuppliers
                           select b).ToList();

            var results = from c in supList
                          join b in _dbcontext.TblSupplierPackages on c.SupCode equals b.SpSupplierId
                          where b.SpPackSuppId == spId
                          orderby b.SpPackSuppId
                          select new SupplierPackagesList
                          {
                              PsId = b.SpPackSuppId,
                              PsPackId = b.SpPackageId,
                              PsSuppId = b.SpSupplierId,
                              PsSupName = c.SupName,
                              PsByBoq = b.SpByBoq,
                              TecCondSent = b.TecCondSent
                          };
            return results.FirstOrDefault();
        }

        public List<SupplierPackagesList> GetSupplierPackagesList(int packageid, string CostConn)
        {
            AccDbContext _dbcontext = new AccDbContext(CostConn);

            var supList = (from b in _mdbContext.TblSuppliers
                           select b).ToList();

            var results = from b in supList
                          join c in _dbcontext.TblSupplierPackages on b.SupCode equals c.SpSupplierId
                          join s in _dbcontext.TblSupplierPackageRevisions on c.SpPackSuppId equals s.PrPackSuppId
                          where c.SpPackageId == packageid
                          group new { b, c, s } by new { c.SpSupplierId, b.SupName } into g
                          select new SupplierPackagesList
                          {
                              PsSuppId = g.Key.SpSupplierId,
                              PsSupName = g.Key.SupName,
                              RevisionStatus = g.Max(x => x.s.StatusId),
                              SupSubmitted = g.Max(x => x.s.StatusId) == 3 ? true : false,
                              PsId = g.Max(x => x.c.SpPackSuppId),
                              PsPackId = g.Max(x => x.c.SpPackageId),
                              PsByBoq = g.Max(x => x.c.SpByBoq),
                              TecCondSent = g.Max(x => x.c.TecCondSent)
                          };

            return results.ToList();
        }

        public List<boqPackageList> GetboqPackageList(int packId, byte byboq, string CostConn)
        {
            AccDbContext _dbcontext = new AccDbContext(CostConn);
            var boqList = new List<boqPackageList>();

            var boqList1 =
                   (from o in _dbcontext.TblOriginalBoqVds

                    join b in _dbcontext.TblBoqVds
                        on o.ItemO equals b.BoqItem into boqJoin

                    from b in boqJoin.DefaultIfEmpty() // LEFT JOIN TblBoqVds

                    join r in _dbcontext.TblResources
                        on b.BoqResSeq equals r.ResSeq into resJoin

                    from r in resJoin.DefaultIfEmpty() // LEFT JOIN TblResources

                    where packId == -1 || packId == 0 || (b != null && b.BoqScope == packId)

                    orderby o.RowNumber

                    select new boqPackageList
                    {
                        l1 = o.L1,
                        l2 = o.L2,
                        l3 = o.L3,
                        l4 = o.L4,
                        l5 = o.L5,
                        l6 = o.L6,
                        l7 = o.L7,
                        l8 = o.L8,
                        l9 = o.L9,
                        l10 = o.L10,

                        l1Ref = o.L1ref,
                        l2Ref = o.L2ref,
                        l3Ref = o.L3ref,
                        l4Ref = o.L4ref,
                        l5Ref = o.L5ref,
                        l6Ref = o.L6ref,
                        l7Ref = o.L7ref,
                        l8Ref = o.L8ref,
                        l9Ref = o.L9ref,
                        l10Ref = o.L10ref,

                        c1 = o.C1,
                        c2 = o.C2,
                        c3 = o.C3,
                        c4 = o.C4,
                        c5 = o.C5,
                        c6 = o.C6,
                        c7 = o.C7,
                        c8 = o.C8,
                        c9 = o.C9,
                        c10 = o.C10,

                        c1Ref = o.C1ref,
                        c2Ref = o.C2ref,
                        c3Ref = o.C3ref,
                        c4Ref = o.C4ref,
                        c5Ref = o.C5ref,
                        c6Ref = o.C6ref,
                        c7Ref = o.C7ref,
                        c8Ref = o.C8ref,
                        c9Ref = o.C9ref,
                        c10Ref = o.C10ref,

                        boqSN = o.RefNumber,
                        item = o.ItemO,
                        boqDesc = o.DescriptionO,
                        unit = o.UnitO,
                        qty = (double)o.QtyO,

                        unitPrice = o.UnitRate,
                        totalPrice = o.QtyO * o.UnitRate,

                        //exportedToSupplier = (byte)((o.ExportedToSupplier == null) ? 0 : o.ExportedToSupplier),

                        obTradeDesc = o.ObTradeDesc,
                        resType = b == null ? null : b.BoqCtg,
                        resCode = b == null ? null : b.BoqPackage,
                        resDesc = r == null ? null : r.ResDescription,
                        resUnit = b == null ? null : b.BoqUnitMesure,
                        resCtg = b == null ? null : b.BoqCtg,
                        resDiv = b == null ? null : b.BoqDiv,
                        resBillQty = b == null ? 0 : b.BoqBillQty,
                        resQty = b == null ? 0 : b.BoqQty,
                        resUnitPrice = b == null ? 0 : b.BoqUprice,
                        resTotalPrice = b == null ? 0 : b.BoqQty * b.BoqUprice,
                        resScopeQty = b == null ? 0 : b.BoqQtyScope,
                        resWbs = b.BoqWbs,

                    }).ToList();

            if (packId == -1)
                return boqList1;

            if (byboq == 1)
            {

                var resCost = from e in _dbcontext.TblBoqVds
                              join o in _dbcontext.TblOriginalBoqVds on e.BoqItem equals o.ItemO
                              where e.BoqScope == packId
                              group e by e.BoqItem into g
                              select new boqPackageList
                              {
                                  item = g.Key,
                                  resTotalPrice = g.Sum(x => x.BoqQty * x.BoqUprice)
                              };

                boqList = (from o in boqList1
                           join b in resCost on o.item equals b.item
                           select new boqPackageList
                           {
                               l1 = o.l1,
                               l2 = o.l2,
                               l3 = o.l3,
                               l4 = o.l4,
                               l5 = o.l5,
                               l6 = o.l6,
                               l7 = o.l7,
                               l8 = o.l8,
                               l9 = o.l9,
                               l10 = o.l10,
                               l1Ref = o.l1Ref,
                               l2Ref = o.l2Ref,
                               l3Ref = o.l3Ref,
                               l4Ref = o.l4Ref,
                               l5Ref = o.l5Ref,
                               l6Ref = o.l6Ref,
                               l7Ref = o.l7Ref,
                               l8Ref = o.l8Ref,
                               l9Ref = o.l9Ref,
                               l10Ref = o.l10Ref,
                               c1 = o.c1,
                               c2 = o.c2,
                               c3 = o.c3,
                               c4 = o.c4,
                               c5 = o.c5,
                               c6 = o.c6,
                               c7 = o.c7,
                               c8 = o.c8,
                               c9 = o.c9,
                               c10 = o.c10,
                               c1Ref = o.c1Ref,
                               c2Ref = o.c2Ref,
                               c3Ref = o.c3Ref,
                               c4Ref = o.c4Ref,
                               c5Ref = o.c5Ref,
                               c6Ref = o.c6Ref,
                               c7Ref = o.c7Ref,
                               c8Ref = o.c8Ref,
                               c9Ref = o.c9Ref,
                               c10Ref = o.c10Ref,
                               item = o.item,
                               boqDesc = o.boqDesc,
                               unit = o.unit,
                               qty = (double)o.qty,
                               unitPrice = b.resTotalPrice / o.qty,
                               totalPrice = b.resTotalPrice,
                               //exportedToSupplier = o.exportedToSupplier,
                               obTradeDesc = o.obTradeDesc
                           }).ToList();

            }
            else
            {
                //AH26052025
                boqList = boqList1
                        .GroupBy(x => new { x.resDesc, x.resUnitPrice, x.resUnit })
                        .Select(p => new boqPackageList
                        {
                            resDesc = p.First().resDesc,
                            resUnit = p.First().resUnit,
                            resBillQty = p.Sum(c => c.resBillQty),
                            resQty = p.Sum(c => c.resQty),
                            resUnitPrice = p.First().resUnitPrice,
                            resTotalPrice = p.Sum(c => c.resQty * c.resUnitPrice),
                            resScopeQty = p.Sum(c => c.resScopeQty),
                            //exportedToSupplier = (byte)p.Max(c => ((c.exportedToSupplier == null) ? 0 : c.exportedToSupplier))
                        }).ToList();
                //26052025
            }

            return boqList;
        }

        public string ValidateExcelBeforeAssign(int packId, byte byBoq, bool withPrice, string CostConn)
        {
            AccDbContext _dbcontext = new AccDbContext(CostConn);

            //AH0702
            //var packageSupp = _dbcontext.TblSupplierPackages.Where(x => x.SpPackageId == packId).FirstOrDefault();
            //byte byBoq = (byte)((packageSupp.SpByBoq==null) ? 0 : packageSupp.SpByBoq);
            //AH0702
            string PackageName = "";
            var package = new Models.MasterModels.TblPackage();

            if (packId != -1)
            {
                package = _mdbContext.TblPackages.Where(x => x.PkgeId == packId).FirstOrDefault();
                if (package == null) return string.Empty;

                PackageName = package.PkgeName;
            }
            var result = GetboqPackageList(packId, byBoq, CostConn);  //.Where(x=>x.exportedToSupplier == null || x.exportedToSupplier == 0);

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("BOQ");
                worksheet.Columns.AutoFit();

                if (!withPrice)
                    worksheet.Protection.IsProtected = true;

                int i, j;
                string Boq = "", OldBoq = "", c1 = "", c2 = "", c3 = "", c4 = "", c5 = "", c6 = "",
                    oldc1 = "", oldc2 = "", oldc3 = "", oldc4 = "", oldc5 = "", oldc6 = "",
                    l1 = "", l2 = "", l3 = "", l4 = "", l5 = "", l6 = "", oldl1 = "", oldl2 = "", oldl3 = "", oldl4 = "", oldl5 = "", oldl6 = "";

                i = 1;
                double total = 0;

                if (byBoq == 1)
                {
                    worksheet.Cells[i, 1].Value = "SN";
                    //worksheet.Column(1).Width = 40;
                    worksheet.Cells[i, 2].Value = "Item";
                    //worksheet.Column(2).Width = 40;
                    worksheet.Cells[i, 3].Value = "Level";
                    worksheet.Column(4).Width = 50;
                    worksheet.Columns[4].Style.WrapText = true;
                    //worksheet.Column(3).AutoFit();
                    worksheet.Cells[i, 4].Value = "Bill Description";
                    worksheet.Cells[i, 5].Value = "Unit";
                    worksheet.Cells[i, 6].Value = "Qty";
                    worksheet.Columns[6].Style.WrapText = true;
                    //worksheet.Column(5).AutoFit();
                    worksheet.Cells[i, 7].Value = "Unit Price";
                    worksheet.Cells[i, 8].Value = "Total Price";

                    if (packId != -1)
                    {
                        worksheet.Cells[i, 9].Value = "Comments";
                        worksheet.Column(9).Width = 50;
                        worksheet.Columns[9].Style.WrapText = true;
                        worksheet.Columns[9].Style.Locked = false;
                    }
                    else
                    {
                        worksheet.Cells[i, 9].Value = "Ressource Type";
                        worksheet.Cells[i, 10].Value = "Ressource Code";
                        worksheet.Cells[i, 11].Value = "Ressource Description";
                        worksheet.Column(11).Width = 50;
                        worksheet.Columns[11].Style.WrapText = true;
                        worksheet.Cells[i, 12].Value = "ResUnit";
                        worksheet.Columns[12].Style.WrapText = true;
                        worksheet.Cells[i, 13].Value = "ResQty";
                        worksheet.Columns[13].Style.WrapText = true;
                        worksheet.Cells[i, 14].Value = "ResUnitPrice";
                        worksheet.Cells[i, 15].Value = "ResTotalPrice";
                        worksheet.Column(15).Width = 25;
                        worksheet.Cells[i, 16].Value = "WBS";
                    }

                }
                else
                {
                    worksheet.Cells[i, 1].Value = "Ressource Type";
                    worksheet.Cells[i, 2].Value = "Ressource Code";
                    worksheet.Cells[i, 3].Value = "Ressource Description";
                    worksheet.Column(3).Width = 50;
                    worksheet.Columns[3].Style.WrapText = true;
                    //worksheet.Column(3).AutoFit();
                    worksheet.Cells[i, 4].Value = "ResUnit";
                    worksheet.Columns[4].Style.WrapText = true;
                    //worksheet.Column(4).AutoFit();
                    worksheet.Cells[i, 5].Value = "ResQty";
                    worksheet.Columns[5].Style.WrapText = true;
                    //worksheet.Column(5).AutoFit();

                    worksheet.Cells[i, 6].Value = "ResUnitPrice";
                    //worksheet.Column(6).AutoFit();
                    worksheet.Cells[i, 7].Value = "ResTotalPrice";
                    worksheet.Column(7).Width = 25;
                    //worksheet.Column(7).AutoFit();
                    worksheet.Cells[i, 8].Value = "Comments";
                    worksheet.Column(8).Width = 50;
                    worksheet.Columns[8].Style.WrapText = true;
                    worksheet.Columns[8].Style.Locked = false;
                    //worksheet.Column(12).AutoFit();                   
                }
                worksheet.Row(i).Style.Font.Bold = true;

                i = 4;
                foreach (var x in result)
                {
                    Boq = x.item;
                    c1 = (x.c1 == null) ? "" : x.c1;
                    c2 = (x.c2 == null) ? "" : x.c2;
                    c3 = (x.c3 == null) ? "" : x.c3;
                    c4 = (x.c4 == null) ? "" : x.c4;
                    c5 = (x.c5 == null) ? "" : x.c5;
                    c6 = (x.c6 == null) ? "" : x.c6;
                    //l1 = (x.l1 == null) ? "" : x.l1;
                    l2 = (x.l2 == null) ? "" : x.l2;
                    l3 = (x.l3 == null) ? "" : x.l3;
                    l4 = (x.l4 == null) ? "" : x.l4;
                    l5 = (x.l5 == null) ? "" : x.l5;
                    l6 = (x.l6 == null) ? "" : x.l6;

                    if (byBoq == 1)
                    {
                        if ((Boq != OldBoq) || (OldBoq == ""))
                        {
                            //if ((l1 != "") && (l1 != oldl1))
                            //{
                            //    worksheet.Cells[i, 2].Value = (x.l1Ref == null) ? "" : x.l1Ref;
                            //    worksheet.Cells[i, 3].Value = "1";
                            //    worksheet.Cells[i, 4].Value = (x.l1 == null) ? "" : x.l1;
                            //    worksheet.SelectedRange[i, 4].Style.Font.Bold = true;
                            //    oldl1 = x.l1;
                            //    i = i + 2;
                            //}
                            if ((l2 != "") && (l2 != oldl2))
                            {
                                worksheet.Cells[i, 2].Value = (x.l2Ref == null) ? "" : x.l2Ref;
                                worksheet.Cells[i, 3].Value = "2";
                                worksheet.Cells[i, 4].Value = (x.l2 == null) ? "" : x.l2;
                                worksheet.SelectedRange[i, 4].Style.Font.Bold = true;
                                oldl2 = x.l2;
                                i = i + 2;
                            }
                            if ((l3 != "") && (l3 != oldl3))
                            {
                                worksheet.Cells[i, 2].Value = (x.l3Ref == null) ? "" : x.l3Ref;
                                worksheet.Cells[i, 3].Value = "3";
                                worksheet.Cells[i, 4].Value = (x.l3 == null) ? "" : x.l3;
                                worksheet.SelectedRange[i, 4].Style.Font.Bold = true;
                                oldl3 = x.l3;
                                i = i + 2;
                            }
                            if ((l4 != "") && (l4 != oldl4))
                            {
                                worksheet.Cells[i, 2].Value = (x.l4Ref == null) ? "" : x.l4Ref;
                                worksheet.Cells[i, 3].Value = "4";
                                worksheet.Cells[i, 4].Value = (x.l4 == null) ? "" : x.l4;
                                worksheet.SelectedRange[i, 4].Style.Font.Bold = true;
                                oldl4 = x.l4;
                                i = i + 2;
                            }
                            if ((l5 != "") && (l5 != oldl5))
                            {
                                worksheet.Cells[i, 2].Value = (x.l5Ref == null) ? "" : x.l5Ref;
                                worksheet.Cells[i, 3].Value = "5";
                                worksheet.Cells[i, 4].Value = (x.l5 == null) ? "" : x.l5;
                                worksheet.SelectedRange[i, 4].Style.Font.Bold = true;
                                oldl5 = x.l5;
                                i = i + 2;
                            }
                            if ((l6 != "") && (l6 != oldl6))
                            {
                                worksheet.Cells[i, 2].Value = (x.l6Ref == null) ? "" : x.l6Ref;
                                worksheet.Cells[i, 3].Value = "6";
                                worksheet.Cells[i, 4].Value = (x.l6 == null) ? "" : x.l6;
                                worksheet.SelectedRange[i, 4].Style.Font.Bold = true;
                                oldl6 = x.l6;
                                i = i + 2;
                            }

                            //if ((C != OldC) | (OldC == ""))
                            //{
                            if ((c1 != "") && (c1 != oldc1))
                            {
                                worksheet.Cells[i, 2].Value = (x.c1Ref == null) ? "" : x.c1Ref;
                                worksheet.Cells[i, 3].Value = "C";
                                worksheet.Cells[i, 4].Value = (x.c1 == null) ? "" : x.c1;
                                worksheet.SelectedRange[i, 4].Style.Font.Bold = true;
                                oldc1 = x.c1;
                                i = i + 2;
                            }
                            if ((c2 != "") && (c2 != oldc2))
                            {
                                worksheet.Cells[i, 2].Value = (x.c2Ref == null) ? "" : x.c2Ref;
                                worksheet.Cells[i, 3].Value = "C";
                                worksheet.Cells[i, 4].Value = (x.c2 == null) ? "" : x.c2;
                                worksheet.SelectedRange[i, 4].Style.Font.Bold = true;
                                oldc2 = x.c2;
                                i = i + 2;
                            }
                            if ((c3 != "") && (c3 != oldc3))
                            {
                                worksheet.Cells[i, 2].Value = (x.c3Ref == null) ? "" : x.c3Ref;
                                worksheet.Cells[i, 3].Value = "C";
                                worksheet.Cells[i, 4].Value = (x.c3 == null) ? "" : x.c3;
                                worksheet.SelectedRange[i, 4].Style.Font.Bold = true;
                                oldc3 = x.c3;
                                i = i + 2;
                            }
                            if ((c4 != "") && (c4 != oldc4))
                            {
                                worksheet.Cells[i, 2].Value = (x.c4Ref == null) ? "" : x.c4Ref;
                                worksheet.Cells[i, 3].Value = "C";
                                worksheet.Cells[i, 4].Value = (x.c4 == null) ? "" : x.c4;
                                worksheet.SelectedRange[i, 4].Style.Font.Bold = true;
                                oldc4 = x.c4;
                                i = i + 2;
                            }
                            if ((c5 != "") && (c5 != oldc5))
                            {
                                worksheet.Cells[i, 2].Value = (x.c5Ref == null) ? "" : x.c5Ref;
                                worksheet.Cells[i, 3].Value = "C";
                                worksheet.Cells[i, 4].Value = (x.c5 == null) ? "" : x.c5;
                                worksheet.SelectedRange[i, 4].Style.Font.Bold = true;
                                oldc5 = x.c5;
                                i = i + 2;
                            }
                            if ((c6 != "") && (c6 != oldc6))
                            {
                                worksheet.Cells[i, 2].Value = (x.c6Ref == null) ? "" : x.c6Ref;
                                worksheet.Cells[i, 3].Value = "C";
                                worksheet.Cells[i, 4].Value = (x.c6 == null) ? "" : x.c6;
                                worksheet.SelectedRange[i, 4].Style.Font.Bold = true;
                                oldc6 = x.c6;
                                i = i + 2;
                            }
                            //}

                            worksheet.Cells[i, 1].Value = (x.boqSN == null) ? "" : x.boqSN;
                            worksheet.Cells[i, 2].Value = (x.item == null) ? "" : x.item;
                            worksheet.Cells[i, 4].Value = (x.boqDesc == null) ? "" : x.boqDesc;
                            worksheet.Cells[i, 5].Value = (x.unit == null) ? "" : x.unit;
                            worksheet.Cells[i, 6].Value = (x.qty == null) ? "" : x.qty;
                            worksheet.Cells[i, 6].Style.Numberformat.Format = "#,##0.###";

                            if (withPrice)
                            {
                                worksheet.Cells[i, 7].Value = (x.unitPrice == null) ? "" : x.unitPrice;
                                worksheet.Cells[i, 7].Style.Numberformat.Format = "#,##0.###";
                                worksheet.Cells[i, 8].Value = (x.totalPrice == null) ? "" : x.totalPrice;
                                worksheet.Cells[i, 8].Style.Numberformat.Format = "#,##0.###";
                                //total += (double)((x.resTotalPrice == null) ? 0 : x.resTotalPrice);
                            }
                            else
                            {
                                worksheet.Cells[i, 7].Style.Locked = false;
                                worksheet.Cells[i, 8].Formula = "= F" + i + "*" + "G" + i;
                                worksheet.Cells[i, 8].Style.Numberformat.Format = "#,##0.0";
                            }

                        }

                        if (packId == -1)
                        {
                            worksheet.Cells[i, 9].Value = (x.resType == null) ? "" : x.resType;
                            worksheet.Cells[i, 10].Value = (x.resCode == null) ? "" : x.resCode;
                            worksheet.Cells[i, 11].Value = (x.resDesc == null) ? "" : x.resDesc;
                            worksheet.Cells[i, 12].Value = (x.resUnit == null) ? "" : x.resUnit;
                            worksheet.Cells[i, 13].Value = (x.resQty == null) ? 0 : x.resQty;
                            worksheet.Cells[i, 13].Style.Numberformat.Format = "#,##0.###";
                            worksheet.Cells[i, 14].Value = (x.resUnitPrice == null) ? "" : x.resUnitPrice;
                            worksheet.Cells[i, 14].Style.Numberformat.Format = "#,##0.###";
                            worksheet.Cells[i, 15].Value = (x.resTotalPrice == null) ? 0 : x.resTotalPrice;
                            worksheet.Cells[i, 15].Style.Numberformat.Format = "#,##0.###";
                            worksheet.Cells[i, 16].Value = (x.resWbs == null) ? "" : x.resWbs;
                            //total += (double)((x.resTotalPrice == null) ? 0 : x.resTotalPrice);
                        }

                        //i = i + 1;
                        OldBoq = Boq;

                    }
                    else  //by Res.                   
                    {
                        worksheet.Cells[i, 1].Value = (x.resType == null) ? "" : x.resType;
                        worksheet.Cells[i, 2].Value = (x.resCode == null) ? "" : x.resCode;
                        worksheet.Cells[i, 3].Value = (x.resDesc == null) ? "" : x.resDesc;
                        worksheet.Cells[i, 4].Value = (x.resUnit == null) ? "" : x.resUnit;
                        worksheet.Cells[i, 5].Value = (x.resScopeQty == null) ? "" : x.resScopeQty;
                        worksheet.Cells[i, 5].Style.Numberformat.Format = "#,##0.###";

                        if (withPrice)
                        {
                            worksheet.Cells[i, 6].Value = (x.resUnitPrice == null) ? "" : x.resUnitPrice;
                            worksheet.Cells[i, 6].Style.Numberformat.Format = "#,##0.###";
                            worksheet.Cells[i, 7].Value = (x.resTotalPrice == null) ? 0 : x.resTotalPrice;
                            worksheet.Cells[i, 7].Style.Numberformat.Format = "#,##0.###";
                            //total += (double)((x.resTotalPrice == null) ? 0 : x.resTotalPrice);
                        }
                        else
                        {
                            worksheet.Cells[i, 6].Style.Locked = false;
                            worksheet.Cells[i, 7].Formula = "=E" + i + "*" + "F" + i;
                            worksheet.Cells[i, 7].Style.Numberformat.Format = "#,##0.0";
                        }

                    }
                    i++;
                }

                //if (withPrice)
                //{
                //    worksheet.Row(i + 1).Style.Font.Bold = true;
                //    worksheet.Cells[i + 1, 6].Value = "Total :";
                //    worksheet.Cells[i + 1, 7].Style.Numberformat.Format = "#,##0.0";
                //    worksheet.Cells[i + 1, 7].Value = total;
                //}

                //Update Exported BOQ
                //if (byBoq == 1)
                //{
                //    var lstBoqo = (from a in result
                //                   join b in _dbcontext.TblOriginalBoqVds on a.item equals b.ItemO
                //                   select b).ToList();

                //    foreach (var item in result)
                //    {
                //        lstBoqo.Where(d => d.ItemO == item.item).First().ExportedToSupplier = 1;
                //    }
                //    _dbcontext.TblOriginalBoqVds.UpdateRange(lstBoqo);
                //    _dbcontext.SaveChanges();
                //}

                var p = _dbcontext.TblParameters.FirstOrDefault();
                string ProjectName = p.Project;

                xlPackage.Save();
                stream.Position = 0;

                string excelName = "";

                if (PackageName != "")
                    excelName = $"{ProjectName}-Package-{PackageName}-{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";
                else
                    excelName = $"{ProjectName}-BKD-{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";

                if (File.Exists(excelName))
                    File.Delete(excelName);

                excelName = excelName.Replace("/", "-");
                excelName = excelName.Replace("&", "-");
                //string filePath = "C:\\App\\ExportExcel\\vendan\\" + excelName;
                xlPackage.SaveAs(excelName);

                package.FilePath = excelName;
                _dbcontext.SaveChanges();

                //excelName = "Package-Aluminum Doors and Windows.xlsx";
                return excelName;
            }
        }

        public bool TestSendMail()
        {

            string path1 = @"C:\App\service_log.txt";
            string dte = DateTime.Now.ToString();
            using (StreamWriter sw = (System.IO.File.Exists(path1)) ? System.IO.File.AppendText(path1) : System.IO.File.CreateText(path1))
            {
                sw.WriteLine("request received on " + dte);
            }

            return true;


            //string sent = "";
            //var AttachmentList = new List<string>();

            //AttachmentList.Clear();

            ////send email
            //string SupEmail = "";
            //    SupEmail = "ahijazi@accsal.com";

            //    List<string> mylistTo = new List<string>();
            //    mylistTo.Add(SupEmail);

            //    List<string> mylistCC = new List<string>();
            //    mylistCC.Add("ahijazi@accsal.com");

            //    List<string> mylistBCC = new List<string>();

            //    string Subject = "Procurement";

            //    string MailBody;


            //    MailBody = "Dear Sir,";
            //    MailBody += Environment.NewLine;
            //    MailBody += Environment.NewLine;
            //    MailBody += "test Email";
            //    MailBody += Environment.NewLine;
            //    MailBody += Environment.NewLine;
            //    MailBody += Environment.NewLine;
            //    MailBody += Environment.NewLine;
            //    MailBody += "Best regards";

            //    //User user = _logonRepository.GetUser("ahijazi");
            //    string userSignature = "";   //(user.UsrEmailSignature == null) ? "" : user.UsrEmailSignature;

            //    if (userSignature != "")
            //        {
            //            MailBody += @"<br><br>";
            //            MailBody += userSignature;
            //        }

            //    List<IFormFile> attachments=new List<IFormFile>();

            //    Mail m = new Mail();
            //    sent = m.SendMail(mylistTo, mylistCC, mylistBCC, Subject, MailBody, AttachmentList, true, attachments);
            //    return true;
        }

        public async Task<bool> AssignPackageSuppliers(
            int packId,
            List<SupplierInputList> supInputList,
            byte ByBoq,
            string UserName,
            List<IFormFile> attachments,
            DateTime ExpiryDate,
            List<string> emailCc,
            List<string> generatedAttachments,
            bool includeRfqAttachment,
            string CostConn,
            string TSConn)
        {
            await using AccDbContext db = new AccDbContext(CostConn);
            await using PolicyDbContext tsDb = new PolicyDbContext(TSConn);
            await using var transaction = await db.Database.BeginTransactionAsync();

            var temporaryFiles = new List<string>();

            string GetSafeWorksheetName(string worksheetName)
            {
                string result = string.IsNullOrWhiteSpace(worksheetName)
                    ? "Sheet"
                    : worksheetName
                        .Replace(":", " ")
                        .Replace("\\", " ")
                        .Replace("/", " ")
                        .Replace("?", " ")
                        .Replace("*", " ")
                        .Replace("[", " ")
                        .Replace("]", " ")
                        .Trim();

                return result.Length > 31
                    ? result.Substring(0, 31)
                    : result;
            }

            void FormatConditionWorksheet(ExcelWorksheet worksheet, int lastRow)
            {
                if (worksheet == null)
                    throw new ArgumentNullException(nameof(worksheet));

                worksheet.Cells[1, 1].Value = "Condition";
                worksheet.Cells[1, 2].Value = "ACC Condition";
                worksheet.Cells[1, 3].Value = "Supplier Condition";
                worksheet.View.FreezePanes(2, 1);
                worksheet.Cells.Style.Locked = true;

                using (ExcelRange header = worksheet.Cells[1, 1, 1, 3])
                {
                    header.Style.Font.Bold = true;
                    header.Style.Font.Color.SetColor(Color.White);
                    header.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    header.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(14, 116, 144));
                    header.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    header.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    header.Style.WrapText = true;
                    header.Style.Locked = true;
                    header.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                    header.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                    header.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                    header.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                }

                worksheet.Row(1).Height = 24;

                if (lastRow >= 2)
                {
                    using (ExcelRange dataRange = worksheet.Cells[2, 1, lastRow, 3])
                    {
                        dataRange.Style.WrapText = true;
                        dataRange.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                        dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                    }

                    using (ExcelRange lockedRange = worksheet.Cells[2, 1, lastRow, 2])
                    {
                        lockedRange.Style.Locked = true;
                        lockedRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        lockedRange.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(242, 242, 242));
                    }

                    using (ExcelRange editableRange = worksheet.Cells[2, 3, lastRow, 3])
                    {
                        editableRange.Style.Locked = false;
                        editableRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
                        editableRange.Style.Fill.BackgroundColor.SetColor(Color.White);
                    }
                }

                worksheet.Column(1).Width = 55;
                worksheet.Column(2).Width = 45;
                worksheet.Column(3).Width = 45;

                worksheet.Cells[1, 1, Math.Max(1, lastRow), 3].AutoFilter = true;
                worksheet.PrinterSettings.Orientation = eOrientation.Landscape;
                worksheet.PrinterSettings.FitToPage = true;
                worksheet.PrinterSettings.FitToWidth = 1;
                worksheet.PrinterSettings.FitToHeight = 0;
                worksheet.PrinterSettings.PrintArea =
                    worksheet.Cells[1, 1, Math.Max(1, lastRow), 3];

                worksheet.Protection.IsProtected = true;
                worksheet.Protection.AllowSelectUnlockedCells = true;
                worksheet.Protection.AllowSelectLockedCells = true;
                worksheet.Protection.AllowAutoFilter = true;
                worksheet.Protection.AllowDeleteColumns = false;
                worksheet.Protection.AllowDeleteRows = false;
                worksheet.Protection.AllowInsertColumns = false;
                worksheet.Protection.AllowInsertRows = false;
                worksheet.Protection.AllowFormatCells = false;
                worksheet.Protection.AllowFormatColumns = false;
                worksheet.Protection.AllowFormatRows = false;
            }

            string CreateRfqWorkbookWithConditionSheets(
                string sourceExcelPath,
                List<Condition> commercialInput,
                List<TblSuppComCondReply> commercialReplies,
                List<Condition> technicalInput,
                List<TblSuppTechCondReply> technicalReplies)
            {
                if (string.IsNullOrWhiteSpace(sourceExcelPath))
                    throw new Exception("The generated RFQ Excel file path is empty.");

                if (!File.Exists(sourceExcelPath))
                    throw new FileNotFoundException(
                        "The generated RFQ Excel file was not found.",
                        sourceExcelPath);

                string sourceDirectory = Path.GetDirectoryName(sourceExcelPath);
                if (string.IsNullOrWhiteSpace(sourceDirectory))
                    sourceDirectory = Path.GetTempPath();

                string targetExcelPath = Path.Combine(
                    sourceDirectory,
                    Path.GetFileNameWithoutExtension(sourceExcelPath) +
                    "-Conditions-" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx");

                File.Copy(sourceExcelPath, targetExcelPath, true);
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using (var package = new ExcelPackage(new FileInfo(targetExcelPath)))
                {
                    string commercialSheetName = GetSafeWorksheetName("Commercial Conditions");
                    ExcelWorksheet oldCommercialSheet = package.Workbook.Worksheets[commercialSheetName];
                    if (oldCommercialSheet != null)
                        package.Workbook.Worksheets.Delete(oldCommercialSheet);

                    ExcelWorksheet commercialSheet =
                        package.Workbook.Worksheets.Add(commercialSheetName);

                    List<Condition> safeCommercialInput =
                        commercialInput ?? new List<Condition>();
                    List<TblSuppComCondReply> safeCommercialReplies =
                        commercialReplies ?? new List<TblSuppComCondReply>();

                    int commercialRow = 2;

                    foreach (Condition condition in safeCommercialInput)
                    {
                        TblSuppComCondReply reply = safeCommercialReplies
                            .FirstOrDefault(x => x.CdComConId == condition.id);

                        commercialSheet.Cells[commercialRow, 1].Value =
                            condition.description ?? string.Empty;
                        commercialSheet.Cells[commercialRow, 2].Value =
                            reply?.CdAccCond ?? condition.ACCCondValue ?? string.Empty;
                        commercialSheet.Cells[commercialRow, 3].Value =
                            reply?.CdSuppReply ?? string.Empty;
                        commercialRow++;
                    }

                    foreach (TblSuppComCondReply reply in safeCommercialReplies)
                    {
                        if (safeCommercialInput.Any(x => x.id == reply.CdComConId))
                            continue;

                        commercialSheet.Cells[commercialRow, 1].Value =
                            "Condition " + reply.CdComConId;
                        commercialSheet.Cells[commercialRow, 2].Value =
                            reply.CdAccCond ?? string.Empty;
                        commercialSheet.Cells[commercialRow, 3].Value =
                            reply.CdSuppReply ?? string.Empty;
                        commercialRow++;
                    }

                    FormatConditionWorksheet(
                        commercialSheet,
                        Math.Max(1, commercialRow - 1));

                    string technicalSheetName = GetSafeWorksheetName("Technical Conditions");
                    ExcelWorksheet oldTechnicalSheet = package.Workbook.Worksheets[technicalSheetName];
                    if (oldTechnicalSheet != null)
                        package.Workbook.Worksheets.Delete(oldTechnicalSheet);

                    ExcelWorksheet technicalSheet =
                        package.Workbook.Worksheets.Add(technicalSheetName);

                    List<Condition> safeTechnicalInput =
                        technicalInput ?? new List<Condition>();
                    List<TblSuppTechCondReply> safeTechnicalReplies =
                        technicalReplies ?? new List<TblSuppTechCondReply>();

                    int technicalRow = 2;

                    foreach (Condition condition in safeTechnicalInput)
                    {
                        TblSuppTechCondReply reply = safeTechnicalReplies
                            .FirstOrDefault(x => x.TcTechConId == condition.id);

                        technicalSheet.Cells[technicalRow, 1].Value =
                            condition.description ?? string.Empty;
                        technicalSheet.Cells[technicalRow, 2].Value =
                            reply?.TcAccCond ?? condition.ACCCondValue ?? string.Empty;
                        technicalSheet.Cells[technicalRow, 3].Value =
                            reply?.TcSuppReply ?? string.Empty;
                        technicalRow++;
                    }

                    foreach (TblSuppTechCondReply reply in safeTechnicalReplies)
                    {
                        if (safeTechnicalInput.Any(x => x.id == reply.TcTechConId))
                            continue;

                        technicalSheet.Cells[technicalRow, 1].Value =
                            "Condition " + reply.TcTechConId;
                        technicalSheet.Cells[technicalRow, 2].Value =
                            reply.TcAccCond ?? string.Empty;
                        technicalSheet.Cells[technicalRow, 3].Value =
                            reply.TcSuppReply ?? string.Empty;
                        technicalRow++;
                    }

                    FormatConditionWorksheet(
                        technicalSheet,
                        Math.Max(1, technicalRow - 1));

                    package.Save();
                }

                return targetExcelPath;
            }

            try
            {
                if (supInputList == null || supInputList.Count == 0)
                    throw new Exception("At least one supplier is required.");

                emailCc = NormalizeRepositoryEmailList(emailCc);

                generatedAttachments = (generatedAttachments ?? new List<string>())
                    .Where(x => !string.IsNullOrWhiteSpace(x))
                    .Select(x => x.Trim())
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                var packageEntity = await _mdbContext.TblPackages
                    .FirstOrDefaultAsync(x => x.PkgeId == packId);

                if (packageEntity == null)
                    throw new Exception("Package was not found.");

                var parameter = await db.TblParameters.FirstOrDefaultAsync();
                if (parameter == null)
                    throw new Exception("Project parameters were not found.");

                var project = await tsDb.Tblprojects
                    .FirstOrDefaultAsync(x => x.Seq == parameter.TsProjId);

                if (project == null)
                    throw new Exception("Project information was not found.");

                User user = _logonRepository.GetUser(UserName);
                string signature = user?.UsrEmailSignature ?? string.Empty;
                string userEmail = user?.UsrEmail ?? string.Empty;

                string baseRfqExcel = generatedAttachments.FirstOrDefault(File.Exists)
                    ?? ValidateExcelBeforeAssign(packId, ByBoq, false, CostConn);

                if (string.IsNullOrWhiteSpace(baseRfqExcel) || !File.Exists(baseRfqExcel))
                    throw new FileNotFoundException(
                        "The RFQ Excel file was not found.",
                        baseRfqExcel);

                string sharedRfqWithConditions = string.Empty;
                var mailQueue = new List<MailForSending>();
                var supplierModels = new List<AddSupplierPackageModel>();
                var revisionModels = new List<AddRevisionModel>();

                foreach (SupplierInputList input in supInputList)
                {
                    if (input?.supplierInput == null)
                        throw new Exception("Invalid supplier information.");

                    SupplierInput supplier = input.supplierInput;

                    bool hasPortalAccount = await _mdbContext.TblSuppliers
                        .AsNoTracking()
                        .Where(x => x.SupCode == supplier.supID)
                        .Select(x => x.IsAccountCreated == true)
                        .FirstOrDefaultAsync();

                    TblSupplierPackage supplierPackage = await db.TblSupplierPackages
                        .FirstOrDefaultAsync(x =>
                            x.SpPackageId == packId &&
                            x.SpSupplierId == supplier.supID);

                    bool isNewSupplierPackage = supplierPackage == null;

                    if (supplierPackage == null)
                    {
                        supplierPackage = new TblSupplierPackage
                        {
                            SpPackageId = packId,
                            SpSupplierId = supplier.supID,
                            SpByBoq = ByBoq
                        };

                        await db.TblSupplierPackages.AddAsync(supplierPackage);
                        await db.SaveChangesAsync();
                    }

                    int packageSupplierId = supplierPackage.SpPackSuppId;
                    int lastRevisionNo =
                        await GetMaxRevisionNumberAsync(packageSupplierId, db);

                    if (lastRevisionNo >= 0)
                    {
                        bool pending = await db.TblSupplierPackageRevisions.AnyAsync(x =>
                            x.PrPackSuppId == packageSupplierId &&
                            x.PrRevNo == lastRevisionNo &&
                            (x.StatusId ?? 0) < 3);

                        if (pending)
                        {
                            throw new Exception(
                                "Revision not returned from Supplier. Supplier ID: " +
                                supplier.supID);
                        }

                        List<TblSupplierPackageRevision> revisions =
                            await db.TblSupplierPackageRevisions
                                .Where(x => x.PrPackSuppId == packageSupplierId)
                                .OrderByDescending(x => x.PrRevNo)
                                .ToListAsync();

                        foreach (TblSupplierPackageRevision revision in revisions)
                            revision.PrRevNo += 1;

                        await db.SaveChangesAsync();
                    }

                    TblSupplierPackageRevision rev1 =
                        await db.TblSupplierPackageRevisions.FirstOrDefaultAsync(x =>
                            x.PrPackSuppId == packageSupplierId &&
                            x.PrRevNo == 1);

                    var rev0 = new TblSupplierPackageRevision
                    {
                        PrRevNo = 0,
                        PrPackSuppId = packageSupplierId,
                        PrTotPrice = rev1?.PrTotPrice ?? 0,
                        PrRevDate = DateTime.Now,
                        PrCurrency = rev1?.PrCurrency ?? Convert.ToInt32(parameter.EstimatedCur),
                        PrExchRate = rev1?.PrExchRate ?? 1,
                        RevExpiryDate = ExpiryDate,
                        InsertedBy = UserName,
                        InsertedByEmail = userEmail
                    };

                    await db.TblSupplierPackageRevisions.AddAsync(rev0);
                    await db.SaveChangesAsync();

                    int rev1Id = rev1?.PrRevId ?? 0;
                    byte byBoq = Convert.ToByte(supplierPackage.SpByBoq ?? 0);

                    List<TblRevisionDetail> details = await InsertRevisionDetail(
                        rev0.PrRevId,
                        packId,
                        byBoq,
                        rev1Id,
                        db) ?? new List<TblRevisionDetail>();

                    if (details.Count > 0)
                    {
                        await db.TblRevisionDetails.AddRangeAsync(details);
                        await db.SaveChangesAsync();
                    }

                    List<TblSuppComCondReply> commercial =
                        await InsertComercialConditions(
                            rev0.PrRevId,
                            packId,
                            rev1Id,
                            input.comercialCondList ?? new List<Condition>(),
                            CostConn) ?? new List<TblSuppComCondReply>();

                    List<TblSuppTechCondReply> technical =
                        await InsertTechnicalConditions(
                            rev0.PrRevId,
                            packId,
                            rev1Id,
                            input.technicalCondList ?? new List<Condition>(),
                            CostConn) ?? new List<TblSuppTechCondReply>();

                    if (string.IsNullOrWhiteSpace(sharedRfqWithConditions))
                    {
                        sharedRfqWithConditions = CreateRfqWorkbookWithConditionSheets(
                            baseRfqExcel,
                            input.comercialCondList,
                            commercial,
                            input.technicalCondList,
                            technical);

                        temporaryFiles.Add(sharedRfqWithConditions);
                    }

                    if (hasPortalAccount)
                    {
                        if (isNewSupplierPackage)
                        {
                            supplierModels.Add(new AddSupplierPackageModel
                            {
                                SpPackSuppId = supplierPackage.SpPackSuppId,
                                SpPackageId = supplierPackage.SpPackageId,
                                SpSupplierId = supplierPackage.SpSupplierId,
                                SpByBoq = supplierPackage.SpByBoq,
                                ProjectCode = project.PrjCode,
                                ProjectName = project.PrjName,
                                TecCondSent = false
                            });
                        }

                        revisionModels.Add(new AddRevisionModel
                        {
                            PrRevId = rev0.PrRevId,
                            PrRevNo = rev0.PrRevNo,
                            PrRevDate = rev0.PrRevDate,
                            PrTotPrice = rev0.PrTotPrice,
                            PrPackSuppId = rev0.PrPackSuppId,
                            PrCurrency = rev0.PrCurrency,
                            PrExchRate = 1,
                            StatusId = 1,
                            ProjectCode = project.PrjCode,
                            IsSynched = false,
                            RevExpiryDate = rev0.RevExpiryDate,
                            RevisionDetails = details.Select(d => new AddRevisionDetailModel
                            {
                                BoqResourceSeq = d.RdResourceSeq,
                                ResourceDescription = GetRessourceDescription(
                                    ByBoq,
                                    d.RdResourceSeq,
                                    d.ResourceDescription,
                                    Convert.ToBoolean(d.IsAlternative),
                                    CostConn),
                                ItemO = d.RdBoqItem,
                                ItemDescription = d.ItemDescription,
                                Quantity = d.RdQty,
                                QuotationQty = d.RdQuotationQty,
                                UnitPrice = d.RdPrice,
                                TotalPrice = d.RdQty * d.UnitPriceAfterDiscount,
                                DiscountPerc = d.RdDiscount,
                                Comments = d.RdComment,
                                CreatedOn = DateTime.Now,
                                IsSynched = false,
                                ProjectCode = project.PrjCode,
                                ParentItemO = d.ParentItemO,
                                ParentResourceId = d.ParentResourceId.ToString(),
                                NewItemId = d.NewItemId,
                                NewItemResourceId = d.NewItemResourceId,
                                IsNewItem = d.IsNew,
                                IsAlternative = d.IsAlternative,
                                UnitPriceAfterDiscount = d.UnitPriceAfterDiscount,
                                UnitO = d.UnitO ?? string.Empty,
                                BoqCtg = d.BoqCtg ?? string.Empty,
                                BoqUnitMesure = d.BoqUnitMesure ?? string.Empty,
                                L1 = d.L1 ?? string.Empty,
                                L2 = d.L2 ?? string.Empty,
                                L3 = d.L3 ?? string.Empty,
                                L4 = d.L4 ?? string.Empty,
                                L5 = d.L5 ?? string.Empty,
                                L6 = d.L6 ?? string.Empty,
                                L7 = d.L7 ?? string.Empty,
                                L8 = d.L8 ?? string.Empty,
                                L9 = d.L9 ?? string.Empty,
                                L10 = d.L10 ?? string.Empty,
                                C1 = d.C1 ?? string.Empty,
                                C2 = d.C2 ?? string.Empty,
                                C3 = d.C3 ?? string.Empty,
                                C4 = d.C4 ?? string.Empty,
                                C5 = d.C5 ?? string.Empty,
                                C6 = d.C6 ?? string.Empty,
                                C7 = d.C7 ?? string.Empty,
                                C8 = d.C8 ?? string.Empty,
                                C9 = d.C9 ?? string.Empty,
                                C10 = d.C10 ?? string.Empty,
                                C11 = d.C11 ?? string.Empty,
                                C12 = d.C12 ?? string.Empty,
                                C13 = d.C13 ?? string.Empty,
                                C14 = d.C14 ?? string.Empty,
                                C15 = d.C15 ?? string.Empty,
                                BoqRefNumber = d.RdBoqRefNumber ?? string.Empty,
                                AccComment = d.RdAccComment ?? string.Empty
                            }).ToList(),
                            CommercialConditions = commercial.Select(x => new AddCondModel
                            {
                                Id = x.CdComConId,
                                CondValue = x.CdSuppReply,
                                ACCCondValue = x.CdAccCond,
                                ProjectCode = project.PrjCode
                            }).ToList(),
                            TechnicalConditions = technical.Select(x => new AddCondModel
                            {
                                Id = x.TcTechConId,
                                CondValue = x.TcSuppReply,
                                ACCCondValue = x.TcAccCond,
                                ProjectCode = project.PrjCode
                            }).ToList()
                        });
                    }

                    List<string> to = NormalizeRepositoryEmailList(input.mailTo);
                    if (to.Count == 0)
                    {
                        throw new Exception(
                            "No Email To address was provided for supplier " +
                            (input.supplierName ?? supplier.supID.ToString()));
                    }

                    var mail = new MailForSending();

                    foreach (string address in to)
                        mail.To.Add(address);

                    foreach (string address in emailCc
                        .Where(x => !to.Contains(x, StringComparer.OrdinalIgnoreCase))
                        .Distinct(StringComparer.OrdinalIgnoreCase))
                    {
                        mail.Cc.Add(address);
                    }

                    if (!string.IsNullOrWhiteSpace(_configuration["mailBcc1"]))
                        mail.Bcc.Add(_configuration["mailBcc1"]);

                    if (!string.IsNullOrWhiteSpace(_configuration["mailBcc2"]))
                        mail.Bcc.Add(_configuration["mailBcc2"]);

                    mail.Subject = $"Job in Hand-{project.PrjName}-{packageEntity.PkgeName}";
                    mail.Body = !string.IsNullOrWhiteSpace(input.EmailTemplate)
                        ? input.EmailTemplate
                        : "Dear Sir,<br><br>Kindly find attachments and fill the price.<br><br>Best regards";

                    if (!string.IsNullOrWhiteSpace(signature))
                        mail.Body += "<br><br>" + signature;

                    var paths = new List<string>();

                    if (!string.IsNullOrWhiteSpace(input.FilePath) && File.Exists(input.FilePath))
                        paths.Add(input.FilePath);

                    if (input.mailAttachments != null)
                    {
                        paths.AddRange(input.mailAttachments.Where(File.Exists));
                    }

                    bool attachRfqToEmail = !hasPortalAccount || includeRfqAttachment;

                    if (attachRfqToEmail)
                    {
                        if (string.IsNullOrWhiteSpace(sharedRfqWithConditions) ||
                            !File.Exists(sharedRfqWithConditions))
                        {
                            throw new Exception(
                                "The RFQ Excel file with Commercial Conditions and Technical Conditions was not created.");
                        }

                        // Attach only the condition-enhanced RFQ, not the original RFQ twice.
                        paths.Add(sharedRfqWithConditions);
                    }

                    mail.Attachments = paths
                        .Where(x => !string.IsNullOrWhiteSpace(x))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToList();

                    mailQueue.Add(mail);
                }

                if (revisionModels.Count > 0)
                {
                    var portalModel = new AddSupplierPackageRevisionModel
                    {
                        SupplierPackageModels = supplierModels,
                        RevisionModels = revisionModels
                    };

                    string body = JsonSerializer.Serialize(portalModel);

                    using var request = new HttpRequestMessage(
                        HttpMethod.Post,
                        _configuration["PortalApiPath"] +
                        "External/AddSupplierRevisionInPortal");

                    request.Content = new StringContent(
                        body,
                        Encoding.UTF8,
                        "application/json");

                    string externalKey = _configuration["External:Key"];
                    if (!string.IsNullOrWhiteSpace(externalKey))
                        request.Headers.TryAddWithoutValidation("Authorization", externalKey);

                    using HttpResponseMessage response =
                        await _httpClient.SendAsync(request);

                    response.EnsureSuccessStatusCode();

                    string portalResult = await response.Content.ReadAsStringAsync();
                    if (!string.Equals(
                        portalResult?.Trim(),
                        "true",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception("An error occurred on the Portal API.");
                    }
                }

                foreach (MailForSending queuedMail in mailQueue)
                {
                    string mailResult = new Mail().SendMail(
                        queuedMail.To,
                        queuedMail.Cc,
                        queuedMail.Bcc,
                        queuedMail.Subject,
                        queuedMail.Body,
                        queuedMail.Attachments,
                        queuedMail.IsBodyHtml,
                        attachments);

                    if (!string.Equals(
                        mailResult,
                        "sent",
                        StringComparison.OrdinalIgnoreCase))
                    {
                        throw new Exception(
                            "Email was not sent to: " +
                            string.Join("; ", queuedMail.To) +
                            ". Mail result: " + mailResult);
                    }
                }

                await transaction.CommitAsync();
                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                foreach (string file in temporaryFiles)
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(file) && File.Exists(file))
                            File.Delete(file);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(
                            "Unable to delete temporary RFQ file: " +
                            file + ". Error: " + ex.Message);
                    }
                }
            }
        }


        // public async Task<bool> AssignPackageSuppliers(int packId, List<SupplierInputList> supInputList, byte ByBoq, string UserName, List<IFormFile> attachments,
        //         DateTime ExpiryDate, List<string> emailCc, List<string> generatedAttachments, string CostConn, string TSConn)
        // {
        //     AccDbContext _dbcontext = new AccDbContext(CostConn);
        //     PolicyDbContext _TSdbcontext = new PolicyDbContext(TSConn);


        //     generatedAttachments = (generatedAttachments ?? new List<string>()).Where(path => !string.IsNullOrWhiteSpace(path))
        //         .Select(path => path.Trim()).Distinct(StringComparer.OrdinalIgnoreCase).ToList();

        //     var transaction = await _dbcontext.Database.BeginTransactionAsync();

        //     string sharedRfqExcelWithConditions = string.Empty;

        //     bool sharedRfqExcelCreated = false;

        //     /*
        //      * Files created specifically by this process.
        //      * They are cleaned after completing or failing.
        //      */
        //     List<string> temporaryFiles = new List<string>();

        //     /*
        //      * Local helper:
        //      * Converts text into a safe Excel worksheet name.
        //      */
        //     string GetSafeWorksheetName(string worksheetName)
        //     {
        //         if (string.IsNullOrWhiteSpace(worksheetName))
        //         {
        //             return "Sheet";
        //         }

        //         string result = worksheetName
        //                 .Replace(":", " ")
        //                 .Replace("\\", " ")
        //                 .Replace("/", " ")
        //                 .Replace("?", " ")
        //                 .Replace("*", " ")
        //                 .Replace("[", " ")
        //                 .Replace("]", " ")
        //                 .Trim();

        //         if (result.Length > 31)
        //         {
        //             result = result.Substring(0, 31);
        //         }

        //         return result;
        //     }

        //     /*
        //      * Local helper:
        //      * Applies common formatting to one condition sheet.
        //      */
        //     /*
        //* Applies common formatting and protection to both:
        //*
        //* 1. Commercial Conditions
        //* 2. Technical Conditions
        //*
        //* Protection rules:
        //*
        //* Column A: Condition           Locked
        //* Column B: ACC Condition       Locked
        //* Column C: Supplier Condition  Editable
        //*
        //* The header row is always locked.
        //*/
        //     void FormatConditionWorksheet(ExcelWorksheet worksheet, int lastRow)
        //     {
        //         if (worksheet == null)
        //         {
        //             throw new ArgumentNullException(
        //                 nameof(worksheet)
        //             );
        //         }

        //         /*
        //          * Worksheet titles.
        //          */
        //         worksheet.Cells[1, 1].Value =
        //             "Condition";

        //         worksheet.Cells[1, 2].Value =
        //             "ACC Condition";

        //         worksheet.Cells[1, 3].Value =
        //             "Supplier Condition";

        //         /*
        //          * Freeze the header row.
        //          */
        //         worksheet.View.FreezePanes(
        //             2,
        //             1
        //         );

        //         /*
        //          * Set all worksheet cells as locked first.
        //          *
        //          * This locks:
        //          * Column A
        //          * Column B
        //          * Header row
        //          * All columns outside the working area
        //          */
        //         worksheet.Cells.Style.Locked = true;

        //         /*
        //          * Only Supplier Condition cells are editable.
        //          *
        //          * Row 1 remains locked because it is the header.
        //          * Rows 2 through lastRow in Column C are unlocked.
        //          */
        //         if (lastRow >= 2)
        //         {
        //             worksheet.Cells[
        //                 2,
        //                 3,
        //                 lastRow,
        //                 3
        //             ].Style.Locked =
        //                 false;
        //         }

        //         /*
        //          * Header formatting.
        //          */
        //         using (
        //             ExcelRange headerRange =
        //                 worksheet.Cells[
        //                     1,
        //                     1,
        //                     1,
        //                     3
        //                 ]
        //         )
        //         {
        //             headerRange.Style.Font.Bold = true;

        //             headerRange.Style.Font.Color.SetColor(
        //                 Color.White
        //             );

        //             headerRange.Style.Fill.PatternType =
        //                 ExcelFillStyle.Solid;

        //             headerRange.Style.Fill.BackgroundColor.SetColor(
        //                 Color.FromArgb(
        //                     14,
        //                     116,
        //                     144
        //                 )
        //             );

        //             headerRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

        //             headerRange.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

        //             headerRange.Style.WrapText = true;

        //             /*
        //              * Explicitly lock the header.
        //              */
        //             headerRange.Style.Locked = true;

        //             headerRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;

        //             headerRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

        //             headerRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;

        //             headerRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //         }

        //         worksheet.Row(1).Height = 24;

        //         /*
        //          * Format condition rows.
        //          */
        //         if (lastRow >= 2)
        //         {
        //             using (
        //                 ExcelRange dataRange =
        //                     worksheet.Cells[
        //                         2,
        //                         1,
        //                         lastRow,
        //                         3
        //                     ]
        //             )
        //             {
        //                 dataRange.Style.WrapText = true;

        //                 dataRange.Style.VerticalAlignment = ExcelVerticalAlignment.Top;

        //                 dataRange.Style.Border.Top.Style = ExcelBorderStyle.Thin;

        //                 dataRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;

        //                 dataRange.Style.Border.Left.Style = ExcelBorderStyle.Thin;

        //                 dataRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
        //             }

        //             /*
        //              * Clearly identify protected ACC fields.
        //              */
        //             using (
        //                 ExcelRange lockedRange =
        //                     worksheet.Cells[
        //                         2,
        //                         1,
        //                         lastRow,
        //                         2
        //                     ]
        //             )
        //             {
        //                 lockedRange.Style.Locked = true;

        //                 lockedRange.Style.Fill.PatternType = ExcelFillStyle.Solid;

        //                 lockedRange.Style.Fill.BackgroundColor.SetColor(
        //                     Color.FromArgb(
        //                         242,
        //                         242,
        //                         242
        //                     )
        //                 );
        //             }

        //             /*
        //              * Supplier Condition is the only editable area.
        //              */
        //             using (
        //                 ExcelRange editableRange =
        //                     worksheet.Cells[
        //                         2,
        //                         3,
        //                         lastRow,
        //                         3
        //                     ]
        //             )
        //             {
        //                 editableRange.Style.Locked = false;

        //                 editableRange.Style.Fill.PatternType = ExcelFillStyle.Solid;

        //                 editableRange.Style.Fill.BackgroundColor.SetColor(
        //                     Color.FromArgb(
        //                         255,
        //                         255,
        //                         255
        //                     )
        //                 );
        //             }
        //         }

        //         /*
        //          * Column widths.
        //          */
        //         worksheet.Column(1).Width = 55;

        //         worksheet.Column(2).Width = 45;

        //         worksheet.Column(3).Width = 45;

        //         /*
        //          * Apply auto-filter to the header.
        //          */
        //         if (lastRow >= 1)
        //         {
        //             worksheet.Cells[
        //                 1,
        //                 1,
        //                 lastRow,
        //                 3
        //             ].AutoFilter =
        //                 true;
        //         }

        //         /*
        //          * Printing configuration.
        //          */
        //         worksheet.PrinterSettings.Orientation = eOrientation.Landscape;

        //         worksheet.PrinterSettings.FitToPage = true;

        //         worksheet.PrinterSettings.FitToWidth = 1;

        //         worksheet.PrinterSettings.FitToHeight = 0;

        //         if (lastRow >= 1)
        //         {
        //             worksheet.PrinterSettings.PrintArea =
        //                 worksheet.Cells[
        //                     1,
        //                     1,
        //                     lastRow,
        //                     3
        //                 ];
        //         }

        //         /*
        //          * Protect the worksheet.
        //          *
        //          * Change this password if required.
        //          */
        //         //const string sheetProtectionPassword =
        //         //    "ACC@RFQ2026";

        //         worksheet.Protection.IsProtected = true;

        //         //worksheet.Protection.SetPassword(
        //         //    sheetProtectionPassword
        //         //);

        //         /*
        //          * Allow supplier to select and edit unlocked
        //          * Supplier Condition cells.
        //          */
        //         worksheet.Protection.AllowSelectUnlockedCells = true;

        //         /*
        //          * Do not allow selection or editing of locked cells.
        //          */
        //         worksheet.Protection.AllowSelectLockedCells = true;

        //         /*
        //          * Keep filter dropdowns usable while protected.
        //          */
        //         worksheet.Protection.AllowAutoFilter = true;

        //         /*
        //          * Prevent structural changes.
        //          */
        //         worksheet.Protection.AllowDeleteColumns = false;

        //         worksheet.Protection.AllowDeleteRows = false;

        //         worksheet.Protection.AllowInsertColumns = false;

        //         worksheet.Protection.AllowInsertRows = false;

        //         worksheet.Protection.AllowFormatCells = false;

        //         worksheet.Protection.AllowFormatColumns = false;

        //         worksheet.Protection.AllowFormatRows = false;
        //     }

        //     /*
        //      * Local helper:
        //      * Creates one shared copy of the RFQ Excel and adds:
        //      *
        //      * 1. Commercial Conditions
        //      * 2. Technical Conditions
        //      *
        //      * The same completed file is sent to all suppliers.
        //      */
        //     string CreateSharedRfqExcelWithConditions(string sourceExcelPath, List<Condition> commercialConditionInput,
        //         List<TblSuppComCondReply> commercialReplies, List<Condition> technicalConditionInput, List<TblSuppTechCondReply> technicalReplies)
        //     {
        //         if (string.IsNullOrWhiteSpace(sourceExcelPath))
        //         {
        //             throw new Exception(
        //                 "The generated RFQ Excel file path is empty."
        //             );
        //         }

        //         if (!File.Exists(sourceExcelPath))
        //         {
        //             throw new FileNotFoundException(
        //                 "The generated RFQ Excel file was not found.",
        //                 sourceExcelPath
        //             );
        //         }

        //         string sourceDirectory =
        //             Path.GetDirectoryName(sourceExcelPath);

        //         if (string.IsNullOrWhiteSpace(sourceDirectory))
        //         {
        //             sourceDirectory =
        //                 Path.GetTempPath();
        //         }

        //         string sourceFileNameWithoutExtension =
        //             Path.GetFileNameWithoutExtension(
        //                 sourceExcelPath
        //             );

        //         string targetFileName =
        //             sourceFileNameWithoutExtension +
        //             "-Conditions-" +
        //             DateTime.Now.ToString(
        //                 "yyyyMMddHHmmssfff"
        //             ) +
        //             ".xlsx";

        //         string targetExcelPath =
        //             Path.Combine(
        //                 sourceDirectory,
        //                 targetFileName
        //             );

        //         File.Copy(
        //             sourceExcelPath,
        //             targetExcelPath,
        //             true
        //         );

        //         ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        //         FileInfo targetFile = new FileInfo(targetExcelPath);

        //         using (ExcelPackage excelPackage = new ExcelPackage(targetFile))
        //         {
        //             /*
        //              * Commercial Conditions sheet.
        //              */
        //             string commercialSheetName = GetSafeWorksheetName("Commercial Conditions");

        //             ExcelWorksheet existingCommercialSheet = excelPackage.Workbook.Worksheets[commercialSheetName];

        //             if (existingCommercialSheet != null)
        //             {
        //                 excelPackage.Workbook.Worksheets.Delete(existingCommercialSheet);
        //             }

        //             ExcelWorksheet commercialSheet = excelPackage.Workbook.Worksheets.Add(commercialSheetName);

        //             int commercialRow = 2;

        //             List<Condition> safeCommercialInput =
        //                 commercialConditionInput ??
        //                 new List<Condition>();

        //             List<TblSuppComCondReply>
        //                 safeCommercialReplies =
        //                     commercialReplies ??
        //                     new List<TblSuppComCondReply>();

        //             /*
        //              * Start from selected/requested commercial conditions
        //              * to preserve the same order displayed in Angular.
        //              */
        //             foreach (Condition condition in safeCommercialInput)
        //             {
        //                 TblSuppComCondReply reply = safeCommercialReplies.FirstOrDefault(x => x.CdComConId == condition.id);

        //                 commercialSheet.Cells[commercialRow, 1].Value = condition.description ?? string.Empty;

        //                 commercialSheet.Cells[commercialRow, 2].Value = reply != null ? reply.CdAccCond ?? string.Empty : condition.ACCCondValue ?? string.Empty;

        //                 commercialSheet.Cells[commercialRow, 3].Value = reply?.CdAccCond ?? string.Empty;

        //                 commercialRow++;
        //             }

        //             /*
        //              * Include any commercial reply that was not found
        //              * in the input list.
        //              */
        //             foreach (TblSuppComCondReply reply in safeCommercialReplies)
        //             {
        //                 bool alreadyAdded = safeCommercialInput.Any(x => x.id == reply.CdComConId);

        //                 if (alreadyAdded)
        //                 {
        //                     continue;
        //                 }

        //                 commercialSheet.Cells[commercialRow, 1].Value = "Condition " + reply.CdComConId;

        //                 commercialSheet.Cells[commercialRow, 2].Value = reply.CdAccCond ?? string.Empty;

        //                 commercialSheet.Cells[commercialRow, 3].Value = reply.CdAccCond ?? string.Empty;

        //                 commercialRow++;
        //             }

        //             FormatConditionWorksheet(commercialSheet, Math.Max(1, commercialRow - 1));

        //             /*
        //              * Technical Conditions sheet.
        //              */
        //             string technicalSheetName = GetSafeWorksheetName("Technical Conditions");

        //             ExcelWorksheet existingTechnicalSheet = excelPackage.Workbook.Worksheets[technicalSheetName];

        //             if (existingTechnicalSheet != null)
        //             {
        //                 excelPackage
        //                     .Workbook
        //                     .Worksheets
        //                     .Delete(
        //                         existingTechnicalSheet
        //                     );
        //             }

        //             ExcelWorksheet technicalSheet =
        //                 excelPackage
        //                     .Workbook
        //                     .Worksheets
        //                     .Add(
        //                         technicalSheetName
        //                     );

        //             int technicalRow = 2;

        //             List<Condition> safeTechnicalInput =
        //                 technicalConditionInput ??
        //                 new List<Condition>();

        //             List<TblSuppTechCondReply>
        //                 safeTechnicalReplies =
        //                     technicalReplies ??
        //                     new List<TblSuppTechCondReply>();

        //             /*
        //              * Start from selected/requested technical conditions
        //              * to preserve the displayed order.
        //              */
        //             foreach (Condition condition in safeTechnicalInput)
        //             {
        //                 TblSuppTechCondReply reply =
        //                     safeTechnicalReplies
        //                         .FirstOrDefault(x =>
        //                             x.TcTechConId ==
        //                                 condition.id
        //                         );

        //                 technicalSheet.Cells[
        //                     technicalRow,
        //                     1
        //                 ].Value =
        //                     condition.description ??
        //                     string.Empty;

        //                 technicalSheet.Cells[
        //                     technicalRow,
        //                     2
        //                 ].Value =
        //                     reply != null
        //                         ? reply.TcAccCond ??
        //                             string.Empty
        //                         : condition.ACCCondValue ??
        //                             string.Empty;

        //                 technicalSheet.Cells[
        //                     technicalRow,
        //                     3
        //                 ].Value = reply?.TcAccCond ?? string.Empty;

        //                 technicalRow++;
        //             }

        //             /*
        //              * Include any technical reply not found
        //              * in the input list.
        //              */
        //             foreach (
        //                 TblSuppTechCondReply reply
        //                 in safeTechnicalReplies
        //             )
        //             {
        //                 bool alreadyAdded =
        //                     safeTechnicalInput.Any(x =>
        //                         x.id ==
        //                         reply.TcTechConId
        //                     );

        //                 if (alreadyAdded)
        //                 {
        //                     continue;
        //                 }

        //                 technicalSheet.Cells[
        //                     technicalRow,
        //                     1
        //                 ].Value =
        //                     "Condition " +
        //                     reply.TcTechConId;

        //                 technicalSheet.Cells[
        //                     technicalRow,
        //                     2
        //                 ].Value =
        //                     reply.TcAccCond ??
        //                     string.Empty;

        //                 technicalSheet.Cells[
        //                     technicalRow,
        //                     3
        //                 ].Value =
        //                     reply.TcSuppReply ??
        //                     string.Empty;

        //                 technicalRow++;
        //             }

        //             FormatConditionWorksheet(
        //                 technicalSheet,
        //                 Math.Max(
        //                     1,
        //                     technicalRow - 1
        //                 )
        //             );

        //             excelPackage.Save();
        //         }

        //         return targetExcelPath;
        //     }

        //     try
        //     {
        //         if (
        //             supInputList == null ||
        //             supInputList.Count == 0
        //         )
        //         {
        //             throw new Exception(
        //                 "At least one supplier is required."
        //             );
        //         }

        //         var package =
        //             await _mdbContext
        //                 .TblPackages
        //                 .FirstOrDefaultAsync(x =>
        //                     x.PkgeId ==
        //                     packId
        //                 );

        //         if (package == null)
        //         {
        //             throw new Exception(
        //                 "Package was not found. Package ID: " +
        //                 packId
        //             );
        //         }

        //         string PackageName =
        //             package.PkgeName ??
        //             string.Empty;

        //         List<MailForSending>
        //             mailListForSending =
        //                 new List<MailForSending>();

        //         var p =
        //             await _dbcontext
        //                 .TblParameters
        //                 .FirstOrDefaultAsync();

        //         if (p == null)
        //         {
        //             throw new Exception(
        //                 "Project parameters were not found."
        //             );
        //         }

        //         var proj =
        //             await _TSdbcontext
        //                 .Tblprojects
        //                 .FirstOrDefaultAsync(x =>
        //                     x.Seq ==
        //                     p.TsProjId
        //                 );

        //         if (proj == null)
        //         {
        //             throw new Exception(
        //                 "Project information was not found."
        //             );
        //         }

        //         User user =
        //             _logonRepository
        //                 .GetUser(UserName);

        //         string userSignature =
        //             user?.UsrEmailSignature ??
        //             string.Empty;

        //         string usrEmail =
        //             user?.UsrEmail ??
        //             string.Empty;

        //         List<AddSupplierPackageModel>
        //             supplierPackageModelList =
        //                 new List<AddSupplierPackageModel>();

        //         List<AddRevisionModel>
        //             revisionModelList =
        //                 new List<AddRevisionModel>();

        //         AddSupplierPackageRevisionModel
        //             supplierPackageRevisionModel =
        //                 new AddSupplierPackageRevisionModel();

        //         /*
        //          * Create the base RFQ once.
        //          */
        //         string baseRfqExcel =
        //             ValidateExcelBeforeAssign(
        //                 packId,
        //                 ByBoq,
        //                 false,
        //                 CostConn
        //             );

        //         if (string.IsNullOrWhiteSpace(baseRfqExcel))
        //         {
        //             throw new Exception(
        //                 "The RFQ Excel file could not be generated."
        //             );
        //         }

        //         if (!File.Exists(baseRfqExcel))
        //         {
        //             throw new FileNotFoundException(
        //                 "The generated RFQ Excel file was not found.",
        //                 baseRfqExcel
        //             );
        //         }

        //         foreach (
        //             SupplierInputList supInput
        //             in supInputList
        //         )
        //         {
        //             if (
        //                 supInput == null ||
        //                 supInput.supplierInput == null
        //             )
        //             {
        //                 throw new Exception(
        //                     "Invalid supplier information."
        //                 );
        //             }

        //             SupplierInput supplier =
        //                 supInput.supplierInput;

        //             int PackageSupplierId =
        //                 0;

        //             /*
        //              * 1. Find or create package supplier.
        //              */
        //             TblSupplierPackage
        //                 supplierPackage =
        //                     await _dbcontext
        //                         .TblSupplierPackages
        //                         .FirstOrDefaultAsync(x =>
        //                             x.SpPackageId ==
        //                                 packId &&
        //                             x.SpSupplierId ==
        //                                 supplier.supID
        //                         );

        //             if (supplierPackage == null)
        //             {
        //                 supplierPackage =
        //                     new TblSupplierPackage
        //                     {
        //                         SpPackageId =
        //                             packId,

        //                         SpSupplierId =
        //                             supplier.supID,

        //                         SpByBoq =
        //                             ByBoq
        //                     };

        //                 await _dbcontext
        //                     .TblSupplierPackages
        //                     .AddAsync(
        //                         supplierPackage
        //                     );

        //                 await _dbcontext
        //                     .SaveChangesAsync();

        //                 supplierPackageModelList.Add(
        //                     new AddSupplierPackageModel
        //                     {
        //                         SpPackSuppId =
        //                             supplierPackage
        //                                 .SpPackSuppId,

        //                         SpPackageId =
        //                             supplierPackage
        //                                 .SpPackageId,

        //                         SpSupplierId =
        //                             supplierPackage
        //                                 .SpSupplierId,

        //                         SpByBoq =
        //                             supplierPackage
        //                                 .SpByBoq,

        //                         ProjectCode =
        //                             proj.PrjCode,

        //                         ProjectName =
        //                             proj.PrjName,

        //                         TecCondSent =
        //                             false
        //                     }
        //                 );
        //             }

        //             PackageSupplierId =
        //                 supplierPackage
        //                     .SpPackSuppId;

        //             /*
        //              * 2. Shift existing revisions.
        //              */
        //             int LastRevNo =
        //                 await GetMaxRevisionNumberAsync(
        //                     PackageSupplierId,
        //                     _dbcontext
        //                 );

        //             if (LastRevNo >= 0)
        //             {
        //                 bool hasPendingRevision =
        //                     await _dbcontext
        //                         .TblSupplierPackageRevisions
        //                         .AnyAsync(x =>
        //                             x.PrPackSuppId ==
        //                                 PackageSupplierId &&
        //                             x.PrRevNo ==
        //                                 LastRevNo &&
        //                             (x.StatusId ?? 0) <
        //                                 3
        //                         );

        //                 if (hasPendingRevision)
        //                 {
        //                     throw new Exception(
        //                         "Revision not returned from Supplier. " +
        //                         "Supplier ID: " +
        //                         supplier.supID +
        //                         ", Package Supplier ID: " +
        //                         PackageSupplierId +
        //                         ", Revision No: " +
        //                         LastRevNo
        //                     );
        //                 }

        //                 List<TblSupplierPackageRevision>
        //                     existingRevisions =
        //                         await _dbcontext
        //                             .TblSupplierPackageRevisions
        //                             .Where(x =>
        //                                 x.PrPackSuppId ==
        //                                     PackageSupplierId
        //                             )
        //                             .OrderByDescending(x =>
        //                                 x.PrRevNo
        //                             )
        //                             .ToListAsync();

        //                 foreach (
        //                     TblSupplierPackageRevision revision
        //                     in existingRevisions
        //                 )
        //                 {
        //                     revision.PrRevNo =
        //                         revision.PrRevNo +
        //                         1;
        //                 }

        //                 await _dbcontext
        //                     .SaveChangesAsync();
        //             }

        //             TblSupplierPackageRevision Rev1 =
        //                 await _dbcontext
        //                     .TblSupplierPackageRevisions
        //                     .FirstOrDefaultAsync(x =>
        //                         x.PrRevNo ==
        //                             1 &&
        //                         x.PrPackSuppId ==
        //                             PackageSupplierId
        //                     );

        //             int rev1Id =
        //                 Rev1?.PrRevId ??
        //                 0;

        //             TblSupplierPackageRevision
        //                 supPackRev;

        //             if (Rev1 != null)
        //             {
        //                 supPackRev =
        //                     new TblSupplierPackageRevision
        //                     {
        //                         PrRevNo =
        //                             0,

        //                         PrPackSuppId =
        //                             PackageSupplierId,

        //                         PrTotPrice =
        //                             Rev1.PrTotPrice,

        //                         PrRevDate =
        //                             DateTime.Now,

        //                         PrCurrency =
        //                             Rev1.PrCurrency,

        //                         PrExchRate =
        //                             Rev1.PrExchRate,

        //                         RevExpiryDate =
        //                             ExpiryDate,

        //                         InsertedBy =
        //                             UserName,

        //                         InsertedByEmail =
        //                             usrEmail
        //                     };
        //             }
        //             else
        //             {
        //                 int projectCurrency =
        //                     Convert.ToInt32(
        //                         p.EstimatedCur
        //                     );

        //                 supPackRev =
        //                     new TblSupplierPackageRevision
        //                     {
        //                         PrRevNo =
        //                             0,

        //                         PrPackSuppId =
        //                             PackageSupplierId,

        //                         PrTotPrice =
        //                             0,

        //                         PrRevDate =
        //                             DateTime.Now,

        //                         PrCurrency =
        //                             projectCurrency,

        //                         PrExchRate =
        //                             1,

        //                         RevExpiryDate =
        //                             ExpiryDate,

        //                         InsertedBy =
        //                             UserName,

        //                         InsertedByEmail =
        //                             usrEmail
        //                     };
        //             }

        //             await _dbcontext
        //                 .TblSupplierPackageRevisions
        //                 .AddAsync(
        //                     supPackRev
        //                 );

        //             await _dbcontext
        //                 .SaveChangesAsync();

        //             /*
        //              * EF Core fills the identity after SaveChanges.
        //              */
        //             int rev0Id =
        //                 supPackRev.PrRevId;

        //             TblSupplierPackageRevision Rev0 =
        //                 supPackRev;

        //             byte byBoq =
        //                 Convert.ToByte(
        //                     supplierPackage
        //                         .SpByBoq ??
        //                     0
        //                 );

        //             List<TblRevisionDetail>
        //                 LstRevDetails =
        //                     await InsertRevisionDetail(
        //                         rev0Id,
        //                         packId,
        //                         byBoq,
        //                         rev1Id,
        //                         _dbcontext
        //                     );

        //             if (
        //                 LstRevDetails != null &&
        //                 LstRevDetails.Count > 0
        //             )
        //             {
        //                 await _dbcontext
        //                     .TblRevisionDetails
        //                     .AddRangeAsync(
        //                         LstRevDetails
        //                     );

        //                 await _dbcontext
        //                     .SaveChangesAsync();
        //             }

        //             /*
        //              * Insert Commercial Conditions.
        //              */
        //             List<TblSuppComCondReply>
        //                 LstComCondReply =
        //                     await InsertComercialConditions(
        //                         rev0Id,
        //                         packId,
        //                         rev1Id,
        //                         supInput.comercialCondList,
        //                         CostConn
        //                     );

        //             /*
        //              * Insert Technical Conditions.
        //              */
        //             List<TblSuppTechCondReply> LstTechCondReply = await InsertTechnicalConditions(
        //                         rev0Id,
        //                         packId,
        //                         rev1Id,
        //                         supInput.technicalCondList,
        //                         CostConn
        //                     );

        //             /*
        //              * Create the shared RFQ Excel only once.
        //              *
        //              * All suppliers use the same selected commercial
        //              * and technical conditions, so the first supplier's
        //              * condition lists are used to build the common file.
        //              */
        //             if (!sharedRfqExcelCreated)
        //             {
        //                 sharedRfqExcelWithConditions =
        //                     CreateSharedRfqExcelWithConditions(
        //                         baseRfqExcel,
        //                         supInput.comercialCondList,
        //                         LstComCondReply,
        //                         supInput.technicalCondList,
        //                         LstTechCondReply
        //                     );

        //                 temporaryFiles.Add(
        //                     sharedRfqExcelWithConditions
        //                 );

        //                 sharedRfqExcelCreated =
        //                     true;
        //             }

        //             /*
        //              * Portal revision model.
        //              */
        //             revisionModelList.Add(
        //                 new AddRevisionModel
        //                 {
        //                     PrRevId =
        //                         Rev0.PrRevId,

        //                     PrRevNo =
        //                         Rev0.PrRevNo,

        //                     PrRevDate =
        //                         Rev0.PrRevDate,

        //                     PrTotPrice =
        //                         Rev0.PrTotPrice,

        //                     PrPackSuppId =
        //                         Rev0.PrPackSuppId,

        //                     PrCurrency =
        //                         Rev0.PrCurrency,

        //                     PrExchRate =
        //                         1,

        //                     StatusId =
        //                         1,

        //                     ProjectCode =
        //                         proj.PrjCode,

        //                     IsSynched =
        //                         false,

        //                     RevExpiryDate =
        //                         Rev0.RevExpiryDate,

        //                     RevisionDetails =
        //                         (
        //                             from d in LstRevDetails
        //                             select new AddRevisionDetailModel
        //                             {
        //                                 BoqResourceSeq =
        //                                     d.RdResourceSeq,

        //                                 ResourceDescription =
        //                                     GetRessourceDescription(
        //                                         ByBoq,
        //                                         d.RdResourceSeq,
        //                                         d.ResourceDescription,
        //                                         Convert.ToBoolean(
        //                                             d.IsAlternative
        //                                         ),
        //                                         CostConn
        //                                     ),

        //                                 ItemO =
        //                                     d.RdBoqItem,

        //                                 ItemDescription =
        //                                     d.ItemDescription,

        //                                 Quantity =
        //                                     d.RdQty,

        //                                 QuotationQty =
        //                                     d.RdQuotationQty,

        //                                 UnitPrice =
        //                                     d.RdPrice,

        //                                 TotalPrice =
        //                                     d.RdQty *
        //                                     d.UnitPriceAfterDiscount,

        //                                 DiscountPerc =
        //                                     d.RdDiscount,

        //                                 Comments =
        //                                     d.RdComment,

        //                                 CreatedOn =
        //                                     DateTime.Now,

        //                                 IsSynched =
        //                                     false,

        //                                 ProjectCode =
        //                                     proj.PrjCode,

        //                                 ParentItemO =
        //                                     d.ParentItemO,

        //                                 ParentResourceId =
        //                                     d.ParentResourceId
        //                                         .ToString(),

        //                                 NewItemId =
        //                                     d.NewItemId,

        //                                 NewItemResourceId =
        //                                     d.NewItemResourceId,

        //                                 IsNewItem =
        //                                     d.IsNew,

        //                                 IsAlternative =
        //                                     d.IsAlternative,

        //                                 UnitPriceAfterDiscount =
        //                                     d.UnitPriceAfterDiscount,

        //                                 UnitO =
        //                                     d.UnitO ??
        //                                     string.Empty,

        //                                 BoqCtg =
        //                                     d.BoqCtg ??
        //                                     string.Empty,

        //                                 BoqUnitMesure =
        //                                     d.BoqUnitMesure ??
        //                                     string.Empty,

        //                                 L1 =
        //                                     d.L1 ??
        //                                     string.Empty,

        //                                 L2 =
        //                                     d.L2 ??
        //                                     string.Empty,

        //                                 L3 =
        //                                     d.L3 ??
        //                                     string.Empty,

        //                                 L4 =
        //                                     d.L4 ??
        //                                     string.Empty,

        //                                 L5 =
        //                                     d.L5 ??
        //                                     string.Empty,

        //                                 L6 =
        //                                     d.L6 ??
        //                                     string.Empty,

        //                                 L7 =
        //                                     d.L7 ??
        //                                     string.Empty,

        //                                 L8 =
        //                                     d.L8 ??
        //                                     string.Empty,

        //                                 L9 =
        //                                     d.L9 ??
        //                                     string.Empty,

        //                                 L10 =
        //                                     d.L10 ??
        //                                     string.Empty,

        //                                 C1 =
        //                                     d.C1 ??
        //                                     string.Empty,

        //                                 C2 =
        //                                     d.C2 ??
        //                                     string.Empty,

        //                                 C3 =
        //                                     d.C3 ??
        //                                     string.Empty,

        //                                 C4 =
        //                                     d.C4 ??
        //                                     string.Empty,

        //                                 C5 =
        //                                     d.C5 ??
        //                                     string.Empty,

        //                                 C6 =
        //                                     d.C6 ??
        //                                     string.Empty,

        //                                 C7 =
        //                                     d.C7 ??
        //                                     string.Empty,

        //                                 C8 =
        //                                     d.C8 ??
        //                                     string.Empty,

        //                                 C9 =
        //                                     d.C9 ??
        //                                     string.Empty,

        //                                 C10 =
        //                                     d.C10 ??
        //                                     string.Empty,

        //                                 C11 =
        //                                     d.C11 ??
        //                                     string.Empty,

        //                                 C12 =
        //                                     d.C12 ??
        //                                     string.Empty,

        //                                 C13 =
        //                                     d.C13 ??
        //                                     string.Empty,

        //                                 C14 =
        //                                     d.C14 ??
        //                                     string.Empty,

        //                                 C15 =
        //                                     d.C15 ??
        //                                     string.Empty,

        //                                 BoqRefNumber =
        //                                     d.RdBoqRefNumber ??
        //                                     string.Empty,

        //                                 AccComment =
        //                                     d.RdAccComment ??
        //                                     string.Empty
        //                             }
        //                         )
        //                         .ToList(),

        //                     CommercialConditions =
        //                         (
        //                             from d in LstComCondReply
        //                             select new AddCondModel
        //                             {
        //                                 Id =
        //                                     d.CdComConId,

        //                                 CondValue =
        //                                     d.CdSuppReply,

        //                                 ACCCondValue =
        //                                     d.CdAccCond,

        //                                 ProjectCode =
        //                                     proj.PrjCode
        //                             }
        //                         )
        //                         .ToList(),

        //                     TechnicalConditions =
        //                         (
        //                             from d in LstTechCondReply
        //                             select new AddCondModel
        //                             {
        //                                 Id =
        //                                     d.TcTechConId,

        //                                 CondValue =
        //                                     d.TcSuppReply,

        //                                 ACCCondValue =
        //                                     d.TcAccCond,

        //                                 ProjectCode =
        //                                     proj.PrjCode
        //                             }
        //                         )
        //                         .ToList()
        //                 }
        //             );

        //             /*
        //              * Build email attachments.
        //              * Every supplier receives the same completed RFQ Excel.
        //              */
        //             List<string> supplierAttachmentList =
        //                 new List<string>();

        //             if (
        //                 !string.IsNullOrWhiteSpace(
        //                     supInput.FilePath
        //                 )
        //             )
        //             {
        //                 supplierAttachmentList.Add(
        //                     supInput.FilePath
        //                 );
        //             }

        //             if (
        //                 supInput.mailAttachments != null
        //             )
        //             {
        //                 foreach (
        //                     string attachment
        //                     in supInput.mailAttachments
        //                 )
        //                 {
        //                     if (
        //                         !string.IsNullOrWhiteSpace(
        //                             attachment
        //                         )
        //                     )
        //                     {
        //                         supplierAttachmentList.Add(
        //                             attachment
        //                         );
        //                     }
        //                 }
        //             }

        //             if (
        //                 string.IsNullOrWhiteSpace(
        //                     sharedRfqExcelWithConditions
        //                 ) ||
        //                 !File.Exists(
        //                     sharedRfqExcelWithConditions
        //                 )
        //             )
        //             {
        //                 throw new Exception(
        //                     "The RFQ Excel file with conditions was not created."
        //                 );
        //             }

        //             supplierAttachmentList.Add(
        //                 sharedRfqExcelWithConditions
        //             );

        //             /*
        //              * Build separate email for this supplier.
        //              */
        //             List<string> supplierEmailTo =
        //                 NormalizeRepositoryEmailList(
        //                     supInput.mailTo
        //                 );

        //             if (supplierEmailTo.Count == 0)
        //             {
        //                 throw new Exception(
        //                     "No Email To address was provided for supplier " +
        //                     (
        //                         supInput.supplierName ??
        //                         supplier.supID.ToString()
        //                     )
        //                 );
        //             }

        //             MailForSending mail =
        //                 new MailForSending();

        //             foreach (
        //                 string toAddress
        //                 in supplierEmailTo
        //             )
        //             {
        //                 mail.To.Add(
        //                     toAddress
        //                 );
        //             }

        //             /*
        //              * Apply the same shared CC to every supplier email.
        //              */
        //             List<string> cleanSharedCc =
        //                 emailCc
        //                     .Where(cc =>
        //                         !supplierEmailTo.Contains(
        //                             cc,
        //                             StringComparer.OrdinalIgnoreCase
        //                         )
        //                     )
        //                     .Distinct(
        //                         StringComparer.OrdinalIgnoreCase
        //                     )
        //                     .ToList();

        //             foreach (
        //                 string ccAddress
        //                 in cleanSharedCc
        //             )
        //             {
        //                 mail.Cc.Add(
        //                     ccAddress
        //                 );
        //             }

        //             if (
        //                 !string.IsNullOrWhiteSpace(
        //                     _configuration["mailBcc1"]
        //                 )
        //             )
        //             {
        //                 mail.Bcc.Add(
        //                     _configuration["mailBcc1"]
        //                 );
        //             }

        //             if (
        //                 !string.IsNullOrWhiteSpace(
        //                     _configuration["mailBcc2"]
        //                 )
        //             )
        //             {
        //                 mail.Bcc.Add(_configuration["mailBcc2"]);
        //             }

        //             mail.Subject = $"Job in Hand-{proj.PrjName}-{PackageName}";

        //             mail.Body = !string.IsNullOrWhiteSpace(supInput.EmailTemplate)
        //                     ? supInput.EmailTemplate
        //                     : @"Dear Sir,<br><br>
        //                  Kindly find attachments and fill the price.
        //                  <br><br>Best regards";

        //             if (
        //                 !string.IsNullOrWhiteSpace(
        //                     userSignature
        //                 )
        //             )
        //             {
        //                 mail.Body +=
        //                     "<br><br>" +
        //                     userSignature;
        //             }

        //             /*
        //              * Copy the attachment list so each email has
        //              * its own independent list instance.
        //              */
        //             mail.Attachments =
        //                 supplierAttachmentList
        //                     .Distinct(
        //                         StringComparer.OrdinalIgnoreCase
        //                     )
        //                     .ToList();

        //             mailListForSending.Add(
        //                 mail
        //             );
        //         }

        //         if (!sharedRfqExcelCreated)
        //         {
        //             throw new Exception(
        //                 "The RFQ Excel file with conditions was not created."
        //             );
        //         }

        //         supplierPackageRevisionModel.SupplierPackageModels = supplierPackageModelList;

        //         supplierPackageRevisionModel.RevisionModels = revisionModelList;

        //         /*
        //          * Post data to portal API.
        //          */
        //         string body = System.Text.Json.JsonSerializer.Serialize(supplierPackageRevisionModel);

        //         string portalApiPath = _configuration["PortalApiPath"];

        //         string key = _configuration["External:Key"];

        //         using StringContent requestContent =
        //             new StringContent(
        //                 body,
        //                 Encoding.UTF8,
        //                 "application/json"
        //             );

        //         /*
        //          * Avoid adding the Authorization header repeatedly
        //          * to the shared HttpClient.
        //          */
        //         using HttpRequestMessage portalRequest = new HttpRequestMessage(HttpMethod.Post, portalApiPath + "External/AddSupplierRevisionInPortal");

        //         portalRequest.Content = requestContent;

        //         if (!string.IsNullOrWhiteSpace(key))
        //         {
        //             portalRequest.Headers.TryAddWithoutValidation("Authorization", key);
        //         }

        //         using HttpResponseMessage response = await _httpClient.SendAsync(portalRequest);

        //         response.EnsureSuccessStatusCode();

        //         string content =
        //             await response.Content
        //                 .ReadAsStringAsync();

        //         if (
        //             !string.Equals(
        //                 content?.Trim(),
        //                 "true",
        //                 StringComparison.OrdinalIgnoreCase
        //             )
        //         )
        //         {
        //             throw new Exception(
        //                 "An error occurred on the Portal API."
        //             );
        //         }

        //         /*
        //          * Send one separate email to every supplier.
        //          * Each email contains the same completed RFQ Excel.
        //          */
        //         if (mailListForSending.Count == 0)
        //         {
        //             throw new Exception(
        //                 "No supplier emails were prepared."
        //             );
        //         }

        //         foreach (
        //             MailForSending email
        //             in mailListForSending
        //         )
        //         {
        //             Mail mailSender =
        //                 new Mail();

        //             string currentSendResult =
        //                 mailSender.SendMail(
        //                     email.To,
        //                     email.Cc,
        //                     email.Bcc,
        //                     email.Subject,
        //                     email.Body,
        //                     email.Attachments,
        //                     email.IsBodyHtml,
        //                     attachments
        //                 );

        //             if (
        //                 !string.Equals(
        //                     currentSendResult,
        //                     "sent",
        //                     StringComparison.OrdinalIgnoreCase
        //                 )
        //             )
        //             {
        //                 throw new Exception(
        //                     "Email was not sent to: " +
        //                     string.Join(
        //                         "; ",
        //                         email.To
        //                     ) +
        //                     ". Mail result: " +
        //                     currentSendResult
        //                 );
        //             }
        //         }

        //         await transaction.CommitAsync();

        //         return true;
        //     }
        //     catch
        //     {
        //         await transaction.RollbackAsync();

        //         throw;
        //     }
        //     finally
        //     {
        //         await transaction.DisposeAsync();

        //         await _dbcontext.DisposeAsync();

        //         await _TSdbcontext.DisposeAsync();

        //         /*
        //          * Delete only the temporary condition-enhanced copy.
        //          *
        //          * The base Excel generated by ValidateExcelBeforeAssign
        //          * is not deleted here because the existing application
        //          * may manage that file separately.
        //          */
        //         foreach (
        //             string temporaryFile
        //             in temporaryFiles
        //         )
        //         {
        //             try
        //             {
        //                 if (
        //                     !string.IsNullOrWhiteSpace(
        //                         temporaryFile
        //                     ) &&
        //                     File.Exists(temporaryFile)
        //                 )
        //                 {
        //                     File.Delete(
        //                         temporaryFile
        //                     );
        //                 }
        //             }
        //             catch (Exception cleanupException)
        //             {
        //                 Console.WriteLine(
        //                     "Unable to delete temporary RFQ file: " +
        //                     temporaryFile +
        //                     ". Error: " +
        //                     cleanupException.Message
        //                 );
        //             }
        //         }
        //     }
        // }



        private static List<string> NormalizeRepositoryEmailList(IEnumerable<string> emails)
        {
            if (emails == null)
            {
                return new List<string>();
            }

            return emails
                .Where(email =>
                    !string.IsNullOrWhiteSpace(email)
                )
                .SelectMany(email =>
                    email.Split(
                        new[]
                        {
                    ',',
                    ';',
                    '\r',
                    '\n'
                        },
                        StringSplitOptions
                            .RemoveEmptyEntries
                    )
                )
                .Select(email =>
                    email.Trim().ToLowerInvariant()
                )
                .Where(email =>
                    !string.IsNullOrWhiteSpace(email)
                )
                .Distinct(
                    StringComparer.OrdinalIgnoreCase
                )
                .ToList();
        }

        private string GetRessourceDescription(byte ByBoq, string boqResSeq, string resourceDescription, bool isAlternative, string CostConn)
        {
            AccDbContext _dbcontext = new AccDbContext(CostConn);

            string resDesc = "";

            if (ByBoq == 1)
                return "";

            if (resourceDescription != "" && resourceDescription != null)
            {
                resDesc = resourceDescription;
            }
            else
            {
                var result = _dbcontext.TblResources.Where(x => x.ResSeq == boqResSeq).FirstOrDefault();

                if (result != null)
                    resDesc = result.ResDescription;
            }

            return resDesc;
        }

        private string GetBoqItemDescription(string boqItemO, string CostConn)
        {
            AccDbContext _dbcontext = new AccDbContext(CostConn);

            var result = _dbcontext.TblOriginalBoqVds.Where(x => x.ItemO == boqItemO).FirstOrDefault();

            if (result == null)
                return "";
            else
                return result.DescriptionO;
        }

        private async Task<List<TblRevisionDetail>> InsertRevisionDetail(int revId, int packId, byte byBoq, int rev1Id, AccDbContext _dbcontext)
        {
            //AccDbContext _dbcontext = new AccDbContext(CostConn);
            List<TblRevisionDetail> LstRevDetails = new List<TblRevisionDetail>();

            if (rev1Id > 0)  //In case of previous revision exists , so you have to insert new details from pervious
            {
                var oldRevDtl = _dbcontext.TblRevisionDetails.Where(x => x.RdRevisionId == rev1Id).ToList();
                if (oldRevDtl != null)
                {
                    foreach (var row in oldRevDtl)
                    {
                        var revdtl = new TblRevisionDetail()
                        {
                            RdRevisionId = revId,
                            RdResourceSeq = row.RdResourceSeq,
                            RdBoqItem = row.RdBoqItem,
                            RdPrice = row.RdPrice,
                            RdPriceOrigCurrency = row.RdPriceOrigCurrency,
                            RdQty = row.RdQty,
                            RdQuotationQty = row.RdQuotationQty,
                            RdComment = row.RdComment,
                            RdMissedPrice = row.RdMissedPrice,
                            RdDiscount = row.RdDiscount,
                            RdAddedItem = row.RdAddedItem,
                            IsAlternative = row.IsAlternative,
                            IsNew = row.IsNew,
                            NewItemId = row.NewItemId,
                            NewItemResourceId = row.NewItemResourceId,
                            ParentItemO = row.ParentItemO,
                            ParentResourceId = row.ParentResourceId,
                            ResourceDescription = row.ResourceDescription,
                            ItemDescription = row.ItemDescription,
                            UnitPriceAfterDiscount = row.UnitPriceAfterDiscount,
                            TotalPrice = Math.Round((double)(row.RdQuotationQty * row.UnitPriceAfterDiscount), 2),
                            UnitO = row.UnitO,
                            BoqCtg = row.BoqCtg,
                            BoqUnitMesure = row.BoqUnitMesure,
                            L1 = row.L1,
                            L2 = row.L2,
                            L3 = row.L3,
                            L4 = row.L4,
                            L5 = row.L5,
                            L6 = row.L6,
                            L7 = row.L7,
                            L8 = row.L8,
                            L9 = row.L9,
                            L10 = row.L10,
                            C1 = row.C1,
                            C2 = row.C2,
                            C3 = row.C3,
                            C4 = row.C4,
                            C5 = row.C5,
                            C6 = row.C6,
                            C7 = row.C7,
                            C8 = row.C8,
                            C9 = row.C9,
                            C10 = row.C10,
                            C11 = row.C11,
                            C12 = row.C12,
                            C13 = row.C13,
                            C14 = row.C14,
                            C15 = row.C15,
                            RdBoqRefNumber = row.RdBoqRefNumber,
                            RdAccComment = row.RdAccComment,
                        };
                        LstRevDetails.Add(revdtl);
                    }
                }
            }
            else
            {
                List<BoqRessourcesList> result = new List<BoqRessourcesList>();
                double discount = 0;

                if (byBoq == 1)
                {
                    result = (from o in _dbcontext.TblOriginalBoqVds
                              join b in _dbcontext.TblBoqVds on o.ItemO equals b.BoqItem
                              where b.BoqScope == packId
                              select new BoqRessourcesList
                              {
                                  RowNumber = o.RowNumber,
                                  SectionO = o.SectionO,
                                  ItemO = o.ItemO,
                                  DescriptionO = o.DescriptionO,
                                  UnitO = o.UnitO,
                                  ScopeQtyO = o.QtyScope,
                                  UnitRateO = o.UnitRate,
                                  ScopeO = o.Scope,
                                  BoqUprice = b.BoqUprice,
                                  BoqQty = b.BoqQty,
                                  BoqCtg = "",
                                  L1 = o.L1,
                                  L2 = o.L2,
                                  L3 = o.L3,
                                  L4 = o.L4,
                                  L5 = o.L5,
                                  L6 = o.L6,
                                  L7 = o.L7,
                                  L8 = o.L8,
                                  L9 = o.L9,
                                  L10 = o.L10,
                                  C1 = o.C1,
                                  C2 = o.C2,
                                  C3 = o.C3,
                                  C4 = o.C4,
                                  C5 = o.C5,
                                  C6 = o.C6,
                                  C7 = o.C7,
                                  C8 = o.C8,
                                  C9 = o.C9,
                                  C10 = o.C10,
                                  C11 = o.C11,
                                  C12 = o.C12,
                                  C13 = o.C13,
                                  C14 = o.C14,
                                  C15 = o.C15,
                                  BoqRefNumber = o.RefNumber,
                                  ObTradeDesc = o.ObTradeDesc,
                              }).ToList();

                    //AH102025
                    var boqGrp = result
                            .GroupBy(x => new { x.ItemO })
                            .Select(p => new BoqRessourcesList
                            {
                                RowNumber = p.First().RowNumber,
                                SectionO = p.First().SectionO,
                                ItemO = p.First().ItemO,
                                DescriptionO = p.First().DescriptionO,
                                UnitO = p.First().UnitO,
                                ScopeQtyO = p.First().ScopeQtyO,
                                UnitRateO = p.First().UnitRateO,
                                ScopeO = p.First().ScopeO,
                                BoqRefNumber = p.First().BoqRefNumber,
                                BoqCtg = p.First().BoqCtg,
                                BoqQty = p.Sum(c => c.BoqQty),
                                BoqTotalPrice = p.Sum(c => c.BoqQty * c.BoqUprice),
                                L1 = p.First().L1,
                                L2 = p.First().L2,
                                L3 = p.First().L3,
                                L4 = p.First().L4,
                                L5 = p.First().L5,
                                L6 = p.First().L6,
                                L7 = p.First().L7,
                                L8 = p.First().L8,
                                L9 = p.First().L9,
                                L10 = p.First().L10,
                                C1 = p.First().C1,
                                C2 = p.First().C2,
                                C3 = p.First().C3,
                                C4 = p.First().C4,
                                C5 = p.First().C5,
                                C6 = p.First().C6,
                                C7 = p.First().C7,
                                C8 = p.First().C8,
                                C9 = p.First().C9,
                                C10 = p.First().C10,
                                C11 = p.First().C11,
                                C12 = p.First().C12,
                                C13 = p.First().C13,
                                C14 = p.First().C14,
                                C15 = p.First().C15,
                                ObTradeDesc = p.First().ObTradeDesc,
                            }).ToList();
                    ///AH102025

                    foreach (var row in boqGrp)
                    {
                        if ((row.ItemO != "") && (row.ScopeQtyO > 0))
                        {
                            var revdtl = new TblRevisionDetail()
                            {
                                RdRevisionId = revId,
                                RdResourceSeq = "0",
                                RdBoqItem = row.ItemO,
                                RdPrice = 0,
                                RdPriceOrigCurrency = 0,
                                RdQty = row.ScopeQtyO,
                                RdQuotationQty = row.ScopeQtyO,
                                RdComment = "",
                                RdMissedPrice = 0,
                                RdDiscount = discount,
                                RdAddedItem = 0,
                                IsAlternative = false,
                                IsNew = false,
                                NewItemId = 0,
                                NewItemResourceId = 0,
                                ParentItemO = "",
                                ParentResourceId = "0",
                                ResourceDescription = "",
                                ItemDescription = row.DescriptionO,
                                UnitPriceAfterDiscount = 0,
                                TotalPrice = 0,
                                UnitO = row.UnitO,
                                BoqCtg = row.BoqCtg,
                                RdBudUnitPrice = (row.BoqTotalPrice / row.ScopeQtyO),
                                BoqUnitMesure = row.BoqUnitMesure,
                                L1 = row.L1,
                                L2 = row.L2,
                                L3 = row.L3,
                                L4 = row.L4,
                                L5 = row.L5,
                                L6 = row.L6,
                                L7 = row.L7,
                                L8 = row.L8,
                                L9 = row.L9,
                                L10 = row.L10,
                                C1 = row.C1,
                                C2 = row.C2,
                                C3 = row.C3,
                                C4 = row.C4,
                                C5 = row.C5,
                                C6 = row.C6,
                                C7 = row.C7,
                                C8 = row.C8,
                                C9 = row.C9,
                                C10 = row.C10,
                                C11 = row.C11,
                                C12 = row.C12,
                                C13 = row.C13,
                                C14 = row.C14,
                                C15 = row.C15,
                                RdBoqRefNumber = row.BoqRefNumber,
                                RdAccComment = row.ObTradeDesc
                            };
                            LstRevDetails.Add(revdtl);
                        }
                    }
                }
                else
                {
                    result = (from o in _dbcontext.TblOriginalBoqVds
                              join b in _dbcontext.TblBoqVds on o.ItemO equals b.BoqItem
                              join r in _dbcontext.TblResources on b.BoqResSeq equals r.ResSeq
                              where b.BoqScope == packId
                              select new BoqRessourcesList
                              {
                                  RowNumber = 0,
                                  BoqItem = b.BoqItem,
                                  BoqSeq = b.BoqSeq,
                                  BoqCtg = b.BoqCtg,
                                  BoqUnitMesure = b.BoqUnitMesure,
                                  BoqScopeQty = b.BoqQtyScope,
                                  BoqUprice = b.BoqUprice,
                                  BoqDiv = b.BoqDiv,
                                  BoqPackage = b.BoqPackage,
                                  BoqScope = b.BoqScope,
                                  DescriptionO = o.DescriptionO,
                                  BoqResSeq = b.BoqResSeq,
                                  ResDescription = r.ResDescription,
                                  ResSeq = r.ResSeq,
                                  UnitO = o.UnitO,
                                  L1 = o.L1,
                                  L2 = o.L2,
                                  L3 = o.L3,
                                  L4 = o.L4,
                                  L5 = o.L5,
                                  L6 = o.L6,
                                  L7 = o.L7,
                                  L8 = o.L8,
                                  L9 = o.L9,
                                  L10 = o.L10,
                                  C1 = o.C1,
                                  C2 = o.C2,
                                  C3 = o.C3,
                                  C4 = o.C4,
                                  C5 = o.C5,
                                  C6 = o.C6,
                                  C7 = o.C7,
                                  C8 = o.C8,
                                  C9 = o.C9,
                                  C10 = o.C10,
                                  C11 = o.C11,
                                  C12 = o.C12,
                                  C13 = o.C13,
                                  C14 = o.C14,
                                  C15 = o.C15,
                                  BoqRefNumber = o.RefNumber
                              }).ToList();

                    var resourcesGrp = result
                            .GroupBy(x => new { x.BoqResSeq, x.ResDescription, x.BoqUnitMesure, x.BoqUprice, x.BoqScope })
                            .Select(p => new BoqRessourcesList
                            {
                                RowNumber = 0,
                                BoqItem = p.First().BoqItem,
                                BoqSeq = p.First().BoqSeq,
                                BoqCtg = p.First().BoqCtg,
                                BoqUnitMesure = p.First().BoqUnitMesure,
                                BoqScopeQty = p.Sum(c => c.BoqScopeQty),
                                BoqUprice = p.First().BoqUprice,
                                BoqDiv = p.First().BoqDiv,
                                BoqPackage = p.First().BoqPackage,
                                BoqScope = p.First().BoqScope,
                                BoqResSeq = p.First().BoqResSeq,
                                DescriptionO = p.First().ResDescription,
                                ResDescription = p.First().ResDescription,
                                BoqRefNumber = p.First().BoqRefNumber
                                //L1 = p.First().L1,
                                //L2 = p.First().L2,
                                //L3 = p.First().L3,
                                //L4 = p.First().L4,
                                //L5 = p.First().L5,
                                //L6 = p.First().L6,
                                //L7 = p.First().L7,
                                //L8 = p.First().L8,
                                //L9 = p.First().L9,
                                //L10 = p.First().L10,
                                //C1 = p.First().C1,
                                //C2 = p.First().C2,
                                //C3 = p.First().C3,
                                //C4 = p.First().C4,
                                //C5 = p.First().C5,
                                //C6 = p.First().C6,
                                //C7 = p.First().C7,
                                //C8 = p.First().C8,
                                //C9 = p.First().C9,
                                //C10 = p.First().C10,
                                //C11 = p.First().C11,
                                //C12 = p.First().C12,
                                //C13 = p.First().C13,
                                //C14 = p.First().C14,
                                //C15 = p.First().C15
                            }).ToList();


                    foreach (var row in resourcesGrp)
                    {
                        if (row.BoqScopeQty > 0)
                        {
                            var revdtl = new TblRevisionDetail()
                            {
                                RdRevisionId = revId,
                                RdResourceSeq = row.BoqResSeq,
                                RdBoqItem = row.BoqItem,
                                RdPrice = 0,
                                BoqUnitMesure = row.BoqUnitMesure,
                                RdQty = row.BoqScopeQty,
                                RdQuotationQty = row.BoqScopeQty,
                                RdComment = "",
                                RdPriceOrigCurrency = 0,
                                RdMissedPrice = 0,
                                RdDiscount = discount,
                                RdAddedItem = 0,
                                IsAlternative = false,
                                IsNew = false,
                                NewItemId = 0,
                                NewItemResourceId = 0,
                                ParentItemO = "",
                                ParentResourceId = "0",
                                ResourceDescription = row.ResDescription,
                                ItemDescription = row.DescriptionO,
                                UnitPriceAfterDiscount = 0,
                                TotalPrice = 0,
                                RdBudUnitPrice = row.BoqUprice,
                                RdBoqRefNumber = row.BoqRefNumber,
                                //L1=row.L1,
                                //L2 = row.L2,
                                //L3 = row.L3,
                                //L4 = row.L4,
                                //L5 = row.L5,
                                //L6 = row.L6,
                                //L7 = row.L7,
                                //L8 = row.L8,
                                //L9 = row.L9,
                                //L10 = row.L10,
                                //C1 = row.C1,
                                //C2 = row.C2,
                                //C3 = row.C3,
                                //C4 = row.C4,
                                //C5 = row.C5,
                                //C6 = row.C6,
                                //C7 = row.C7,
                                //C8 = row.C8,
                                //C9 = row.C9,
                                //C10 = row.C10,
                                //C11 = row.C11,
                                //C12 = row.C12,
                                //C13 = row.C13,
                                //C14 = row.C14,
                                //C15 = row.C15
                            };
                            LstRevDetails.Add(revdtl);
                        }
                    }
                }
            }

            //AH23112025
            //if (LstRevDetails.Count() > 0)
            //    {
            //        await _dbcontext.AddRangeAsync(LstRevDetails);
            //        await _dbcontext.SaveChangesAsync();
            //    }               
            ///AH23112025
            ///
            return LstRevDetails;
        }

        private async Task<List<TblSuppComCondReply>> InsertComercialConditions(int revId, int packId, int rev1Id, List<Condition> comCondList, string CostConn)
        {
            AccDbContext _dbcontext = new AccDbContext(CostConn);

            List<TblSuppComCondReply> LstComCondReply = new List<TblSuppComCondReply>();

            foreach (var comCond in comCondList)
            {
                var ComCondReply = new TblSuppComCondReply()
                {
                    CdRevisionId = revId,
                    CdComConId = comCond.id,
                    CdSuppReply = "",
                    CdAccCond = comCond.ACCCondValue
                };
                LstComCondReply.Add(ComCondReply);
            }

            if (LstComCondReply.Count() > 0)
            {
                await _dbcontext.AddRangeAsync(LstComCondReply);
                await _dbcontext.SaveChangesAsync();
            }


            //Send List with Supplier Reply from Old Revision  
            List<TblSuppComCondReply> lstComCondReplyPortal = new List<TblSuppComCondReply>();
            foreach (var comCond in comCondList)
            {
                var ComCondReply = new TblSuppComCondReply()
                {
                    CdRevisionId = revId,
                    CdComConId = comCond.id,
                    CdSuppReply = "",
                    CdAccCond = comCond.ACCCondValue
                };
                lstComCondReplyPortal.Add(ComCondReply);
            }

            //In case of previous revision exists , so you have to insert new details from pervious
            if (rev1Id > 0)
            {
                var oldRevDtl = _dbcontext.TblSuppComCondReplies.Where(x => x.CdRevisionId == rev1Id).ToList();
                if (oldRevDtl != null)
                {
                    //foreach (var row in oldRevDtl)
                    //{
                    //    var ComCondReply = new TblSuppComCondReply()
                    //    {
                    //        CdRevisionId = revId,
                    //        CdComConId = row.CdComConId,
                    //        CdSuppReply = row.CdSuppReply,
                    //        CdAccCond = row.CdAccCond
                    //    };
                    //    LstComCondReply.Add(ComCondReply);
                    //}

                    foreach (var cond in oldRevDtl)
                    {
                        var replyVal = lstComCondReplyPortal.FirstOrDefault(x => x.CdComConId == cond.CdComConId);
                        if (replyVal != null)
                        {
                            replyVal.CdSuppReply = cond.CdSuppReply;
                        }
                    }
                }
                //var newConditions = comCondList.Where(s => !oldRevDtl.Where(es => es.CdComConId == s.id).Any());
                //foreach (var row in newConditions)
                //{
                //    var ComCondReply = new TblSuppComCondReply()
                //    {
                //        CdRevisionId = revId,
                //        CdComConId = row.id,
                //        CdSuppReply = "",
                //        CdAccCond = row.ACCCondValue
                //    };
                //    LstComCondReply.Add(ComCondReply);
                //}
            }

            return lstComCondReplyPortal;
        }

        private async Task<List<TblSuppTechCondReply>> InsertTechnicalConditions(int revId, int packId, int rev1Id, List<Condition> techCondList, string CostConn)
        {
            AccDbContext _dbcontext = new AccDbContext(CostConn);

            List<TblSuppTechCondReply> LstTechCondReply = new List<TblSuppTechCondReply>();

            foreach (var Cond in techCondList)
            {
                var CondReply = new TblSuppTechCondReply()
                {
                    TcRevisionId = revId,
                    TcTechConId = Cond.id,
                    TcSuppReply = "",
                    TcAccCond = Cond.ACCCondValue
                };
                LstTechCondReply.Add(CondReply);
            }

            if (LstTechCondReply.Count() > 0)
            {
                await _dbcontext.AddRangeAsync(LstTechCondReply);
                await _dbcontext.SaveChangesAsync();
            }


            //Send List with Supplier Reply from Old Revision 
            List<TblSuppTechCondReply> lstTechCondReplyPortal = new List<TblSuppTechCondReply>();
            foreach (var Cond in techCondList)
            {
                var CondReply = new TblSuppTechCondReply()
                {
                    TcRevisionId = revId,
                    TcTechConId = Cond.id,
                    TcSuppReply = "",
                    TcAccCond = Cond.ACCCondValue
                };
                lstTechCondReplyPortal.Add(CondReply);
            }

            if (rev1Id > 0)  //In case of previous revision exists , so you have to insert new details from pervious
            {
                var oldRevDtl = _dbcontext.TblSuppTechCondReplies.Where(x => x.TcRevisionId == rev1Id).ToList();
                if (oldRevDtl != null)
                {

                    foreach (var cond in oldRevDtl)
                    {
                        var replyVal = lstTechCondReplyPortal.FirstOrDefault(x => x.TcTechConId == cond.TcTechConId);
                        if (replyVal != null)
                        {
                            replyVal.TcSuppReply = cond.TcSuppReply;
                        }
                    }

                    //foreach (var row in oldRevDtl)
                    //{
                    //    var techCondReply = new TblSuppTechCondReply()
                    //    {
                    //        TcRevisionId = revId,
                    //        TcTechConId = row.TcTechConId,
                    //        TcSuppReply = row.TcSuppReply, 
                    //        TcAccCond = row.TcAccCond
                    //    };
                    //    LsttechCondReply.Add(techCondReply);
                    //}

                    //var newConditions = techCondList.Where(s => !oldRevDtl.Where(es => es.TcTechConId == s.id).Any());
                    //foreach (var row in newConditions)
                    //{
                    //    var CondReply = new TblSuppTechCondReply()
                    //    {
                    //        TcRevisionId = revId,
                    //        TcTechConId = row.id,
                    //        TcSuppReply = "",
                    //        TcAccCond = row.ACCCondValue
                    //    };
                    //    LsttechCondReply.Add(CondReply);
                    //}
                }
            }

            return lstTechCondReplyPortal;
        }

        private async Task<int> GetMaxRevisionNumberAsync(int packageSupplierId, AccDbContext dbContext)
        {
            int? maxRevisionNumber =
                await dbContext
                    .TblSupplierPackageRevisions
                    .Where(x =>
                        x.PrPackSuppId == packageSupplierId
                    )
                    .MaxAsync(x =>
                        (int?)x.PrRevNo
                    );

            return maxRevisionNumber ?? -1;
        }

        public string SendComercialConditions(int packId, List<Condition> comCondList, string CostConn)
        {
            AccDbContext _dbcontext = new AccDbContext(CostConn);

            var package = _mdbContext.TblPackages.Where(x => x.PkgeId == packId).FirstOrDefault();
            string PackageName = package.PkgeName;

            var p = _dbcontext.TblParameters.FirstOrDefault();
            //var proj = _pdbcontext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefault();
            //string ProjectName = proj.PrjName;
            string ProjectName = p.Project;

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("BOQ");
                worksheet.Columns.AutoFit();
                //worksheet.Protection.IsProtected = true;

                int i, j;

                worksheet.Cells[1, 1].Value = "Project :" + ProjectName;
                worksheet.Cells["A1:C1"].Merge = true;

                worksheet.Cells[2, 1].Value = "";
                worksheet.Cells["A2:C2"].Merge = true;

                worksheet.Cells[3, 1].Value = "ACC conditions";
                worksheet.Cells["A3:B3"].Merge = true;

                worksheet.Cells[3, 3].Value = "Supplier/subcontractor reply";
                worksheet.Column(3).Width = 40;
                worksheet.Columns[3].Style.WrapText = true;
                worksheet.Column(3).AutoFit();

                worksheet.Cells[4, 1].Value = "Commercial Conditions";
                worksheet.Cells[4, 1].Style.Font.Bold = true;
                worksheet.Cells[4, 1].Style.Font.UnderLine = true;
                worksheet.Cells["A4:B4"].Merge = true;

                i = 5;
                foreach (var x in comCondList)
                {
                    worksheet.Cells[i, 2].Value = (x.description == null) ? "" : x.description;
                    worksheet.Column(2).Width = 50;

                    i++;
                }

                xlPackage.Save();
                stream.Position = 0;
                string excelName = $"{ProjectName}-Commercial Conditions-{PackageName}-{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";

                //string path = @"C:\App\";

                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //string FullPath = path + excelName;

                if (File.Exists(excelName))
                    File.Delete(excelName);

                excelName = excelName.Replace("/", "-");
                excelName = excelName.Replace("&", "-");

                xlPackage.SaveAs(excelName);

                return excelName;
            }
        }

    }


    internal static class SharedRfqWorkbookBuilder
    {
        public static string Create(
            string sourceExcelPath,
            List<Condition> commercialInput,
            List<TblSuppComCondReply> commercialReplies,
            List<Condition> technicalInput,
            List<TblSuppTechCondReply> technicalReplies)
        {
            if (string.IsNullOrWhiteSpace(sourceExcelPath) || !File.Exists(sourceExcelPath))
                throw new FileNotFoundException("The generated RFQ Excel file was not found.", sourceExcelPath);

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            string directory = Path.GetDirectoryName(sourceExcelPath) ?? Path.GetTempPath();
            string target = Path.Combine(directory,
                Path.GetFileNameWithoutExtension(sourceExcelPath) + "-Conditions-" +
                DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xlsx");

            File.Copy(sourceExcelPath, target, true);

            using ExcelPackage package = new ExcelPackage(new FileInfo(target));
            WriteCommercial(package, commercialInput, commercialReplies);
            WriteTechnical(package, technicalInput, technicalReplies);
            package.Save();
            return target;
        }

        private static void WriteCommercial(
            ExcelPackage package,
            List<Condition> input,
            List<TblSuppComCondReply> replies)
        {
            const string name = "Commercial Conditions";
            ExcelWorksheet oldSheet = package.Workbook.Worksheets[name];
            if (oldSheet != null) package.Workbook.Worksheets.Delete(oldSheet);
            ExcelWorksheet sheet = package.Workbook.Worksheets.Add(name);

            int row = 2;
            foreach (Condition condition in input ?? new List<Condition>())
            {
                TblSuppComCondReply reply = (replies ?? new List<TblSuppComCondReply>())
                    .FirstOrDefault(x => x.CdComConId == condition.id);
                string acc = reply?.CdAccCond ?? condition.ACCCondValue ?? string.Empty;
                string supplier = string.IsNullOrWhiteSpace(reply?.CdSuppReply) ? acc : reply.CdSuppReply;
                sheet.Cells[row, 1].Value = condition.description ?? string.Empty;
                sheet.Cells[row, 2].Value = acc;
                sheet.Cells[row, 3].Value = supplier;
                row++;
            }

            FormatAndProtect(sheet, Math.Max(1, row - 1));
        }

        private static void WriteTechnical(
            ExcelPackage package,
            List<Condition> input,
            List<TblSuppTechCondReply> replies)
        {
            const string name = "Technical Conditions";
            ExcelWorksheet oldSheet = package.Workbook.Worksheets[name];
            if (oldSheet != null) package.Workbook.Worksheets.Delete(oldSheet);
            ExcelWorksheet sheet = package.Workbook.Worksheets.Add(name);

            int row = 2;
            foreach (Condition condition in input ?? new List<Condition>())
            {
                TblSuppTechCondReply reply = (replies ?? new List<TblSuppTechCondReply>())
                    .FirstOrDefault(x => x.TcTechConId == condition.id);
                string acc = reply?.TcAccCond ?? condition.ACCCondValue ?? string.Empty;
                string supplier = string.IsNullOrWhiteSpace(reply?.TcSuppReply) ? acc : reply.TcSuppReply;
                sheet.Cells[row, 1].Value = condition.description ?? string.Empty;
                sheet.Cells[row, 2].Value = acc;
                sheet.Cells[row, 3].Value = supplier;
                row++;
            }

            FormatAndProtect(sheet, Math.Max(1, row - 1));
        }

        private static void FormatAndProtect(ExcelWorksheet sheet, int lastRow)
        {
            sheet.Cells[1, 1].Value = "Condition";
            sheet.Cells[1, 2].Value = "ACC Condition";
            sheet.Cells[1, 3].Value = "Supplier Condition";
            sheet.View.FreezePanes(2, 1);
            sheet.Cells.Style.Locked = true;

            using (ExcelRange header = sheet.Cells[1, 1, 1, 3])
            {
                header.Style.Font.Bold = true;
                header.Style.Font.Color.SetColor(Color.White);
                header.Style.Fill.PatternType = ExcelFillStyle.Solid;
                header.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(14, 116, 144));
                header.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                header.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                header.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                header.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                header.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                header.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            }

            if (lastRow >= 2)
            {
                using ExcelRange data = sheet.Cells[2, 1, lastRow, 3];
                data.Style.WrapText = true;
                data.Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                data.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                data.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                data.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                data.Style.Border.Right.Style = ExcelBorderStyle.Thin;

                using ExcelRange locked = sheet.Cells[2, 1, lastRow, 2];
                locked.Style.Locked = true;
                locked.Style.Fill.PatternType = ExcelFillStyle.Solid;
                locked.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(242, 242, 242));

                using ExcelRange editable = sheet.Cells[2, 3, lastRow, 3];
                editable.Style.Locked = false;
                editable.Style.Fill.PatternType = ExcelFillStyle.Solid;
                editable.Style.Fill.BackgroundColor.SetColor(Color.White);
            }

            sheet.Column(1).Width = 55;
            sheet.Column(2).Width = 45;
            sheet.Column(3).Width = 45;
            sheet.Cells[1, 1, lastRow, 3].AutoFilter = true;
            sheet.Protection.IsProtected = true;
            sheet.Protection.AllowSelectUnlockedCells = true;
            sheet.Protection.AllowSelectLockedCells = true;
            sheet.Protection.AllowAutoFilter = true;
            sheet.Protection.AllowDeleteColumns = false;
            sheet.Protection.AllowDeleteRows = false;
            sheet.Protection.AllowInsertColumns = false;
            sheet.Protection.AllowInsertRows = false;
            sheet.Protection.AllowFormatCells = false;
            sheet.Protection.AllowFormatColumns = false;
            sheet.Protection.AllowFormatRows = false;
        }
    }


}
