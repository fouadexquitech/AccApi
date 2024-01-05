using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblAlertEmailsDtl")]
    public partial class TblAlertEmailsDtl
    {
        [Key]
        [Column("aedSeq")]
        public int AedSeq { get; set; }
        [Key]
        [Column("aedSeqHdr")]
        public int AedSeqHdr { get; set; }
        [Column("aedEmail")]
        [StringLength(250)]
        public string AedEmail { get; set; }
    }
}
