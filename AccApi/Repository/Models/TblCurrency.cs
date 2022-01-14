using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblCurrency")]
    public partial class TblCurrency
    {
        [Key]
        [Column("curID")]
        public int CurId { get; set; }
        [Column("cudCode")]
        [StringLength(3)]
        public string CudCode { get; set; }
        [Column("curDesc")]
        [StringLength(20)]
        public string CurDesc { get; set; }
    }
}
