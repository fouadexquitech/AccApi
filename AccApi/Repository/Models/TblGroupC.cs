using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblGroupC")]
    public partial class TblGroupC
    {
        [Key]
        [Column("cSeq")]
        public int CSeq { get; set; }
        [Column("cNo")]
        [StringLength(50)]
        public string CNo { get; set; }
        [Column("cDesc")]
        [StringLength(5000)]
        public string CDesc { get; set; }
    }
}
