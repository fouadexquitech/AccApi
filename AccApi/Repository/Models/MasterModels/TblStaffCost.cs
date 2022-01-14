using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblStaffCost")]
    public partial class TblStaffCost
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Column("scpDate", TypeName = "datetime")]
        public DateTime? ScpDate { get; set; }
        [Column("scpYear")]
        public int? ScpYear { get; set; }
        [Column("scpMonth")]
        public int? ScpMonth { get; set; }
        [Column("scpSheetType")]
        [StringLength(50)]
        public string ScpSheetType { get; set; }
        [Column("scpName")]
        [StringLength(200)]
        public string ScpName { get; set; }
        [Column("scpJob")]
        [StringLength(200)]
        public string ScpJob { get; set; }
        [Column("scpProjOffice")]
        [StringLength(50)]
        public string ScpProjOffice { get; set; }
        [Column("scpWBSE")]
        [StringLength(200)]
        public string ScpWbse { get; set; }
        [Column("scpElementDesc")]
        [StringLength(50)]
        public string ScpElementDesc { get; set; }
        [Column("scpCompany")]
        [StringLength(50)]
        public string ScpCompany { get; set; }
        [Column("scpPercent")]
        public float? ScpPercent { get; set; }
        [Column("scpCoAccount")]
        [StringLength(50)]
        public string ScpCoAccount { get; set; }
        [Column("scpPersonNormal")]
        public int? ScpPersonNormal { get; set; }
        [Column("scpCost")]
        public int? ScpCost { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column("scpProjMonth")]
        public int? ScpProjMonth { get; set; }
        [Column("scpProject")]
        public int? ScpProject { get; set; }
        [Column("scpUnit")]
        public int? ScpUnit { get; set; }
        [Column("scpStaffSalary")]
        public int? ScpStaffSalary { get; set; }
        [Column("scpType")]
        [StringLength(10)]
        public string ScpType { get; set; }
    }
}
