using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tmpImportAttendance")]
    public partial class TmpImportAttendance
    {
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [Column("ID")]
        [StringLength(10)]
        public string Id { get; set; }
        [StringLength(20)]
        public string ProjectCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Timein { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Timeout { get; set; }
        [Column("WBS")]
        [StringLength(30)]
        public string Wbs { get; set; }
        [StringLength(50)]
        public string Team { get; set; }
        [StringLength(50)]
        public string Area { get; set; }
    }
}
