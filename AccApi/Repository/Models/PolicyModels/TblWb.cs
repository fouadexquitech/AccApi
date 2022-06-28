using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblWBS")]
    public partial class TblWb
    {
        [Key]
        [Column("ProjID")]
        public int ProjId { get; set; }
        [Key]
        [Column("wbsCode")]
        [StringLength(30)]
        public string WbsCode { get; set; }
        [Key]
        [Column("wbsProject")]
        [StringLength(9)]
        public string WbsProject { get; set; }
        [Key]
        public short WbsLevel { get; set; }
        [Column("wbs")]
        [StringLength(50)]
        public string Wbs { get; set; }
        [StringLength(5)]
        public string Div { get; set; }
        [StringLength(5)]
        public string SubDiv { get; set; }
        [StringLength(5)]
        public string Trade { get; set; }
        [StringLength(5)]
        public string SubTrade { get; set; }
        [Column("CCType")]
        public byte? Cctype { get; set; }
        [Column("wbsRow")]
        public int? WbsRow { get; set; }
        [Column("wbsDesc")]
        [StringLength(255)]
        public string WbsDesc { get; set; }
        [Column("wbsType")]
        [StringLength(1)]
        public string WbsType { get; set; }
        [Column("wbsTrade")]
        public int? WbsTrade { get; set; }
        [Column("wbsHidden")]
        public byte? WbsHidden { get; set; }
        [StringLength(10)]
        public string Unit { get; set; }
        public byte? Used { get; set; }
        [Column("WBSUsage")]
        public byte? Wbsusage { get; set; }
        [Column("WBSMap")]
        [StringLength(50)]
        public string Wbsmap { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(25)]
        public string InsertedBy { get; set; }
        [StringLength(25)]
        public string LastUserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [StringLength(50)]
        public string RelatedArea { get; set; }
        public byte? MapBuilding { get; set; }
    }
}
