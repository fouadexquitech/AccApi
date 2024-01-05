using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblAlertEmailHdr")]
    public partial class TblAlertEmailHdr
    {
        [Key]
        [Column("aehSeq")]
        public int AehSeq { get; set; }
        [Column("aehProjCode")]
        public int? AehProjCode { get; set; }
        [Column("aehStoredProc")]
        [StringLength(250)]
        public string AehStoredProc { get; set; }
        [Column("aehAlertType")]
        public int? AehAlertType { get; set; }
        [Column("aehInsertBy")]
        [StringLength(10)]
        public string AehInsertBy { get; set; }
        [Column("aehUpdateDate", TypeName = "datetime")]
        public DateTime? AehUpdateDate { get; set; }
    }
}
