using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblAccomFloor")]
    public partial class TblAccomFloor
    {
        [Key]
        [Column("flId")]
        [StringLength(10)]
        public string FlId { get; set; }
        [Key]
        [Column("flCampSeq")]
        public int FlCampSeq { get; set; }
        [Required]
        [Column("flAbv")]
        [StringLength(10)]
        public string FlAbv { get; set; }

        [ForeignKey(nameof(FlCampSeq))]
        [InverseProperty(nameof(TblAccomLocation.TblAccomFloors))]
        public virtual TblAccomLocation FlCampSeqNavigation { get; set; }
    }
}
