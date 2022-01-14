using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblRndSel")]
    public partial class TblRndSel
    {
        [Required]
        [Column("rnsRnd")]
        [StringLength(30)]
        public string RnsRnd { get; set; }
        [Column("rnsWho")]
        public short RnsWho { get; set; }
        [Required]
        [Column("rnsUser")]
        [StringLength(30)]
        public string RnsUser { get; set; }
        [Column("rnsCod")]
        [StringLength(50)]
        public string RnsCod { get; set; }
        [Column("rnsDsc")]
        [StringLength(255)]
        public string RnsDsc { get; set; }
        [Column("rnsSel")]
        public byte? RnsSel { get; set; }
        [Column("rnsBaseUnit")]
        public int? RnsBaseUnit { get; set; }
        [Column("rnsContUnit")]
        public int? RnsContUnit { get; set; }
        [Column("rnsFrac")]
        public short? RnsFrac { get; set; }
        [Column("rnsClass")]
        public int? RnsClass { get; set; }
        [Column("rnsSubClass")]
        public int? RnsSubClass { get; set; }
        [Column("rnsDate", TypeName = "datetime")]
        public DateTime? RnsDate { get; set; }
        [Column("rnsTime", TypeName = "datetime")]
        public DateTime? RnsTime { get; set; }
        [Column("rnsType")]
        public byte? RnsType { get; set; }
        [Column("rnsDiv")]
        [StringLength(30)]
        public string RnsDiv { get; set; }
        [Column("rnsSubDiv")]
        [StringLength(30)]
        public string RnsSubDiv { get; set; }
        [Column("rnsTrade")]
        [StringLength(50)]
        public string RnsTrade { get; set; }
        [Column("rnsSubTrade")]
        [StringLength(30)]
        public string RnsSubTrade { get; set; }
        [Key]
        [Column("rnsSeq")]
        public int RnsSeq { get; set; }
    }
}
