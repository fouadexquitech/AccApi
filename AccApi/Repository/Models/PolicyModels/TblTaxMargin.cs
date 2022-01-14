using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblTaxMargin")]
    public partial class TblTaxMargin
    {
        [Key]
        [Column("tmStatus")]
        public int TmStatus { get; set; }
        [Key]
        [Column("tmFrom")]
        public double TmFrom { get; set; }
        [Column("tmTo")]
        public double? TmTo { get; set; }
        [Column("tmRate")]
        public float? TmRate { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
    }
}
