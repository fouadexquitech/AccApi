using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblBOQUnitRate")]
    public partial class TblBoqunitRate
    {
        [Key]
        [Column("burItem")]
        [StringLength(25)]
        public string BurItem { get; set; }
        [Key]
        [Column("burRev")]
        public short BurRev { get; set; }
        [Key]
        [Column("burBackUpDate", TypeName = "datetime")]
        public DateTime BurBackUpDate { get; set; }
        [Column("burQty")]
        public double? BurQty { get; set; }
        [Column("burUnitRate")]
        public double? BurUnitRate { get; set; }
        [Column("burSubmitted")]
        public double? BurSubmitted { get; set; }
        [Column("burBOQSellRate")]
        public double? BurBoqsellRate { get; set; }
        [Column("burProject")]
        [StringLength(10)]
        public string BurProject { get; set; }
        [Column("burBillQty")]
        public double? BurBillQty { get; set; }
        [Column("burBillSubmitted")]
        public double? BurBillSubmitted { get; set; }

        [ForeignKey(nameof(BurBackUpDate))]
        [InverseProperty(nameof(TblBoqbackUp.TblBoqunitRates))]
        public virtual TblBoqbackUp BurBackUpDateNavigation { get; set; }
    }
}
