using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblInsuranceCompany")]
    public partial class TblInsuranceCompany
    {
        [Key]
        [Column("InsurancecomID")]
        public int InsurancecomId { get; set; }
        [StringLength(255)]
        public string InsurancecomName { get; set; }
    }
}
