using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblPayments")]
    public partial class TblPayment
    {
        [Key]
        public int PayNb { get; set; }
        public int? Week { get; set; }
        [StringLength(10)]
        public string Project { get; set; }
        [Column("payCertified")]
        public byte? PayCertified { get; set; }
        [Column("paySkip")]
        public byte? PaySkip { get; set; }
        [StringLength(25)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(25)]
        public string LastUserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        public double? PayOverHead { get; set; }
    }
}
