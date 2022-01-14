using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class User
    {
        public string UsrId { get; set; }
        public string UsrDesc { get; set; }
        public string UsrPwd { get; set; }
        public bool? UsrAdmin { get; set; }
        public string UsrEmail { get; set; }
    }
}
