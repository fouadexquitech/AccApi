using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("tblDiscounts")]
    public partial class TblDiscount
    {
        [StringLength(50)]
        public string Project { get; set; }
        [Column("disc1")]
        public bool? Disc1 { get; set; }
        [StringLength(50)]
        public string Note1 { get; set; }
        [Column("disc2")]
        public bool? Disc2 { get; set; }
        [StringLength(50)]
        public string Note2 { get; set; }
        [Column("disc3")]
        public bool? Disc3 { get; set; }
        [StringLength(50)]
        public string Note3 { get; set; }
        [Column("disc4")]
        public bool? Disc4 { get; set; }
        [StringLength(50)]
        public string Note4 { get; set; }
    }
}
