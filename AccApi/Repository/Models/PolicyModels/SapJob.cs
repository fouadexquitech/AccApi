using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("SAP_Jobs")]
    public partial class SapJob
    {
        [Column("sn")]
        public double? Sn { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [Column("code")]
        public double? Code { get; set; }
    }
}
