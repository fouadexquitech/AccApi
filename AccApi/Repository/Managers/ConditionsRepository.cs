using AccApi.Data_Layer;
using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.Models.MasterModels;
using AccApi.Repository.View_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.Managers
{
    public class ConditionsRepository : IConditionsRepository
    {
        private readonly MasterDbContext _mdbcontext;
        private readonly PolicyDbContext _pdbcontext;
        private readonly AccDbContext _dbcontext;

        public ConditionsRepository(AccDbContext Context, MasterDbContext mdbcontext, PolicyDbContext pdbcontext)
        {
            _dbcontext = Context;
            _mdbcontext = mdbcontext;
            _pdbcontext = pdbcontext;
        }

        public List<ComConditions> GetComConditions()
        {
            var result = from b in _mdbcontext.TblComConds
                         select new ComConditions
                         {
                             CmSeq = b.CmSeq,
                             CmDescription = b.CmDescription
                         };

            return result.ToList();
        }

        public List<TechConditions> GetTechConditions(int packId)
        {
            var result = from b in _mdbcontext.TblTechConds.Where(x => x.TcPackId == packId)
                         select new TechConditions
                         {
                             TcSeq = b.TcSeq,
                             TcDescription = b.TcDescription,
                             TcPackId = b.TcPackId
                         };

            return result.ToList();
        }

        public List<TmpConditionsReply> GetComConditionsReply(int PackageSupliersID)
        {
            //var comcond = (from b in _mdbcontext.TblComConds
            //               select b).ToList();

            //var result = (from b in comcond
            //              join a in _dbcontext.TblSuppComCondReplies on b.CmSeq equals a.CdComConId
            //              join c in _dbcontext.TblSupplierPackages on a.CdPackageSupliersId equals c.SpPackSuppId
            //              join d in _dbcontext.TblSuppliers on c.SpSupplierId equals d.SupCode
            //              where (a.CdPackageSupliersId == PackageSupliersID)
            //              select new ConditionsReply
            //              {
            //                  condId = a.CdComConId,
            //                  condDesc = b.CmDescription,
            //                  supId = d.SupCode,
            //                  supName = d.SupName,
            //                  condReply = a.CdSuppReply
            //              });

            var param1 = new SqlParameter("@PackageSupliersID", PackageSupliersID);
            var param2 = new SqlParameter("@Type", 1);

            List<TmpConditionsReply> list = _dbcontext
                        .TmpConditionsReplies
                        .FromSqlRaw("exec SP_GetConditionsReply @PackageSupliersID,@Type", param1, param2)
                        .ToList();

            return list;
        }

        public List<TmpConditionsReply> GetPackageComConditionsReply(int PackageID)
        {        
            var param1 = new SqlParameter("@PackageID", PackageID);
            var param2 = new SqlParameter("@Type", 1);

            List<TmpConditionsReply> list = _dbcontext
                        .TmpConditionsReplies
                        .FromSqlRaw("exec SP_GetPackageConditionsReply @PackageID,@Type", param1, param2)
                        .ToList();

            return list;
        }

        public List<TmpConditionsReply> GetTechConditionsReply(int PackageSupliersID)
        {
            //var techcond = (from b in _mdbcontext.TblTechConds
            //                select b).ToList();

            //var result = (from b in techcond
            //              join a in _dbcontext.TblSuppTechCondReplies on b.TcSeq equals a.TcComConId
            //              join c in _dbcontext.TblSupplierPackages on a.TcPackageSupliersId equals c.SpPackSuppId
            //              join d in _dbcontext.TblSuppliers on c.SpSupplierId equals d.SupCode
            //              where (a.TcComConId == b.TcSeq && a.TcPackageSupliersId == PackageSupliersID)

            //              select new ConditionsReply
            //              {
            //                  condId = a.TcComConId,
            //                  supId = d.SupCode,
            //                  supName = d.SupName,
            //                  condReply = a.TcSuppReply
            //              });

            //return result.ToList();

            var param1 = new SqlParameter("@PackageSupliersID", PackageSupliersID);
            var param2 = new SqlParameter("@Type", 2);

            List<TmpConditionsReply> list = _dbcontext
                        .TmpConditionsReplies
                        .FromSqlRaw("exec SP_GetConditionsReply @PackageSupliersID,@Type", param1, param2)
                        .ToList();

            return list;
        }

        public List<TmpConditionsReply> GetPackageTechConditionsReply(int PackageID)
        {
            var param1 = new SqlParameter("@PackageID", PackageID);
            var param2 = new SqlParameter("@Type", 2);

            List<TmpConditionsReply> list = _dbcontext
                        .TmpConditionsReplies
                        .FromSqlRaw("exec SP_GetPackageConditionsReply @PackageID,@Type", param1, param2)
                        .ToList();

            return list;
        }

        public bool SendTechnicalConditions(int packId)
        {
            var package = _dbcontext.PackagesNetworks.Where(x => x.IdPkge == packId).FirstOrDefault();
            string PackageName = package.PkgeName;

            var p = _dbcontext.TblParameters.FirstOrDefault();
            var proj = _pdbcontext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefault();
            string ProjectName = proj.PrjName;

            var result = _mdbcontext.TblTechConds.Where(x => x.TcPackId == packId).ToList();

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var sent = false;
                var worksheet = xlPackage.Workbook.Worksheets.Add("BOQ");
                worksheet.Columns.AutoFit();
                //worksheet.Protection.IsProtected = false;

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

                worksheet.Cells[4, 1].Value = "Technical Conditions";
                worksheet.Cells[4, 1].Style.Font.Bold = true;
                worksheet.Cells[4, 1].Style.Font.UnderLine = true;
                worksheet.Cells["A4:B4"].Merge = true;

                i = 5;
                foreach (var x in result)
                {
                    worksheet.Cells[i, 2].Value = (x.TcDescription == null) ? "" : x.TcDescription;
                    worksheet.Column(2).Width = 50;

                    i++;
                }

                xlPackage.Save();
                stream.Position = 0;
                string excelName = $"Technical Conditions-{PackageName}-{ProjectName}.xlsx";

                string path = @"C:\App\";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string FullPath = path + excelName;

                if (File.Exists(FullPath))
                    File.Delete(FullPath);

                xlPackage.SaveAs(FullPath);


                //send email
                var SupPack = _dbcontext.TblSupplierPackages.Where(x => x.SpPackageId == packId).ToList();

                foreach (var sp in SupPack)
                {
                    string SupEmail = (from r in _dbcontext.TblSuppliers
                                       where r.SupCode == sp.SpSupplierId
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

                        string Subject = "Technical Conditions";

                        string MailBody;

                        MailBody = "Dear Sir,";
                        MailBody += Environment.NewLine;
                        MailBody += Environment.NewLine;
                        MailBody += "Please find attached , and fill requirements";
                        MailBody += Environment.NewLine;
                        MailBody += Environment.NewLine;
                        MailBody += Environment.NewLine;
                        MailBody += Environment.NewLine;
                        MailBody += "Best regards";

                        var AttachmentList = new List<string>();
                        AttachmentList.Add(FullPath);

                        Mail m = new Mail();
                        var res = m.SendMail(mylistTo, mylistCC, Subject, MailBody, AttachmentList, false,null);
                        sent = (res == "sent");

                        sp.TecCondSent = true;
                        _dbcontext.TblSupplierPackages.Update(sp);
                        _dbcontext.SaveChanges();
                    }
                }
                return sent;
            }
        }

        public bool UpdateCommercialConditions(int PackageSupliersID, IFormFile ExcelFile)
        {
            if (ExcelFile?.Length > 0)
            {
                var stream = ExcelFile.OpenReadStream();
                List<TblSuppComCondReply> LstSuppComCondReply = new List<TblSuppComCondReply>();

                try
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.First();
                        var rowCount = worksheet.Dimension.Rows;

                        for (var row = 2; row <= rowCount; row++)
                        {
                            try
                            {
                                string desc = worksheet.Cells[row, 2].Value == null ? "" : worksheet.Cells[row, 2].Value.ToString();
                                string reply = worksheet.Cells[row, 3].Value == null ? "" : worksheet.Cells[row, 3].Value.ToString();
                                double boqQty = worksheet.Cells[row, 5].Value == null ? 0 : (double)worksheet.Cells[row, 5].Value;

                                if ((desc != "") && (reply != "") && (!desc.Contains("Commercial Condition")) && (!desc.Contains("ACC condition")))
                                {
                                    var comCond = _mdbcontext.TblComConds.Where(x => x.CmDescription == desc).FirstOrDefault();
                                    if (comCond != null)      
                                    {
                                        int comcondId = comCond.CmSeq == null ? 0 : comCond.CmSeq;

                                        if (comcondId > 0)
                                        {
                                            var comCondExist = _dbcontext.TblSuppComCondReplies.Where(x => x.CdComConId == comcondId && x.CdPackageSupliersId == PackageSupliersID).FirstOrDefault();
                                            //int comcondIdExist = comCondExist.CdComConId == null ? 0 : comCondExist.CdComConId;

                                            if (comCondExist == null)
                                            {
                                                var SuppCom = new TblSuppComCondReply()
                                                {
                                                    CdComConId = comcondId,
                                                    CdPackageSupliersId = PackageSupliersID,
                                                    CdSuppReply = reply
                                                };
                                                LstSuppComCondReply.Add(SuppCom);
                                            }
                                            else
                                            {
                                                comCondExist.CdSuppReply = reply;
                                                _dbcontext.TblSuppComCondReplies.Update(comCondExist);
                                                _dbcontext.SaveChanges();
                                            }
                                        }
                                    }
                            }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    _dbcontext.AddRange(LstSuppComCondReply);
                    _dbcontext.SaveChanges();
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return true;

        }

        public bool UpdateTechnicalConditions(int packageId, int PackageSupliersID, IFormFile ExcelFile)
        {
            if (ExcelFile?.Length > 0)
            {
                var stream = ExcelFile.OpenReadStream();
                List<TblSuppTechCondReply> LstSuppComCondReply = new List<TblSuppTechCondReply>();

                try
                {
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (var package = new ExcelPackage(stream))
                    {
                        var worksheet = package.Workbook.Worksheets.First();
                        var rowCount = worksheet.Dimension.Rows;

                        for (var row = 2; row <= rowCount; row++)
                        {
                            try
                            {
                                string desc = worksheet.Cells[row, 2].Value == null ? "" : worksheet.Cells[row, 2].Value.ToString();
                                string reply = worksheet.Cells[row, 3].Value == null ? "" : worksheet.Cells[row, 3].Value.ToString();
                                double boqQty = worksheet.Cells[row, 5].Value == null ? 0 : (double)worksheet.Cells[row, 5].Value;

                                if ((desc != "") && (reply != "") && (!desc.Contains("Technical Condition")) && (!desc.Contains("ACC condition")))
                                {
                                    var comCond = _mdbcontext.TblTechConds.Where(x => x.TcDescription == desc && x.TcPackId == packageId).FirstOrDefault();
                                    if (comCond != null)
                                    {
                                        int condId = comCond.TcSeq == null ? 0 : comCond.TcSeq;

                                        if (condId > 0)
                                        {
                                            var comCondExist = _dbcontext.TblSuppTechCondReplies.Where(x => x.TcComConId == condId && x.TcPackageSupliersId == PackageSupliersID).FirstOrDefault();
                                            bool comcondIdExist = (comCondExist != null);

                                            if (!comcondIdExist)
                                            {
                                                var SuppCom = new TblSuppTechCondReply()
                                                {
                                                    TcComConId = condId,
                                                    TcPackageSupliersId = PackageSupliersID,
                                                    TcSuppReply = reply
                                                };
                                                LstSuppComCondReply.Add(SuppCom);
                                            }
                                            else
                                            {
                                                comCondExist.TcSuppReply = reply;
                                                _dbcontext.TblSuppTechCondReplies.Update(comCondExist);
                                                _dbcontext.SaveChanges();
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                                return false;
                            }
                        }
                    }
                    _dbcontext.AddRange(LstSuppComCondReply);
                    _dbcontext.SaveChanges();
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return true;
        }


        public bool AddComConditions(List<ComConditions> cond)
        {
            foreach (var item in cond)
            {
                var result = new TblComCond { CmDescription=item.CmDescription,CmSelected=0};
                _mdbcontext.Add<TblComCond>(result);
                _mdbcontext.SaveChanges();
            }

            return true;
        }

        public bool UpdateComConditions(ComConditions cond)
        {
            var result = _mdbcontext.TblComConds.Where(x => x.CmSeq == cond.CmSeq).FirstOrDefault();
            result.CmDescription = cond.CmDescription;
            result.CmSelected = 0;
           
            if (result != null)
            {
                _mdbcontext.TblComConds.Update(result);
                _mdbcontext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool DelComConditions(int id)
        {
            var result = _mdbcontext.TblComConds.Where(x => x.CmSeq == id).FirstOrDefault();

            if (result != null)
            {
                _mdbcontext.TblComConds.Remove(result);
                _mdbcontext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool AddTechConditions(List<TechConditions> cond)
        {
            foreach (var item in cond)
            {
                var result = new TblTechCond { TcDescription = item.TcDescription,TcPackId=item.TcPackId,TcSelected = 0 };
                _mdbcontext.Add<TblTechCond>(result);
                _mdbcontext.SaveChanges();
            }

            return true;
        }

        public bool UpdateTechConditions(TechConditions cond)
        {
            var result = _mdbcontext.TblTechConds.Where(x => x.TcSeq == cond.TcSeq).FirstOrDefault();
            result.TcDescription = cond.TcDescription;
            result.TcPackId = cond.TcPackId;
            result.TcSelected = 0;

            if (result != null)
            {
                _mdbcontext.TblTechConds.Update(result);
                _mdbcontext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool DelTechConditions(int id)
        {
            var result = _mdbcontext.TblTechConds.Where(x => x.TcSeq == id).FirstOrDefault();

            if (result != null)
            {
                _mdbcontext.TblTechConds.Remove(result);
                _mdbcontext.SaveChanges();
                return true;
            }
            else
                return false;
        }


    }
}

