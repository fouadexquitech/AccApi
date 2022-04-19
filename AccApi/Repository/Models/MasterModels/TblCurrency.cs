using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblCurrency")]
    [Index(nameof(CurCode), Name = "IX_tblCurrency", IsUnique = true)]
    public partial class TblCurrency
    {
        [Key]
        [Column("curID")]
        public int CurId { get; set; }
        [Required]
        [Column("curCode")]
        [StringLength(50)]
        public string CurCode { get; set; }
        [Column("curDesc")]
        [StringLength(50)]
        public string CurDesc { get; set; }
    }
}
