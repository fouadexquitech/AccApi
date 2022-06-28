using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tmpImportLaborHistogram")]
    public partial class TmpImportLaborHistogram
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Mth { get; set; }
        [StringLength(20)]
        public string Div { get; set; }
        [StringLength(250)]
        public string Occupation { get; set; }
        public int? Manpowers { get; set; }
    }
}
