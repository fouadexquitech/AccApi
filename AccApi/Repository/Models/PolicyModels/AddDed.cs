using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("AddDed")]
    public partial class AddDed
    {
        [StringLength(10)]
        public string FileNo { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? EndDate { get; set; }
        public double? Amount { get; set; }
        [StringLength(1)]
        public string Type { get; set; }
        [StringLength(255)]
        public string Remark { get; set; }
    }
}
