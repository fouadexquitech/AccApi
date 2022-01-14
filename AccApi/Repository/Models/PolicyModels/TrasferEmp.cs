using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("TrasferEmp")]
    public partial class TrasferEmp
    {
        [Key]
        [Column("empId")]
        [StringLength(10)]
        public string EmpId { get; set; }
        [Key]
        [Column(TypeName = "datetime")]
        public DateTime TransDate { get; set; }
        [Key]
        [Column("fromProj")]
        [StringLength(20)]
        public string FromProj { get; set; }
        [Key]
        [Column("toProj")]
        [StringLength(20)]
        public string ToProj { get; set; }
    }
}
