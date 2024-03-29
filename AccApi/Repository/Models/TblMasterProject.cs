﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
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
        [Column("msDCname")]
        [StringLength(100)]
        public string MsDcname { get; set; }
        [Column("msDCemail")]
        [StringLength(100)]
        public string MsDcemail { get; set; }
        public int? NewProject { get; set; }
    }
}
