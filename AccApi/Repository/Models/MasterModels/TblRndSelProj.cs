using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Keyless]
    [Table("tblRndSelProj")]
    public partial class TblRndSelProj
    {
        [Column("rspRnd")]
        public int RspRnd { get; set; }
        [Column("rspWho")]
        public short RspWho { get; set; }
        [Required]
        [Column("rspUser")]
        [StringLength(10)]
        public string RspUser { get; set; }
        [Column("rspCod")]
        [StringLength(50)]
        public string RspCod { get; set; }
        [Column("rspDsc")]
        [StringLength(250)]
        public string RspDsc { get; set; }
        [Column("rspSel")]
        public bool? RspSel { get; set; }
    }
}
