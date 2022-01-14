using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblAssignWbsActivitySubWbs")]
    public partial class TblAssignWbsActivitySubWb
    {
        [Key]
        [Column("asTrade")]
        [StringLength(30)]
        public string AsTrade { get; set; }
        [Key]
        [Column("asActSubWbs")]
        public int AsActSubWbs { get; set; }
    }
}
