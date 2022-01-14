using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Keyless]
    [Table("tmp")]
    public partial class Tmp
    {
        [Column("TK_ID")]
        [StringLength(150)]
        public string TkId { get; set; }
        [Column("TS_ID")]
        [StringLength(150)]
        public string TsId { get; set; }
        [Column("ID3")]
        [StringLength(150)]
        public string Id3 { get; set; }
        [Column("ID4")]
        [StringLength(150)]
        public string Id4 { get; set; }
        public double? DayFee { get; set; }
    }
}
