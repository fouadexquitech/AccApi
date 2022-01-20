using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("tblCompanies")]
    public partial class TblCompany
    {
        [Column("comSeq")]
        public int ComSeq { get; set; }
        [Required]
        [Column("comName")]
        [StringLength(35)]
        public string ComName { get; set; }
        [Required]
        [Column("comConnection")]
        [StringLength(15)]
        public string ComConnection { get; set; }
        [Required]
        [Column("comServer")]
        [StringLength(15)]
        public string ComServer { get; set; }
        [Column("comActive")]
        public byte ComActive { get; set; }
    }
}
