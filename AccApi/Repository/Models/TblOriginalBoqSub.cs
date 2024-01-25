using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblOriginalBOQ_Sub")]
    public partial class TblOriginalBoqSub
    {
        [Key]
        [Column("subBoqItem")]
        [StringLength(25)]
        public string SubBoqItem { get; set; }
        [Key]
        [Column("subItem")]
        [StringLength(25)]
        public string SubItem { get; set; }
        [Column("subProject")]
        [StringLength(10)]
        public string SubProject { get; set; }
        [Column("subSection")]
        [StringLength(3)]
        public string SubSection { get; set; }
        [Column("subDescription")]
        public string SubDescription { get; set; }
        [Column("subUnit")]
        [StringLength(255)]
        public string SubUnit { get; set; }
        [Column("subQty")]
        public double? SubQty { get; set; }
        [Column("subBillQty")]
        public double? SubBillQty { get; set; }
        [Column("subSubmitted")]
        public double? SubSubmitted { get; set; }
        [Column("subRowNumber")]
        public int? SubRowNumber { get; set; }
        [Column("subUnitRate")]
        public double? SubUnitRate { get; set; }
        [StringLength(25)]
        public string Luser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column("insertedBy")]
        [StringLength(20)]
        public string InsertedBy { get; set; }
        [Column("insertedDate", TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [Column("subBackUpDate", TypeName = "datetime")]
        public DateTime? SubBackUpDate { get; set; }
    }
}
