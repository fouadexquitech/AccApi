using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{

    public class LoginResponse
    {
        public bool Success { get; set; }
        public User User { get; set; }
        public string Message { get; set; }
    }

    public class User
    {
        public string UsrId { get; set; }
        public string UsrDesc { get; set; }
        public string UsrPwd { get; set; }
        public bool? UsrAdmin { get; set; }
        public string UsrEmail { get; set; }
        public string UsrEmailSignature { get; set; }
        public string? UsrLoggedProjectName { get; set; }
        public string? usrLoggedConnString { get; set; }
        public string? usrLoggedCostDB { get; set; }
        public string? usrLoggedTSConnString { get; set; }        
    }
}
