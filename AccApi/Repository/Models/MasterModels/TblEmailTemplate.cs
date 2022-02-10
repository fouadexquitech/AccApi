using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblEmailTemplate")]
    public partial class TblEmailTemplate
    {
        [Key]
        [Column("etSeq")]
        public int EtSeq { get; set; }
        [Column("etContent", TypeName = "text")]
        public string EtContent { get; set; }
        [Column("etLang")]
        public string EtLang { get; set; }
    }
}
