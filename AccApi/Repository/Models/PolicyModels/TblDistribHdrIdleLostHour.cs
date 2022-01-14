using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblDistribHdrIdleLostHours")]
    public partial class TblDistribHdrIdleLostHour
    {
        [Key]
        public int Seq { get; set; }
        [Column("disDate", TypeName = "datetime")]
        public DateTime? DisDate { get; set; }
        [Column("disLab")]
        [StringLength(10)]
        public string DisLab { get; set; }
        [Column("disTimein", TypeName = "datetime")]
        public DateTime? DisTimein { get; set; }
        [Column("disTimeout", TypeName = "datetime")]
        public DateTime? DisTimeout { get; set; }
        [Column("disStatus")]
        public short? DisStatus { get; set; }
        [Column("disHours")]
        public double? DisHours { get; set; }
        [Column("disProject")]
        [StringLength(9)]
        public string DisProject { get; set; }
        [Column("disEntry")]
        public byte? DisEntry { get; set; }
        [Column("disProjectDef")]
        [StringLength(9)]
        public string DisProjectDef { get; set; }
        [Column("disTimeInAct", TypeName = "datetime")]
        public DateTime? DisTimeInAct { get; set; }
        [Column("disTimeOutAct", TypeName = "datetime")]
        public DateTime? DisTimeOutAct { get; set; }
        [Column("disNight")]
        [StringLength(5)]
        public string DisNight { get; set; }
        [Column("disTimeinRnd", TypeName = "datetime")]
        public DateTime? DisTimeinRnd { get; set; }
        [Column("disTimeoutRnd", TypeName = "datetime")]
        public DateTime? DisTimeoutRnd { get; set; }
        public bool? Confirmed { get; set; }
        [StringLength(20)]
        public string ConfirmedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ConfirmedDate { get; set; }
        [StringLength(20)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(10)]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column("disDeleted")]
        public byte? DisDeleted { get; set; }
        [Column("disDeletedBy")]
        [StringLength(10)]
        public string DisDeletedBy { get; set; }
        [Column("disDeletedOn", TypeName = "datetime")]
        public DateTime? DisDeletedOn { get; set; }
    }
}
