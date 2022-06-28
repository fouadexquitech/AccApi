using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tempAYAPPReports")]
    public partial class TempAyappreport1
    {
        [StringLength(2)]
        public string Div { get; set; }
        [StringLength(5)]
        public string DivSubDiv { get; set; }
        [Column("disForman")]
        [StringLength(12)]
        public string DisForman { get; set; }
        [StringLength(50)]
        public string Area { get; set; }
        [StringLength(50)]
        public string Zone { get; set; }
        [Column("disProj")]
        [StringLength(15)]
        public string DisProj { get; set; }
        [Column("TS_Designation")]
        [StringLength(150)]
        public string TsDesignation { get; set; }
        [Column("AYAPP_Designation")]
        [StringLength(150)]
        public string AyappDesignation { get; set; }
        [Column("disLab")]
        [StringLength(14)]
        public string DisLab { get; set; }
        [StringLength(50)]
        public string Descrip { get; set; }
        [StringLength(150)]
        public string Sponsor { get; set; }
        [Column("cnt")]
        public int? Cnt { get; set; }
        [Column("WBS")]
        [StringLength(30)]
        public string Wbs { get; set; }
        [Column("projectDef")]
        [StringLength(30)]
        public string ProjectDef { get; set; }
        [StringLength(100)]
        public string Forman { get; set; }
        public double? TotCost { get; set; }
        [StringLength(5)]
        public string DayNight { get; set; }
        public int? Location { get; set; }
        public int? Labjob { get; set; }
        [Column("disDate", TypeName = "date")]
        public DateTime? DisDate { get; set; }
        [StringLength(100)]
        public string SiteEng { get; set; }
        [StringLength(100)]
        public string SectionEng { get; set; }
        public double? Hrs { get; set; }
    }
}
