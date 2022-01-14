using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    public partial class ViewOtherAmount
    {
        public double? OtherCost { get; set; }
        public byte Ctg { get; set; }
    }
}
