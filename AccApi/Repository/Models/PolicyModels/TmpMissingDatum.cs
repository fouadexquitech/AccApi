using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tmpMissingData")]
    public partial class TmpMissingDatum
    {
        [Column("labname")]
        [StringLength(75)]
        public string Labname { get; set; }
        [Column("labId")]
        [StringLength(10)]
        public string LabId { get; set; }
        [Column("codDescE")]
        [StringLength(50)]
        public string CodDescE { get; set; }
        [Column("disDate", TypeName = "datetime")]
        public DateTime? DisDate { get; set; }
        [StringLength(50)]
        public string ProjectName { get; set; }
        [Column("WBS")]
        [StringLength(50)]
        public string Wbs { get; set; }
        [StringLength(30)]
        public string UserName { get; set; }
        [StringLength(100)]
        public string Type { get; set; }
    }
}
