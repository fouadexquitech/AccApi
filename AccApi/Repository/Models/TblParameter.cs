using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblParameters")]
    public partial class TblParameter
    {
        [Key]
        [StringLength(10)]
        public string Project { get; set; }
        public double? Per { get; set; }
        public double? FuelPrice { get; set; }
        public float? HousingPriceA { get; set; }
        public float? HousingPriceB { get; set; }
        public float? HousingPriceC { get; set; }
        public float? HousingPriceD { get; set; }
        public float? TicketPrice { get; set; }
        public float? AssumingFinacnialValue { get; set; }
        public float? ProfitPer { get; set; }
        public float? AssumingContractValue { get; set; }
        public float? Diff { get; set; }
        public float? ContractValue { get; set; }
        public float? ProjectDuration { get; set; }
        public byte? ContractValueMethod { get; set; }
        public int? LocalCur { get; set; }
        public float? ExchRate { get; set; }
        public int? EstimatedCur { get; set; }
        public double? ProvisionalSum { get; set; }
        public double? TotalContractValue { get; set; }
        public int? ImportTkFrom { get; set; }
        [StringLength(50)]
        public string ImportTkFromServer { get; set; }
        [Column("UseWBS")]
        public byte? UseWbs { get; set; }
        [StringLength(30)]
        public string ProjectDefinition { get; set; }
        public byte? UseMapWbs { get; set; }
        [Column("ImportTK_JV")]
        public byte? ImportTkJv { get; set; }
        public byte? EstimMethode { get; set; }
        [Column("WBS_to_CC_Methode")]
        public byte? WbsToCcMethode { get; set; }
        [Column("Estim_BOQ_from_CC")]
        public byte? EstimBoqFromCc { get; set; }
        [Column("Import_TK_Exported")]
        public byte? ImportTkExported { get; set; }
        [Column("Use_TS_Policy")]
        public int UseTsPolicy { get; set; }
        [Column("TS_Policy")]
        [StringLength(50)]
        public string TsPolicy { get; set; }
        [Column("TS_Proj_ID")]
        public int? TsProjId { get; set; }
        [Column("BOQSellRate")]
        public double? BoqsellRate { get; set; }
        [Column("Import_TK_ByDate")]
        public byte? ImportTkByDate { get; set; }
        [Column("AyappProjID")]
        public int? AyappProjId { get; set; }
        [Column("Proj_Ayapp_ID")]
        public int? ProjAyappId { get; set; }
        [Column("ExchToUSD")]
        public byte? ExchToUsd { get; set; }
        [Column("ExchToUSD_Frac")]
        [StringLength(5)]
        public string ExchToUsdFrac { get; set; }
        [Column("ExchLoanToUSD")]
        public byte? ExchLoanToUsd { get; set; }
        [Column("ExchLoanToUSD_Frac")]
        [StringLength(5)]
        public string ExchLoanToUsdFrac { get; set; }
        public double? StafffBudSal { get; set; }
        public double? StaffBudAllow { get; set; }
        public double? StaffBudAir { get; set; }
        public double? StaffBudVisa { get; set; }
        public double? StaffBudMed { get; set; }
        public double? StaffActSal { get; set; }
        public double? StaffActAllow { get; set; }
        public double? StaffActMed { get; set; }
        public double? StaffActVisa { get; set; }
        [Column("totBudDeprecRent")]
        public double? TotBudDeprecRent { get; set; }
        [Column("totBudFuel")]
        public double? TotBudFuel { get; set; }
        [Column("totBudOperator")]
        public double? TotBudOperator { get; set; }
        [Column("totBudOD")]
        public double? TotBudOd { get; set; }
        public double? InsurancePriceA { get; set; }
        public double? InsurancePriceB { get; set; }
        public double? InsurancePriceC { get; set; }
        public double? CarPriceA { get; set; }
        public double? CarPriceB { get; set; }
        public double? CarPriceC { get; set; }
        public double? TicketBusPrice { get; set; }
        public double? TicketEcoPrice { get; set; }
        [Column("is_SilverCoast")]
        public byte? IsSilverCoast { get; set; }
        [Column("projCompletiontDte", TypeName = "datetime")]
        public DateTime? ProjCompletiontDte { get; set; }
        [Column("projCommencementDte", TypeName = "datetime")]
        public DateTime? ProjCommencementDte { get; set; }
        [Column("projProgressPlan")]
        public double? ProjProgressPlan { get; set; }
        [Column("projProgressAct")]
        public double? ProjProgressAct { get; set; }
        [Column("is_UsedNewEstimDB")]
        public byte? IsUsedNewEstimDb { get; set; }
        public int? Country { get; set; }
        [StringLength(50)]
        public string PolicySource { get; set; }
        public double? MobileAllowA { get; set; }
        public double? MobileAllowB { get; set; }
        public double? MobileAllowC { get; set; }
        [Column("readyVenDan")]
        public byte? ReadyVenDan { get; set; }
        [Column("simsomProjId")]
        public int? SimsomProjId { get; set; }
    }
}
