using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblAyappMapOccupDiv01")]
    public partial class TblAyappMapOccupDiv01
    {
        [Key]
        public int Seq { get; set; }
        [Column("TSOccup")]
        [StringLength(100)]
        public string Tsoccup { get; set; }
        [StringLength(100)]
        public string AyappOccup { get; set; }
        [Column("WBS")]
        [StringLength(20)]
        public string Wbs { get; set; }
    }
}
