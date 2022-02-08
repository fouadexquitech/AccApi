using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblLoginLogs")]
    public partial class TblLoginLog
    {
        [Key]
        [Column("seq")]
        public long Seq { get; set; }
        [Column("userid")]
        [StringLength(50)]
        public string Userid { get; set; }
        [Column("datetime", TypeName = "datetime")]
        public DateTime? Datetime { get; set; }
        [Column("ip")]
        [StringLength(50)]
        public string Ip { get; set; }
        [Column("pcName")]
        [StringLength(50)]
        public string PcName { get; set; }
    }
}
