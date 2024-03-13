using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblBoqLevel")]
    public partial class TblBoqLevel
    {
        [Key]
        [Column("blBoqItem")]
        [StringLength(25)]
        public string BlBoqItem { get; set; }
        [Key]
        [Column("blLevelSeq")]
        public int BlLevelSeq { get; set; }
    }
}
