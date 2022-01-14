using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEntrySaudiCouncil")]
    public partial class TblEntrySaudiCouncil
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Key]
        [StringLength(50)]
        public string EntryNoHdr { get; set; }
        [StringLength(10)]
        public string FileNo { get; set; }
        [Column("scIssued")]
        public byte? ScIssued { get; set; }
        [Column("scIssDate", TypeName = "datetime")]
        public DateTime? ScIssDate { get; set; }
        [Column("scID")]
        [StringLength(50)]
        public string ScId { get; set; }
        [Column("scNote")]
        [StringLength(200)]
        public string ScNote { get; set; }
        [Column("scAttach")]
        [StringLength(150)]
        public string ScAttach { get; set; }
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
