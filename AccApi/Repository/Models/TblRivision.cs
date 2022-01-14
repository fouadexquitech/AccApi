using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblRivision")]
    public partial class TblRivision
    {
        [Key]
        [Column("rivSeq")]
        public short RivSeq { get; set; }
        [Column("rivDate", TypeName = "datetime")]
        public DateTime? RivDate { get; set; }
    }
}
