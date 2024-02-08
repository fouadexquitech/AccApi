using System.Collections.Generic;
using System.Linq;
using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using System.IO;
using OfficeOpenXml;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Dynamic.Core;
using System.Net.Http;
using Microsoft.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Text;
using System.Threading.Tasks;
using AccApi.Repository.Models.MasterModels;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Database;
using AccApi.Data_Layer;
using System.Reflection.Emit;

namespace AccApi.Repository.Managers
{
    public class SupplierPackagesRepository : ISupplierPackagesRepository
    {
        private readonly AccDbContext _dbcontext;
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
            _pdbcontext = pdbcontext;
            _mdbContext = mdbContext;
            _logonRepository = logonRepository;
            _globalLists = globalLists;
            _dbcontext = new AccDbContext(_globalLists.GetAccDbconnectionString());
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public SupplierPackagesList GetSupplierPackage(int spId)
        {
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

        public List<SupplierPackagesList> GetSupplierPackagesList(int packageid)
        {
            var supList = (from b in _mdbContext.TblSuppliers
                           select b).ToList();

            var results = from b in supList
                          join c in _dbcontext.TblSupplierPackages on b.SupCode equals c.SpSupplierId
                          where c.SpPackageId == packageid
                          orderby c.SpPackSuppId
                          select new SupplierPackagesList
                          {
                              PsId = c.SpPackSuppId,
                              PsPackId = c.SpPackageId,
                              PsSuppId = c.SpSupplierId,
                              PsSupName = b.SupName,
                              PsByBoq = c.SpByBoq,
                              TecCondSent = c.TecCondSent
                          };
            return results.ToList();
        }

        public List<boqPackageList> boqPackageList(int packId, byte byboq)
        {
            if (byboq == 1)
            {
                var pack = (from o in _dbcontext.TblOriginalBoqs
                            where o.Scope == packId
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
                                exportedToSupplier = (byte)((o.ExportedToSupplier == null) ? 0 : o.ExportedToSupplier)
                            }).ToList();

                return pack;
            }
            else
            {
                var pack = (from o in _dbcontext.TblOriginalBoqs
                            join b in _dbcontext.TblBoqs on o.ItemO equals b.BoqItem
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
                                resType = b.BoqCtg,
                                resCode = b.BoqPackage,
                                resDesc = r.ResDescription,
                                ResUnit = b.BoqUnitMesure,
                                boqBillQty = b.BoqBillQty,
                                boqQty = b.BoqQty,
                                boqScopeQty = b.BoqQtyScope,
                                exportedToSupplier = (byte)((o.ExportedToSupplier == null) ? 0 : o.ExportedToSupplier)
                            }).ToList();

                return pack;
            }
        }

        public string ValidateExcelBeforeAssign(int packId, byte byBoq)
        {
            //AH0702
            //var packageSupp = _dbcontext.TblSupplierPackages.Where(x => x.SpPackageId == packId).FirstOrDefault();
            //byte byBoq = (byte)((packageSupp.SpByBoq==null) ? 0 : packageSupp.SpByBoq);
            //AH0702

            var package = _mdbContext.TblPackages.Where(x => x.PkgeId == packId).FirstOrDefault();
            if (package == null) return string.Empty;
            string PackageName = package.PkgeName;

            var result = boqPackageList(packId, byBoq);  //.Where(x=>x.exportedToSupplier == null || x.exportedToSupplier == 0);

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("BOQ");
                worksheet.Columns.AutoFit();
                worksheet.Protection.IsProtected = true;

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

                if (byBoq == 1)
                {
                    worksheet.Cells[i, 6].Value = "Unit Price";
                    worksheet.Cells[i, 7].Value = "Discount %";
                    worksheet.Cells[i, 8].Value = "Unit Price After Disc.";
                    worksheet.Cells[i, 9].Value = "Total Price";
                    worksheet.Cells[i, 10].Value = "Comments";
                    worksheet.Column(10).Width = 50;
                    worksheet.Columns[10].Style.WrapText = true;
                    //worksheet.Column(7).AutoFit();
                }
                else
                {
                    worksheet.Cells[i, 6].Value = "Ressouce Type";
                    worksheet.Cells[i, 7].Value = "Ressouce Code";
                    worksheet.Column(8).Width = 50;
                    worksheet.Columns[8].Style.WrapText = true;
                    worksheet.Column(8).AutoFit();
                    worksheet.Cells[i, 8].Value = "Ressouce Description";
                    worksheet.Cells[i, 9].Value = "Ressouce Unit";
                    worksheet.Cells[i, 10].Value = "Ressouce Qty";
                    worksheet.Column(10).AutoFit();
                    worksheet.Cells[i, 11].Value = "Unit Price";
                    worksheet.Cells[i, 12].Value = "Discount %";
                    worksheet.Cells[i, 13].Value = "Unit Price After Disc.";
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
                        worksheet.Cells[i, 6].Value = (x.resType == null) ? "" : x.resType;
                        worksheet.Cells[i, 7].Value = (x.resCode == null) ? "" : x.resCode;
                        worksheet.Cells[i, 8].Value = (x.resDesc == null) ? "" : x.resDesc;
                        worksheet.Cells[i, 9].Value = (x.ResUnit == null) ? "" : x.ResUnit;
                        worksheet.Cells[i, 10].Value = (x.boqScopeQty == null) ? "" : x.boqScopeQty;
                        worksheet.Cells[i, 10].Style.Numberformat.Format = "#,##0.0";
                        worksheet.Cells[i, 13].Formula = "= (K" + i + ") - (K" + i + "*" + "L" + i + "/100)";
                        worksheet.Cells[i, 13].Style.Numberformat.Format = "#,##0.0";
                        worksheet.Cells[i, 14].Formula = "=J" + i + "*" + "M" + i;
                        worksheet.Cells[i, 14].Style.Numberformat.Format = "#,##0.0";
                        worksheet.Cells[i, 11].Style.Locked = false;
                        worksheet.Cells[i, 12].Style.Locked = false;
                        worksheet.Cells[i, 15].Style.Locked = false;
                    }
                    i++;
                }

                //Update Exported BOQ
                if (byBoq == 1)
                {
                    var lstBoqo = (from a in result
                                   join b in _dbcontext.TblOriginalBoqs on a.item equals b.ItemO
                                   select b).ToList();

                    foreach (var item in result)
                    {
                        lstBoqo.Where(d => d.ItemO == item.item).First().ExportedToSupplier = 1;
                    }
                    _dbcontext.TblOriginalBoqs.UpdateRange(lstBoqo);
                    _dbcontext.SaveChanges();
                }

                xlPackage.Save();
                stream.Position = 0;
                string excelName = $"Package-{PackageName}.xlsx";

                if (File.Exists(excelName))
                    File.Delete(excelName);

                xlPackage.SaveAs(excelName);

                package.FilePath = excelName;
                _dbcontext.SaveChanges();

                //excelName = "Package-Aluminum Doors and Windows.xlsx";
                return excelName;
            }
        }

        public async Task<bool> AssignPackageSuppliers(int packId, List<SupplierInputList> supInputList, byte ByBoq, string UserName, List<IFormFile> attachments)
        {
            var t = await _dbcontext.Database.BeginTransactionAsync();
            try
            {
                string sent = "";
                string ComCondAttch = "";
                var AttachmentList = new List<string>();

                var p = await _dbcontext.TblParameters.FirstOrDefaultAsync();
                var proj = await _pdbcontext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefaultAsync();

                //Get User Email Signature
                User user = _logonRepository.GetUser(UserName);
                string userSignature = (user.UsrEmailSignature == null) ? "" : user.UsrEmailSignature;

                int PackageSupplierId = 0;
                List<AddSupplierPackageModel> supplierPackageModelList = new List<AddSupplierPackageModel>();
                List<AddRevisionModel> revisionModelList = new List<AddRevisionModel>();
                AddSupplierPackageRevisionModel supplierPackageRevisionModel = new AddSupplierPackageRevisionModel();

                foreach (var supInput in supInputList)
                {
                    //1.Add PackageSupplier
                    SupplierInput supplier = supInput.supplierInput;

                    if (!_dbcontext.TblSupplierPackages.Any(a => (a.SpPackageId == packId) && (a.SpSupplierId == supplier.supID)))
                    {
                        var spack = new TblSupplierPackage { SpPackageId = packId, SpSupplierId = supplier.supID, SpByBoq = ByBoq };
                        _dbcontext.Add<TblSupplierPackage>(spack);
                        _dbcontext.SaveChanges();

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
                        });
                    }
                    else
                    {
                        var supPack = await _dbcontext.TblSupplierPackages.Where(a => (a.SpPackageId == packId) && (a.SpSupplierId == supplier.supID)).FirstOrDefaultAsync();
                        PackageSupplierId = supPack.SpPackSuppId;
                    }

                    //2.Add Revision
                    int LastRevNo = GetMaxRevisionNumber(PackageSupplierId);
                    if (LastRevNo != -1)
                    {
                        int i = LastRevNo;
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

                    var Rev1 = await _dbcontext.TblSupplierPackageRevisions.SingleOrDefaultAsync(b => b.PrRevNo == 1 && b.PrPackSuppId == PackageSupplierId);
                    var supPackRev = new TblSupplierPackageRevision();
                    int rev1Id = 0;

                    if (Rev1 != null)
                    {
                        supPackRev = new TblSupplierPackageRevision { PrRevNo = 0, PrPackSuppId = PackageSupplierId, PrTotPrice = Rev1.PrTotPrice, PrRevDate = DateTime.Now, PrCurrency = Rev1.PrCurrency, PrExchRate = Rev1.PrExchRate };
                        rev1Id = Rev1.PrRevId;
                    }
                    else
                    {
                        int prjCurrency = (int)(await _dbcontext.TblParameters.FirstOrDefaultAsync()).EstimatedCur;
                        supPackRev = new TblSupplierPackageRevision { PrRevNo = 0, PrPackSuppId = PackageSupplierId, PrTotPrice = 0, PrRevDate = DateTime.Now, PrCurrency = prjCurrency };
                        rev1Id = 0;
                    }
                    _dbcontext.Add<TblSupplierPackageRevision>(supPackRev);
                    _dbcontext.SaveChanges();

                    //Get inserted Revison ID
                    var Rev0 = await _dbcontext.TblSupplierPackageRevisions.SingleOrDefaultAsync(b => (b.PrPackSuppId == PackageSupplierId) && (b.PrRevNo == 0));
                    int rev0Id = Rev0.PrRevId;

                    var packageSupp = await _dbcontext.TblSupplierPackages.Where(x => x.SpPackSuppId == PackageSupplierId).FirstOrDefaultAsync();
                    byte byBoq = (byte)((packageSupp.SpByBoq == null) ? 0 : packageSupp.SpByBoq);

                    List<TblRevisionDetail> LstRevDetails = await InsertRevisionDetail(rev0Id, packId, byBoq, rev1Id);

                    //Insert ComConditions Conditions Revision
                    List<TblSuppComCondReply> LstComCondReply = await InsertComercialConditions(rev0Id, packId, rev1Id, supInput.comercialCondList);

                    //Insert Technical Conditions Revision
                    List<TblSuppTechCondReply> LstTechCondReply = await InsertTechnicalConditions(rev0Id, packId, rev1Id, supInput.technicalCondList);


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
                            RevisionDetails = (from d in LstRevDetails
                                               select new AddRevisionDetailModel
                                               {
                                                   BoqResourceSeq = d.RdResourceSeq,
                                                   ResourceDescription = ByBoq == 1 ? "" : GetRessourceDescription(d.RdResourceSeq),
                                                   ItemO = d.RdBoqItem,
                                                   ItemDescription = GetBoqItemDescription(d.RdBoqItem),
                                                   Quantity = d.RdQty,
                                                   UnitPrice = d.RdPrice,
                                                   TotalPrice = (d.RdQty) * (d.RdPrice),
                                                   DiscountPerc = 0,
                                                   Comments = "",
                                                   CreatedOn = DateTime.Now,
                                                   IsSynched = false,
                                                   ProjectCode = proj.PrjCode,
                                                   ParentItemO = d.ParentItemO,
                                                   ParentResourceId = d.ParentResourceId,
                                                   NewItemId = d.NewItemId,
                                                   NewItemResourceId = d.NewItemResourceId
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


                    //send email
                    string SupEmail = "";
                    var Sup = _mdbContext.TblSuppliers.Where(s => s.SupCode == supplier.supID).FirstOrDefault();

                    if (Sup != null)
                        SupEmail = Sup.SupEmail;

                    if (SupEmail != "")
                    {
                        List<string> mylistTo = new List<string>();
                        mylistTo.Add(SupEmail);

                        List<string> mylistCC = new List<string>();
                        if (supInput.mailCC != null)
                        {
                            foreach (var ccMail in supInput.mailCC)
                            {
                                mylistCC.Add(ccMail);
                            }
                        }

                        List<string> mylistBCC = new List<string>();
                        if (user.UsrEmail != "")
                            mylistBCC.Add(user.UsrEmail);

                        string Subject = "Procurement";

                        string MailBody;

                        if (supInput.EmailTemplate != "")
                        {
                            MailBody = supInput.EmailTemplate;
                        }
                        else
                        {
                            MailBody = "Dear Sir,";
                            MailBody += Environment.NewLine;
                            MailBody += Environment.NewLine;
                            MailBody += "Kindly find attachments , and fill the price ";
                            MailBody += Environment.NewLine;
                            MailBody += Environment.NewLine;
                            MailBody += Environment.NewLine;
                            MailBody += Environment.NewLine;
                            MailBody += "Best regards";
                        }

                        if (userSignature != "")
                        {
                            MailBody += @"<br><br>";
                            MailBody += userSignature;
                        }

                        Mail m = new Mail();
                        sent = m.SendMail(mylistTo, mylistCC, mylistBCC, Subject, MailBody, AttachmentList, true, attachments);
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
                    await t.CommitAsync();
                    return true;
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

        private string GetRessourceDescription(int boqSeq)
        {
            var result = from a in _dbcontext.TblBoqs
                         join b in _dbcontext.TblResources on a.BoqResSeq equals b.ResSeq
                         where a.BoqSeq == boqSeq
                         select new RessourceList
                         {
                             resSeq=a.BoqResSeq,
                             resDesc=b.ResDescription
                         };

            if (result == null)
                return "";
            else
                return result.FirstOrDefault().resDesc;
        }

        private string GetBoqItemDescription(string boqItemO)
        {
            var result = _dbcontext.TblOriginalBoqs.Where(x => x.ItemO == boqItemO).FirstOrDefault();

            if (result == null)
                return "";
            else
                return result.DescriptionO;
        }

        private async Task<List<TblRevisionDetail>> InsertRevisionDetail(int revId, int packId, byte byBoq, int rev1Id)
        {
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
                            RdComment = row.RdComment,
                            RdMissedPrice = row.RdMissedPrice,
                            RdDiscount = row.RdDiscount,
                            RdAddedItem = row.RdAddedItem,
                            IsAlternative = row.IsAlternative,
                            IsNew=row.IsNew,
                            NewItemId=row.NewItemId,
                            NewItemResourceId=row.NewItemResourceId,
                            ParentItemO = row.ParentItemO,
                            ParentResourceId = row.ParentResourceId
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
                    result = (from o in _dbcontext.TblOriginalBoqs
                              where o.Scope == packId
                              select new BoqRessourcesList
                              {
                                  RowNumber = o.RowNumber,
                                  SectionO = o.SectionO,
                                  ItemO = o.ItemO,
                                  DescriptionO = o.DescriptionO,
                                  UnitO = o.UnitO,
                                  QtyO = o.QtyO,
                                  UnitRateO = o.UnitRate,
                                  ScopeO = o.Scope
                              }).ToList();

                    foreach (var row in result)
                    {
                        if ((row.ItemO != "") && (row.QtyO > 0))
                        {
                            var revdtl = new TblRevisionDetail()
                            {
                                RdRevisionId = revId,
                                RdResourceSeq = 0,
                                RdBoqItem = row.ItemO,
                                RdPrice = 0,
                                RdPriceOrigCurrency = 0,
                                RdQty = row.QtyO,
                                RdComment = "",
                                RdMissedPrice = 0,
                                RdDiscount = discount,
                                RdAddedItem = 0,
                                IsAlternative = false,
                                IsNew = false,
                                NewItemId = 0,
                                NewItemResourceId = 0,
                                ParentItemO ="",
                                ParentResourceId=0
                            };
                            LstRevDetails.Add(revdtl);
                        }
                    }
                }
                else
                {
                    result = (from b in _dbcontext.TblBoqs
                              where b.BoqScope == packId
                              select new BoqRessourcesList
                              {
                                  RowNumber = 0,
                                  BoqItem = b.BoqItem,
                                  BoqSeq = b.BoqSeq,
                                  BoqCtg = b.BoqCtg,
                                  BoqUnitMesure = b.BoqUnitMesure,
                                  BoqQty = b.BoqQty,
                                  BoqUprice = b.BoqUprice,
                                  BoqDiv = b.BoqDiv,
                                  BoqPackage = b.BoqPackage,
                                  BoqScope = b.BoqScope
                              }).ToList();

                    foreach (var row in result)
                    {
                        if ((row.BoqPackage != "") && (row.BoqQty > 0))
                        {
                            var revdtl = new TblRevisionDetail()
                            {
                                RdRevisionId = revId,
                                RdResourceSeq = row.BoqSeq,
                                RdBoqItem = row.BoqItem,
                                RdPrice = 0,
                                RdQty = row.BoqQty,
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
                                ParentResourceId = 0
                            };
                            LstRevDetails.Add(revdtl);
                        }
                    }
                }
            }

            if (LstRevDetails.Count() > 0)
                {
                    await _dbcontext.AddRangeAsync(LstRevDetails);
                    await _dbcontext.SaveChangesAsync();
                }               

            return LstRevDetails;
        }

        private async Task<List<TblSuppComCondReply>> InsertComercialConditions(int revId, int packId, int rev1Id,List<Condition> comCondList)
        {
            List<TblSuppComCondReply> LstComCondReply = new List<TblSuppComCondReply>();

            if (rev1Id > 0)  //In case of previous revision exists , so you have to insert new details from pervious
            {
                var oldRevDtl = _dbcontext.TblSuppComCondReplies.Where(x => x.CdRevisionId == rev1Id).ToList();
                if (oldRevDtl != null)
                {
                    foreach (var row in oldRevDtl)
                    {
                        var ComCondReply = new TblSuppComCondReply()
                        {
                            CdRevisionId = revId,
                            CdComConId = row.CdComConId,
                            CdSuppReply = row.CdSuppReply,
                            CdAccCond = row.CdAccCond
                        };
                        LstComCondReply.Add(ComCondReply);
                    }

                    var newConditions = comCondList.Where(s => !oldRevDtl.Where(es => es.CdComConId == s.id).Any());
                    foreach (var row in newConditions)
                    {
                        var ComCondReply = new TblSuppComCondReply()
                        {
                            CdRevisionId = revId,
                            CdComConId = row.id,
                            CdSuppReply = "",
                            CdAccCond = row.ACCCondValue
                        };
                        LstComCondReply.Add(ComCondReply);
                    }
                }
            }
            else
            {
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
            }

            if (LstComCondReply.Count() > 0)
            {
                await _dbcontext.AddRangeAsync(LstComCondReply);
                await _dbcontext.SaveChangesAsync();
            }

            return LstComCondReply;
        }

        private async Task<List<TblSuppTechCondReply>> InsertTechnicalConditions(int revId, int packId, int rev1Id,List<Condition> techCondList)
        {
            List<TblSuppTechCondReply> LsttechCondReply = new List<TblSuppTechCondReply>();

            if (rev1Id > 0)  //In case of previous revision exists , so you have to insert new details from pervious
            {
                var oldRevDtl = _dbcontext.TblSuppComCondReplies.Where(x => x.CdRevisionId == rev1Id).ToList();
                if (oldRevDtl != null)
                {
                    foreach (var row in oldRevDtl)
                    {
                        var ComCondReply = new TblSuppTechCondReply()
                        {
                            TcRevisionId = revId,
                            TcTechConId = row.CdComConId,
                            TcSuppReply = row.CdSuppReply,
                            TcAccCond = row.CdAccCond
                        };
                        LsttechCondReply.Add(ComCondReply);
                    }

                    var newConditions = techCondList.Where(s => !oldRevDtl.Where(es => es.CdComConId == s.id).Any());
                    foreach (var row in newConditions)
                    {
                        var ComCondReply = new TblSuppTechCondReply()
                        {
                            TcRevisionId = revId,
                            TcTechConId = row.id,
                            TcSuppReply = "",
                            TcAccCond = row.ACCCondValue
                        };
                        LsttechCondReply.Add(ComCondReply);
                    }
                }
            }
            else
            {
                foreach (var comCond in techCondList)
                {
                    var ComCondReply = new TblSuppTechCondReply()
                    {
                        TcRevisionId = revId,
                        TcTechConId = comCond.id,
                        TcSuppReply = "",
                        TcAccCond = comCond.ACCCondValue
                    };
                    LsttechCondReply.Add(ComCondReply);
                }
            }

            if (LsttechCondReply.Count() > 0)
            {
                await _dbcontext.AddRangeAsync(LsttechCondReply);
                await _dbcontext.SaveChangesAsync();
            }

            return LsttechCondReply;
        }

        public int GetMaxRevisionNumber(int PackageSupplierId)
        {
            var query = _dbcontext.TblSupplierPackageRevisions.Where(x => x.PrPackSuppId == PackageSupplierId);
            var MaxRevisionNumber = query.Any() ? query.Max(x => x.PrRevNo) : -1;
            return (int)MaxRevisionNumber;
        }

        public string SendComercialConditions(int packId, List<Condition> comCondList)
        {
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
                string excelName = $"Commercial Conditions-{PackageName}-{ProjectName}.xlsx";

                //string path = @"C:\App\";

                //if (!Directory.Exists(path))
                //{
                //    Directory.CreateDirectory(path);
                //}
                //string FullPath = path + excelName;

                if (File.Exists(excelName))
                    File.Delete(excelName);

                xlPackage.SaveAs(excelName);

                return excelName;
            }
        }

    }
}
