using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblCompany")]
    public partial class TblCompany
    {
        [Key]
        [Column("comProjID")]
        public int ComProjId { get; set; }
        [Key]
        [Column("comProjCode")]
        [StringLength(9)]
        public string ComProjCode { get; set; }
        [Column("comCompany")]
        [StringLength(10)]
        public string ComCompany { get; set; }
        [Column("comOrderInDailyReport")]
        public int? ComOrderInDailyReport { get; set; }
        [Column("comCostCenter")]
        [StringLength(20)]
        public string ComCostCenter { get; set; }
    }
}
