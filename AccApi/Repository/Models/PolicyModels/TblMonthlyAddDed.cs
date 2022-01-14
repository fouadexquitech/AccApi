using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblMonthlyAddDed")]
    [Index(nameof(MadLabId), Name = "IX_tblMonthlyAddDed")]
    [Index(nameof(MadEndDate), Name = "IX_tblMonthlyAddDed_1")]
    [Index(nameof(MadProjectDef), Name = "IX_tblMonthlyAddDed_2")]
    public partial class TblMonthlyAddDed
    {
        [Key]
        [Column("madSeq")]
        public int MadSeq { get; set; }
        [Required]
        [Column("madLabID")]
        [StringLength(8)]
        public string MadLabId { get; set; }
        [Column("madEndDate", TypeName = "datetime")]
        public DateTime MadEndDate { get; set; }
        [Required]
        [Column("madProjectDef")]
        [StringLength(20)]
        public string MadProjectDef { get; set; }
        [Column("madLonParts")]
        public int MadLonParts { get; set; }
        [Column("madProject")]
        [StringLength(25)]
        public string MadProject { get; set; }
        [Column("madAmount")]
        public double? MadAmount { get; set; }
        [Column("madType")]
        [StringLength(1)]
        public string MadType { get; set; }
        [Column("madRemark")]
        [StringLength(255)]
        public string MadRemark { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(15)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [StringLength(15)]
        public string UpdatedBy { get; set; }
        public byte? Deleted { get; set; }
        [StringLength(15)]
        public string DeletedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DeletedOn { get; set; }
        [Column("madPayrollNo")]
        public int? MadPayrollNo { get; set; }

        [ForeignKey(nameof(MadLabId))]
        [InverseProperty(nameof(TblLab.TblMonthlyAddDeds))]
        public virtual TblLab MadLab { get; set; }
    }
}
