using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblPermissions")]
    public partial class TblPermission
    {
        [Key]
        [Column("prmFuncID")]
        [StringLength(30)]
        public string PrmFuncId { get; set; }
        [Key]
        [Column("prmGrpUsrID")]
        [StringLength(10)]
        public string PrmGrpUsrId { get; set; }
        [Key]
        [Column("prmGrpUsr")]
        [StringLength(1)]
        public string PrmGrpUsr { get; set; }
        [Column("prmRead")]
        public byte? PrmRead { get; set; }
        [Column("prmWrite")]
        public byte? PrmWrite { get; set; }
        [Column("prmUpdate")]
        public byte? PrmUpdate { get; set; }
        [Column("prmDelete")]
        public byte? PrmDelete { get; set; }
        [Column("prmUpdPeriod")]
        public short? PrmUpdPeriod { get; set; }
        public short? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
    }
}
