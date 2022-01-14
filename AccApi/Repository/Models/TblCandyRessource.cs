using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblCandyRessources")]
    public partial class TblCandyRessource
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Column("crLevel")]
        public int? CrLevel { get; set; }
        [Column("crType")]
        [StringLength(3)]
        public string CrType { get; set; }
        [Column("crCode")]
        [StringLength(15)]
        public string CrCode { get; set; }
        [Column("crDescription")]
        [StringLength(100)]
        public string CrDescription { get; set; }
    }
}
