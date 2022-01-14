using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("JobsList")]
    public partial class JobsList
    {
        [Key]
        [Column("labjob")]
        public double Labjob { get; set; }
        [Column("codDescA")]
        [StringLength(255)]
        public string CodDescA { get; set; }
        [StringLength(255)]
        public string NewJob { get; set; }
    }
}
