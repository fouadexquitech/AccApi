using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("TempImportAcc")]
    public partial class TempImportAcc
    {
        [StringLength(255)]
        public string Account { get; set; }
        [StringLength(255)]
        public string Cat { get; set; }
        public double? Num { get; set; }
        [StringLength(255)]
        public string Designation { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [Column("co")]
        public double? Co { get; set; }
        [Column("oc_1")]
        [StringLength(255)]
        public string Oc1 { get; set; }
        [Column("cu_1")]
        [StringLength(255)]
        public string Cu1 { get; set; }
        [Column("debit_1")]
        public double? Debit1 { get; set; }
        [Column("credit_1")]
        public double? Credit1 { get; set; }
        [Column("balance_db")]
        public double? BalanceDb { get; set; }
        [Column("balance_cr")]
        public double? BalanceCr { get; set; }
        [Column("cu_2")]
        [StringLength(255)]
        public string Cu2 { get; set; }
        [Column("debit_2")]
        public double? Debit2 { get; set; }
        [Column("credit_2")]
        public double? Credit2 { get; set; }
        [Column("balance")]
        public double? Balance { get; set; }
        [Column("debit_or")]
        public double? DebitOr { get; set; }
        [Column("credit_or")]
        public double? CreditOr { get; set; }
        [Column("debit_ll")]
        public double? DebitLl { get; set; }
        [Column("credit_ll")]
        public double? CreditLl { get; set; }
        [Column("debit_l2")]
        public double? DebitL2 { get; set; }
        [Column("credit_l2")]
        public double? CreditL2 { get; set; }
        [Column("SAP")]
        public double? Sap { get; set; }
        public double? Adjustment { get; set; }
    }
}
