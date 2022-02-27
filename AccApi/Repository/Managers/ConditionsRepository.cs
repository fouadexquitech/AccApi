using AccApi.Data_Layer;
using AccApi.Repository.Interfaces;
using AccApi.Repository.Models.MasterModels;
using AccApi.Repository.View_Models;
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

        public List<TblComCond> GetComConditions()
        {
            var result = _mdbcontext.TblComConds.ToList();
            return result;
        }

        public List<TblTechCond> GetTechConditions(int packId)
        {
            var result = _mdbcontext.TblTechConds.Where(x => x.TcPackId == packId).ToList();
            return result;
        }

        public bool SendTechnicalConditions(int packId)
        {
            var package = _dbcontext.PackagesNetworks.Where(x => x.IdPkge == packId).FirstOrDefault();
            string PackageName = package.PkgeName;

            var p= _dbcontext.TblParameters.FirstOrDefault();
            var proj = _pdbcontext.Tblprojects.Where(x => x.Seq == p.TsProjId).FirstOrDefault();
            string ProjectName = proj.PrjName;


            var result = _mdbcontext.TblTechConds.Where(x => x.TcPackId == packId).ToList();

            var stream = new MemoryStream();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var xlPackage = new ExcelPackage(stream))
            {
                var worksheet = xlPackage.Workbook.Worksheets.Add("BOQ");
                worksheet.Columns.AutoFit();
                worksheet.Protection.IsProtected = true;

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
                string excelName = $"Technical Conditions-{PackageName}-{ProjectName}-{DateTime.Now.ToString("ddMMyyyy")}.xlsx";
                xlPackage.SaveAs(excelName);


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
                        mylistTo.Add(cc);

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
                        AttachmentList.Add(excelName);

                        Mail m = new Mail();

                        m.SendMail(mylistTo, mylistCC, Subject, MailBody, AttachmentList, false);
                    }
                }                   
                return true;
            }
        }

    }
}
