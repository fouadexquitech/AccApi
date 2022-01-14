using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    public partial class ViewBudgetByCc
    {
        [Column("riiDiv")]
        [StringLength(2)]
        public string RiiDiv { get; set; }
        [Column("riiSubDivCode")]
        [StringLength(3)]
        public string RiiSubDivCode { get; set; }
        public double? TotalAmount { get; set; }
        public double? OtherAmount { get; set; }
        public double? FullAmount { get; set; }
    }
}
