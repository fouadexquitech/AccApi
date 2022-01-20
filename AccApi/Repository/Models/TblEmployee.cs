using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblEmployee")]
    public partial class TblEmployee
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Column("empPSC")]
        public int? EmpPsc { get; set; }
        [Column("empName")]
        [StringLength(100)]
        public string EmpName { get; set; }
        [Column("empjob")]
        [StringLength(50)]
        public string Empjob { get; set; }
        [Column("empCeiling")]
        public int? EmpCeiling { get; set; }
    }
}
