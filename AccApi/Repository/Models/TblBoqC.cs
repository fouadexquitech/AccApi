using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblBoqC")]
    public partial class TblBoqC
    {
        [Key]
        [Column("bcBoqItem")]
        [StringLength(25)]
        public string BcBoqItem { get; set; }
        [Key]
        [Column("bcCSeq")]
        public int BcCseq { get; set; }
    }
}
