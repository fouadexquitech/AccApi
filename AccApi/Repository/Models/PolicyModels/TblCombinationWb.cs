using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblCombinationWBS")]
    public partial class TblCombinationWb
    {
        [Key]
        [Column("comProjSapWBS")]
        [StringLength(50)]
        public string ComProjSapWbs { get; set; }
        [Column("comProjSAPName")]
        [StringLength(75)]
        public string ComProjSapname { get; set; }
        [Column("comProjDef")]
        [StringLength(20)]
        public string ComProjDef { get; set; }
        [Column("comSponsor")]
        public int? ComSponsor { get; set; }
        [Column("comTax")]
        public byte? ComTax { get; set; }
        [Column("comDiv")]
        [StringLength(2)]
        public string ComDiv { get; set; }
        public bool? Selected { get; set; }
        public double? TotalEarning { get; set; }
        public double? Tax { get; set; }
        public double? Loans { get; set; }
        public double? Round { get; set; }
        public double? NetSalaries { get; set; }
    }
}
