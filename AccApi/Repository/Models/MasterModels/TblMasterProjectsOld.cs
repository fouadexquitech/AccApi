using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Keyless]
    [Table("tblMasterProjectsOld")]
    public partial class TblMasterProjectsOld
    {
        [Column("msSeq")]
        public int MsSeq { get; set; }
        [Required]
        [Column("msDesc")]
        [StringLength(25)]
        public string MsDesc { get; set; }
        [Required]
        [Column("msConnection")]
        [StringLength(25)]
        public string MsConnection { get; set; }
        [Required]
        [Column("msServer")]
        [StringLength(15)]
        public string MsServer { get; set; }
        [Column("msActive")]
        public int MsActive { get; set; }
        [Column("msSelect")]
        public int MsSelect { get; set; }
        [Column("msDescSAP")]
        [StringLength(50)]
        public string MsDescSap { get; set; }
    }
}
