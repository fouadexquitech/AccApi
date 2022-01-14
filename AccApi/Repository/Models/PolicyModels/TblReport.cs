﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblReports")]
    public partial class TblReport
    {
        [Key]
        [Column("rptSeq")]
        public byte RptSeq { get; set; }
        [Column("rptDesc")]
        [StringLength(255)]
        public string RptDesc { get; set; }
        [Column("rptObject")]
        [StringLength(30)]
        public string RptObject { get; set; }
        [Column("rptHasColumns")]
        public byte? RptHasColumns { get; set; }
        public byte? PermissionExcept { get; set; }
        [Column("rptRunSSRS")]
        public byte? RptRunSsrs { get; set; }
        [Column("rptIsDynamicCol")]
        public byte? RptIsDynamicCol { get; set; }
        [Column("rptFixSpace")]
        public double? RptFixSpace { get; set; }
    }
}
