using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tmp2")]
    public partial class Tmp2
    {
        [Column("labID")]
        [StringLength(100)]
        public string LabId { get; set; }
        [Column("labID1")]
        [StringLength(8)]
        public string LabId1 { get; set; }
        [Column("labID3")]
        [StringLength(50)]
        public string LabId3 { get; set; }
        [Column("attdate", TypeName = "datetime")]
        public DateTime? Attdate { get; set; }
        [Column("attdate1", TypeName = "datetime")]
        public DateTime? Attdate1 { get; set; }
    }
}
