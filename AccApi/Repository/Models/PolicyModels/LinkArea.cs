using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    public partial class LinkArea
    {
        [Key]
        [StringLength(50)]
        public string OldArea { get; set; }
        [Key]
        public int NewArea { get; set; }
    }
}
