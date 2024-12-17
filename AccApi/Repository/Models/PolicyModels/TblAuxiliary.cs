using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblAuxiliary")]
    public partial class TblAuxiliary
    {
        [Key]
        [Column("axDiv")]
        [StringLength(2)]
        public string AxDiv { get; set; }
        [Key]
        [Column("axSubDiv")]
        [StringLength(2)]
        public string AxSubDiv { get; set; }
        [Key]
        [Column("axTrade")]
        [StringLength(2)]
        public string AxTrade { get; set; }
        [Key]
        [Column("axNatioID")]
        public int AxNatioId { get; set; }
        [Key]
        [Column("axJob")]
        public int AxJob { get; set; }
        [Key]
        [Column("axCampID")]
        public int AxCampId { get; set; }
        [Key]
        [Column("axDateFrom", TypeName = "date")]
        public DateTime AxDateFrom { get; set; }
        [Key]
        [Column("axDateTo", TypeName = "date")]
        public DateTime AxDateTo { get; set; }
        [Column("axNatioAuxRate")]
        public double? AxNatioAuxRate { get; set; }
        [Column("axCampAuxRate")]
        public double? AxCampAuxRate { get; set; }
        [Column("axTransAuxRate")]
        public double? AxTransAuxRate { get; set; }
        [Column("axOtherAuxRate")]
        public double? AxOtherAuxRate { get; set; }
        [Column("axProjCode")]
        [StringLength(15)]
        public string AxProjCode { get; set; }
        [Column("axDedProvidentFund")]
        public double? AxDedProvidentFund { get; set; }
        [Column("axDedSocialIns")]
        public double? AxDedSocialIns { get; set; }
        [Column("axDedMedicalFixed")]
        public double? AxDedMedicalFixed { get; set; }
        [Column("axDedUnionsFund")]
        public double? AxDedUnionsFund { get; set; }
        [Column("axContProvidentFund")]
        public double? AxContProvidentFund { get; set; }
        [Column("axContUnions")]
        public double? AxContUnions { get; set; }
        [Column("axContLeave")]
        public double? AxContLeave { get; set; }
        [Column("axContSocialIns")]
        public double? AxContSocialIns { get; set; }
        [Column("axContMedicalFixed")]
        public double? AxContMedicalFixed { get; set; }
        [Column("axContUnion")]
        public double? AxContUnion { get; set; }
        [Column("axContChochesion")]
        public double? AxContChochesion { get; set; }
        [Column("axContReduduncy")]
        public double? AxContReduduncy { get; set; }
        [Column("axContIndustrial")]
        public double? AxContIndustrial { get; set; }
        [Column("axContMedicalUnion")]
        public double? AxContMedicalUnion { get; set; }
        [Column("axSponsor")]
        public int? AxSponsor { get; set; }
        [Column("axDedIncomeTax")]
        public double? AxDedIncomeTax { get; set; }
        [Column("axContGHS")]
        public double? AxContGhs { get; set; }
        [Column("LUser")]
        [StringLength(50)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdateDate { get; set; }
        [StringLength(50)]
        public string LastUpdate { get; set; }
    }
}
