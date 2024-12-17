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
        [StringLength(255)]
        public string FileNo { get; set; }
        [StringLength(255)]
        public string Foreman { get; set; }
        [StringLength(255)]
        public string Area { get; set; }
        [Column("From Date")]
        [StringLength(255)]
        public string FromDate { get; set; }
        [Column("To Date")]
        [StringLength(255)]
        public string ToDate { get; set; }
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
        [StringLength(255)]
        public string NewProjectCode { get; set; }
        [StringLength(255)]
        public string NewArea { get; set; }
        [Column("New File No")]
        [StringLength(255)]
        public string NewFileNo { get; set; }
        [StringLength(255)]
        public string OldSponsor { get; set; }
        [StringLength(255)]
        public string NewSponsor { get; set; }
        [Column("projId")]
        [StringLength(255)]
        public string ProjId { get; set; }
        [StringLength(255)]
        public string NewprojId { get; set; }
        [Column("projCode")]
        [StringLength(255)]
        public string ProjCode { get; set; }
        [StringLength(255)]
        public string NewProjCode { get; set; }
        [StringLength(255)]
        public string NewForeman { get; set; }
    }
}
