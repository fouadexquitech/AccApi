using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblActivitiesGroup")]
    public partial class TblActivitiesGroup
    {
        [Key]
        [Column("grpID")]
        public int GrpId { get; set; }
        [Column("grpDesc")]
        [StringLength(100)]
        public string GrpDesc { get; set; }
        [Column("grpProjectCode")]
        [StringLength(50)]
        public string GrpProjectCode { get; set; }
    }
}
