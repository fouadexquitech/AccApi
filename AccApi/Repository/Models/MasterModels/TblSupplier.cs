using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblSuppliers")]
    public partial class TblSupplier
    {
        [Key]
        [Column("supCode")]
        public int SupCode { get; set; }
        [Column("supProj")]
        public int? SupProj { get; set; }
        [Column("supName")]
        public string SupName { get; set; }
        [Column("supAddress")]
        public string SupAddress { get; set; }
        [Column("supPhone")]
        public string SupPhone { get; set; }
        [Column("supRef")]
        [StringLength(30)]
        public string SupRef { get; set; }
        [Column("supRelDate", TypeName = "datetime")]
        public DateTime? SupRelDate { get; set; }
        [Column("supOpenBal")]
        public float? SupOpenBal { get; set; }
        [Column("supPrevBal")]
        public float? SupPrevBal { get; set; }
        [Column("supCredit")]
        public float? SupCredit { get; set; }
        [Column("supDebit")]
        public float? SupDebit { get; set; }
        [Column("supCurrent")]
        public float? SupCurrent { get; set; }
        [Column("supCurr")]
        public short? SupCurr { get; set; }
        [Column("supRemark", TypeName = "ntext")]
        public string SupRemark { get; set; }
        [Column("supStop")]
        public bool? SupStop { get; set; }
        [Column("supVATID")]
        [StringLength(15)]
        public string SupVatid { get; set; }
        [Column("supFinID")]
        [StringLength(15)]
        public string SupFinId { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column("supSel")]
        public short? SupSel { get; set; }
        [Column("supPSC")]
        [StringLength(10)]
        public string SupPsc { get; set; }
        public short? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("supMobile")]
        [StringLength(16)]
        public string SupMobile { get; set; }
        [Column("supFax")]
        [StringLength(16)]
        public string SupFax { get; set; }
        [Column("supEmail")]
        public string SupEmail { get; set; }
        [Column("supAccPreson")]
        public string SupAccPreson { get; set; }
        [Column("supPayTerm")]
        public short? SupPayTerm { get; set; }
    }
}
