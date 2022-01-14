using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblDesignation")]
    public partial class TblDesignation
    {
        [Key]
        [Column("ID")]
        [StringLength(10)]
        public string Id { get; set; }
        [StringLength(500)]
        public string Name { get; set; }
        [StringLength(200)]
        public string EmpGrp { get; set; }
        [StringLength(200)]
        public string Payroll { get; set; }
        [StringLength(200)]
        public string Job { get; set; }
        [Column("nation")]
        [StringLength(200)]
        public string Nation { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
    }
}
