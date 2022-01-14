using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblOccupGroup")]
    public partial class TblOccupGroup
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Column("grpName")]
        [StringLength(50)]
        public string GrpName { get; set; }
        [Column("grpSort")]
        public int? GrpSort { get; set; }
    }
}
