using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblAccomLocation")]
    public partial class TblAccomLocation
    {
        public TblAccomLocation()
        {
            TblAccomFloors = new HashSet<TblAccomFloor>();
        }

        [Key]
        [Column("alID")]
        public int AlId { get; set; }
        [Required]
        [Column("alAbv")]
        [StringLength(100)]
        public string AlAbv { get; set; }
        [Column("alLocation")]
        [StringLength(100)]
        public string AlLocation { get; set; }
        [Column("alProjectCode")]
        [StringLength(9)]
        public string AlProjectCode { get; set; }
        [Column("alDailyRate")]
        public double? AlDailyRate { get; set; }

        [InverseProperty(nameof(TblAccomFloor.FlCampSeqNavigation))]
        public virtual ICollection<TblAccomFloor> TblAccomFloors { get; set; }
    }
}
