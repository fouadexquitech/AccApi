using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("Missing")]
    public partial class Missing
    {
        [Required]
        [Column("disLab")]
        [StringLength(14)]
        public string DisLab { get; set; }
        [Column("disDate", TypeName = "datetime")]
        public DateTime? DisDate { get; set; }
        [Column("description")]
        [StringLength(50)]
        public string Description { get; set; }
    }
}
