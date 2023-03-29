using AccApi.Repository.Interfaces;
using AccApi.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.Managers
{
    public class AuditRepository : IAuditRepository
    {
        private readonly AccDbContext _accDbContext;
        private readonly GlobalLists _globalLists;


        public AuditRepository(AccDbContext accDbContext, GlobalLists globalLists)
        {
            _globalLists = globalLists;
            _accDbContext = new AccDbContext(_globalLists.GetAccDbconnectionString());
        }

        public bool SetAuditLog(string tablename, string userid, DateTime dateTime, string action, string primarykeyvalue)
        {
            var auditLog = new TblAuditLog()
            {
                Tablename = tablename,
                Userid = userid,
                Datetime = dateTime,
                Action = action,
                Primarykeyvalue = primarykeyvalue
            };
            _accDbContext.Add(auditLog);
            _accDbContext.SaveChanges();
            return true;
        }

        public bool SetLoginLog(string userid, DateTime dateTime, string ip, string pcName)
        {
            var loginLog = new TblLoginLog()
            {
                Userid = userid,
                Datetime = dateTime,
                Ip = ip,
                PcName = pcName
            };
            _accDbContext.Add(loginLog);
            _accDbContext.SaveChanges();
            return true;
        }
    }
}
