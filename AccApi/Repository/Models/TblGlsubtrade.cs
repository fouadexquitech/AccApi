using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblGLSubtrade")]
    public partial class TblGlsubtrade
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Column("GLAcct")]
        [StringLength(20)]
        public string Glacct { get; set; }
        [Column("GLDesc")]
        [StringLength(75)]
        public string Gldesc { get; set; }
        [Column("GLSubTrade")]
        [StringLength(5)]
        public string GlsubTrade { get; set; }
        [Column("glWBS")]
        [StringLength(20)]
        public string GlWbs { get; set; }
    }
}
