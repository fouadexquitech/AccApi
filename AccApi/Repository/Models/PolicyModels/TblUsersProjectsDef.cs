using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblUsersProjectsDef")]
    public partial class TblUsersProjectsDef
    {
        [Key]
        [Column("updUserID")]
        [StringLength(10)]
        public string UpdUserId { get; set; }
        [Key]
        [Column("updProject")]
        public int UpdProject { get; set; }
        [Key]
        [Column("updProjCode")]
        [StringLength(20)]
        public string UpdProjCode { get; set; }

        [ForeignKey("UpdUserId,UpdProject")]
        [InverseProperty(nameof(TblUsersProject.TblUsersProjectsDefs))]
        public virtual TblUsersProject Upd { get; set; }
    }
}
