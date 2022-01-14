using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblLaborHistogram")]
    public partial class TblLaborHistogram
    {
        [Key]
        [Column("lhseq")]
        public int Lhseq { get; set; }
        [Column("lhMth", TypeName = "datetime")]
        public DateTime? LhMth { get; set; }
        [Column("lhDiv")]
        [StringLength(20)]
        public string LhDiv { get; set; }
        [Column("lhOccupation")]
        public int? LhOccupation { get; set; }
        public int? Manpowers { get; set; }
        [Column("lhProjID")]
        public int? LhProjId { get; set; }
    }
}
