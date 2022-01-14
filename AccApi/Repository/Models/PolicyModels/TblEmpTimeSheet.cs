using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEmpTimeSheet")]
    public partial class TblEmpTimeSheet
    {
        [Key]
        [Column("etsProject")]
        [StringLength(9)]
        public string EtsProject { get; set; }
        [Key]
        [Column("etsProjectDef")]
        [StringLength(9)]
        public string EtsProjectDef { get; set; }
        [Key]
        [Column("etsDate", TypeName = "datetime")]
        public DateTime EtsDate { get; set; }
        [Key]
        [Column("etsEmpPsc")]
        [StringLength(10)]
        public string EtsEmpPsc { get; set; }
        [Column("etsEmpName")]
        [StringLength(100)]
        public string EtsEmpName { get; set; }
        [Column("etsEmpType")]
        public byte? EtsEmpType { get; set; }
        [Column("etsJobTitle")]
        public int? EtsJobTitle { get; set; }
        [Column("etsStatus")]
        public int? EtsStatus { get; set; }
        [Column("etsTimeIn", TypeName = "datetime")]
        public DateTime? EtsTimeIn { get; set; }
        [Column("etsTimeOut", TypeName = "datetime")]
        public DateTime? EtsTimeOut { get; set; }
        [Column("etsRemark")]
        [StringLength(255)]
        public string EtsRemark { get; set; }
        [Column("etsStatus1")]
        public int? EtsStatus1 { get; set; }
        [Column("etsStatus2")]
        public int? EtsStatus2 { get; set; }
        [Column("etsExport")]
        public byte? EtsExport { get; set; }
        [Column("etsSent")]
        public byte? EtsSent { get; set; }
        [Column("etsProjectID")]
        public int? EtsProjectId { get; set; }
    }
}
