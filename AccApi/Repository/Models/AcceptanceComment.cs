using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    public partial class AcceptanceComment
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Caption { get; set; }
        public bool? Enabled { get; set; }
    }
}
