using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEproTsCompany")]
    public partial class TblEproTsCompany
    {
        [Key]
        [StringLength(10)]
        public string EproCompany { get; set; }
        [Key]
        [StringLength(200)]
        public string EproDatabase { get; set; }
        [Key]
        [Column("TSSponsor")]
        public int Tssponsor { get; set; }
        [Column("SAPCompanyCode")]
        [StringLength(5)]
        public string SapcompanyCode { get; set; }
        [StringLength(10)]
        public string TimeAdmin { get; set; }
    }
}
