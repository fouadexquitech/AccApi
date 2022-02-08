using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class EmailTemplate
    {
        public int EtSeq { get; set; }   
        public string EtContent { get; set; }
        public byte? EtLang { get; set; }
    }
}
