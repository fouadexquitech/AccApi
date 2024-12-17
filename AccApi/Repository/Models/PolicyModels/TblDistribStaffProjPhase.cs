using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblDistribStaffProjPhase")]
    [Index(nameof(Psc), nameof(StartDate), nameof(ProjectPhase), Name = "IX_tblDistribStaffProjPhase", IsUnique = true)]
    public partial class TblDistribStaffProjPhase
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Required]
        [Column("PSC")]
        [StringLength(10)]
        public string Psc { get; set; }
        [Column("startDate", TypeName = "datetime")]
        public DateTime StartDate { get; set; }
        [Column("projectPhase")]
        public byte ProjectPhase { get; set; }
        [Column("isStaff")]
        [StringLength(10)]
        public string IsStaff { get; set; }
        [StringLength(500)]
        public string Name { get; set; }
        [Column("occupation")]
        [StringLength(50)]
        public string Occupation { get; set; }
        [Column("designation")]
        [StringLength(50)]
        public string Designation { get; set; }
        [Column("projId")]
        public int? ProjId { get; set; }
        [StringLength(50)]
        public string InsertedBy { get; set; }
        [Column("insertedDate", TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [Column("percentage")]
        public double? Percentage { get; set; }
    }
}
