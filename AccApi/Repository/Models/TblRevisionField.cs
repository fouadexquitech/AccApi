using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblRevisionFields")]
    public partial class TblRevisionField
    {
        [Key]
        public int Id { get; set; }
        [Column("revisionId")]
        public int RevisionId { get; set; }
        [Required]
        [Column("label")]
        [StringLength(100)]
        public string Label { get; set; }
        [Column("value")]
        public int Value { get; set; }
    }
}
