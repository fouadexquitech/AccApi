﻿using AccApi.Repository.Interfaces;
using AccApi.Repository.Models.MasterModels;
using AccApi.Repository.View_Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.Managers
{
    public class LogonRepository : IlogonRepository
    {
        private readonly MasterDbContext _mdbcontext;
        private readonly PolicyDbContext _pdbcontext;
        private readonly AccDbContext _dbcontext;

        public IConfiguration Configuration { get; }

        public LogonRepository (MasterDbContext mdbcontext, PolicyDbContext pdbcontext, AccDbContext dbcontext,IConfiguration configuration)
        {
            _mdbcontext = mdbcontext;
            _pdbcontext = pdbcontext;
            _dbcontext = dbcontext;
            Configuration = configuration;
        }
       
        public List<ProjectCountries> GetProjectCountries()
        {
            var result = from b in _mdbcontext.TblDataBases
                         where b.DbActive == 1
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
                        };
            return result.ToList();
        }

        public List<Project> GetProjects(int dbSeq)
        {
            var conn = _mdbcontext.TblDataBases.Where(x => x.DbSeq == dbSeq).FirstOrDefault();
            string ConName = conn.DbConnection;
            var db = _pdbcontext.CreateConnectionFromOut(ConName);

            var result = from b in db.Tblprojects
                         where b.PrjCostDatabase != null
                         select new Project
                         {
                             Seq = b.Seq,
                             PrjCostDatabase = b.PrjCostDatabase
                         };
            return result.ToList();
        }

        public ProjectCurrency GetProjectCurrency()
        {
            var result = from a in _dbcontext.TblParameters
                         join b in _dbcontext.TblCurrencies
                         on a.EstimatedCur equals b.CurId
                         select new ProjectCurrency
                         {
                             curId = (int)a.EstimatedCur,
                             curCode = b.CudCode
                         };

            return result.FirstOrDefault();
        }

        //fouad
        public User GetLogin(string user, string pass, int projSeq)
        {
            User usr = new User();
            usr = checkCredentials(user, pass);

            if (!checkAccessProject(user, projSeq))
                usr = null;
            else if (!connectToProject(projSeq))
                usr = null; 
            
            return usr;
        }

        private Boolean connectToProject(int projSeq)
        {
            var result = _pdbcontext.Tblprojects.Where(x => x.Seq == projSeq).FirstOrDefault();
            string costDb = result.PrjCostDatabase;

            if ((costDb != "") && (costDb != null))
            {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                costDb = costDb + "_CostData";
                var connection = new SqlConnectionStringBuilder(connectionString);
                connection.InitialCatalog = costDb;
              
                string conName = connection.ConnectionString.ToString();
                var db = _dbcontext.CreateConnectionFromOut(conName);

                return true;
            }
            else
                return false;
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
            var result = from u in _pdbcontext.TblUsers                
                        where u.UsrId == username &&
                        u.UsrPwd == password
                        select new User
                        {UsrId=u.UsrId,
                            UsrDesc=u.UsrDesc,
                            UsrPwd=u.UsrPwd,
                            UsrAdmin=u.UsrAdmin,
                            UsrEmail=u.UsrEmail
                        };

            User user = new User();
            user = result.FirstOrDefault();
   
            return user;
        }

        private bool checkAccessProject(string username, int projSeq)
        {
            var query = from p in _pdbcontext.Tblprojects
                        join u in _pdbcontext.TblUsersProjects 
                        on p.Seq equals u.UpProject
                        where u.UpUserId == username &&
                         p.Seq== projSeq
                        select u;

            return query.FirstOrDefault() != null;
        }

        public EmailTemplate GetSuppliersEmailTemplate(string Lang)
        {
            var result = from b in _mdbcontext.TblEmailTemplates
                         where b.EtLang == Lang
                         select new EmailTemplate
                         {
                             EtSeq=b.EtSeq,
                             EtContent=b.EtContent
                         };

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
    }
}
