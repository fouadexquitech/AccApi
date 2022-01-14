using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblRevisionDetails")]
    public partial class TblRevisionDetail
    {
        [Key]
        [Column("rdRevisionId")]
        public int RdRevisionId { get; set; }
        [Key]
        [Column("rdResourceSeq")]
        public int RdResourceSeq { get; set; }
        [Column("rdPrice")]
        public double? RdPrice { get; set; }
        [Column("rdQty")]
        public double? RdQty { get; set; }
        [Column("rdComment", TypeName = "text")]
        public string RdComment { get; set; }
        [Column("rdAssignedPerc")]
        public double? RdAssignedPerc { get; set; }
        [Column("rdAssignedQty")]
        public double? RdAssignedQty { get; set; }
        [Column("rdAssignedPrice")]
        public double? RdAssignedPrice { get; set; }
    }
}
