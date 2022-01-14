using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblSubContractorDiscounts")]
    public partial class TblSubContractorDiscount
    {
        [Key]
        [Column("sdSubID")]
        [StringLength(12)]
        public string SdSubId { get; set; }
        [Key]
        [Column("sdDiv")]
        [StringLength(2)]
        public string SdDiv { get; set; }
        [Column("srDiscPer")]
        public double? SrDiscPer { get; set; }
    }
}
