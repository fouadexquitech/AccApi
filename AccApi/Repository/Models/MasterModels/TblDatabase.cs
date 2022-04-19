using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblDataBases")]
    public partial class TblDatabase
    {
        [Key]
        [Column("dbSeq")]
        public int DbSeq { get; set; }
        [Key]
        [Column("dbName")]
        [StringLength(50)]
        public string DbName { get; set; }
        [Key]
        [Column("dbDescription")]
        [StringLength(50)]
        public string DbDescription { get; set; }
        [Column("dbConnection")]
        [StringLength(250)]
        public string DbConnection { get; set; }
        [Column("dbServer")]
        [StringLength(50)]
        public string DbServer { get; set; }
        [Column("dbServerName")]
        [StringLength(50)]
        public string DbServerName { get; set; }
        [Column("dbActive")]
        public byte? DbActive { get; set; }
        [Column("dbUserID")]
        [StringLength(20)]
        public string DbUserId { get; set; }
        [Column("dbPass")]
        [StringLength(20)]
        public string DbPass { get; set; }
        [Column("SSRS_Server")]
        [StringLength(100)]
        public string SsrsServer { get; set; }
        [Column("SSRS_User")]
        [StringLength(20)]
        public string SsrsUser { get; set; }
        [Column("SSRS_Pass")]
        [StringLength(20)]
        public string SsrsPass { get; set; }
        [Column("SSRS_Domain")]
        [StringLength(20)]
        public string SsrsDomain { get; set; }
        [Column("dbLocation")]
        [StringLength(50)]
        public string DbLocation { get; set; }
        [Column("dbWebPortal")]
        [StringLength(50)]
        public string DbWebPortal { get; set; }
    }
}
