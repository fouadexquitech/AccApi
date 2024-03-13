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
        [StringLength(500)]
        public string TkId { get; set; }
        [Column("TS_ID")]
        public string TsId { get; set; }
        [Column("ID3")]
        public string Id3 { get; set; }
        [Column("ID4")]
        public string Id4 { get; set; }
        [Column("ID5")]
        public string Id5 { get; set; }
        [Column("ID6")]
        public string Id6 { get; set; }
        [Column("ID7")]
        public string Id7 { get; set; }
        [Column("ID8")]
        public string Id8 { get; set; }
        [Column("ID9")]
        public string Id9 { get; set; }
        [Column("ID10")]
        public string Id10 { get; set; }
        public string DayFee { get; set; }
        [Column("ZoneID")]
        public string ZoneId { get; set; }
        [Column("VillaTypeID")]
        public string VillaTypeId { get; set; }
    }
}
