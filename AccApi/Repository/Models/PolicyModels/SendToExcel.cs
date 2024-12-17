using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("SendToExcel")]
    public partial class SendToExcel
    {
        [Column("Att_Date")]
        [StringLength(8)]
        public string AttDate { get; set; }
        [Column("Personnel_Number")]
        [StringLength(25)]
        public string PersonnelNumber { get; set; }
        [Column("COAr")]
        [StringLength(1)]
        public string Coar { get; set; }
        [StringLength(30)]
        public string SendCctr { get; set; }
        [StringLength(1)]
        public string AcTyp { get; set; }
        [StringLength(1)]
        public string InternalOrder { get; set; }
        [Column("WBS")]
        [StringLength(24)]
        public string Wbs { get; set; }
        [StringLength(24)]
        public string CostCenter { get; set; }
        [Column("AA_Type")]
        public short? AaType { get; set; }
        [StringLength(1)]
        public string Total { get; set; }
        public double? Hours { get; set; }
        [StringLength(6)]
        public string FromTime { get; set; }
        [StringLength(6)]
        public string ToTime { get; set; }
        [StringLength(75)]
        public string Project { get; set; }
        [Column("SapWBS")]
        [StringLength(75)]
        public string SapWbs { get; set; }
        [StringLength(20)]
        public string ProjDef { get; set; }
        public int? LabSponsor { get; set; }
        [StringLength(5)]
        public string Building { get; set; }
        public byte? Holiday { get; set; }
        public double? NormalHours { get; set; }
        [StringLength(50)]
        public string UserName { get; set; }
    }
}
