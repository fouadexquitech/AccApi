﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblSupplierPackageRevision")]
    public partial class TblSupplierPackageRevision
    {
        [Key]
        [Column("prRevId")]
        public int PrRevId { get; set; }
        [Column("prRevNo")]
        public int? PrRevNo { get; set; }
        [Column("prRevDate", TypeName = "datetime")]
        public DateTime? PrRevDate { get; set; }
        [Column("prTotPrice", TypeName = "money")]
        public decimal? PrTotPrice { get; set; }
        [Column("prPackSuppId")]
        public int? PrPackSuppId { get; set; }
        [Column("prCurrency")]
        public int? PrCurrency { get; set; }
        [Column("prExchRate")]
        public double? PrExchRate { get; set; }
    }
}
