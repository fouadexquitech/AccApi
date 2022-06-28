using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tmpReplaceWBS")]
    public partial class TmpReplaceWb
    {
        [Column("File NO")]
        [StringLength(150)]
        public string FileNo { get; set; }
        [StringLength(50)]
        public string Foreman { get; set; }
        [StringLength(50)]
        public string Area { get; set; }
        [Column("From Date", TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column("To Date", TypeName = "datetime")]
        public DateTime? ToDate { get; set; }
        [Column("Old WBS")]
        [StringLength(255)]
        public string OldWbs { get; set; }
        [Column("New WBS")]
        [StringLength(255)]
        public string NewWbs { get; set; }
        [Column("Old Project Definition")]
        [StringLength(255)]
        public string OldProjectDefinition { get; set; }
        [Column("New Project Definition")]
        [StringLength(255)]
        public string NewProjectDefinition { get; set; }
        [StringLength(30)]
        public string NewProjectCode { get; set; }
        [StringLength(50)]
        public string NewArea { get; set; }
        [Column("New File No")]
        [StringLength(8)]
        public string NewFileNo { get; set; }
        [StringLength(150)]
        public string OldSponsor { get; set; }
        [StringLength(150)]
        public string NewSponsor { get; set; }
    }
}
