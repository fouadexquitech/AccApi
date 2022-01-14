using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tmpJoinTrsfStaffWorkers")]
    public partial class TmpJoinTrsfStaffWorker
    {
        [Key]
        [Column("seq")]
        public long Seq { get; set; }
        public byte? Staff { get; set; }
        [Column("PSC")]
        [StringLength(10)]
        public string Psc { get; set; }
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(75)]
        public string JobDesc { get; set; }
        public byte? JoinTrsfResign { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? JoinTrsfDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ResignTrsfDate { get; set; }
        public short? Sort { get; set; }
        [StringLength(15)]
        public string ProjectDef { get; set; }
        [StringLength(30)]
        public string UserName { get; set; }
        [Column("ID")]
        [StringLength(10)]
        public string Id { get; set; }
        [StringLength(10)]
        public string ProjCode { get; set; }
    }
}
