using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEntryInsurance")]
    public partial class TblEntryInsurance
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Key]
        [StringLength(50)]
        public string EntryNoHdr { get; set; }
        [StringLength(10)]
        public string FileNo { get; set; }
        [Column("insIssued")]
        public byte? InsIssued { get; set; }
        [Column("insIssDate", TypeName = "datetime")]
        public DateTime? InsIssDate { get; set; }
        [Column("insEndDate", TypeName = "datetime")]
        public DateTime? InsEndDate { get; set; }
        [Column("insID")]
        [StringLength(50)]
        public string InsId { get; set; }
        [Column("insNote")]
        [StringLength(200)]
        public string InsNote { get; set; }
        [Column("insAttach")]
        [StringLength(150)]
        public string InsAttach { get; set; }
        [StringLength(20)]
        public string Luser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [StringLength(20)]
        public string LuserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LdateUpdate { get; set; }
    }
}
