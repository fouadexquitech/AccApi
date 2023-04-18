﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblMasterProjects")]
    public partial class TblMasterProject
    {
        [Key]
        [Column("msSeq")]
        public int MsSeq { get; set; }
        [Required]
        [Column("msDesc")]
        [StringLength(25)]
        public string MsDesc { get; set; }
        [Column("msConnection")]
        [StringLength(30)]
        public string MsConnection { get; set; }
        [Required]
        [Column("msServer")]
        [StringLength(15)]
        public string MsServer { get; set; }
        [Column("msActive")]
        public int MsActive { get; set; }
        [Column("msSelect")]
        public int MsSelect { get; set; }
        [Column("msDescSAP")]
        [StringLength(50)]
        public string MsDescSap { get; set; }
        [Column("msSSRS_Server")]
        [StringLength(50)]
        public string MsSsrsServer { get; set; }
        [Column("msPBI_Report")]
        [StringLength(50)]
        public string MsPbiReport { get; set; }
        public int? NewProject { get; set; }
        [Column("msReportsPortal")]
        [StringLength(50)]
        public string MsReportsPortal { get; set; }
        [Column("msIsEMS")]
        public bool? MsIsEms { get; set; }
        [Column("msEMasPM")]
        public bool? MsEmasPm { get; set; }
        [Column("msPlannedStartDate", TypeName = "date")]
        public DateTime? MsPlannedStartDate { get; set; }
        [Column("msPlannedEndDate", TypeName = "date")]
        public DateTime? MsPlannedEndDate { get; set; }
        [Column("msPlannedDuration")]
        public int? MsPlannedDuration { get; set; }
        [Column("msActualStartDate", TypeName = "date")]
        public DateTime? MsActualStartDate { get; set; }
        [Column("msActualEndDate", TypeName = "date")]
        public DateTime? MsActualEndDate { get; set; }
        [Column("msActualDuration")]
        public int? MsActualDuration { get; set; }
        [Column("msEngEndDate", TypeName = "date")]
        public DateTime? MsEngEndDate { get; set; }
        [Column("msIsFreeze")]
        public bool? MsIsFreeze { get; set; }
        [Column("msFreezeDate", TypeName = "date")]
        public DateTime? MsFreezeDate { get; set; }
        [Column("msUnFreezeDate", TypeName = "date")]
        public DateTime? MsUnFreezeDate { get; set; }
    }
}
