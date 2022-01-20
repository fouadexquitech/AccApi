using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblPayrollDataBases")]
    public partial class TblPayrollDatabase
    {
        [Key]
        [Column("dbName")]
        [StringLength(50)]
        public string DbName { get; set; }
        [Key]
        [Column("dbDescription")]
        [StringLength(50)]
        public string DbDescription { get; set; }
        [Column("dbConnection")]
        [StringLength(150)]
        public string DbConnection { get; set; }
        [Column("dbServer")]
        [StringLength(50)]
        public string DbServer { get; set; }
        [Column("dbActive")]
        public byte? DbActive { get; set; }
        [Column("dbUserID")]
        [StringLength(20)]
        public string DbUserId { get; set; }
        [Column("dbPass")]
        [StringLength(20)]
        public string DbPass { get; set; }
        [Column("dbLocation")]
        [StringLength(50)]
        public string DbLocation { get; set; }
    }
}
