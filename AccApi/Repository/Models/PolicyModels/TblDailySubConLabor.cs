using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblDailySubConLabors")]
    public partial class TblDailySubConLabor
    {
        [Key]
        [Column("dslProject")]
        [StringLength(9)]
        public string DslProject { get; set; }
        [Key]
        [Column("dslDate", TypeName = "datetime")]
        public DateTime DslDate { get; set; }
        [Key]
        [Column("dslSubID")]
        public int DslSubId { get; set; }
        [Key]
        [Column("dslJob")]
        public int DslJob { get; set; }
        [Key]
        [Column("dslTrade")]
        public int DslTrade { get; set; }
        [Column("dslLabors")]
        public short? DslLabors { get; set; }
        [Column("dslFormen")]
        public short? DslFormen { get; set; }
    }
}
