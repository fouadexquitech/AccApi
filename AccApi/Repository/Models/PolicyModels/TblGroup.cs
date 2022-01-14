using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblGroups")]
    public partial class TblGroup
    {
        [Key]
        [Column("grpID")]
        [StringLength(10)]
        public string GrpId { get; set; }
        [Column("grpDesc")]
        [StringLength(40)]
        public string GrpDesc { get; set; }
        [Column("grpPWD")]
        [StringLength(10)]
        public string GrpPwd { get; set; }
        [Column("grpAdmin")]
        public bool? GrpAdmin { get; set; }
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
