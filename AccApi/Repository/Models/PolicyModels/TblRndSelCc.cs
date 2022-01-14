using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblRndSelCC")]
    public partial class TblRndSelCc
    {
        [Column("rnsRnd")]
        [StringLength(30)]
        public string RnsRnd { get; set; }
        [Column("rnsWho")]
        public short? RnsWho { get; set; }
        [Column("rnsUser")]
        [StringLength(10)]
        public string RnsUser { get; set; }
        [Column("rnsCod")]
        [StringLength(50)]
        public string RnsCod { get; set; }
        [Column("rnsDsc")]
        [StringLength(250)]
        public string RnsDsc { get; set; }
        [Column("rnsSel")]
        public byte? RnsSel { get; set; }
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
        [Column("rnsWBS")]
        [StringLength(30)]
        public string RnsWbs { get; set; }
        [Column("rnsWBSCode")]
        [StringLength(30)]
        public string RnsWbscode { get; set; }
    }
}
