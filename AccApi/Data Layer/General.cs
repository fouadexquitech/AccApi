using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Data_Layer
{
    public class General
    {
        public int Seq { get; set; }
        public string mail { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
    }

    public class mailCCAttach
    {
        public List<string> mailCC { get; set; }
        public List<string> mailAttachments { get; set; }
    }

}
