using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblLastLogon")]
    public partial class TblLastLogon
    {
        [Key]
        [Column("llLogonDate", TypeName = "datetime")]
        public DateTime LlLogonDate { get; set; }
        [Column("conCatalog")]
        [StringLength(20)]
        public string ConCatalog { get; set; }
        [Column("conServer")]
        [StringLength(20)]
        public string ConServer { get; set; }
        [Column("conUserID")]
        [StringLength(20)]
        public string ConUserId { get; set; }
        [Column("conPwd")]
        [StringLength(20)]
        public string ConPwd { get; set; }
    }
}
