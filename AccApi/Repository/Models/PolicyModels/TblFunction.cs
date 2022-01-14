using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblFunctions")]
    public partial class TblFunction
    {
        [Key]
        [Column("funID")]
        [StringLength(30)]
        public string FunId { get; set; }
        [Column("funDesc")]
        [StringLength(75)]
        public string FunDesc { get; set; }
        [Column("funSys")]
        public byte? FunSys { get; set; }
        public short? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
    }
}
