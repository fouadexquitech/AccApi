using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tempwbs")]
    public partial class Tempwb
    {
        [Column("levelsss")]
        [StringLength(50)]
        public string Levelsss { get; set; }
        [Column("WBS")]
        [StringLength(100)]
        public string Wbs { get; set; }
        [Column("wbsdesc")]
        [StringLength(250)]
        public string Wbsdesc { get; set; }
    }
}
