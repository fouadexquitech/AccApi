using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblSubcontractor")]
    public partial class TblSubcontractor
    {
        [Key]
        [Column("scSubID")]
        public int ScSubId { get; set; }
        [Column("scName")]
        [StringLength(50)]
        public string ScName { get; set; }
        [Column("scAbv")]
        [StringLength(5)]
        public string ScAbv { get; set; }
        [Column("scNote")]
        [StringLength(255)]
        public string ScNote { get; set; }
        [Column("scCO")]
        [StringLength(4)]
        public string ScCo { get; set; }
        [Column("scAddress")]
        [StringLength(75)]
        public string ScAddress { get; set; }
        [Column("scPhone")]
        [StringLength(15)]
        public string ScPhone { get; set; }
        [Column("scFax")]
        [StringLength(20)]
        public string ScFax { get; set; }
        [Column("scEmail")]
        [StringLength(100)]
        public string ScEmail { get; set; }
        [StringLength(10)]
        public string InsertBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertDate { get; set; }
        [StringLength(10)]
        public string LastUpdateBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdateDate { get; set; }
    }
}
