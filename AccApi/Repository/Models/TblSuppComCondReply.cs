using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblSuppComCondReply")]
    public partial class TblSuppComCondReply
    {
        [Key]
        [Column("cdPackageSupliersID")]
        public int CdPackageSupliersId { get; set; }
        [Key]
        [Column("cdComConID")]
        public int CdComConId { get; set; }
        [Column("cdSuppReply", TypeName = "text")]
        public string CdSuppReply { get; set; }
    }
}
