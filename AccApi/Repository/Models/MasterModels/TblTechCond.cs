using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblTechCond")]
    public partial class TblTechCond
    {
        [Key]
        [Column("tcSeq")]
        public int TcSeq { get; set; }
        [Column("tcPackId")]
        public int? TcPackId { get; set; }
        [Column("tcDescription")]
        public string TcDescription { get; set; }
        [Column("tcSelected")]
        public byte? TcSelected { get; set; }
        public bool? IsSynced { get; set; }
        [Column("tcAccCond")]
        public string TcAccCond { get; set; }
    }
}
