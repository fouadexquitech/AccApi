using AccApi.Data_Layer;
using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using AccApi.Repository.Models.MasterModels;
using AccApi.Repository.View_Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly GlobalLists _globalLists;

        private readonly IlogonRepository _logonRepository;

        IConfiguration configuration;

        public ConditionsRepository(AccDbContext Context, MasterDbContext mdbcontext, PolicyDbContext pdbcontext, IlogonRepository logonRepository, GlobalLists globalLists)
        {
            _mdbcontext = mdbcontext;
            _pdbcontext = pdbcontext;
            _logonRepository = logonRepository;
            _globalLists = globalLists;
            _dbcontext = new AccDbContext(_globalLists.GetAccDbconnectionString());
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
        public List<TechConditions> GetTechConditions(int packId, string? filter)
        {
            var techCond = (from b in _mdbcontext.TblTechConds
                            select b).ToList();
                         
            var result = (from c in techCond
                          join b in _dbcontext.TblTechCondGroups on c.TcSeq equals b.TechCondId 
                          join a in _dbcontext.ComparisonPackageGroups on b.GroupId equals a.Id                            
                          where a.PackageId== packId 
                
                select new TechConditions
                {
                    TcDescription=c.TcDescription,
                    TcPackId=packId,                   
                    TcSeq=c.TcSeq,
                }).GroupBy(p => p.TcSeq)
                  .Select(g => g.First())
                  .ToList();

            if (filter != null)
            {
                if (filter != "")
                {
                    result = result.Where(x => x.TcDescription.ToUpper().Contains(filter.ToUpper())).ToList();
                }
            }

            foreach (var tc in result)
            {
                tc.techConditionGroups = (from b in _dbcontext.TblTechCondGroups
                                          join a in _dbcontext.ComparisonPackageGroups on b.GroupId equals a.Id
                                          where b.TechCondId == tc.TcSeq

                                          select new TechConditionGroup
                                          {
                                              groupId = b.GroupId,
                                              groupDescription = a.Name
                                          }).ToList();
            }

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

        public bool SendTechnicalConditions(int packId, TechCondModel techCondModel, string UserName)
        {
            var package = _mdbcontext.TblPackages.Where(x => x.PkgeId == packId).FirstOrDefault();
            string PackageName = package.PkgeName;

            var p = _dbcontext.TblParameters.FirstOrDefault();
            //var proj = _pdbcontext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefault();
            //string ProjectName = proj.PrjName;
            string ProjectName = p.Project;

            //var result =  _mdbcontext.TblTechConds.Where(x => x.TcPackId == packId).ToList();

            var result = GetTechConditions(packId,"");

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var sent = false;
                var worksheet = xlPackage.Workbook.Worksheets.Add("BOQ");
                worksheet.Columns.AutoFit();
                worksheet.Protection.IsProtected = true;

                int i, j;

                worksheet.Cells[1, 1].Value = "Project :" + ProjectName;
                worksheet.Cells["A1:C1"].Merge = true;

                worksheet.Cells[2, 1].Value = "";
                worksheet.Cells["A2:C2"].Merge = true;

                worksheet.Cells[3, 1].Value = "ACC Conditions";
                worksheet.Cells["A3:B3"].Merge = true;

                worksheet.Cells[4, 1].Value = "Technical Conditions";
                worksheet.Cells[4, 1].Style.Font.Bold = true;
                worksheet.Cells[4, 1].Style.Font.UnderLine = true;
                worksheet.Cells["A4:B4"].Merge = true;

                worksheet.Cells[4, 3].Value = "ACC Conditions";
                worksheet.Cells[4, 3].Style.Font.Bold = true;
                worksheet.Cells[4, 3].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                worksheet.Cells[4, 4].Value = "Supplier/subcontractor reply";
                worksheet.Column(4).Width = 50;
                worksheet.Columns[4].Style.WrapText = true;
                worksheet.Cells[4, 4].Style.Font.Bold = true;
                worksheet.Cells[4, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;

                i = 5;
                foreach (var x in result)
                {
                    worksheet.Cells[i, 2].Value = (x.TcDescription == null) ? "" : x.TcDescription;
                    worksheet.Column(2).Width = 50;

                    var accCond = techCondModel.AccCondList.FirstOrDefault(z => z.condId == x.TcSeq);
                    if (accCond != null)
                    {
                        worksheet.Cells[i, 3].Value = accCond.AccCondition;
                        worksheet.Column(3).AutoFit();
                    }

                    worksheet.Cells[i, 4].Style.Locked = false;
                    worksheet.Cells[i, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    i++;
                }

                xlPackage.Save();
                stream.Position = 0;
                string excelName = $"Technical Conditions-{PackageName}-{ProjectName}.xlsx";

                if (File.Exists(excelName))
                    File.Delete(excelName);

                xlPackage.SaveAs(excelName);


                //send email
                var SupPack = _dbcontext.TblSupplierPackages.Where(x => x.SpPackageId == packId).ToList();

                foreach (var sp in SupPack)
                {
                    string SupEmail = (from r in _mdbcontext.TblSuppliers
                                       where r.SupCode == sp.SpSupplierId
                                       select r.SupEmail).First<string>();

                    if (SupEmail != "")
                    {
                        List<string> mylistTo = new List<string>();
                        mylistTo.Add(SupEmail);

                        List<string> mylistCC = new List<string>();
                        //mylistCC = null;
                        foreach (var mail in techCondModel.ListCC)
                        {
                            mylistCC.Add(mail);
                        }

                        //BCC
                        List<string> mylistBCC = new List<string>();
                        //mylistBCC = null;
                        User user = _logonRepository.GetUser(UserName);
                        if (user.UsrEmail != "")
                            mylistBCC.Add(user.UsrEmail);

                        string Subject = "Technical Conditions";

                        string MailBody;

                        MailBody = "Dear Sir,";
                        MailBody += Environment.NewLine;
                        MailBody += Environment.NewLine;
                        MailBody += "Please find attached , and fill requirements.";
                        MailBody += Environment.NewLine;
                        MailBody += Environment.NewLine;
                        MailBody += Environment.NewLine;
                        MailBody += Environment.NewLine;
                        //MailBody += "Best regards";

                        var AttachmentList = new List<string>();
                        AttachmentList.Add(excelName);

                        string userSignature = (user.UsrEmailSignature == null) ? "" : user.UsrEmailSignature;                       
                        if (userSignature != "")
                        {
                            MailBody += @"<br><br>";
                            MailBody += userSignature;
                        }

                        Mail m = new Mail();
                        var res = m.SendMail(mylistTo, mylistCC, mylistBCC, Subject, MailBody, AttachmentList, false,null);
                        sent = (res == "sent");

                        sp.TecCondSent = true;
                        _dbcontext.TblSupplierPackages.Update(sp);
                        _dbcontext.SaveChanges();
                    }
                }
                return sent;
            }
        }
        public bool UpdateCommercialConditions(int PackageSupliersRevisionID, IFormFile ExcelFile)
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
                                string reply = worksheet.Cells[row, 3].Text == null ? "" : worksheet.Cells[row, 3].Text.ToString();
                              
                                if ((desc != "") && (!desc.Contains("Commercial Condition")) && (!desc.Contains("ACC condition")))
                                {
                                    var comCond = _mdbcontext.TblComConds.Where(x => x.CmDescription == desc).FirstOrDefault();
                                    if (comCond != null)      
                                    {
                                        int comcondId = comCond.CmSeq == null ? 0 : comCond.CmSeq;

                                        if (comcondId > 0)
                                        {
                                            var comCondExist = _dbcontext.TblSuppComCondReplies.Where(x => x.CdComConId == comcondId && x.CdRevisionId == PackageSupliersRevisionID).FirstOrDefault();
                                            //int comcondIdExist = comCondExist.CdComConId == null ? 0 : comCondExist.CdComConId;

                                            if (comCondExist == null)
                                            {
                                                var SuppCom = new TblSuppComCondReply()
                                                {
                                                    CdComConId = comcondId,
                                                    CdRevisionId = PackageSupliersRevisionID,
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
        public bool UpdateTechnicalConditions(int packageId, int PackageSupliersRevisionID, IFormFile ExcelFile)
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
                                string AccCond = worksheet.Cells[row, 3].Text == null ? "" : worksheet.Cells[row, 3].Text.ToString();
                                string reply = worksheet.Cells[row, 4].Text == null ? "" : worksheet.Cells[row, 4].Text.ToString();

                                if ((desc != "") && (!desc.Contains("Technical Condition")) && (!desc.Contains("ACC condition")))
                                {
                                    var comCond = _mdbcontext.TblTechConds.Where(x => x.TcDescription == desc && x.TcPackId == packageId).FirstOrDefault();
                                    if (comCond != null)
                                    {
                                        int condId = comCond.TcSeq == null ? 0 : comCond.TcSeq;

                                        if (condId > 0)
                                        {
                                            var comCondExist = _dbcontext.TblSuppTechCondReplies.Where(x => x.TcTechConId == condId && x.TcRevisionId == PackageSupliersRevisionID).FirstOrDefault();
                                            bool comcondIdExist = (comCondExist != null);

                                            if (!comcondIdExist)
                                            {
                                                var SuppCom = new TblSuppTechCondReply()
                                                {
                                                    TcTechConId = condId,
                                                    TcRevisionId = PackageSupliersRevisionID,
                                                    TcSuppReply = reply,
                                                    TcAccCond = AccCond
                                                };
                                                LstSuppComCondReply.Add(SuppCom);
                                            }
                                            else
                                            {
                                                comCondExist.TcSuppReply = reply;
                                                comCondExist.TcAccCond = AccCond;
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

        public bool AddTechConditions(TechConditions item)
        {
                int condSeq;
                var techCond = _mdbcontext.TblTechConds.Where(x => x.TcDescription == item.TcDescription).FirstOrDefault();

                if (techCond == null)
                {
                    var result = new TblTechCond { TcDescription = item.TcDescription, TcPackId = item.TcPackId, TcSelected = 0 };
                    _mdbcontext.Add<TblTechCond>(result);
                    _mdbcontext.SaveChanges();
               
                    condSeq = result.TcSeq;
                }
                else
                    condSeq = techCond.TcSeq;

                 foreach (var group in item.techConditionGroups)
                {
                    var result1 = new TblTechCondGroup { GroupId = group.groupId, TechCondId = condSeq };
                    _dbcontext.Add<TblTechCondGroup>(result1);
                    _dbcontext.SaveChanges();
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

                if (cond.techConditionGroups.Count > 0)
                {
                    //remove existing group 
                    var groupList = _dbcontext.TblTechCondGroups.Where(x => x.TechCondId == cond.TcSeq).ToList();
                    if (groupList != null)
                    {
                        _dbcontext.TblTechCondGroups.RemoveRange(groupList);
                        _dbcontext.SaveChanges();
                    }

                    //add new groups
                    foreach(var grp in cond.techConditionGroups)
                    {
                        var techCondGroup = new TblTechCondGroup();
                        techCondGroup.TechCondId = cond.TcSeq;
                        techCondGroup.GroupId = grp.groupId;
                        _dbcontext.Add<TblTechCondGroup>(techCondGroup);
                        _dbcontext.SaveChanges();
                    }
                }
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
                var groupList = _dbcontext.TblTechCondGroups.Where(x => x.TechCondId == id).ToList();
                if (groupList != null)
                {
                    _dbcontext.TblTechCondGroups.RemoveRange(groupList);
                    _dbcontext.SaveChanges();
                }

                _mdbcontext.TblTechConds.Remove(result);
                _mdbcontext.SaveChanges();

                return true;
            }
            else
                return false;
        }

    }
}

