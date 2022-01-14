using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    public partial class ViewOtherAmountsByCc
    {
        public double? OtherCost { get; set; }
        public byte Ctg { get; set; }
        [StringLength(2)]
        public string Div { get; set; }
        [StringLength(3)]
        public string SubDiv { get; set; }
    }
}
