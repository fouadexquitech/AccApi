using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("Packages-Network")]
    public partial class PackagesNetwork
    {
        [Key]
        [Column("ID-Pkge")]
        public int IdPkge { get; set; }
        [StringLength(150)]
        public string PkgeName { get; set; }
        public bool? Selected { get; set; }
        public short? Duration { get; set; }
        [StringLength(5)]
        public string Division { get; set; }
        public bool? Standard { get; set; }
        public short? Trade { get; set; }
        [StringLength(200)]
        public string FilePath { get; set; }
    }
}
