using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblGroupsUsers")]
    public partial class TblGroupsUser
    {
        [Key]
        [Column("guUser")]
        [StringLength(10)]
        public string GuUser { get; set; }
        [Key]
        [Column("guGroup")]
        [StringLength(10)]
        public string GuGroup { get; set; }
        public short? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
    }
}
