using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblSubConstractorSalary")]
    public partial class TblSubConstractorSalary
    {
        [Key]
        [Column("subConID")]
        public int SubConId { get; set; }
        [Key]
        [StringLength(3)]
        public string SubConClass { get; set; }
        public double? SubConClassSalary { get; set; }
    }
}
