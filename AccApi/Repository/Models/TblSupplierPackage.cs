using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblSupplierPackages")]
    public partial class TblSupplierPackage
    {
        [Key]
        [Column("spPackSuppId")]
        public int SpPackSuppId { get; set; }
        [Column("spPackageId")]
        public int? SpPackageId { get; set; }
        [Column("spSupplierId")]
        public int? SpSupplierId { get; set; }
        [Column("spByBoq")]
        public byte? SpByBoq { get; set; }
        public bool? TecCondSent { get; set; }
        public bool? IsSynched { get; set; }
        [Column("insertDate", TypeName = "datetime")]
        public DateTime? InsertDate { get; set; }
    }
}
