using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblproject")]
    public partial class Tblproject
    {
        public Tblproject()
        {
            TblProjectWeeks = new HashSet<TblProjectWeek>();
        }

        [Key]
        public int Seq { get; set; }
        [Column("prjName")]
        [StringLength(50)]
        public string PrjName { get; set; }
        [Column("prjAbv")]
        [StringLength(10)]
        public string PrjAbv { get; set; }
        [Column("prjCode")]
        [StringLength(9)]
        public string PrjCode { get; set; }
        [Column("prjPhase")]
        public int? PrjPhase { get; set; }
        [Column("prjDefault")]
        public bool? PrjDefault { get; set; }
        [Column("prjSel")]
        public bool? PrjSel { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column("prjSelRpt")]
        public bool? PrjSelRpt { get; set; }
        [Column("prjPath")]
        [StringLength(255)]
        public string PrjPath { get; set; }
        [Column("prjFile")]
        [StringLength(15)]
        public string PrjFile { get; set; }
        [Column("prjLogoPath")]
        [StringLength(50)]
        public string PrjLogoPath { get; set; }
        public byte? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("prjTitle")]
        [StringLength(255)]
        public string PrjTitle { get; set; }
        [Column("prjArea")]
        public int? PrjArea { get; set; }
        [Column("prTimeShedNightFrom")]
        [StringLength(20)]
        public string PrTimeShedNightFrom { get; set; }
        [Column("sdDataCollectorType")]
        public short? SdDataCollectorType { get; set; }
        [Column("prjDataCollectorSaveFile")]
        [StringLength(50)]
        public string PrjDataCollectorSaveFile { get; set; }
        [Column("prjDataCollectorPort")]
        public int? PrjDataCollectorPort { get; set; }
        [StringLength(25)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(25)]
        public string LastUserUpdate { get; set; }
        [Column("prjHasBldgs")]
        public bool? PrjHasBldgs { get; set; }
        [Column("prjHasDesig")]
        public bool? PrjHasDesig { get; set; }
        [Column("prjHasLocation")]
        public bool? PrjHasLocation { get; set; }
        [Column("prjNewTsVers")]
        public byte? PrjNewTsVers { get; set; }
        [Column("prjSAPCode")]
        [StringLength(5)]
        public string PrjSapcode { get; set; }
        [Column("prjProjectType")]
        public byte? PrjProjectType { get; set; }
        [Column("prjCostDatabase")]
        [StringLength(50)]
        public string PrjCostDatabase { get; set; }
        [Column("prjCountry")]
        [StringLength(50)]
        public string PrjCountry { get; set; }
        [Column("prjAllowOT")]
        public bool? PrjAllowOt { get; set; }
        [Column("prjAllowOT_WE")]
        public bool? PrjAllowOtWe { get; set; }
        [Column("prjAllowOT_Hol")]
        public bool? PrjAllowOtHol { get; set; }
        [Column("prjAllowContra")]
        public bool? PrjAllowContra { get; set; }

        [InverseProperty(nameof(TblProjectWeek.PwkProjectNavigation))]
        public virtual ICollection<TblProjectWeek> TblProjectWeeks { get; set; }
    }
}
