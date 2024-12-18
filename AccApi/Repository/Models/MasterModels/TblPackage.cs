﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblPackages")]
    public partial class TblPackage
    {
        [Key]
        public int PkgeId { get; set; }
        [StringLength(150)]
        public string PkgeName { get; set; }
        public bool? Selected { get; set; }
        public short? Duration { get; set; }
        [StringLength(2)]
        public string Division { get; set; }
        public bool? Standard { get; set; }
        [StringLength(1000)]
        public string Trade { get; set; }
        [StringLength(200)]
        public string FilePath { get; set; }
        public bool? IsSynched { get; set; }
    }
}
