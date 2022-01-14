using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblLaborVacations")]
    public partial class TblLaborVacation
    {
        [Key]
        [Column("lvLabor")]
        [StringLength(10)]
        public string LvLabor { get; set; }
        [Key]
        [Column("lvFromDate", TypeName = "datetime")]
        public DateTime LvFromDate { get; set; }
        [Key]
        [Column("lvToDate", TypeName = "datetime")]
        public DateTime LvToDate { get; set; }
    }
}
