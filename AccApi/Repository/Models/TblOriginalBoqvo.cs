using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblOriginalBOQVO")]
    public partial class TblOriginalBoqvo
    {
        [Key]
        [Column("Item-o")]
        [StringLength(25)]
        public string ItemO { get; set; }
        [Column("Project-o")]
        [StringLength(20)]
        public string ProjectO { get; set; }
        [Column("section-o")]
        [StringLength(30)]
        public string SectionO { get; set; }
        [Column("Description-o")]
        public string DescriptionO { get; set; }
        [Column("Unit-o")]
        [StringLength(255)]
        public string UnitO { get; set; }
        [Column("Qty-o")]
        public double? QtyO { get; set; }
        public int? Area { get; set; }
        public int? Scope { get; set; }
        [StringLength(10)]
        public string Sort { get; set; }
        public bool? Subcontracting { get; set; }
        public bool? Selected { get; set; }
        public double? Submitted { get; set; }
        [Column("obSheet")]
        public byte? ObSheet { get; set; }
        [Column("obInDirect")]
        public bool? ObInDirect { get; set; }
        [Column("obSeq")]
        public int ObSeq { get; set; }
        [Column("obSheetDesc")]
        [StringLength(500)]
        public string ObSheetDesc { get; set; }
        public int? RowNumber { get; set; }
        [StringLength(50)]
        public string RefNumber { get; set; }
        public double? UnitRate { get; set; }
        [StringLength(12)]
        public string Zone { get; set; }
        [StringLength(10)]
        public string Prefix { get; set; }
        public bool? CandyTemplate { get; set; }
        [StringLength(25)]
        public string Luser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column("obBOQSellRate")]
        public double? ObBoqsellRate { get; set; }
        [Column("obBOQSellTotPrice")]
        public double? ObBoqsellTotPrice { get; set; }
        [Column("obSkipWBSQty")]
        public bool? ObSkipWbsqty { get; set; }
        public string C1 { get; set; }
        public string C2 { get; set; }
        public string C3 { get; set; }
        public string C4 { get; set; }
        public string C5 { get; set; }
        public string C6 { get; set; }
        public double? QtyScope { get; set; }
        [Column("obBillPage")]
        [StringLength(50)]
        public string ObBillPage { get; set; }
        [Column("obPriceCode")]
        [StringLength(50)]
        public string ObPriceCode { get; set; }
        [Column("obLevel")]
        [StringLength(50)]
        public string ObLevel { get; set; }
        [Column("obBillQty")]
        public double? ObBillQty { get; set; }
        public string L1 { get; set; }
        public string L2 { get; set; }
        public string L3 { get; set; }
        public string L4 { get; set; }
        public string L5 { get; set; }
        public string L6 { get; set; }
        [Column(TypeName = "money")]
        public decimal? BillSubmitted { get; set; }
        public int? GroupId { get; set; }
        [Column("C1Ref")]
        [StringLength(100)]
        public string C1ref { get; set; }
        [Column("C2Ref")]
        [StringLength(100)]
        public string C2ref { get; set; }
        [Column("C3Ref")]
        [StringLength(100)]
        public string C3ref { get; set; }
        [Column("C4Ref")]
        [StringLength(100)]
        public string C4ref { get; set; }
        [Column("C5Ref")]
        [StringLength(100)]
        public string C5ref { get; set; }
        [Column("C6Ref")]
        [StringLength(100)]
        public string C6ref { get; set; }
        [Column("C7Ref")]
        [StringLength(100)]
        public string C7ref { get; set; }
        [Column("C8Ref")]
        [StringLength(100)]
        public string C8ref { get; set; }
        [Column("C9Ref")]
        [StringLength(100)]
        public string C9ref { get; set; }
        [Column("C10Ref")]
        [StringLength(100)]
        public string C10ref { get; set; }
        public string C7 { get; set; }
        public string C8 { get; set; }
        public string C9 { get; set; }
        public string C10 { get; set; }
        [Column("obItemCode")]
        [StringLength(100)]
        public string ObItemCode { get; set; }
        [Column("L1Ref")]
        [StringLength(100)]
        public string L1ref { get; set; }
        [Column("L2Ref")]
        [StringLength(100)]
        public string L2ref { get; set; }
        [Column("L3Ref")]
        [StringLength(100)]
        public string L3ref { get; set; }
        [Column("L4Ref")]
        [StringLength(100)]
        public string L4ref { get; set; }
        [Column("L5Ref")]
        [StringLength(100)]
        public string L5ref { get; set; }
        [Column("L6Ref")]
        [StringLength(100)]
        public string L6ref { get; set; }
        [Column("L7Ref")]
        [StringLength(100)]
        public string L7ref { get; set; }
        [Column("L8Ref")]
        [StringLength(100)]
        public string L8ref { get; set; }
        [Column("L9Ref")]
        [StringLength(100)]
        public string L9ref { get; set; }
        [Column("L10Ref")]
        [StringLength(100)]
        public string L10ref { get; set; }
        public string L7 { get; set; }
        public string L8 { get; set; }
        public string L9 { get; set; }
        public string L10 { get; set; }
        public string C11 { get; set; }
        public string C12 { get; set; }
        public string C13 { get; set; }
        public string C14 { get; set; }
        public string C15 { get; set; }
        [Column("obBackUpDate", TypeName = "datetime")]
        public DateTime? ObBackUpDate { get; set; }
        [Column("obTradeDesc")]
        [StringLength(500)]
        public string ObTradeDesc { get; set; }
        [Column("obExchangeTo")]
        [StringLength(20)]
        public string ObExchangeTo { get; set; }
        [Column("obExchangeRate")]
        public double? ObExchangeRate { get; set; }
        [Column("insertedBy")]
        [StringLength(20)]
        public string InsertedBy { get; set; }
        [Column("insertedDate", TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        public byte? ExportedToSupplier { get; set; }
        public bool? IsSynched { get; set; }
        [Column("obStatus")]
        [StringLength(50)]
        public string ObStatus { get; set; }
    }
}
