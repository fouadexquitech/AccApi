using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.Interfaces
{
    public interface IAuditRepository
    {
        bool SetAuditLog(string tablename, string userid, DateTime dateTime, string action, string primarykeyvalue);
        bool SetLoginLog(string userid, DateTime dateTime, string ip, string pcName);
    }
}
