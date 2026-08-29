using System;
using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class TopManagementTemplateModel
    {
        public int PackageId { get; set; }
        public string Template { get; set; }
        public List<TopManagement> TopManagements { get; set; }
        public List<string> ListCC { get; set; }
        public List<string> ListAttach { get; set; }
        public string UserName { get; set; }
    }

    //public class AssignPackageTemplateModel
    //{
    //    public List<SupplierInputList> supInputList { get; set; }
    //    public int packId { get; set; }
    //    public byte ByBoq { get; set; }
    //    public string UserName { get; set; }
    //    public List<string> ListTo { get; set; }
    //    public List<string> ListCC { get; set; }
    //    public List<string> ListAttach { get; set; }
    //    public DateTime RevisionExpiryDate { get; set; }

    //}

    //public class AssignPackageTemplateModel
    //{
    //    public byte ByBoq { get; set; }

    //    public List<string> ListAttach { get; set; }

    //    /*
    //     * One shared CC list only.
    //     */
    //    public List<string> ListCC
    //    {
    //        get; set;
    //    }

    //    public int PackId { get; set; }

    //    public List<SupplierInputList> SupInputList { get; set; }

    //    public string UserName { get; set; }

    //    public DateTime RevisionExpiryDate { get; set; }

    //    public AssignPackageTemplateModel()
    //    {
    //        ListAttach = new List<string>();

    //        ListCC = new List<string>();

    //        SupInputList = new List<SupplierInputList>();
    //    }
    //}

    public class AssignPackageTemplateModel
    {
        public byte ByBoq { get; set; }
        public List<string> ListAttach { get; set; } = new List<string>();
        public List<string> ListCC { get; set; } = new List<string>();
        public int PackId { get; set; }
        public List<SupplierInputList> SupInputList { get; set; } = new List<SupplierInputList>();
        public string UserName { get; set; }
        public DateTime RevisionExpiryDate { get; set; }

        // true: attach RFQ to supplier email.
        // false: allowed only for suppliers with portal accounts.
        public bool IncludeRfqAttachment { get; set; } = true;
    }

}
