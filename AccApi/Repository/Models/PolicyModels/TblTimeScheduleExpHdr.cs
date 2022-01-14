using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblTimeScheduleExpHdr")]
    public partial class TblTimeScheduleExpHdr
    {
        public TblTimeScheduleExpHdr()
        {
            TblTimeScheduleExpDtls = new HashSet<TblTimeScheduleExpDtl>();
        }

        [Key]
        [Column("tsehSeq")]
        public int TsehSeq { get; set; }
        [Column("tsehProjID")]
        public int TsehProjId { get; set; }
        [Required]
        [Column("tsehProjectDef")]
        [StringLength(20)]
        public string TsehProjectDef { get; set; }
        [Required]
        [Column("tsehDesc")]
        [StringLength(30)]
        public string TsehDesc { get; set; }
        [Column("tsehDateFrom", TypeName = "datetime")]
        public DateTime TsehDateFrom { get; set; }
        [Column("tsehDateTo", TypeName = "datetime")]
        public DateTime TsehDateTo { get; set; }

        [InverseProperty(nameof(TblTimeScheduleExpDtl.TsedHdrSeqNavigation))]
        public virtual ICollection<TblTimeScheduleExpDtl> TblTimeScheduleExpDtls { get; set; }
    }
}
