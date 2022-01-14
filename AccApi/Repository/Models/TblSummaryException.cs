using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblSummaryException")]
    public partial class TblSummaryException
    {
        [Key]
        [Column("seProject")]
        [StringLength(10)]
        public string SeProject { get; set; }
        [Key]
        [Column("seCC")]
        [StringLength(20)]
        public string SeCc { get; set; }
        [Column("seDiv")]
        [StringLength(2)]
        public string SeDiv { get; set; }
        [Column("seSubDiv")]
        [StringLength(3)]
        public string SeSubDiv { get; set; }
        [Column("seTrade")]
        [StringLength(5)]
        public string SeTrade { get; set; }
        [Column("seSubTrade")]
        [StringLength(3)]
        public string SeSubTrade { get; set; }
        [Column("seExcepReport")]
        public byte? SeExcepReport { get; set; }
        [StringLength(25)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(25)]
        public string LastUserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
    }
}
