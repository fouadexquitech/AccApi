using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblJobCodesGrouping")]
    public partial class TblJobCodesGrouping
    {
        [Key]
        [Column("jcSeq")]
        public int JcSeq { get; set; }
        [Key]
        [Column("grouping")]
        [StringLength(100)]
        public string Grouping { get; set; }
    }
}
