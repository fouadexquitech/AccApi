using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblUsersProjects")]
    public partial class TblUsersProject
    {
        [Key]
        [Column("UsrProjID")]
        public int UsrProjId { get; set; }
        [Key]
        [Column("usrID")]
        [StringLength(20)]
        public string UsrId { get; set; }
    }
}
