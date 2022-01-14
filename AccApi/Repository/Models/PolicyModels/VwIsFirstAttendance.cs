using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    public partial class VwIsFirstAttendance
    {
        [Column(TypeName = "datetime")]
        public DateTime? SecAttDate { get; set; }
        [Column("disLab")]
        [StringLength(8)]
        public string DisLab { get; set; }
        public int? Cnt { get; set; }
        public int? SecAttSec { get; set; }
        public int? FirstAttSec { get; set; }
        [Column("disProjectDef")]
        [StringLength(9)]
        public string DisProjectDef { get; set; }
    }
}
