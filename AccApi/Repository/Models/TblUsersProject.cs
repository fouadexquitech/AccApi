using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblUsersProjects")]
    public partial class TblUsersProject
    {
        [Key]
        [Column("UsrProjID")]
        public int UsrProjId { get; set; }
        [Required]
        [Column("usrID")]
        [StringLength(10)]
        public string UsrId { get; set; }
        [Column("usrDesc")]
        [StringLength(40)]
        public string UsrDesc { get; set; }
        [Column("usrPWD")]
        [StringLength(10)]
        public string UsrPwd { get; set; }
        [Column("usrAdmin")]
        public bool? UsrAdmin { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("usrSTID")]
        public short? UsrStid { get; set; }
        [Column("usrSiteOffice")]
        public byte? UsrSiteOffice { get; set; }
        [Column("usrProjectDataBase")]
        [StringLength(15)]
        public string UsrProjectDataBase { get; set; }
        [Column("usrProjectServer")]
        [StringLength(15)]
        public string UsrProjectServer { get; set; }
        [Column("usrProjectID")]
        public int? UsrProjectId { get; set; }
    }
}
