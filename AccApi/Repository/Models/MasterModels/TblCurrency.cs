using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Keyless]
    [Table("tblCurrency")]
    public partial class TblCurrency
    {
        [Required]
        [Column("cudCode")]
        [StringLength(3)]
        public string CudCode { get; set; }
        [Column("curDesc")]
        [StringLength(20)]
        public string CurDesc { get; set; }
    }
}
