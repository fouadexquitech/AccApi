using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblCompanyCode")]
    public partial class TblCompanyCode
    {
        [Key]
        [StringLength(10)]
        public string CompanyCode { get; set; }
        [StringLength(50)]
        public string CompanyName { get; set; }
        [StringLength(50)]
        public string CompanyLocation { get; set; }
        [Column("CompanyPDefStart")]
        [StringLength(10)]
        public string CompanyPdefStart { get; set; }
        [Column("CompanyPDefLen")]
        public int? CompanyPdefLen { get; set; }
    }
}
