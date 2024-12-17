using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblBoqDiscount")]
    public partial class TblBoqDiscount
    {
        [Key]
        [Column("boqdCtg")]
        [StringLength(10)]
        public string BoqdCtg { get; set; }
        [Key]
        [Column("boqdDiv")]
        [StringLength(10)]
        public string BoqdDiv { get; set; }
        [Column("boqdSubDiv")]
        [StringLength(10)]
        public string BoqdSubDiv { get; set; }
        [Column("boqdTrade")]
        [StringLength(10)]
        public string BoqdTrade { get; set; }
        [Column("boqddiscount")]
        public float? Boqddiscount { get; set; }
        [Column("boqdDiscountAll")]
        public double? BoqdDiscountAll { get; set; }
        [Column("LUser")]
        [StringLength(50)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertDate { get; set; }
        [StringLength(50)]
        public string InsertBy { get; set; }
    }
}
