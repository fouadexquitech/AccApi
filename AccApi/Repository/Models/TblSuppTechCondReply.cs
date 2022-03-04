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
        [Column("tcPackageSupliersID")]
        public int TcPackageSupliersId { get; set; }
        [Key]
        [Column("tcComConID")]
        public int TcComConId { get; set; }
        [Column("tcSuppReply")]
        public string TcSuppReply { get; set; }
    }
}
