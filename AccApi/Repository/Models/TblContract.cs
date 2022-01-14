using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblContract")]
    public partial class TblContract
    {
        [Column("cntSeq")]
        public int CntSeq { get; set; }
        [Key]
        [Column("cntVendor")]
        public int CntVendor { get; set; }
        [Key]
        [Column("cntPackage")]
        public int CntPackage { get; set; }
        [Key]
        [Column("cntProject")]
        [StringLength(50)]
        public string CntProject { get; set; }
        [Column("cntDate", TypeName = "datetime")]
        public DateTime? CntDate { get; set; }
        [Column("cntContAmount", TypeName = "money")]
        public decimal? CntContAmount { get; set; }
        [Column("cntFirstPayment")]
        public double? CntFirstPayment { get; set; }
        [Column("cntRef")]
        [StringLength(50)]
        public string CntRef { get; set; }
    }
}
