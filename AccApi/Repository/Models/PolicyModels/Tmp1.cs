using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tmp1")]
    public partial class Tmp1
    {
        [Column("labID")]
        [StringLength(8)]
        public string LabId { get; set; }
        [Column("labID1")]
        [StringLength(8)]
        public string LabId1 { get; set; }
        [Column("attdate", TypeName = "datetime")]
        public DateTime? Attdate { get; set; }
        [Column("attdate1", TypeName = "datetime")]
        public DateTime? Attdate1 { get; set; }
    }
}
