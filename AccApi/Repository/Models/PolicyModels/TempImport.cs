using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("TempImport")]
    public partial class TempImport
    {
        [Column("labid")]
        [StringLength(50)]
        public string Labid { get; set; }
        [Column("labACCnew")]
        [StringLength(50)]
        public string LabAccnew { get; set; }
        [Column("labACCbranch")]
        [StringLength(50)]
        public string LabAccbranch { get; set; }
        [Column("labbank")]
        [StringLength(50)]
        public string Labbank { get; set; }
        public float? Currrency { get; set; }
    }
}
