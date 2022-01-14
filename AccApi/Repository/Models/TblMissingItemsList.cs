using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblMissingItemsList")]
    public partial class TblMissingItemsList
    {
        [Key]
        [Column("milWrongItem")]
        [StringLength(25)]
        public string MilWrongItem { get; set; }
        [Key]
        [Column("milCorrectItem")]
        [StringLength(25)]
        public string MilCorrectItem { get; set; }
        [Key]
        [Column("milUnitRate")]
        public double MilUnitRate { get; set; }
    }
}
