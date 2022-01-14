using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblNationality")]
    public partial class TblNationality
    {
        [Key]
        [Column("natSeq")]
        public int NatSeq { get; set; }
        [Column("natDescA")]
        [StringLength(100)]
        public string NatDescA { get; set; }
        [Column("natDescE")]
        [StringLength(100)]
        public string NatDescE { get; set; }
        [Column("natTax")]
        public byte? NatTax { get; set; }
    }
}
