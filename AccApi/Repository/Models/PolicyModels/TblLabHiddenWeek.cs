using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblLabHiddenWeek")]
    public partial class TblLabHiddenWeek
    {
        [Key]
        [Column("lhw")]
        public int Lhw { get; set; }
        [Column("lhwLabSeq")]
        [StringLength(8)]
        public string LhwLabSeq { get; set; }
        [Column("lhwWeekFr")]
        public int? LhwWeekFr { get; set; }
        [Column("lwhDateFr", TypeName = "datetime")]
        public DateTime LwhDateFr { get; set; }
        [Column("lwhDateTo", TypeName = "datetime")]
        public DateTime LwhDateTo { get; set; }
        [Column("lhwNotes", TypeName = "ntext")]
        public string LhwNotes { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        public short? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
    }
}
