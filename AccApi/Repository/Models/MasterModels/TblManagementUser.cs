using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblManagementUsers")]
    public partial class TblManagementUser
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Column("userName")]
        [StringLength(10)]
        public string UserName { get; set; }
        [Column("mail")]
        [StringLength(50)]
        public string Mail { get; set; }
        [Column("occupation")]
        [StringLength(50)]
        public string Occupation { get; set; }
    }
}
