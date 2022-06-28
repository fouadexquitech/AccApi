using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tmp")]
    public partial class Tmp
    {
        [Column("ID")]
        [StringLength(100)]
        public string Id { get; set; }
        [Column("DescripID")]
        [StringLength(100)]
        public string DescripId { get; set; }
        [StringLength(100)]
        public string Dte { get; set; }
        [Column("value")]
        [StringLength(100)]
        public string Value { get; set; }
        [StringLength(100)]
        public string Description { get; set; }
    }
}
