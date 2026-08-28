using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class SupplierInput
    {
        public int supID { get; set; }
    }

    public class Condition
    {
        public int id { get; set; }
        public string description { get; set; }
        public string? ACCCondValue { get; set; }
    }

    //public class SupplierInputList
    //{
    //    public SupplierInput supplierInput { get; set; }
    //    public string EmailTemplate { get; set; }
    //    public string FilePath { get; set; }
    //    public List<Condition> comercialCondList { get; set; }
    //    public List<Condition> technicalCondList { get; set; }

    //    public List<string> mailCC { get; set; }
    //    public List<string> mailAttachments { get; set; }
    //}

    public class SupplierInputList
    {
        public SupplierInput supplierInput
        {
            get;
            set;
        }

        public string supplierName
        {
            get;
            set;
        }

        /*
         * Editable To addresses for this supplier only.
         */
        public List<string> mailTo
        {
            get;
            set;
        }

        public List<Condition> comercialCondList
        {
            get;
            set;
        }

        public List<Condition> technicalCondList
        {
            get;
            set;
        }

        public string FilePath
        {
            get;
            set;
        }

        public string EmailTemplate
        {
            get;
            set;
        }

        public List<string> mailCC
        {
            get;
            set;
        }

        public List<string> mailAttachments
        {
            get;
            set;
        }

        public SupplierInputList()
        {
            mailTo =
                new List<string>();

            mailCC =
                new List<string>();

            mailAttachments =
                new List<string>();

            comercialCondList =
                new List<Condition>();

            technicalCondList =
                new List<Condition>();
        }
    }

}
