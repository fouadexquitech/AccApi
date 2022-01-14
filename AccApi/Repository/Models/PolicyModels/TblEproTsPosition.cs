using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEproTsPosition")]
    public partial class TblEproTsPosition
    {
        [Key]
        [Column("EproJobID")]
        [StringLength(7)]
        public string EproJobId { get; set; }
        [Key]
        [Column("TSJobID")]
        public int TsjobId { get; set; }
        [StringLength(70)]
        public string EproJobDesc { get; set; }
        [StringLength(10)]
        public string Eprocompany { get; set; }
    }
}
