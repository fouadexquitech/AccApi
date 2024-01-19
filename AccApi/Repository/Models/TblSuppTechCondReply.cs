using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblSuppTechCondReply")]
    public partial class TblSuppTechCondReply
    {
        [Key]
        [Column("tcRevisionId")]
        public int TcRevisionId { get; set; }
        [Key]
        [Column("tcComConID")]
        public int TcComConId { get; set; }
        [Column("tcSuppReply")]
        public string TcSuppReply { get; set; }
        [Column("tcAccCond")]
        public string TcAccCond { get; set; }
    }
}
