using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Keyless]
    [Table("tblExchange")]
    public partial class TblExchange
    {
        [Required]
        [StringLength(5)]
        public string CurId { get; set; }
        public double? ExchangeToDollar { get; set; }
    }
}
