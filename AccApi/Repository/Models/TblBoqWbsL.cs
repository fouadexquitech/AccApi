using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblBoqWbsLS")]
    public partial class TblBoqWbsL
    {
        [Key]
        [Column("lsWBS")]
        [StringLength(10)]
        public string LsWbs { get; set; }
    }
}
