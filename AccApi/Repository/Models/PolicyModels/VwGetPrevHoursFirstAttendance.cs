using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    public partial class VwGetPrevHoursFirstAttendance
    {
        public double? PrevHrs { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? PrevDate { get; set; }
        [StringLength(15)]
        public string PrevLab { get; set; }
        public double? PrevPrevHrs { get; set; }
        [Column("disProjectDef")]
        [StringLength(9)]
        public string DisProjectDef { get; set; }
    }
}
