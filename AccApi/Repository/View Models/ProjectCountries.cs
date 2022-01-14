using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class ProjectCountries
    {
        public int dbSeq { get; set; }
        public byte? dbActive { get; set; }
        public string dbLocation { get; set; }
        public string dbServer { get; set; }
        public string dbUserId { get; set; }
        public string dbPass { get; set; }
        public string dbName { get; set; }
        public string dbDescription { get; set; }
    }
}
