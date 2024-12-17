using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tmpEarnedValue")]
    public partial class TmpEarnedValue
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Column("WBS")]
        [StringLength(50)]
        public string Wbs { get; set; }
        [StringLength(2)]
        public string Div { get; set; }
        [Column("ccSubDiv")]
        [StringLength(5)]
        public string CcSubDiv { get; set; }
        public double? E { get; set; }
        public double? L { get; set; }
        public double? M { get; set; }
        public double? O { get; set; }
        public double? S { get; set; }
        public double? Total { get; set; }
    }
}
