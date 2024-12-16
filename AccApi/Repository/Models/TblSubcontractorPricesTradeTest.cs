using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblSubcontractorPricesTradeTest")]
    public partial class TblSubcontractorPricesTradeTest
    {
        [Key]
        [Column("sptSeq")]
        public int SptSeq { get; set; }
        [StringLength(10)]
        public string Project { get; set; }
        [StringLength(12)]
        public string IdSub { get; set; }
        [StringLength(10)]
        public string Trade { get; set; }
        [Column("boq")]
        [StringLength(50)]
        public string Boq { get; set; }
        public double? Price { get; set; }
        [StringLength(255)]
        public string Note { get; set; }
        [StringLength(50)]
        public string SubDivision { get; set; }
        [Column("des")]
        [StringLength(50)]
        public string Des { get; set; }
        [Column("unitRate")]
        public double? UnitRate { get; set; }
        [Column("insertdate", TypeName = "datetime")]
        public DateTime? Insertdate { get; set; }
        [Column("insertBy")]
        [StringLength(50)]
        public string InsertBy { get; set; }
        [Column("updatedBy")]
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        [Column("updatedDate", TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
    }
}
