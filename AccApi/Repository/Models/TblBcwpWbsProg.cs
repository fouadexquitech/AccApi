using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblBcwpWbsProg")]
    public partial class TblBcwpWbsProg
    {
        [Key]
        [Column("bwpProject")]
        [StringLength(10)]
        public string BwpProject { get; set; }
        [Key]
        [Column("bwpWeek")]
        public int BwpWeek { get; set; }
        [Key]
        [Column("bwpWBS")]
        [StringLength(20)]
        public string BwpWbs { get; set; }
        [Key]
        [Column("bwpLevel")]
        [StringLength(5)]
        public string BwpLevel { get; set; }
        [Key]
        [Column("bwpUnit")]
        [StringLength(5)]
        public string BwpUnit { get; set; }
        [Key]
        [Column("BOQ")]
        [StringLength(25)]
        public string Boq { get; set; }
        [Column("bwpWBSDesc")]
        [StringLength(250)]
        public string BwpWbsdesc { get; set; }
        [Column("bwpType")]
        [StringLength(5)]
        public string BwpType { get; set; }
        [Column("bwpCnt")]
        public double? BwpCnt { get; set; }
        [Column("bwpQty")]
        public double? BwpQty { get; set; }
        [Column("bwpUnitPrice")]
        public double? BwpUnitPrice { get; set; }
        [Column("bwpExecQty")]
        public double? BwpExecQty { get; set; }
        [Column("bwpTotAmnt")]
        public double? BwpTotAmnt { get; set; }
        [Column("bwpPrelim")]
        public bool? BwpPrelim { get; set; }
        [Column("bwpSubcQty")]
        public double? BwpSubcQty { get; set; }
        [Column("bwpSubcExecQty")]
        public double? BwpSubcExecQty { get; set; }
        [Column("bwpMaterialExecQty")]
        public double? BwpMaterialExecQty { get; set; }
        [Column("bwpMatQtySource")]
        public byte? BwpMatQtySource { get; set; }
    }
}
