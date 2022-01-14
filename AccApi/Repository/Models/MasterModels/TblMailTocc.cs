using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblMailTOCC")]
    public partial class TblMailTocc
    {
        [Key]
        [Column("mldSeq")]
        public int MldSeq { get; set; }
        [Key]
        [Column("mldHdrSeq")]
        public int MldHdrSeq { get; set; }
        [Required]
        [Column("mldMail")]
        [StringLength(50)]
        public string MldMail { get; set; }
        [Required]
        [Column("mldToCC")]
        [StringLength(1)]
        public string MldToCc { get; set; }
        [Column("mldMailDisabled")]
        public byte? MldMailDisabled { get; set; }
        [Column("mldMailSort")]
        public int? MldMailSort { get; set; }
    }
}
