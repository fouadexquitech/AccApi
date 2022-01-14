using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tmpWeekEndDays")]
    public partial class TmpWeekEndDay
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Column("WE_Count")]
        public int? WeCount { get; set; }
        [Column("WE_Hrs")]
        public int? WeHrs { get; set; }
        [Column("WE_Project")]
        public int? WeProject { get; set; }
        [Column("WE_Date", TypeName = "datetime")]
        public DateTime? WeDate { get; set; }
        [Column("WE_ProjectDef")]
        [StringLength(30)]
        public string WeProjectDef { get; set; }
    }
}
