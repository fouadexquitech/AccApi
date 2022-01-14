using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblDistribFormanWBS")]
    public partial class TblDistribFormanWb
    {
        [Key]
        [Column("disformanid")]
        public int Disformanid { get; set; }
        [Key]
        [Column("disprojectdef")]
        [StringLength(50)]
        public string Disprojectdef { get; set; }
        [Key]
        [Column("disWBS")]
        [StringLength(50)]
        public string DisWbs { get; set; }
        [Column("disfromdate", TypeName = "datetime")]
        public DateTime? Disfromdate { get; set; }
        [Column("distodate", TypeName = "datetime")]
        public DateTime? Distodate { get; set; }
        [Column("disinsertby")]
        [StringLength(50)]
        public string Disinsertby { get; set; }
        [Column("disinsertDate")]
        [StringLength(50)]
        public string DisinsertDate { get; set; }
    }
}
