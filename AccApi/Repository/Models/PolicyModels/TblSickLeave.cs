using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblSickLeave")]
    public partial class TblSickLeave
    {
        [Key]
        [Column("slFromDay")]
        public short SlFromDay { get; set; }
        [Key]
        [Column("slToDay")]
        public short SlToDay { get; set; }
        [Column("slPer")]
        public double? SlPer { get; set; }
    }
}
