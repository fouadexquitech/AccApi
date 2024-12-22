using AccApi.Repository.Interfaces;
using AccApi.Repository.Models.MasterModels;
using AccApi.Repository.Models.PolicyModels;
using AccApi.Repository.View_Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace AccApi.Repository.Managers
{
    public class LogonRepository : IlogonRepository 
    {
        private readonly MasterDbContext _mdbcontext;
        private readonly PolicyDbContext _pdbcontext;
        private readonly AccDbContext _dbcontext;
        private GlobalLists _globalLists;
        public IConfiguration Configuration { get; }

        private static PolicyDbContext pdb;
        private static PolicyDbContext _tsdbcontext;
        //private static AccDbContext costDB;

        public LogonRepository (MasterDbContext mdbcontext, PolicyDbContext pdbcontext, AccDbContext dbcontext,IConfiguration configuration, GlobalLists globalLists)
        {
            _mdbcontext = mdbcontext;
            _pdbcontext = pdbcontext;
             Configuration = configuration;
            _globalLists = globalLists;
            _dbcontext = dbcontext;
        }

        public List<ProjectCountries> GetProjectCountries()
        {
            var result = (from b in _mdbcontext.TblDataBases
                         where b.DbActive == 1 && b.DbLocation !="" orderby b.DbLocation
                          select new ProjectCountries
                         {
                            dbSeq = b.DbSeq,
                            dbActive = b.DbActive,
                            dbLocation = b.DbLocation,
                            dbServer = b.DbServer,
                            dbUserId = b.DbUserId,
                            dbPass = b.DbPass,
                            dbName=b.DbName,
                            dbDescription=b.DbDescription
                        }).ToList();
            return result;
        }

        public List<Project> GetProjects(int dbSeq)
        {
            var conn = _mdbcontext.TblDataBases.Where(x => x.DbSeq == dbSeq).FirstOrDefault();
            string ConName = conn.DbConnection;
            pdb = _pdbcontext.CreateConnectionFromOut(ConName);


            string connectionString = conn.DbConnection; ;
            var TSconnection = new SqlConnectionStringBuilder(connectionString);
            string TsConName = TSconnection.ConnectionString.ToString();

            _globalLists.SetTimeSheetDbConnectionString(TsConName);

            _tsdbcontext = new PolicyDbContext(_globalLists.GetTimeSheetDbconnectionString());


            var result = (from b in _tsdbcontext.Tblprojects
                         where ((b.PrjCostDatabase != "") && (b.PrjCostDatabase != null)) && (b.PrjVendan=="1")
                         select new Project
                         {
                             Seq = b.Seq,
                             PrjCostDatabase = b.PrjName,
                             projectName=b.PrjName
                         });
            return result.OrderBy(x=> x.projectName).ToList();
        }

        public ProjectCurrency GetProjectCurrency(int projSeq)
        {
            var result = _tsdbcontext.Tblprojects.Where(x => x.Seq == projSeq).FirstOrDefault();
            string costDb = (result.PrjCostDatabase == null) ? "" : result.PrjCostDatabase;

            if ((costDb != "") && (costDb != null))
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                costDb = costDb + "_CostData";
                var connection = new SqlConnectionStringBuilder(connectionString);
                connection.InitialCatalog = costDb;

                string conName = connection.ConnectionString.ToString();
                //costDB = _dbcontext.CreateConnectionFromOut(conName);

                _globalLists.SetAccDbConnectionString(conName);

                AccDbContext _context = new AccDbContext(_globalLists.GetAccDbconnectionString());

                var curList = (from b in _mdbcontext.TblCurrencies
                               select b).ToList();

                int EstimCurrID =(int)_context.TblParameters.FirstOrDefault().EstimatedCur;

                var cur = curList.Where(x => x.CurId == EstimCurrID).FirstOrDefault();

                ProjectCurrency projCur = new ProjectCurrency();
                projCur.curId = cur.CurId;
                projCur.curCode = cur.CurCode;  
                    
                return projCur;
            }
            else
                return null;
        }

        //fouad
        public User GetLogin(string username, string pass, int projSeq)
        {
            
            //usr = checkCredentials(user, pass);

            var result = _tsdbcontext.TblUsers.Where(x => x.UsrId == username && x.UsrPwd == pass).FirstOrDefault();

            User usr = new User();
            if (result != null) {
                usr.UsrId = result.UsrId;
                usr.UsrDesc = result.UsrDesc;
                usr.UsrPwd = result.UsrPwd;
                usr.UsrAdmin = result.UsrAdmin;
                usr.UsrEmail = result.UsrEmail;
                usr.UsrEmailSignature = result.EmailSignature;
            }
            else
            {
                usr = null;
                return usr;
            }

            bool isAdmin = (bool)(usr.UsrAdmin==null ? false : usr.UsrAdmin);
            if (!isAdmin)
            {
                var accAllProjects = _tsdbcontext.TblPermGrpUsrs.Where(x => x.PrmUser == username && x.PrmFuncId == "AccessAllProjects" && x.MinOfprmRead == 1).FirstOrDefault();
                if (accAllProjects == null)
                {
                    var query = _tsdbcontext.TblUsersProjects.Where(x => x.UpUserId == username && x.UpProject == projSeq).FirstOrDefault();
                    if (query == null)
                    {
                        usr = null;
                        return usr;
                    }
                }
                //if (!checkAccessProject(username, projSeq))
                //{
                //    usr = null;
                //    return usr;
                //}
            }

            string connString = connectToProject(projSeq);

            if (connString=="")
                usr = null; 
            
            if (usr != null)
            {
                var prj = _tsdbcontext.Tblprojects.Where(x => x.Seq == projSeq).FirstOrDefault();
                if(prj != null)
                {
                  usr.UsrLoggedProjectName = prj.PrjName;
                  usr.usrLoggedConnString = connString;
                  usr.usrLoggedCostDB = prj.PrjCostDatabase;
                } 
            }
            return usr;
        }

        private string  connectToProject(int projSeq)
        {
            var result = _tsdbcontext.Tblprojects.Where(x => x.Seq == projSeq).FirstOrDefault();
            string costDb = (result.PrjCostDatabase == null) ? "" : result.PrjCostDatabase ;

            if ((costDb != "") && (costDb != null))
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                costDb = costDb + "_CostData";
                var connection = new SqlConnectionStringBuilder(connectionString);
                connection.InitialCatalog = costDb;
              
                string conName = connection.ConnectionString.ToString();
                var db = _dbcontext.CreateConnectionFromOut(conName);

                _globalLists.SetAccDbConnectionString(conName);

                return conName;
            }
            else
                return "";
        }

        public bool ConnectToDB(string connString)
        {
            //var connection = new SqlConnectionStringBuilder(connString);

            //string conName = connection.ConnectionString.ToString();
            //var db = _dbcontext.CreateConnectionFromOut(conName);

            _globalLists.SetAccDbConnectionString(connString);
            return true;
        }

        private static void ChangeSqlDatabase(string connectionString)
        {
            // Assumes connectionString represents a valid connection string
            // to the AdventureWorks sample database.
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.ChangeDatabase("Northwind");
                Console.WriteLine("Database: {0}", connection.Database);
            }
        }

        private User checkCredentials(string username, string password)
        {
            var result = from u in _tsdbcontext.TblUsers                
                        where u.UsrId == username &&
                        u.UsrPwd == password
                        select new User
                        {UsrId=u.UsrId,
                            UsrDesc=u.UsrDesc,
                            UsrPwd=u.UsrPwd,
                            UsrAdmin=u.UsrAdmin,
                            UsrEmail=u.UsrEmail,
                            UsrEmailSignature=u.EmailSignature
                        };
            User user = new User();
            user = result.FirstOrDefault();
   
            return user;
        }

        public User GetUser(string username)
        {
            var result = from u in _tsdbcontext.TblUsers
                         where u.UsrId == username
                         select new User
                         {
                             UsrId = (u.UsrId == null) ? "" : u.UsrId,
                             UsrDesc = (u.UsrDesc == null) ? "" : u.UsrDesc,
                             UsrPwd = (u.UsrPwd == null) ? "" : u.UsrPwd,
                             UsrAdmin = (u.UsrAdmin == null) ? false : u.UsrAdmin,
                             UsrEmail = (u.UsrEmail == null) ? "" : u.UsrEmail,
                             UsrEmailSignature = (u.EmailSignature == null) ? "" : u.EmailSignature
                         };

            return result.FirstOrDefault();
        }

        private bool checkAccessProject(string username, int projSeq)
        {
            var query = from p in _tsdbcontext.Tblprojects
                        join u in _tsdbcontext.TblUsersProjects 
                        on p.Seq equals u.UpProject
                        where u.UpUserId == username &&
                         p.Seq== projSeq
                        select u;

            return query.FirstOrDefault() != null;
        }

        public List<EmailTemplate> GetSuppliersEmailTemplate(string Lang)
        {
            var result = (from b in _mdbcontext.TblEmailTemplates
                         where (Lang == null || b.EtLang == Lang) 
                         select new EmailTemplate
                         {
                             EtSeq=b.EtSeq,
                             EtContent=b.EtContent,
                             EtLang=b.EtLang
                         }).ToList();
            //return result.FirstOrDefault();
            return result;
        }

        public EmailTemplate GetDefaultProjectEmailTemplate(string costDb)
        {
            var lang = _tsdbcontext.Tblprojects.Where(x => x.PrjCostDatabase == costDb).Select(p => p.PrjCostDbEmailTemplate).FirstOrDefault();
            var result = _mdbcontext.TblEmailTemplates.Where(x => x.EtLang == lang).Select(b => new EmailTemplate
            {
                EtSeq = b.EtSeq,
                EtContent = b.EtContent,
                EtLang = b.EtLang
            });

            return result.FirstOrDefault();
        }


        public bool SaveEmailTemplate(int id, string emailbody)
        {
            var result = _mdbcontext.TblEmailTemplates.Where(x => x.EtSeq == id).FirstOrDefault();
            result.EtContent = emailbody;

            if (result != null)
            {
                _mdbcontext.TblEmailTemplates.Update(result);
                _mdbcontext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public List<TopManagement> GetManagementEmail(string filter)
        {
            var result = (from b in _mdbcontext.TblManagementUsers
                         select new TopManagement
                         {
                             Id = b.Seq,
                             UserName=b.UserName,
                             Mail = b.Mail,
                             Occupation = b.Occupation
                         }).ToList();

            if (filter != null)
            {
                result = result.Where(x => string.Concat(x.UserName.ToUpper(), x.Mail.ToUpper(), x.Occupation.ToUpper()).Contains(filter.ToUpper())).ToList();
            }
            return result.ToList();
        }

        public bool AddManagementEmail(List<TopManagement> users)
        {
            foreach(var item in users)
            { 
               var result = new TblManagementUser {  Mail = item.Mail, UserName = item.UserName, Occupation = item.Occupation};
              _mdbcontext.Add<TblManagementUser>(result);
              _mdbcontext.SaveChanges();
            }
            
            return true;
        }

        public bool UpdateManagementEmail(TopManagement user)
        {
            var result = _mdbcontext.TblManagementUsers.Where(x => x.Seq == user.Id).FirstOrDefault();
            result.Mail = user.Mail;
            result.Occupation = user.Occupation;
            result.UserName = user.UserName;

            if (result != null)
            {
                _mdbcontext.TblManagementUsers.Update(result);
                _mdbcontext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool DeleteManagementEmail(int id)
        {
            var result = _mdbcontext.TblManagementUsers.Where(x => x.Seq == id).FirstOrDefault();

            if (result != null)
            {
                _mdbcontext.TblManagementUsers.Remove(result);
                _mdbcontext.SaveChanges();
                return true;
            }
            else
                return false;
        }

       public bool hasPermission(string user, string functionId)
        {  
            var result = _tsdbcontext.TblPermissions.Where(x => x.PrmGrpUsrId == user && x.PrmFuncId==functionId).FirstOrDefault();
            if (result != null)
            {
                return true;
            }
            else
                return false;
        }


    }
}
