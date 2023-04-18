using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblComCond")]
    public partial class TblComCond
    {
        [Key]
        [Column("cmSeq")]
        public int CmSeq { get; set; }
        [Column("cmDescription")]
        public string CmDescription { get; set; }
        [Column("cmSelected")]
        public byte? CmSelected { get; set; }
    }
}
