using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("TblForman")]
    public partial class TblForman
    {
        [Key]
        [Column("forId")]
        public int ForId { get; set; }
        [Key]
        [Column("forFileNumber")]
        [StringLength(10)]
        public string ForFileNumber { get; set; }
        [Column("forProj")]
        public int? ForProj { get; set; }
        [Column("forName")]
        [StringLength(50)]
        public string ForName { get; set; }
        [Column("forType")]
        public short? ForType { get; set; }
        [Column("forAbv")]
        [StringLength(5)]
        public string ForAbv { get; set; }
        [Column("forWrkLocation")]
        public int? ForWrkLocation { get; set; }
        public int? InsertForman { get; set; }
        [StringLength(25)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(25)]
        public string LastUserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("forusername")]
        [StringLength(15)]
        public string Forusername { get; set; }
        [Column("forCM")]
        public int? ForCm { get; set; }
        [Column("forSection")]
        public int? ForSection { get; set; }
        [Column("forSiteEng")]
        public int? ForSiteEng { get; set; }
        [Column("forCompany")]
        public int? ForCompany { get; set; }
    }
}
