using AccApi.Repository.Interfaces;
using AccApi.Repository.View_Models;
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

        public LogonRepository (MasterDbContext mdbcontext, PolicyDbContext pdbcontext)
        {
            _mdbcontext = mdbcontext;
            _pdbcontext = pdbcontext;
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
            var result = from b in _pdbcontext.Tblprojects
                         where b.PrjCostDatabase != null
                         select new Project
                         {
                             Seq = b.Seq,
                             PrjCostDatabase = b.PrjCostDatabase
                         };
            return result.ToList();
        }
        public User GetLogin(string user, string pass, int projSeq)
        {
            User usr = new User();
            usr = checkCredentials(user, pass);

            if (!checkAccessProject(user, projSeq))
                usr = null;
            
            return usr;
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
    }
}
