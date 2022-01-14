using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEproSAPMarStat")]
    public partial class TblEproSapmarStat
    {
        [Key]
        [Column("EproMarStatID")]
        [StringLength(2)]
        public string EproMarStatId { get; set; }
        [StringLength(20)]
        public string EproMarStatDesc { get; set; }
        [StringLength(10)]
        public string SapMarStatDesc { get; set; }
    }
}
