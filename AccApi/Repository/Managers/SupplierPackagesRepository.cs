using System.Collections.Generic;
using System.Linq;
using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.View_Models;
using AccApi.Repository.View_Models.Request;
using System.IO;
using OfficeOpenXml;
using AccApi.Data_Layer;
using System;
using Microsoft.AspNetCore.Http;

namespace AccApi.Repository.Managers
{
    public class SupplierPackagesRepository : ISupplierPackagesRepository
    {
        private readonly AccDbContext _dbcontext;
        private readonly MasterDbContext _mdbcontext;
        private readonly PolicyDbContext _pdbcontext;

        public SupplierPackagesRepository(AccDbContext Context, MasterDbContext mdbcontext, PolicyDbContext pdbcontext)
        {
            _dbcontext = Context;
            _mdbcontext = mdbcontext;
            _pdbcontext = pdbcontext;
        }

        public SupplierPackagesList GetSupplierPackage(int spId)
        {
            var results = from b in _dbcontext.TblSupplierPackages
                          join c in _dbcontext.TblSuppliers on b.SpSupplierId equals c.SupCode
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

        public List<SupplierPackagesList> SupplierPackagesList(int packageid)
        {
            var results = from b in _dbcontext.TblSupplierPackages
                          join c in _dbcontext.TblSuppliers on b.SpSupplierId equals c.SupCode
                          where b.SpPackageId == packageid
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
            return results.ToList();
        }

        private List<boqPackageList> boqPackageList(int packId, byte byboq)
        {
            if (byboq == 1)
            {
                var pack = from o in _dbcontext.TblOriginalBoqs
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
                               c1 = o.C1,
                               c2 = o.C2,
                               c3 = o.C3,
                               c4 = o.C4,
                               c5 = o.C5,
                               c6 = o.C6,
                               item = o.ItemO,
                               boqDesc = o.DescriptionO,
                               unit = o.UnitO,
                               qty = (double)o.QtyO
                           };
                return pack.ToList();
            }
            else
            {
                var pack = from o in _dbcontext.TblOriginalBoqs
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
                               qty = (double)o.QtyO,
                               resType = b.BoqCtg,
                               resCode = b.BoqPackage,
                               resDesc = r.ResDescription,
                               ResUnit = b.BoqUnitMesure,
                               boqQtyScope = (double)b.BoqQtyScope
                           };
                return pack.ToList();
            }
        }

        public string ValidateExcelBeforeAssign(int packId,byte byBoq)
        {
            //AH0702
            //var packageSupp = _dbcontext.TblSupplierPackages.Where(x => x.SpPackageId == packId).FirstOrDefault();
            //byte byBoq = (byte)((packageSupp.SpByBoq==null) ? 0 : packageSupp.SpByBoq);
            //AH0702

            var package = _dbcontext.PackagesNetworks.Where(x => x.IdPkge == packId).FirstOrDefault();
            string PackageName = package.PkgeName;

            var result = boqPackageList(packId,byBoq);

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
                worksheet.Cells[i, 2].Value = "Level";
                worksheet.Column(3).Width = 50;
                worksheet.Columns[3].Style.WrapText = true;                
                worksheet.Column(3).AutoFit();
                worksheet.Cells[i, 3].Value = "Bill Description";
                worksheet.Cells[i, 4].Value = "Unit";
                worksheet.Cells[i, 5].Value = "Qty";

                if (byBoq==1)
                {
                    worksheet.Cells[i, 6].Value = "Unit Price";
                    worksheet.Cells[i, 7].Value = "Comments";
                    worksheet.Column(7).Width = 50;              
                    worksheet.Columns[7].Style.WrapText = true;
                    worksheet.Column(7).AutoFit();
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
                    worksheet.Cells[i, 11].Value = "Unit Price";
                    worksheet.Column(12).Width = 50;
                    worksheet.Columns[12].Style.WrapText = true;
                    worksheet.Column(12).AutoFit();
                    worksheet.Cells[i, 12].Value = "Comments";
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
                            worksheet.Cells[i, 2].Value = "1";
                            worksheet.Cells[i, 3].Value = (x.l1 == null) ? "" : x.l1;
                            worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                            oldl1 = x.l1;
                            i = i + 2;
                        }
                        if ((l2 != "") && (l2 != oldl2))
                        {
                            worksheet.Cells[i, 2].Value = "2";
                            worksheet.Cells[i, 3].Value = (x.l2 == null) ? "" : x.l2;
                            worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                            oldl2 = x.l2;
                            i = i + 2;
                        }
                        if ((l3 != "") && (l3 != oldl3))
                        {
                            worksheet.Cells[i, 2].Value = "3";
                            worksheet.Cells[i, 3].Value = (x.l3 == null) ? "" : x.l3;
                            worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                            oldl3 = x.l3;
                            i = i + 2;
                        }
                        if ((l4 != "") && (l4 != oldl4))
                        {
                            worksheet.Cells[i, 2].Value = "4";
                            worksheet.Cells[i, 3].Value = (x.l4 == null) ? "" : x.l4;
                            worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                            oldl4 = x.l4;
                            i = i + 2;
                        }
                        if ((l5 != "") && (l5 != oldl5))
                        {
                            worksheet.Cells[i, 2].Value = "5";
                            worksheet.Cells[i, 3].Value = (x.l5 == null) ? "" : x.l5;
                            worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                            oldl5 = x.l5;
                            i = i + 2;
                        }
                        if ((l6 != "") && (l6 != oldl6))
                        {
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
                                worksheet.Cells[i, 2].Value = "C";
                                worksheet.Cells[i, 3].Value = (x.c1 == null) ? "" : x.c1;
                                worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                i = i + 2;
                                OldC = C;
                            }
                            if (((x.c2 == null) ? "" : x.c2) != "")
                            {
                                worksheet.Cells[i, 2].Value = "C";
                                worksheet.Cells[i, 3].Value = (x.c2 == null) ? "" : x.c2;
                                worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                i = i + 2;
                            }
                            if (((x.c3 == null) ? "" : x.c3) != "")
                            {
                                worksheet.Cells[i, 2].Value = "C";
                                worksheet.Cells[i, 3].Value = (x.c3 == null) ? "" : x.c3;
                                worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                i = i + 2;
                            }
                            if (((x.c4 == null) ? "" : x.c4) != "")
                            {
                                worksheet.Cells[i, 2].Value = "C";
                                worksheet.Cells[i, 3].Value = (x.c4 == null) ? "" : x.c4;
                                worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                i = i + 2;
                            }
                            if (((x.c5 == null) ? "" : x.c5) != "")
                            {
                                worksheet.Cells[i, 2].Value = "C";
                                worksheet.Cells[i, 3].Value = (x.c5 == null) ? "" : x.c5;
                                worksheet.SelectedRange[i, 3].Style.Font.Bold = true;
                                i = i + 2;
                            }
                            if (((x.c6 == null) ? "" : x.c6) != "")
                            {
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

                        if (byBoq == 1)
                        {
                            worksheet.Cells[i, 6].Style.Locked = false;
                            worksheet.Cells[i, 7].Style.Locked = false;
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
                        worksheet.Cells[i, 10].Value = (x.boqQtyScope == null) ? "" : x.boqQtyScope;
                        worksheet.Cells[i, 11].Style.Locked = false;
                        worksheet.Cells[i, 12].Style.Locked = false;
                    }             
                    
                    i++;
                }

                xlPackage.Save();
                stream.Position = 0;
                string excelName = $"Package-{PackageName}.xlsx";

                string path = @"C:\App\";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string FullPath = path + excelName;

                if (File.Exists(FullPath))
                    File.Delete(FullPath);

                xlPackage.SaveAs(excelName);

                package.FilePath = excelName;
                _dbcontext.SaveChanges();

                return excelName;
            }
        }

        public bool AssignPackageSuppliers(int packId, List<SupplierInputList> supInputList, string FilePath, string EmailContent,byte ByBoq)
        {
            string sent = "";
            string ComCondAttch = "";

            var AttachmentList = new List<string>();

            foreach(var item in supInputList)
            {
            AttachmentList.Clear();
            AttachmentList.Add(FilePath);

            if (item.comercialCondList.Count >0)
            {
                    if (ComCondAttch=="")
                        ComCondAttch = SendComercialConditions(packId, item.comercialCondList);

                AttachmentList.Add(ComCondAttch);
            }

                SupplierInput supplier = item.supplierInput;

                if (!_dbcontext.TblSupplierPackages.Any(a => (a.SpPackageId == packId) && (a.SpSupplierId == supplier.supID)))
                {
                    var spack = new TblSupplierPackage { SpPackageId = packId, SpSupplierId = supplier.supID, SpByBoq = ByBoq };
                    _dbcontext.Add<TblSupplierPackage>(spack);
                    _dbcontext.SaveChanges();

                    //send email
                    string SupEmail = (from r in _dbcontext.TblSuppliers
                                       where r.SupCode == supplier.supID
                                       select r.SupEmail).First<string>();

                    if (SupEmail != "")
                    {
                        List<General> mylistTo = new List<General>();
                        General g = new General();
                        g.mail = (string)SupEmail;
                        mylistTo.Add(g);

                        List<General> mylistCC = new List<General>();
                        General cc = new General();
                        cc.mail = (string)SupEmail;
                        mylistCC.Add(cc);

                        string Subject = "Procurement";

                        string MailBody;

                        if (EmailContent != "")
                        {
                            MailBody = EmailContent;
                        }
                        else
                        {
                            MailBody = "Dear Sir,";
                            MailBody += Environment.NewLine;
                            MailBody += Environment.NewLine;
                            MailBody += "Please find attached , and fill the price ";
                            MailBody += Environment.NewLine;
                            MailBody += Environment.NewLine;
                            MailBody += Environment.NewLine;
                            MailBody += Environment.NewLine;
                            MailBody += "Best regards";
                        }

                       Mail m = new Mail();
                       sent= m.SendMail(mylistTo, mylistCC, Subject, MailBody, AttachmentList, false,null);
                    }
                }
            }
            return (sent=="sent");
        }

        public string SendComercialConditions(int packId, List<ComercialCond> comCondList)
        {
            var package = _dbcontext.PackagesNetworks.Where(x => x.IdPkge == packId).FirstOrDefault();
            string PackageName = package.PkgeName;

            var p = _dbcontext.TblParameters.FirstOrDefault();
            var proj = _pdbcontext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefault();
            string ProjectName = proj.PrjName;

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

                string path = @"C:\App\";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string FullPath = path + excelName;

                if (File.Exists(FullPath))           
                    File.Delete(FullPath);
                
                xlPackage.SaveAs(FullPath);

                return FullPath;
            }
        }

    }
}
