using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("emsReport")]
    public partial class EmsReport
    {
        [Key]
        [Column("repID")]
        public int RepId { get; set; }
        [Column("repSeq")]
        public int? RepSeq { get; set; }
        [Column("repName")]
        public string RepName { get; set; }
        [Column("repIsActive")]
        public bool? RepIsActive { get; set; }
        [Column("repIsEM")]
        public bool? RepIsEm { get; set; }
        [Column("repIsSrEng")]
        public bool? RepIsSrEng { get; set; }
        [Column("repIsEng")]
        public bool? RepIsEng { get; set; }
    }
}
