using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblInsCompany")]
    public partial class TblInsCompany
    {
        [Key]
        [Column("comID")]
        public int ComId { get; set; }
        [Column("comName")]
        [StringLength(255)]
        public string ComName { get; set; }
        [Column("comPolicyNo")]
        [StringLength(255)]
        public string ComPolicyNo { get; set; }
        [Column("comMail")]
        [StringLength(255)]
        public string ComMail { get; set; }
        [Column("comLocation")]
        [StringLength(255)]
        public string ComLocation { get; set; }
    }
}
