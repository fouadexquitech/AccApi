using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblMapNewLabId")]
    public partial class TblMapNewLabId
    {
        [Column("OldLabID")]
        [StringLength(10)]
        public string OldLabId { get; set; }
        [Key]
        [Column("NewLabID")]
        [StringLength(10)]
        public string NewLabId { get; set; }
    }
}
