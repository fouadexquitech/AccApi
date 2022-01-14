using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblWbsMap")]
    public partial class TblWbsMap
    {
        [Key]
        [Column("wbsCost")]
        [StringLength(50)]
        public string WbsCost { get; set; }
        [Key]
        [Column("wbsTK")]
        [StringLength(50)]
        public string WbsTk { get; set; }
        [StringLength(200)]
        public string WbsTkDesc { get; set; }
        public byte? Used { get; set; }
    }
}
