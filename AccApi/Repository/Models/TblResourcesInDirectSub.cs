using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblResourcesInDirectSub")]
    public partial class TblResourcesInDirectSub
    {
        [Key]
        [Column("risHdrSeq")]
        [StringLength(14)]
        public string RisHdrSeq { get; set; }
        [Key]
        [Column("risProject")]
        [StringLength(10)]
        public string RisProject { get; set; }
        [Key]
        [Column("risRivision")]
        public short RisRivision { get; set; }
        [Key]
        [Column("risGrp")]
        [StringLength(14)]
        public string RisGrp { get; set; }
        [Key]
        [Column("risMonthFrom")]
        public float RisMonthFrom { get; set; }
        [Key]
        [Column("risMonthTo")]
        public float RisMonthTo { get; set; }
        [Column("risDuration")]
        public float? RisDuration { get; set; }
        [Column("risQty")]
        public float? RisQty { get; set; }
        [Column("risBonus")]
        public byte? RisBonus { get; set; }
        [Column("risBonusCost")]
        public float? RisBonusCost { get; set; }
        [Column("risLeave")]
        public byte? RisLeave { get; set; }
        [Column("risLeaveCost")]
        public float? RisLeaveCost { get; set; }
        [Column("risAcmA")]
        public float? RisAcmA { get; set; }
        [Column("risAcmB")]
        public float? RisAcmB { get; set; }
        [Column("risAcmC")]
        public float? RisAcmC { get; set; }
        [Column("risAcmD")]
        public float? RisAcmD { get; set; }
        [Column("risAccmCost")]
        public double? RisAccmCost { get; set; }
        [Column("risTickets")]
        public float? RisTickets { get; set; }
        [Column("risTicketsCost")]
        public float? RisTicketsCost { get; set; }
        [Column("risSS")]
        public float? RisSs { get; set; }
        [Column("risSSCost")]
        public float? RisSscost { get; set; }
        [Column("risTotalAmount")]
        public float? RisTotalAmount { get; set; }
        [Column("risTotalCost")]
        public float? RisTotalCost { get; set; }
        [Column("risMonthlyCost")]
        public float? RisMonthlyCost { get; set; }
        [Column("risMedical")]
        public float? RisMedical { get; set; }
        [Column("risCarsCount")]
        public float? RisCarsCount { get; set; }
        [Column("risCarsCost")]
        public float? RisCarsCost { get; set; }
        [Column("risComputers")]
        public float? RisComputers { get; set; }
        [Column("risMobile")]
        public float? RisMobile { get; set; }
        [Column("risOthers")]
        public float? RisOthers { get; set; }
        [Column("risFrac")]
        public float? RisFrac { get; set; }
        [Column("risIsImported")]
        public bool? RisIsImported { get; set; }
        [Column("risApplicableVal")]
        public float? RisApplicableVal { get; set; }
        [Column("risYearly")]
        public float? RisYearly { get; set; }
        [Column("risMedicalClass")]
        public byte? RisMedicalClass { get; set; }
        [Column("risCarsClass")]
        public byte? RisCarsClass { get; set; }
        [Column("risYearlyIncCost")]
        public float? RisYearlyIncCost { get; set; }
        [Column("risFracCost")]
        public float? RisFracCost { get; set; }
        [Column("risMobileClass")]
        public byte? RisMobileClass { get; set; }
        [Column("risMthOperatorCost")]
        public float? RisMthOperatorCost { get; set; }
        [Column("risTotOperCost")]
        public float? RisTotOperCost { get; set; }
        [Column("risFuel")]
        public float? RisFuel { get; set; }
        [Column("risFuelTotalCost")]
        public float? RisFuelTotalCost { get; set; }
        [Column("risSpareQty")]
        public float? RisSpareQty { get; set; }
        [Column("risMthlyResidCost")]
        public float? RisMthlyResidCost { get; set; }
        [Column("risMthlyMedicalCost")]
        public float? RisMthlyMedicalCost { get; set; }
        [Column("risMthlyOtherAuxCost")]
        public float? RisMthlyOtherAuxCost { get; set; }
        [Column("risSparePartsMthlyCost")]
        public float? RisSparePartsMthlyCost { get; set; }
        [Column("risSparePartsTotCost")]
        public float? RisSparePartsTotCost { get; set; }
    }
}
