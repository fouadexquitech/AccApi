using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblMissingPrice")]
    public partial class TblMissingPrice
    {
        [Key]
        [Column("RevisionID")]
        public int RevisionId { get; set; }
        [Key]
        [Column("boqResourceSeq")]
        public int BoqResourceSeq { get; set; }
    }
}
