using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblPrelimsCategory")]
    public partial class TblPrelimsCategory
    {
        [Key]
        [Column("prcGroup")]
        public int PrcGroup { get; set; }
        [Key]
        [Column("prcCode")]
        public int PrcCode { get; set; }
        [Column("prcDescription")]
        [StringLength(150)]
        public string PrcDescription { get; set; }
    }
}
