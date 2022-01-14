using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblCompany")]
    [Index(nameof(ComCompany), Name = "IX_tblCompany")]
    [Index(nameof(ComProjCode), Name = "IX_tblCompany_1", IsUnique = true)]
    public partial class TblCompany
    {
        [Key]
        [Column("comProjID")]
        public int ComProjId { get; set; }
        [Key]
        [Column("comProjCode")]
        [StringLength(20)]
        public string ComProjCode { get; set; }
        [Column("comCompany")]
        [StringLength(50)]
        public string ComCompany { get; set; }
        [Column("comOrderInDailyReport")]
        public int? ComOrderInDailyReport { get; set; }
        [Column("comCostCenter")]
        [StringLength(20)]
        public string ComCostCenter { get; set; }
    }
}
