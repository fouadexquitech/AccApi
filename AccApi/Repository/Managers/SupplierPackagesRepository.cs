using System.Collections.Generic;
using System.Linq;
using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.View_Models;
using System.IO;
using OfficeOpenXml;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using AccApi.Data_Layer;

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

            //var results = from b in supList                         
            //              join c in _dbcontext.TblSupplierPackages on b.SupCode equals c.SpSupplierId
            //              where c.SpPackageId == packageid
            //              orderby c.SpPackSuppId
            //              select new SupplierPackagesList
            //              {
            //                  PsId = c.SpPackSuppId,
            //                  PsPackId = c.SpPackageId,
            //                  PsSuppId = c.SpSupplierId,
            //                  PsSupName = b.SupName,
            //                  PsByBoq = c.SpByBoq,
            //                  TecCondSent = c.TecCondSent
            //              };

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
                              SupSubmitted= g.Max(x => x.s.StatusId) ==3 ? true : false,
                              PsId = g.Max(x => x.c.SpPackSuppId),
                              PsPackId = g.Max(x => x.c.SpPackageId),
                              PsByBoq = g.Max(x => x.c.SpByBoq),
                              TecCondSent = g.Max(x => x.c.TecCondSent)
                          };

            return results.ToList();
        }

        public List<boqPackageList> GetboqPackageList(int packId, byte byboq,  string CostConn)
        {
            AccDbContext _dbcontext = new AccDbContext(CostConn);
            var boqList = new List<boqPackageList>();

            if (byboq == 1)
            {
               var  origBoqList = (from o in _dbcontext.TblOriginalBoqVds
                            join b in _dbcontext.TblBoqVds on o.ItemO equals b.BoqItem
                            where b.BoqScope == packId                            
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
                                item = o.ItemO,
                                boqDesc = o.DescriptionO,
                                unit = o.UnitO,
                                qty = (double)o.QtyScope,
                                unitPrice = o.UnitRate,
                                totalPrice = o.QtyO * o.UnitRate,
                                exportedToSupplier = (byte)((o.ExportedToSupplier == null) ? 0 : o.ExportedToSupplier),
                                obTradeDesc=o.ObTradeDesc,
                            }).ToList();

                //var resCost = from e in _dbcontext.TblBoqVds.Where(x => x.BoqScope == packId)
                //                 group e by e.BoqItem into g
                //                 select new boqPackageList
                //                 {
                //                     item = g.Key,
                //                     resTotalPrice = g.Sum(x => x.BoqQty * x.BoqUprice)
                //                 };

                var resCost =   from e in _dbcontext.TblBoqVds
                                join o in _dbcontext.TblOriginalBoqVds on e.BoqItem equals o.ItemO
                                where e.BoqScope == packId
                                group e by e.BoqItem into g
                                select new boqPackageList
                                {
                                    item = g.Key,
                                    resTotalPrice = g.Sum(x => x.BoqQty * x.BoqUprice)
                                };

                boqList = (from o in origBoqList
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
                                       unitPrice = b.resTotalPrice / o.qty ,
                                       totalPrice = b.resTotalPrice,
                                       exportedToSupplier = o.exportedToSupplier ,
                                       obTradeDesc=o.obTradeDesc
                                   }).ToList();

            }
            else
            {
                var boqList1 = (from o in _dbcontext.TblOriginalBoqVds
                            join b in _dbcontext.TblBoqVds on o.ItemO equals b.BoqItem
                            join r in _dbcontext.TblResources on b.BoqResSeq equals r.ResSeq
                            where b.BoqScope == packId
                            orderby o.RowNumber
                            select new boqPackageList
                            {
                                l1 = o.L1,
                                l2 = o.L2,
                                l3 = o.L3,
                                l4 = o.L4,
                                l5 = o.L5,
                                l6 = o.L6,
                                c1 = o.C1,
                                c2 = o.C2,
                                c3 = o.C3,
                                c4 = o.C4,
                                c5 = o.C5,
                                c6 = o.C6,
                                item = o.ItemO,
                                boqDesc = o.DescriptionO,
                                unit = o.UnitO,
                                qty = (double)o.QtyScope,
                                unitPrice = o.UnitRate,
                                totalPrice = o.QtyO * o.UnitRate,
                                resType = b.BoqCtg,
                                resCode = b.BoqPackage,
                                resDesc = r.ResDescription,
                                resUnit = b.BoqUnitMesure,   
                                resCtg= b.BoqCtg,
                                resDiv = b.BoqDiv,
                                boqBillQty = b.BoqBillQty,
                                boqQty = b.BoqQty,
                                resUnitPrice=b.BoqUprice,
                                resTotalPrice = b.BoqQty * b.BoqUprice,
                                boqScopeQty = b.BoqQtyScope,
                                exportedToSupplier = (byte)((o.ExportedToSupplier == null) ? 0 : o.ExportedToSupplier)
                            }).ToList();

                //AH26052025
                boqList = boqList1
                              .GroupBy(x => new { x.resDesc,x.resUnitPrice, x.resUnit })
                              .Select(p => new boqPackageList
                              {
                                  resDesc = p.First().resDesc,
                                  resUnit = p.First().resUnit,     
                                  boqBillQty = p.Sum(c => c.boqBillQty),
                                  boqQty = p.Sum(c => c.boqQty),
                                  resUnitPrice = p.First().resUnitPrice,
                                  resTotalPrice = p.Sum(c => c.boqQty * c.resUnitPrice),
                                  boqScopeQty = p.Sum(c => c.boqScopeQty),
                                  exportedToSupplier = (byte)p.Max(c => ((c.exportedToSupplier == null) ? 0 : c.exportedToSupplier))
                              }).ToList();
                //26052025
            }

            //Calculate Discount
            //var lstDiscount = _dbcontext.TblBoqDiscounts.Where(x => (x.BoqdDiscountAll + x.Boqddiscount) > 0).ToList();

            //foreach (var d in lstDiscount.Where(d => (d.BoqdDiscountAll + d.Boqddiscount) > 0))
            //{
            //    foreach (var boq in boqList.Where(x => x.resCtg == d.BoqdCtg && x.resDiv == d.BoqdDiv))
            //    {
            //        boq.resUnitPrice = boq.resUnitPrice - (boq.resUnitPrice * ((d.BoqdDiscountAll + d.Boqddiscount) / 100));
            //        boq.resTotalPrice = boq.resTotalPrice - (boq.resTotalPrice * ((d.BoqdDiscountAll + d.Boqddiscount) / 100));
            //    }
            //}

            //var boqCost = boqList
            //              .GroupBy(x => new { x.item})
            //              .Select(p => new boqPackageList
            //              {
            //                    item = p.First().item,
            //                    totalPrice = p.Sum(c=> c.resTotalPrice)
            //                }).ToList();

            //foreach (var boqc in boqCost)
            //{
            //    foreach (var boq in boqList.Where(x => x.item == boqc.item))
            //    {
            //        boq.totalPrice = boqc.totalPrice;
            //        boq.unitPrice = boqc.totalPrice / boq.qty;
            //    }
            //}

            return boqList;
        }

        public string ValidateExcelBeforeAssign(int packId, byte byBoq,bool withPrice, string CostConn)
        {
            AccDbContext _dbcontext = new AccDbContext(CostConn);

            //AH0702
            //var packageSupp = _dbcontext.TblSupplierPackages.Where(x => x.SpPackageId == packId).FirstOrDefault();
            //byte byBoq = (byte)((packageSupp.SpByBoq==null) ? 0 : packageSupp.SpByBoq);
            //AH0702
            var package = _mdbContext.TblPackages.Where(x => x.PkgeId == packId).FirstOrDefault();
            if (package == null) return string.Empty;
            string PackageName = package.PkgeName;

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
                string Boq = "", OldBoq = "", C = "", OldC = "", l1 = "", l2 = "", l3 = "", l4 = "", l5 = "", l6 = "", oldl1 = "", oldl2 = "", oldl3 = "", oldl4 = "", oldl5 = "", oldl6 = "";
                i = 1;
                double total = 0;

                if (byBoq == 1)
                {
                    worksheet.Cells[i, 1].Value = "Item";
                    worksheet.Column(1).Width = 40;
                    worksheet.Cells[i, 2].Value = "Level";
                    worksheet.Column(3).Width = 50;
                    worksheet.Columns[3].Style.WrapText = true;
                    //worksheet.Column(3).AutoFit();
                    worksheet.Cells[i, 3].Value = "Bill Description";
                    worksheet.Cells[i, 4].Value = "Unit";
                    worksheet.Cells[i, 5].Value = "Qty";
                    worksheet.Columns[5].Style.WrapText = true;
                    //worksheet.Column(5).AutoFit();

                    if (withPrice)
                    {
                        worksheet.Cells[i, 6].Value = "Unit Price";
                        worksheet.Cells[i, 7].Value = "Total Price";
                        worksheet.Cells[i, 8].Value = "Comments";
                        worksheet.Column(8).Width = 50;
                        worksheet.Columns[8].Style.WrapText = true;
                    }
                    else
                    { 
                        worksheet.Cells[i, 6].Value = "Comments";
                        worksheet.Column(6).Width = 50;
                        worksheet.Columns[6].Style.WrapText = true;
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

                    if (withPrice)
                    {
                        worksheet.Cells[i, 6].Value = "ResUnitPrice";
                        //worksheet.Column(6).AutoFit();
                        worksheet.Cells[i, 7].Value = "ResTotalPrice";
                        worksheet.Column(7).Width = 25;
                        //worksheet.Column(7).AutoFit();
                        worksheet.Cells[i, 8].Value = "Comments";
                        worksheet.Column(8).Width = 50;
                        worksheet.Columns[8].Style.WrapText = true;
                    }
                    //worksheet.Column(12).AutoFit();                   
                }
                worksheet.Row(i).Style.Font.Bold = true;

                i = 4;
                foreach (var x in result)
                {
                    Boq = x.item;
                    C = x.c1;
                    l1 = (x.l1 == null) ? "" : x.l1;
                    l2 = (x.l2 == null) ? "" : x.l2;
                    l3 = (x.l3 == null) ? "" : x.l3;
                    l4 = (x.l4 == null) ? "" : x.l4;
                    l5 = (x.l5 == null) ? "" : x.l5;
                    l6 = (x.l6 == null) ? "" : x.l6;

                    if (byBoq == 1)
                    {
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

                            worksheet.Cells[i, 1].Value = (x.item == null) ? "" : x.item;
                            worksheet.Cells[i, 3].Value = (x.boqDesc == null) ? "" : x.boqDesc;
                            worksheet.Cells[i, 4].Value = (x.unit == null) ? "" : x.unit;
                            worksheet.Cells[i, 5].Value = (x.qty == null) ? "" : x.qty;
                            worksheet.Cells[i, 5].Style.Numberformat.Format = "#,##0.0";

                            if (withPrice)
                            {
                                worksheet.Cells[i, 6].Value = (x.unitPrice == null) ? "" : x.unitPrice;
                                worksheet.Cells[i, 6].Style.Numberformat.Format = "#,##0.0";
                                worksheet.Cells[i, 7].Value = (x.totalPrice == null) ? "" : x.totalPrice;
                                worksheet.Cells[i, 7].Style.Numberformat.Format = "#,##0.0";
                                total += (double)((x.resTotalPrice == null) ? 0 : x.resTotalPrice);
                            }

                            if (byBoq == 1)
                            {
                                worksheet.Cells[i, 8].Value = (x.obTradeDesc == null) ? "" : x.obTradeDesc;
                                //worksheet.Cells[i, 8].Formula = "= (F" + i + ") - (F" + i + "*" + "G" + i + "/100)";
                                //worksheet.Cells[i, 8].Style.Numberformat.Format = "#,##0.0";
                                //worksheet.Cells[i, 9].Formula = "=E" + i + "*" + "H" + i;
                                //worksheet.Cells[i, 9].Style.Numberformat.Format = "#,##0.0";
                                //worksheet.Cells[i, 6].Style.Locked = false;
                                //worksheet.Cells[i, 7].Style.Locked = false;
                                //worksheet.Cells[i, 10].Style.Locked = false;
                            }
                            i = i + 1;
                            OldBoq = Boq;
                        }
                    }
                    else  //by Res.                   
                    {
                        worksheet.Cells[i, 1].Value = (x.resType == null) ? "" : x.resType;
                        worksheet.Cells[i, 2].Value = (x.resCode == null) ? "" : x.resCode;
                        worksheet.Cells[i, 3].Value = (x.resDesc == null) ? "" : x.resDesc;
                        worksheet.Cells[i, 4].Value = (x.resUnit == null) ? "" : x.resUnit;
                        worksheet.Cells[i, 5].Value = (x.boqScopeQty == null) ? "" : x.boqScopeQty;
                        worksheet.Cells[i, 5].Style.Numberformat.Format = "#,##0.0";

                        if (withPrice)
                        {
                            worksheet.Cells[i, 6].Value = (x.resUnitPrice == null) ? "" : x.resUnitPrice;
                            worksheet.Cells[i, 6].Style.Numberformat.Format = "#,##0.0";
                            worksheet.Cells[i, 7].Value = (x.resTotalPrice == null) ? 0 : x.resTotalPrice;
                            worksheet.Cells[i, 7].Style.Numberformat.Format = "#,##0.0";
                            total += (double)((x.resTotalPrice == null) ? 0 : x.resTotalPrice);
                        }
                        //worksheet.Cells[i, 13].Formula = "= (K" + i + ") - (K" + i + "*" + "L" + i + "/100)";
                        //worksheet.Cells[i, 13].Style.Numberformat.Format = "#,##0.0";
                        //worksheet.Cells[i, 14].Formula = "=J" + i + "*" + "M" + i;
                        //worksheet.Cells[i, 14].Style.Numberformat.Format = "#,##0.0";
                        //worksheet.Cells[i, 11].Style.Locked = false;
                        //worksheet.Cells[i, 12].Style.Locked = false;
                        //worksheet.Cells[i, 15].Style.Locked = false;
                    }
                    i++;
                }

                if (withPrice)
                {
                    worksheet.Row(i + 1).Style.Font.Bold = true;
                    worksheet.Cells[i + 1, 6].Value = "Total :";
                    worksheet.Cells[i + 1, 7].Style.Numberformat.Format = "#,##0.0";
                    worksheet.Cells[i + 1, 7].Value = total;
                }

                //Update Exported BOQ
                if (byBoq == 1)
                {
                    var lstBoqo = (from a in result
                                   join b in _dbcontext.TblOriginalBoqVds on a.item equals b.ItemO
                                   select b).ToList();

                    foreach (var item in result)
                    {
                        lstBoqo.Where(d => d.ItemO == item.item).First().ExportedToSupplier = 1;
                    }
                    _dbcontext.TblOriginalBoqVds.UpdateRange(lstBoqo);
                    _dbcontext.SaveChanges();
                }

                var p = _dbcontext.TblParameters.FirstOrDefault();
                string ProjectName = p.Project;

                xlPackage.Save();
                stream.Position = 0;
                string excelName = $"{ProjectName}-Package-{PackageName}-{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";

                if (File.Exists(excelName))
                    File.Delete(excelName);

                excelName = excelName.Replace("/", "-");

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
        public async Task<bool> AssignPackageSuppliers(int packId, List<SupplierInputList> supInputList, byte ByBoq, string UserName, List<IFormFile> attachments,DateTime ExpiryDate, string CostConn,string TSConn)
        {
            AccDbContext _dbcontext = new AccDbContext(CostConn);
            PolicyDbContext _TSdbcontext = new PolicyDbContext(TSConn);
            var t = await _dbcontext.Database.BeginTransactionAsync();

            try
            {
                var package = _mdbContext.TblPackages.Where(x => x.PkgeId == packId).FirstOrDefault();
                string PackageName = package.PkgeName;

                var mailListForSending = new List<MailForSending>();
                var AttachmentList = new List<string>();
                string sent = "";

                var p = await _dbcontext.TblParameters.FirstOrDefaultAsync();
                var proj = await _TSdbcontext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefaultAsync();

                //Get User Email Signature
                User user = _logonRepository.GetUser(UserName);
                string userSignature = (user.UsrEmailSignature == null) ? "" : user.UsrEmailSignature;
                string usrEmail = (user.UsrEmail == null) ? "" : user.UsrEmail;

                int PackageSupplierId = 0;
                List<AddSupplierPackageModel> supplierPackageModelList = new List<AddSupplierPackageModel>();
                List<AddRevisionModel> revisionModelList = new List<AddRevisionModel>();
                AddSupplierPackageRevisionModel supplierPackageRevisionModel = new AddSupplierPackageRevisionModel();

                string attachExcel = ValidateExcelBeforeAssign(packId, ByBoq, false,CostConn);

                foreach (var supInput in supInputList)
                {
                    //1.Add PackageSupplier
                    SupplierInput supplier = supInput.supplierInput;

                    if (!_dbcontext.TblSupplierPackages.Any(a => (a.SpPackageId == packId) && (a.SpSupplierId == supplier.supID)))
                    {
                        var spack = new TblSupplierPackage { SpPackageId = packId, SpSupplierId = supplier.supID, SpByBoq = ByBoq };
                        await _dbcontext.AddAsync<TblSupplierPackage>(spack);
                        await _dbcontext.SaveChangesAsync();

                        PackageSupplierId = spack.SpPackSuppId;

                        //1.1 Add AddSupplierPackageModel
                        supplierPackageModelList.Add(new AddSupplierPackageModel
                        {
                            SpPackSuppId = spack.SpPackSuppId,
                            SpPackageId = spack.SpPackageId,
                            SpSupplierId = spack.SpSupplierId,
                            SpByBoq = ByBoq,
                            ProjectCode = proj.PrjCode,
                            ProjectName = proj.PrjName,
                            TecCondSent= false
                        });
                    }
                    else
                    {
                        var supPack = await _dbcontext.TblSupplierPackages.Where(a => (a.SpPackageId == packId) && (a.SpSupplierId == supplier.supID)).FirstOrDefaultAsync();
                        PackageSupplierId = supPack.SpPackSuppId;
                    }

                    //2.Add Revision
                    int LastRevNo = GetMaxRevisionNumber(PackageSupplierId,CostConn);
                    
                    if (LastRevNo != -1)
                    {
                        int i = LastRevNo;
                        //check if last revision is submitted by supp or rejected or expired
                        if (_dbcontext.TblSupplierPackageRevisions.Any(a => (a.PrRevNo == i) && (a.PrPackSuppId == PackageSupplierId) && ((a.StatusId ?? 0) < 3)))
                        {
                            throw new Exception("Revision not returned from Supplier !");
                        }
                        else
                        {
                            do
                            {
                                var res = await _dbcontext.TblSupplierPackageRevisions.SingleOrDefaultAsync(b => b.PrRevNo == i && b.PrPackSuppId == PackageSupplierId);
                                if (res != null)
                                {
                                    res.PrRevNo = i + 1;
                                    await _dbcontext.SaveChangesAsync();
                                }
                                i--;
                            }
                            while (i >= 0);
                        }
                    }


                    var Rev1 = await _dbcontext.TblSupplierPackageRevisions.SingleOrDefaultAsync(b => b.PrRevNo == 1 && b.PrPackSuppId == PackageSupplierId);
                    var supPackRev = new TblSupplierPackageRevision();
                    int rev1Id = 0;

                    if (Rev1 != null)
                    {
                        supPackRev = new TblSupplierPackageRevision { PrRevNo = 0, PrPackSuppId = PackageSupplierId, PrTotPrice = Rev1.PrTotPrice, PrRevDate = DateTime.Now, PrCurrency = Rev1.PrCurrency, PrExchRate = Rev1.PrExchRate, RevExpiryDate = ExpiryDate , InsertedBy = UserName, InsertedByEmail = usrEmail };
                        rev1Id = Rev1.PrRevId;
                    }
                    else
                    {
                        int prjCurrency = (int)(await _dbcontext.TblParameters.FirstOrDefaultAsync()).EstimatedCur;
                        supPackRev = new TblSupplierPackageRevision { PrRevNo = 0, PrPackSuppId = PackageSupplierId, PrTotPrice = 0, PrRevDate = DateTime.Now, PrCurrency = prjCurrency , RevExpiryDate= ExpiryDate,InsertedBy= UserName,InsertedByEmail= usrEmail };
                        rev1Id = 0;
                    }
                    await _dbcontext.AddAsync<TblSupplierPackageRevision>(supPackRev);
                    await _dbcontext.SaveChangesAsync();

                    //Get inserted Revison ID
                    var Rev0 = await _dbcontext.TblSupplierPackageRevisions.SingleOrDefaultAsync(b => (b.PrPackSuppId == PackageSupplierId) && (b.PrRevNo == 0));
                    int rev0Id = Rev0.PrRevId;

                    var packageSupp = await _dbcontext.TblSupplierPackages.Where(x => x.SpPackSuppId == PackageSupplierId).FirstOrDefaultAsync();
                    byte byBoq = (byte)((packageSupp.SpByBoq == null) ? 0 : packageSupp.SpByBoq);

                    List<TblRevisionDetail> LstRevDetails = await InsertRevisionDetail(rev0Id, packId, byBoq, rev1Id, _dbcontext);
                    if (LstRevDetails.Count() > 0)
                    {
                        await _dbcontext.AddRangeAsync(LstRevDetails);
                        await _dbcontext.SaveChangesAsync();
                    }

                    //Insert ComConditions Conditions Revision
                    List<TblSuppComCondReply> LstComCondReply = await InsertComercialConditions(rev0Id, packId, rev1Id, supInput.comercialCondList,CostConn);

                    //Insert Technical Conditions Revision
                    List<TblSuppTechCondReply> LstTechCondReply = await InsertTechnicalConditions(rev0Id, packId, rev1Id, supInput.technicalCondList, CostConn);


                    //2.2 Add RevisionModel
                    //if (ByBoq ==1)
                    //{ 
                        revisionModelList.Add(new AddRevisionModel
                        {
                            PrRevId = Rev0.PrRevId,
                            PrRevNo = Rev0.PrRevNo,
                            PrRevDate = Rev0.PrRevDate,
                            PrTotPrice = Rev0.PrTotPrice,
                            PrPackSuppId = Rev0.PrPackSuppId,
                            PrCurrency = Rev0.PrCurrency,
                            PrExchRate = 1,
                            StatusId = 1,
                            ProjectCode = proj.PrjCode,
                            IsSynched = false,
                            RevExpiryDate = Rev0.RevExpiryDate,
                            RevisionDetails = (from d in LstRevDetails
                                               select new AddRevisionDetailModel
                                               {
                                                   BoqResourceSeq = d.RdResourceSeq,
                                                   ResourceDescription = GetRessourceDescription(ByBoq,d.RdResourceSeq,d.ResourceDescription,(bool) d.IsAlternative,CostConn),
                                                   ItemO = d.RdBoqItem,
                                                   ItemDescription = d.ItemDescription,//GetBoqItemDescription(d.RdBoqItem),
                                                   Quantity = d.RdQty,
                                                   QuotationQty = d.RdQuotationQty,
                                                   UnitPrice = d.RdPrice,
                                                   TotalPrice = (d.RdQty) * (d.UnitPriceAfterDiscount),
                                                   DiscountPerc = d.RdDiscount,
                                                   Comments = d.RdComment,
                                                   CreatedOn = DateTime.Now,
                                                   IsSynched = false,
                                                   ProjectCode = proj.PrjCode,
                                                   ParentItemO = d.ParentItemO,
                                                   ParentResourceId = d.ParentResourceId.ToString(),
                                                   NewItemId = d.NewItemId,
                                                   NewItemResourceId = d.NewItemResourceId,
                                                   IsNewItem = d.IsNew,
                                                   IsAlternative = d.IsAlternative,
                                                   UnitPriceAfterDiscount=d.UnitPriceAfterDiscount,
                                                   UnitO = (d.UnitO == null) ? "" : d.UnitO,
                                                   BoqCtg = (d.BoqCtg == null) ? "" : d.BoqCtg,
                                                   BoqUnitMesure = (d.BoqUnitMesure == null) ? "" : d.BoqUnitMesure,
                                                   L1 = (d.L1 == null) ? "" : d.L1,
                                                   L2 = (d.L2 == null) ? "" : d.L2,
                                                   L3 = (d.L3 == null) ? "" : d.L3,
                                                   L4 = (d.L4 == null) ? "" : d.L4,
                                                   L5 = (d.L5 == null) ? "" : d.L5,
                                                   L6 = (d.L6 == null) ? "" : d.L6,
                                                   L7 = (d.L7 == null) ? "" : d.L7,
                                                   L8 = (d.L8 == null) ? "" : d.L8,
                                                   L9 = (d.L9 == null) ? "" : d.L9,
                                                   L10 = (d.L10 == null) ? "" : d.L10,
                                                   C1 = (d.C1 == null) ? "" : d.C1,
                                                   C2 = (d.C2 == null) ? "" : d.C2,
                                                   C3 = (d.C3 == null) ? "" : d.C3,
                                                   C4 = (d.C4 == null) ? "" : d.C4,
                                                   C5 = (d.C5 == null) ? "" : d.C5,
                                                   C6 = (d.C6 == null) ? "" : d.C6,
                                                   C7 = (d.C7 == null) ? "" : d.C7,
                                                   C8 = (d.C8 == null) ? "" : d.C8,
                                                   C9 = (d.C9 == null) ? "" : d.C9,
                                                   C10 = (d.C10 == null) ? "" : d.C10,
                                                   C11 = (d.C11 == null) ? "" : d.C11,
                                                   C12 = (d.C12 == null) ? "" : d.C12,
                                                   C13 = (d.C13 == null) ? "" : d.C13,
                                                   C14 = (d.C14 == null) ? "" : d.C14,
                                                   C15 = (d.C15 == null) ? "" : d.C15,
                                                   BoqRefNumber= (d.RdBoqRefNumber == null) ? "" : d.RdBoqRefNumber,
                                                   AccComment= (d.RdAccComment == null) ? "" : d.RdAccComment,
                                               }).ToList(),
                            CommercialConditions= (from d in LstComCondReply
                                                   select new AddCondModel
                                                   {
                                                       Id = d.CdComConId,
                                                       CondValue = d.CdSuppReply,
                                                       ACCCondValue=d.CdAccCond,
                                                       ProjectCode = proj.PrjCode
                                                   }).ToList(),
                            TechnicalConditions = (from d in LstTechCondReply
                                                   select new AddCondModel
                                                    {
                                                        Id = d.TcTechConId,
                                                        CondValue = d.TcSuppReply,
                                                        ACCCondValue = d.TcAccCond,
                                                        ProjectCode = proj.PrjCode
                                                   }).ToList()
                        });
                    //}

                    //Send Email with Attachments to this Supplier
                    AttachmentList.Clear();
                    AttachmentList.Add(supInput.FilePath);
                    if (supInput.mailAttachments != null)
                    {
                        foreach (var attach in supInput.mailAttachments)
                        {
                            AttachmentList.Add(attach);
                        }
                    }

                    //if (supInput.comercialCondList.Count > 0)
                    //{
                    //    if (ComCondAttch == "")
                    //        ComCondAttch = SendComercialConditions(packId, supInput.comercialCondList);

                    //    AttachmentList.Add(ComCondAttch);
                    //}

                    AttachmentList.Add(attachExcel);

                    //send email
                    string SupEmail = "";
                    var Sup = _mdbContext.TblSuppliers.Where(s => s.SupCode == supplier.supID).FirstOrDefault();

                    if (Sup != null)
                        SupEmail = Sup.SupEmail;

                    if (!string.IsNullOrEmpty(SupEmail))
                    {
                        var mail = new MailForSending();
                        mail.To.Add(SupEmail);

                        //List<string> mylistTo = new List<string>();
                        //mylistTo.Add(SupEmail);

                        //List<string> mylistCC = new List<string>();
                        //if (usrEmail !="")
                        //    mylistCC.Add(usrEmail);

                        //if (supInput.mailCC != null)
                        //{
                        //    foreach (var ccMail in supInput.mailCC)
                        //    {
                        //        mylistCC.Add(ccMail);
                        //    }
                        //}

                        //List<string> mylistBCC = new List<string>();
                        //mylistBCC.Add("sdasuki@accsal.com");

                        //string MailBody;
                        //if (supInput.EmailTemplate != "")
                        //{
                        //    MailBody = supInput.EmailTemplate;
                        //}
                        //else
                        //{
                        //    MailBody = "Dear Sir,";
                        //    MailBody += Environment.NewLine;
                        //    MailBody += Environment.NewLine;
                        //    MailBody += "Kindly find attachments , and fill the price ";
                        //    MailBody += Environment.NewLine;
                        //    MailBody += Environment.NewLine;
                        //    MailBody += Environment.NewLine;
                        //    MailBody += Environment.NewLine;
                        //    MailBody += "Best regards";
                        //}
                        //if (userSignature != "")
                        //{
                        //    MailBody += @"<br><br>";
                        //    MailBody += userSignature;
                        //}                     

                        //Mail m = new Mail();
                        //sent = m.SendMail(mylistTo, mylistCC, mylistBCC, Subject, MailBody, AttachmentList, true, attachments);

                        if (!string.IsNullOrEmpty(usrEmail))
                            mail.Cc.Add(usrEmail);

                        if (supInput.mailCC != null)
                        {
                            foreach (var ccMail in supInput.mailCC)
                            {
                                mail.Cc.Add(ccMail);
                            }
                        }

                        if (!string.IsNullOrEmpty(_configuration["mailBcc1"]))
                            mail.Bcc.Add(_configuration["mailBcc1"]);
                        if (!string.IsNullOrEmpty(_configuration["mailBcc2"]))
                            mail.Bcc.Add(_configuration["mailBcc2"]);

                        string Subject = $"Job in Hand-{proj.PrjName}-{PackageName}";
                        mail.Subject = Subject;
                     
                        mail.Body = !string.IsNullOrEmpty(supInput.EmailTemplate)
                                    ? supInput.EmailTemplate
                                    : @"Dear Sir,<br><br>Kindly find attachments and fill the price.<br><br>Best regards";

                        if (!string.IsNullOrEmpty(userSignature))
                            mail.Body += "<br><br>" + userSignature;

                        mail.Attachments = AttachmentList;

                        mailListForSending.Add(mail);
                    }
                }

                supplierPackageRevisionModel.SupplierPackageModels = supplierPackageModelList;
                supplierPackageRevisionModel.RevisionModels = revisionModelList;

                //Post the portal API (Create supplier package and revision on portal)
                var body = JsonSerializer.Serialize(supplierPackageRevisionModel);
                var portalApiPath = _configuration["PortalApiPath"];
                var key = _configuration["External:Key"];
                var requestContent = new StringContent(body, Encoding.UTF8, "application/json");
                _httpClient.DefaultRequestHeaders.Add("Authorization", key);
                var response = await _httpClient.PostAsync(portalApiPath + "External/AddSupplierRevisionInPortal", requestContent);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                if (content == "true")
                {
                    //Send email to suppliers
                    foreach (var email in mailListForSending)
                    {
                        var mail = new Mail();

                        sent = mail.SendMail(email.To, email.Cc,email.Bcc,email.Subject,email.Body,email.Attachments,email.IsBodyHtml,attachments);
                    }

                    if (sent == "sent")
                    {
                        await t.CommitAsync();
                        return true;
                    }
                    else
                        throw new Exception("Email didn't sent !");
                }
                else
                {
                    throw new Exception("An error occured on the Portal API");
                }
            }
            catch (Exception ex)
            {            
                await t.RollbackAsync();
                throw;
            }
        }

        private string GetRessourceDescription(byte ByBoq,string boqResSeq, string resourceDescription, bool isAlternative, string CostConn)
        {
            AccDbContext _dbcontext = new AccDbContext(CostConn);

            string resDesc = "";

            if (ByBoq == 1)
                return "";

            if (resourceDescription!="" && resourceDescription!=null)
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

            if (rev1Id>0)  //In case of previous revision exists , so you have to insert new details from pervious
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
                            RdQuotationQty=row.RdQuotationQty,
                            RdComment = row.RdComment,
                            RdMissedPrice = row.RdMissedPrice,
                            RdDiscount = row.RdDiscount,
                            RdAddedItem = row.RdAddedItem,
                            IsAlternative = row.IsAlternative,
                            IsNew=row.IsNew,
                            NewItemId=row.NewItemId,
                            NewItemResourceId=row.NewItemResourceId,
                            ParentItemO = row.ParentItemO,
                            ParentResourceId = row.ParentResourceId,
                            ResourceDescription = row.ResourceDescription,
                            ItemDescription=row.ItemDescription ,
                            UnitPriceAfterDiscount = row.UnitPriceAfterDiscount,
                            TotalPrice= Math.Round((double) (row.RdQuotationQty*row.UnitPriceAfterDiscount),2),
                            UnitO=row.UnitO,
                            BoqCtg=row.BoqCtg,
                            BoqUnitMesure=row.BoqUnitMesure,
                            L1=row.L1,
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
                            RdAccComment= row.RdAccComment,
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
                                  BoqUprice=b.BoqUprice,
                                  BoqQty=b.BoqQty,
                                  BoqCtg="",
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
                                  BoqRefNumber=o.RefNumber,
                                  ObTradeDesc=o.ObTradeDesc,
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
                                BoqTotalPrice= p.Sum(c => c.BoqQty  *  c.BoqUprice),
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
                                ParentItemO ="",
                                ParentResourceId="0",
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
                                RdBoqRefNumber=row.BoqRefNumber,
                                RdAccComment=row.ObTradeDesc
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
                                  DescriptionO=o.DescriptionO,
                                  BoqResSeq=b.BoqResSeq,
                                  ResDescription=r.ResDescription,
                                  ResSeq=r.ResSeq,
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
                                  BoqRefNumber=o.RefNumber
                              }).ToList();

                    var resourcesGrp = result
                            .GroupBy(x => new { x.BoqResSeq, x.ResDescription, x.BoqUnitMesure,x.BoqUprice,x.BoqScope })
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
                        if  (row.BoqScopeQty > 0)
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

        private async Task<List<TblSuppComCondReply>> InsertComercialConditions(int revId, int packId, int rev1Id,List<Condition> comCondList,string CostConn)
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

        private async Task<List<TblSuppTechCondReply>> InsertTechnicalConditions(int revId, int packId, int rev1Id,List<Condition> techCondList, string CostConn)
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

        public int GetMaxRevisionNumber(int PackageSupplierId, string CostConn)
        {
            AccDbContext _context = new AccDbContext(CostConn);

            var query = _context.TblSupplierPackageRevisions.Where(x => x.PrPackSuppId == PackageSupplierId);
            var MaxRevisionNumber = query.Any() ? query.Max(x => x.PrRevNo) : -1;
            return (int)MaxRevisionNumber;
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
                xlPackage.SaveAs(excelName);

                return excelName;
            }
        }

    }
}
