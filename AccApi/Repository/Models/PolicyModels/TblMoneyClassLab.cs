using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblMoneyClassLab")]
    public partial class TblMoneyClassLab
    {
        [Key]
        [Column("mclSeq")]
        public int MclSeq { get; set; }
        [Column("mclValue")]
        public double? MclValue { get; set; }
        [Column("mclCount")]
        public short? MclCount { get; set; }
        [Column("mclLab")]
        [StringLength(14)]
        public string MclLab { get; set; }
    }
}
