using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Keyless]
    [Table("tblRndSel")]
    public partial class TblRndSel
    {
        [Required]
        [Column("rnsRnd")]
        [StringLength(20)]
        public string RnsRnd { get; set; }
        [Column("rnsWho")]
        public short RnsWho { get; set; }
        [Required]
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
        public bool? RnsSel { get; set; }
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
    }
}
