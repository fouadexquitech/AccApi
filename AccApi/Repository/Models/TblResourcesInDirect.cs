using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblResourcesInDirect")]
    public partial class TblResourcesInDirect
    {
        [Key]
        [Column("rinHdrSeq")]
        [StringLength(14)]
        public string RinHdrSeq { get; set; }
        [Key]
        [Column("rinProject")]
        [StringLength(10)]
        public string RinProject { get; set; }
        [Key]
        [Column("rinRivision")]
        public short RinRivision { get; set; }
        [Key]
        [Column("rinGrp")]
        [StringLength(14)]
        public string RinGrp { get; set; }
        [Column("rinMonthFrom")]
        public float? RinMonthFrom { get; set; }
        [Column("rinMonthTo")]
        public float? RinMonthTo { get; set; }
        [Column("rinLocal")]
        [StringLength(1)]
        public string RinLocal { get; set; }
        [Column("rinCtg")]
        public byte RinCtg { get; set; }
        [Column("rinApplicablePer")]
        public float? RinApplicablePer { get; set; }
        [Column("rinApplicableVal")]
        public float? RinApplicableVal { get; set; }
        [Column("rinAdditionalVal")]
        public float? RinAdditionalVal { get; set; }
        [Column("rinCostPer")]
        public float? RinCostPer { get; set; }
        [Column("rinYearly")]
        public byte? RinYearly { get; set; }
        [Column("rinDuration")]
        public float? RinDuration { get; set; }
        [Column("rinDurationExtra")]
        public float? RinDurationExtra { get; set; }
        [Column("rinToDiv01")]
        public bool? RinToDiv01 { get; set; }
        [Column("rinByOthers")]
        public bool? RinByOthers { get; set; }
        [Column("rinBonus")]
        public byte? RinBonus { get; set; }
        [Column("rinBonusCost")]
        public float? RinBonusCost { get; set; }
        [Column("rinLeave")]
        public byte? RinLeave { get; set; }
        [Column("rinLeaveCost")]
        public float? RinLeaveCost { get; set; }
        [Column("rinAcmA")]
        public float? RinAcmA { get; set; }
        [Column("rinAcmB")]
        public float? RinAcmB { get; set; }
        [Column("rinAcmC")]
        public float? RinAcmC { get; set; }
        [Column("rinAcmD")]
        public float? RinAcmD { get; set; }
        [Column("rinAccmCost")]
        public double? RinAccmCost { get; set; }
        [Column("rinTickets")]
        public float? RinTickets { get; set; }
        [Column("rinTicketsCost")]
        public float? RinTicketsCost { get; set; }
        [Column("rinMedical")]
        public float? RinMedical { get; set; }
        [Column("rinSS")]
        public float? RinSs { get; set; }
        [Column("rinSSCost")]
        public float? RinSscost { get; set; }
        [Column("rinOthers")]
        public float? RinOthers { get; set; }
        [Column("rinQty")]
        public float? RinQty { get; set; }
        [Column("rinLifeTime")]
        public float? RinLifeTime { get; set; }
        [Column("rinDepreciation")]
        public float? RinDepreciation { get; set; }
        [Column("rinFuel")]
        public float? RinFuel { get; set; }
        [Column("rinMaintenance")]
        public float? RinMaintenance { get; set; }
        [Column("rinSpareParts")]
        public float? RinSpareParts { get; set; }
        [Column("rinRent")]
        public bool? RinRent { get; set; }
        [Column("rinRentMonths")]
        public short? RinRentMonths { get; set; }
        [Column("rinSkilledOPR")]
        public float? RinSkilledOpr { get; set; }
        [Column("rinNonSKilledOPR")]
        public float? RinNonSkilledOpr { get; set; }
        [Column("rinFee")]
        public float? RinFee { get; set; }
        [Column("rinFurniture")]
        public float? RinFurniture { get; set; }
        [Column("rinGeneral")]
        public float? RinGeneral { get; set; }
        [Column("rinWages")]
        public float? RinWages { get; set; }
        [Column("rinConstruction")]
        public float? RinConstruction { get; set; }
        [Column("rinRunning")]
        public float? RinRunning { get; set; }
        [Column("rinWorkHrs")]
        public byte? RinWorkHrs { get; set; }
        [Column("rinWorkDays")]
        public byte? RinWorkDays { get; set; }
        [Column("rinCarsCount")]
        public float? RinCarsCount { get; set; }
        [Column("rinCarsCost")]
        public float? RinCarsCost { get; set; }
        [Column("rinComputers")]
        public float? RinComputers { get; set; }
        [Column("rinTotalAmount")]
        public float? RinTotalAmount { get; set; }
        [Column("rinTotalCost")]
        public float? RinTotalCost { get; set; }
        [Column("rinMonthlyCost")]
        public float? RinMonthlyCost { get; set; }
        [Column("rinNote")]
        [StringLength(255)]
        public string RinNote { get; set; }
        [Column("rinDesc")]
        [StringLength(100)]
        public string RinDesc { get; set; }
        [Column("rinRentAmt")]
        public float? RinRentAmt { get; set; }
        [Column("rinMobile")]
        public float? RinMobile { get; set; }
        [Column("rinInvest")]
        public bool? RinInvest { get; set; }
        [Column("rinStatus")]
        public byte? RinStatus { get; set; }
        [Column("rinDerivedFrom")]
        public byte? RinDerivedFrom { get; set; }
        [Column("rinFrac")]
        public float? RinFrac { get; set; }
        [Column("rinIsImported")]
        public bool? RinIsImported { get; set; }
        [Column("rinIsLabors")]
        public bool? RinIsLabors { get; set; }
        [Column("rinTotalCostLabor")]
        public float? RinTotalCostLabor { get; set; }
        [Column("rinTotalCostNonLabor")]
        public float? RinTotalCostNonLabor { get; set; }
        [Column("rinMedicalClass")]
        public byte? RinMedicalClass { get; set; }
        [Column("rinCarsClass")]
        public byte? RinCarsClass { get; set; }
        [Column("rinYearlyIncCost")]
        public float? RinYearlyIncCost { get; set; }
        [Column("rinFracCost")]
        public float? RinFracCost { get; set; }
        [Column("rinBasicMthOperatorCost")]
        public float? RinBasicMthOperatorCost { get; set; }
        [Column("rinOperatorFactor")]
        public float? RinOperatorFactor { get; set; }
        [Column("rinMthOperatorCost")]
        public float? RinMthOperatorCost { get; set; }
        public byte? Updated { get; set; }
        [Column("rinFuelTotalCost")]
        public float? RinFuelTotalCost { get; set; }
        [Column("rinFuelMonthlyQty")]
        public float? RinFuelMonthlyQty { get; set; }
        [Column("rinFuelGalonPrice")]
        public float? RinFuelGalonPrice { get; set; }
        [Column("rinMobileClass")]
        public byte? RinMobileClass { get; set; }
        [Column("rinMinQty")]
        public double? RinMinQty { get; set; }
        [Column("rinMaxQty")]
        public double? RinMaxQty { get; set; }
        [Column("rinFuelQty")]
        public double? RinFuelQty { get; set; }
        [Column("rinSpareQty")]
        public double? RinSpareQty { get; set; }
        [Column("rinArea")]
        public double? RinArea { get; set; }
        [Column("rinConstFinishAC")]
        public float? RinConstFinishAc { get; set; }
        [Column("rinMthlyInterest")]
        public double? RinMthlyInterest { get; set; }
        [Column("rinMthlyResidCost")]
        public float? RinMthlyResidCost { get; set; }
        [Column("rinMthlyMedicalCost")]
        public float? RinMthlyMedicalCost { get; set; }
        [Column("rinMthlyOtherAuxCost")]
        public float? RinMthlyOtherAuxCost { get; set; }
        [Column("rinWBS")]
        [StringLength(50)]
        public string RinWbs { get; set; }
        [Column("rinDiv")]
        [StringLength(50)]
        public string RinDiv { get; set; }
        [Column("rinSubDiv")]
        [StringLength(50)]
        public string RinSubDiv { get; set; }
        [Column("rinTrade")]
        [StringLength(50)]
        public string RinTrade { get; set; }
        [Column("rinSubTrade")]
        [StringLength(50)]
        public string RinSubTrade { get; set; }
        [Column("rinSeparatFuelAmt")]
        public bool? RinSeparatFuelAmt { get; set; }
        [Column("rinSeparatOperatAmt")]
        public bool? RinSeparatOperatAmt { get; set; }

        [ForeignKey(nameof(RinHdrSeq))]
        [InverseProperty(nameof(TblResourcesInDirectIndex.TblResourcesInDirects))]
        public virtual TblResourcesInDirectIndex RinHdrSeqNavigation { get; set; }
    }
}
