using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblReadyMixHdr")]
    public partial class TblReadyMixHdr
    {
        [Key]
        [Column("rmHdrSeq")]
        [StringLength(19)]
        public string RmHdrSeq { get; set; }
        [Column("rmDate", TypeName = "datetime")]
        public DateTime? RmDate { get; set; }
        [Column("rmDeliveryDate", TypeName = "datetime")]
        public DateTime? RmDeliveryDate { get; set; }
        [Column("rmDeliveryNote")]
        [StringLength(7)]
        public string RmDeliveryNote { get; set; }
        [Column("rmNight")]
        public bool? RmNight { get; set; }
        [Column("rmRequestBy")]
        [StringLength(12)]
        public string RmRequestBy { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [Column("rmRequestByName")]
        [StringLength(150)]
        public string RmRequestByName { get; set; }
        [Column("rmReqOnSite", TypeName = "datetime")]
        public DateTime? RmReqOnSite { get; set; }
        [Column("rmEntryDate", TypeName = "datetime")]
        public DateTime? RmEntryDate { get; set; }
        [Column("rmReqOnSiteTime", TypeName = "datetime")]
        public DateTime? RmReqOnSiteTime { get; set; }
    }
}
