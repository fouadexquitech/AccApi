using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblImportWbs")]
    public partial class TblImportWb
    {
        [Key]
        public int Seq { get; set; }
        [Column("iwProjDef")]
        [StringLength(50)]
        public string IwProjDef { get; set; }
        [Column("iwWbsCode")]
        [StringLength(75)]
        public string IwWbsCode { get; set; }
        [Column("iwLevel")]
        public byte? IwLevel { get; set; }
        [Column("iwDivision")]
        [StringLength(50)]
        public string IwDivision { get; set; }
        [Column("iwSubDiv")]
        [StringLength(50)]
        public string IwSubDiv { get; set; }
        [Column("iwTrade")]
        [StringLength(50)]
        public string IwTrade { get; set; }
        [Column("iwSubTrade")]
        [StringLength(50)]
        public string IwSubTrade { get; set; }
        [Column("iwDescription")]
        [StringLength(300)]
        public string IwDescription { get; set; }
        [Column("iwUsed")]
        public byte? IwUsed { get; set; }
        [Column("iwWbsMap")]
        [StringLength(300)]
        public string IwWbsMap { get; set; }
    }
}
