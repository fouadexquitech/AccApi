using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    public partial class NewItemResource
    {
        [Key]
        public int Id { get; set; }
        [StringLength(150)]
        public string ResourceDescription { get; set; }
        [StringLength(50)]
        public string ResourceUnit { get; set; }
        [StringLength(50)]
        public string ResourceType { get; set; }
        public int? NewItemId { get; set; }
    }
}
