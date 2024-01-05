using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblMailTaskSchedule")]
    public partial class TblMailTaskSchedule
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Column("dayName")]
        [StringLength(50)]
        public string DayName { get; set; }
        [Column("spName")]
        [StringLength(50)]
        public string SpName { get; set; }
    }
}
