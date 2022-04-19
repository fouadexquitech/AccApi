using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblBcwpWbsProgBOQ")]
    public partial class TblBcwpWbsProgBoq
    {
        [Column("bwpbProject")]
        [StringLength(10)]
        public string BwpbProject { get; set; }
        [Key]
        [Column("bwpbWeek")]
        public int BwpbWeek { get; set; }
        [Key]
        [Column("bwpbWBS")]
        [StringLength(20)]
        public string BwpbWbs { get; set; }
        [Key]
        [Column("bwpbLevel")]
        [StringLength(5)]
        public string BwpbLevel { get; set; }
        [Key]
        [Column("bwpbUnit")]
        [StringLength(5)]
        public string BwpbUnit { get; set; }
        [Column("bwpbWBSDesc")]
        [StringLength(250)]
        public string BwpbWbsdesc { get; set; }
        [Column("bwpbType")]
        [StringLength(5)]
        public string BwpbType { get; set; }
        [Column("bwpbCnt")]
        public double? BwpbCnt { get; set; }
        [Column("bwpbQty")]
        public double? BwpbQty { get; set; }
        [Column("bwpbUnitPrice")]
        public double? BwpbUnitPrice { get; set; }
        [Column("bwpbExecQty")]
        public double? BwpbExecQty { get; set; }
        [Column("bwpbTotAmnt")]
        public double? BwpbTotAmnt { get; set; }
        [Column("bwpbPrelim")]
        public bool? BwpbPrelim { get; set; }
        [Key]
        [Column("BOQ")]
        [StringLength(25)]
        public string Boq { get; set; }
        [Column("bwpbSubcQty")]
        public double? BwpbSubcQty { get; set; }
        [Column("bwpbSubcExecQty")]
        public double? BwpbSubcExecQty { get; set; }
        [Column("bwpbMaterialExecQty")]
        public double? BwpbMaterialExecQty { get; set; }
        [Column("bwpbMatQtySource")]
        public byte? BwpbMatQtySource { get; set; }
        [Column("BOQDesc")]
        public string Boqdesc { get; set; }
    }
}
