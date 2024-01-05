using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblUsersCompany")]
    public partial class TblUsersCompany
    {
        [Key]
        [Column("ucUsrID")]
        [StringLength(20)]
        public string UcUsrId { get; set; }
        [Key]
        [Column("ucCompanyID")]
        public int UcCompanyId { get; set; }
        [Key]
        [Column("ucProjID")]
        public int UcProjId { get; set; }
        [Column("ucNotes")]
        [StringLength(500)]
        public string UcNotes { get; set; }
        [Column("ucInsertBy")]
        [StringLength(20)]
        public string UcInsertBy { get; set; }
        [Column("ucInsertTime", TypeName = "datetime")]
        public DateTime? UcInsertTime { get; set; }
    }
}
