using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblMasterProjects")]
    public partial class TblMasterProject
    {
        [Key]
        [Column("msSeq")]
        public int MsSeq { get; set; }
        [Column("msDesc")]
        [StringLength(25)]
        public string MsDesc { get; set; }
        [Column("msConnection")]
        [StringLength(15)]
        public string MsConnection { get; set; }
        [Column("msServer")]
        [StringLength(15)]
        public string MsServer { get; set; }
        [Column("msActive")]
        public byte? MsActive { get; set; }
    }
}
