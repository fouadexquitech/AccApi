using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblProjectWeeks")]
    public partial class TblProjectWeek
    {
        [Key]
        [Column("pwkProject")]
        public int PwkProject { get; set; }
        [Key]
        [Column("pwkWeek")]
        public int PwkWeek { get; set; }
        [Column("pwkStartDate", TypeName = "datetime")]
        public DateTime? PwkStartDate { get; set; }
        [Column("pwkEndDate", TypeName = "datetime")]
        public DateTime? PwkEndDate { get; set; }
        [Column("pwkExhange")]
        public float? PwkExhange { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column("pwkLock")]
        public bool? PwkLock { get; set; }
        public short? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("pwkPayroll")]
        public short? PwkPayroll { get; set; }
        [Column("pwkBenzPrx")]
        public float? PwkBenzPrx { get; set; }
        [Column("pwkDieselPrx")]
        public float? PwkDieselPrx { get; set; }
        [Column("pwkMonth")]
        [StringLength(2)]
        public string PwkMonth { get; set; }
        [Column("pwkYear")]
        public short? PwkYear { get; set; }

        [ForeignKey(nameof(PwkProject))]
        [InverseProperty(nameof(Tblproject.TblProjectWeeks))]
        public virtual Tblproject PwkProjectNavigation { get; set; }
    }
}
