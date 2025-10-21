using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblRemarkDivision")]
    public partial class TblRemarkDivision
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(20)]
        public string DivCode { get; set; }
        [StringLength(250)]
        public string DivRemark { get; set; }
    }
}
