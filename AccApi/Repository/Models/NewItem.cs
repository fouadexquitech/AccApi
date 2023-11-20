using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    public partial class NewItem
    {
        [Key]
        public int Id { get; set; }
        [StringLength(5000)]
        public string ItemDescription { get; set; }
        [StringLength(255)]
        public string UnitO { get; set; }
        [StringLength(5000)]
        public string L1 { get; set; }
        [StringLength(5000)]
        public string L2 { get; set; }
        [StringLength(5000)]
        public string L3 { get; set; }
        [StringLength(5000)]
        public string L4 { get; set; }
        [StringLength(5000)]
        public string L5 { get; set; }
        [StringLength(5000)]
        public string L6 { get; set; }
        [StringLength(5000)]
        public string L7 { get; set; }
        [StringLength(5000)]
        public string L8 { get; set; }
        [StringLength(5000)]
        public string L9 { get; set; }
        [StringLength(5000)]
        public string L10 { get; set; }
        [StringLength(5000)]
        public string C1 { get; set; }
        [StringLength(5000)]
        public string C2 { get; set; }
        [StringLength(5000)]
        public string C3 { get; set; }
        [StringLength(5000)]
        public string C4 { get; set; }
        [StringLength(5000)]
        public string C5 { get; set; }
        [StringLength(5000)]
        public string C6 { get; set; }
        [StringLength(5000)]
        public string C7 { get; set; }
        [StringLength(5000)]
        public string C8 { get; set; }
        [StringLength(5000)]
        public string C9 { get; set; }
        [StringLength(5000)]
        public string C10 { get; set; }
        [StringLength(5000)]
        public string C11 { get; set; }
        [StringLength(5000)]
        public string C12 { get; set; }
        [StringLength(5000)]
        public string C13 { get; set; }
        [StringLength(5000)]
        public string C14 { get; set; }
        [StringLength(5000)]
        public string C15 { get; set; }
    }
}
