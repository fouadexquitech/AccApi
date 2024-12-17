using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblproject")]
    public partial class Tblproject
    {
        [Key]
        public int Seq { get; set; }
        [Key]
        [Column("prjTSSeq")]
        public int PrjTsseq { get; set; }
        [StringLength(50)]
        public string Country { get; set; }
        [Column("prjName")]
        [StringLength(50)]
        public string PrjName { get; set; }
        [Column("prjAbv")]
        [StringLength(10)]
        public string PrjAbv { get; set; }
        [Column("prjCode")]
        [StringLength(8)]
        public string PrjCode { get; set; }
        [Column("prjPhase")]
        public int? PrjPhase { get; set; }
        [Column("prjDefault")]
        public bool? PrjDefault { get; set; }
        [Column("prjSel")]
        public bool? PrjSel { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column("prjSelRpt")]
        public byte? PrjSelRpt { get; set; }
        [Column("prjLogoPath")]
        [StringLength(50)]
        public string PrjLogoPath { get; set; }
        public short? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("prjTK")]
        [StringLength(50)]
        public string PrjTk { get; set; }
        [Column("prjSK")]
        [StringLength(50)]
        public string PrjSk { get; set; }
        [Column("prjModify")]
        [StringLength(50)]
        public string PrjModify { get; set; }
        [StringLength(50)]
        public string Location { get; set; }
        [StringLength(50)]
        public string Client { get; set; }
        [StringLength(50)]
        public string Engineer { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PurchaseDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? SubmissionDate { get; set; }
        public int? ProjectCurrency { get; set; }
        public int? ProjectDuration { get; set; }
        [StringLength(50)]
        public string TypeOfConst { get; set; }
        public bool? Closed { get; set; }
        [Column(TypeName = "smallmoney")]
        public decimal? Percentage { get; set; }
        [Column(TypeName = "money")]
        public decimal? SellPrice { get; set; }
        [Column(TypeName = "money")]
        public decimal? ContractValue { get; set; }
        public byte? ContractValueMethod { get; set; }
        public int? LocalCur { get; set; }
        public float? ExchRate { get; set; }
        public int? EstimatedCur { get; set; }
        public double? ProvisionalSum { get; set; }
        public double? TotalContractValue { get; set; }
        public float? CoveredArea { get; set; }
        public float? Concrete { get; set; }
        public int? ConstPeriod { get; set; }
        public float? NoFloorEq { get; set; }
        public float? NoFloorEqHeightMtr { get; set; }
        public float? OperatorsFactor { get; set; }
        public float? DieselGalon { get; set; }
        [Column("MthlyAVGLab_Prod")]
        public float? MthlyAvglabProd { get; set; }
        [Column("MthlyAVGLab_NonProd")]
        public float? MthlyAvglabNonProd { get; set; }
        public double? ManhoursAddPercent { get; set; }
        public double? Vat { get; set; }
        [Column("OPERATORMH")]
        public double? Operatormh { get; set; }
        public double? SubTotalDry { get; set; }
        public double? ConcreteRebars { get; set; }
        [Column("DLPDuration")]
        public int? Dlpduration { get; set; }
        public double? DirectCostValue { get; set; }
        public double? PrelimsValue { get; set; }
        public byte? BuildingType { get; set; }
        public byte? BuildingLayout { get; set; }
        public int? BuildingNo { get; set; }
        public int? FloorsSuperStruct { get; set; }
        public int? FloorsSubStruct { get; set; }
        public float? ProdManhours { get; set; }
        public float? NonProdManhours { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ContractSigned { get; set; }
        [Column("TSProjId")]
        public int? TsprojId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CommencementDate { get; set; }
        [Column("ConstFinishAC")]
        public float? ConstFinishAc { get; set; }
    }
}
