using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    public partial class Parameter
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        public double? Per { get; set; }
        public double? B1 { get; set; }
        public double? B2 { get; set; }
        public double? B3 { get; set; }
        public double? B4 { get; set; }
        public double? B5 { get; set; }
        public double? L1 { get; set; }
        public double? L2 { get; set; }
        public double? L3 { get; set; }
        public double? L4 { get; set; }
        public double? L5 { get; set; }
        public double? M1 { get; set; }
        public double? M2 { get; set; }
        public double? M3 { get; set; }
        public double? M4 { get; set; }
        public double? M5 { get; set; }
        public double? S1 { get; set; }
        public double? S2 { get; set; }
        public double? S3 { get; set; }
        public double? S4 { get; set; }
        public double? S5 { get; set; }
        public double? E1 { get; set; }
        public double? E2 { get; set; }
        public double? E3 { get; set; }
        public double? E4 { get; set; }
        public double? E5 { get; set; }
        public double? B6 { get; set; }
        public double? L6 { get; set; }
        public double? M6 { get; set; }
        public double? S6 { get; set; }
        public double? E6 { get; set; }
        public double? O6 { get; set; }
        public double? O1 { get; set; }
        public double? O2 { get; set; }
        public double? O3 { get; set; }
        public double? O4 { get; set; }
        public double? O5 { get; set; }
        [Column("txtBudOD")]
        public double? TxtBudOd { get; set; }
        [Column("txtSalEstimated_Tot")]
        public double? TxtSalEstimatedTot { get; set; }
        [Column("txtVisaEstimated_Tot")]
        public double? TxtVisaEstimatedTot { get; set; }
        [Column("txtAffairEstimated_Tot")]
        public double? TxtAffairEstimatedTot { get; set; }
        [Column("txtAllowEstimated_Tot")]
        public double? TxtAllowEstimatedTot { get; set; }
        [Column("txtMedEstimated_Tot")]
        public double? TxtMedEstimatedTot { get; set; }
        [Column("txtBudDeprecRent")]
        public double? TxtBudDeprecRent { get; set; }
        [Column("txtBudFuel")]
        public double? TxtBudFuel { get; set; }
        [Column("txtBudOperator")]
        public double? TxtBudOperator { get; set; }
        [Column("txtFWEstimated_Tot")]
        public double? TxtFwestimatedTot { get; set; }
        [Column("txtPLEstimated_Tot")]
        public double? TxtPlestimatedTot { get; set; }
        [Column("txtFinEstimated")]
        public double? TxtFinEstimated { get; set; }
        [Column("txtSalEstimated")]
        public double? TxtSalEstimated { get; set; }
        [Column("txtVisaEstimated")]
        public double? TxtVisaEstimated { get; set; }
        [Column("txtAffairEstimated")]
        public double? TxtAffairEstimated { get; set; }
        [Column("txtAllowEstimated")]
        public double? TxtAllowEstimated { get; set; }
        [Column("txtMedEstimated")]
        public double? TxtMedEstimated { get; set; }
        [Column("txtDepEstimated")]
        public double? TxtDepEstimated { get; set; }
        [Column("txtFuelEstimated")]
        public double? TxtFuelEstimated { get; set; }
        [Column("txtOperatorEstimated")]
        public double? TxtOperatorEstimated { get; set; }
        [Column("txtFWEstimated")]
        public double? TxtFwestimated { get; set; }
        [Column("txtPLEstimated")]
        public double? TxtPlestimated { get; set; }
        [Column("txtFinActual")]
        public double? TxtFinActual { get; set; }
        [Column("txtSalActual")]
        public double? TxtSalActual { get; set; }
        [Column("txtVisaActual")]
        public double? TxtVisaActual { get; set; }
        [Column("txtAffairActual")]
        public double? TxtAffairActual { get; set; }
        [Column("txtAllowActual")]
        public double? TxtAllowActual { get; set; }
        [Column("txtMedActual")]
        public double? TxtMedActual { get; set; }
        [Column("txtDepActual")]
        public double? TxtDepActual { get; set; }
        [Column("txtFuelActual")]
        public double? TxtFuelActual { get; set; }
        [Column("txtOperatorActual")]
        public double? TxtOperatorActual { get; set; }
        [Column("txtFwActual")]
        public double? TxtFwActual { get; set; }
        [Column("txtPLActual")]
        public double? TxtPlactual { get; set; }
    }
}
