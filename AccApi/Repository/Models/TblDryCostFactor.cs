using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblDryCostFactor")]
    public partial class TblDryCostFactor
    {
        [Key]
        [Column("dcfDiv")]
        [StringLength(2)]
        public string DcfDiv { get; set; }
        [Key]
        [Column("dcfPhase")]
        public int DcfPhase { get; set; }
        [Column("dcfFactor")]
        public double? DcfFactor { get; set; }
    }
}
