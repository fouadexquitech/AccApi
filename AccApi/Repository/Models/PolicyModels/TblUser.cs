using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblUsers")]
    public partial class TblUser
    {
        [Key]
        [Column("usrID")]
        [StringLength(10)]
        public string UsrId { get; set; }
        [Column("usrDesc")]
        [StringLength(40)]
        public string UsrDesc { get; set; }
        [Column("usrPWD")]
        [StringLength(15)]
        public string UsrPwd { get; set; }
        [Column("usrAdmin")]
        public bool? UsrAdmin { get; set; }
        [Column("usrSTID")]
        public short? UsrStid { get; set; }
        public bool? AllowAccess { get; set; }
        public short? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("usrEmail")]
        [StringLength(50)]
        public string UsrEmail { get; set; }
        [Column("usrSignature")]
        [StringLength(100)]
        public string UsrSignature { get; set; }
        [Column("emailSignature", TypeName = "text")]
        public string EmailSignature { get; set; }
    }
}
