using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblAcmClass")]
    public partial class TblAcmClass
    {
        [Key]
        [Column("acSeq")]
        public byte AcSeq { get; set; }
        [Required]
        [Column("acClass")]
        [StringLength(5)]
        public string AcClass { get; set; }
        [Column("acMonthlyRental")]
        public double? AcMonthlyRental { get; set; }
    }
}
