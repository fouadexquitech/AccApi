using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblMoneyClass")]
    public partial class TblMoneyClass
    {
        [Key]
        [Column("mcSeq")]
        public int McSeq { get; set; }
        [Column("mcValue")]
        public float? McValue { get; set; }
    }
}
