using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblUsersProjects")]
    public partial class TblUsersProject
    {
        public TblUsersProject()
        {
            TblUsersProjectsDefs = new HashSet<TblUsersProjectsDef>();
        }

        [Key]
        [Column("upUserID")]
        [StringLength(10)]
        public string UpUserId { get; set; }
        [Key]
        [Column("upProject")]
        public int UpProject { get; set; }

        [InverseProperty(nameof(TblUsersProjectsDef.Upd))]
        public virtual ICollection<TblUsersProjectsDef> TblUsersProjectsDefs { get; set; }
    }
}
