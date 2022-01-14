using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblSubcontractorOffers")]
    public partial class TblSubcontractorOffer
    {
        [Key]
        [Column("soSubcontractor")]
        [StringLength(12)]
        public string SoSubcontractor { get; set; }
        [Key]
        [Column("soItem")]
        [StringLength(25)]
        public string SoItem { get; set; }
        [Key]
        [Column("soDate", TypeName = "datetime")]
        public DateTime SoDate { get; set; }
        [Column("soPrice")]
        public double? SoPrice { get; set; }
        [Column("soCurr")]
        [StringLength(50)]
        public string SoCurr { get; set; }
        [Column("soNote")]
        [StringLength(255)]
        public string SoNote { get; set; }
        [Column("soRef")]
        [StringLength(25)]
        public string SoRef { get; set; }
        [Column("soOurRef")]
        [StringLength(50)]
        public string SoOurRef { get; set; }
        [Column("soOurDate", TypeName = "datetime")]
        public DateTime? SoOurDate { get; set; }
    }
}
