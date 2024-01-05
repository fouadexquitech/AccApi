using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblCJI3_SAP")]
    public partial class TblCji3Sap
    {
        [Column("Creation Date")]
        public string CreationDate { get; set; }
        [Column("Document Date")]
        public string DocumentDate { get; set; }
        [Column("Posting Date")]
        public string PostingDate { get; set; }
        public string Project { get; set; }
        [Column("WBS")]
        public string Wbs { get; set; }
        [Column("WBS Description")]
        public string WbsDescription { get; set; }
        [Column("Company Code")]
        public string CompanyCode { get; set; }
        public string Period { get; set; }
        [Column("Fiscal Year")]
        public string FiscalYear { get; set; }
        [Column("Purchase Order Text")]
        public string PurchaseOrderText { get; set; }
        public string Name { get; set; }
        [Column("Total quantity")]
        [StringLength(200)]
        public string TotalQuantity { get; set; }
        [Column("Unit of Measure")]
        public string UnitOfMeasure { get; set; }
        [Column("Amount in Object Currency")]
        public double? AmountInObjectCurrency { get; set; }
        [Column("Object Currency")]
        public string ObjectCurrency { get; set; }
        [Column("Amount in Controling Area Currency")]
        public double? AmountInControlingAreaCurrency { get; set; }
        [Column("Controling Area Currency")]
        public string ControlingAreaCurrency { get; set; }
        [Column("Cost Element")]
        public string CostElement { get; set; }
        [Column("Cost Element Name")]
        public string CostElementName { get; set; }
        [Column("Cost Element Description")]
        public string CostElementDescription { get; set; }
        [Column("Offsetting Account Number")]
        public string OffsettingAccountNumber { get; set; }
        [Column("Offsetting Account Type")]
        public string OffsettingAccountType { get; set; }
        [Column("Name of Offsetting Account")]
        public string NameOfOffsettingAccount { get; set; }
        [Column("Document Header Text")]
        public string DocumentHeaderText { get; set; }
        [Column("Material Number")]
        public string MaterialNumber { get; set; }
        [Column("Material description")]
        public string MaterialDescription { get; set; }
        [Column("Purchasing Document Number")]
        public string PurchasingDocumentNumber { get; set; }
        [Column("Item Number of Purchasing Document")]
        public string ItemNumberOfPurchasingDocument { get; set; }
        [Column("Reference Document Number")]
        public string ReferenceDocumentNumber { get; set; }
        [Column("Project Area")]
        public string ProjectArea { get; set; }
        [Column("Document Type of Reference Document")]
        public string DocumentTypeOfReferenceDocument { get; set; }
        [Column("Object Type")]
        public string ObjectType { get; set; }
        [Column("User Name")]
        public string UserName { get; set; }
        [Key]
        [Column("Document Number")]
        [StringLength(100)]
        public string DocumentNumber { get; set; }
        [Key]
        [Column("Posting Row")]
        [StringLength(100)]
        public string PostingRow { get; set; }
        [Column("Document Type")]
        public string DocumentType { get; set; }
        [Column("CO Business Transaction")]
        public string CoBusinessTransaction { get; set; }
        [Column("Original CO Business Transaction")]
        public string OriginalCoBusinessTransaction { get; set; }
        [Column("Cost Element Group")]
        public string CostElementGroup { get; set; }
        [Column("Cost Element Group Name")]
        public string CostElementGroupName { get; set; }
        public string Vendor { get; set; }
        [Column("Vendor Name")]
        public string VendorName { get; set; }
        public string Customer { get; set; }
        [Column("Customer name")]
        public string CustomerName { get; set; }
        [Column("fileName")]
        public string FileName { get; set; }
        [Column("insertDateFromFTP", TypeName = "datetime")]
        public DateTime? InsertDateFromFtp { get; set; }
    }
}
