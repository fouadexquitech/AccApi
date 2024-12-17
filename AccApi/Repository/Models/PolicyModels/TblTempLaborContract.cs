using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tblTempLaborContract")]
    public partial class TblTempLaborContract
    {
        [StringLength(14)]
        public string LabId { get; set; }
        [Column("labname")]
        [StringLength(200)]
        public string Labname { get; set; }
        [Column("labnameE")]
        [StringLength(200)]
        public string LabnameE { get; set; }
        [Column("labDayFee")]
        public float? LabDayFee { get; set; }
        [Column("prjName")]
        [StringLength(100)]
        public string PrjName { get; set; }
        [StringLength(50)]
        public string LabJobE { get; set; }
        [StringLength(50)]
        public string LabJobA { get; set; }
        [Column("labIdNo")]
        [StringLength(20)]
        public string LabIdNo { get; set; }
        [Column("labWDate", TypeName = "datetime")]
        public DateTime? LabWdate { get; set; }
        [Column("labLDate", TypeName = "datetime")]
        public DateTime? LabLdate { get; set; }
        public double? LabFood { get; set; }
        [StringLength(50)]
        public string LabNatE { get; set; }
        [StringLength(50)]
        public string LabNatA { get; set; }
        [StringLength(100)]
        public string LabNbPass { get; set; }
        public double? LabFixMon { get; set; }
        [StringLength(50)]
        public string LabPassNatA { get; set; }
        [Column("labContact")]
        [StringLength(50)]
        public string LabContact { get; set; }
        [Column("labDeg")]
        [StringLength(1)]
        public string LabDeg { get; set; }
        [Column("labWrkRef")]
        [StringLength(5)]
        public string LabWrkRef { get; set; }
        [Column("labEndRef")]
        [StringLength(5)]
        public string LabEndRef { get; set; }
    }
}
