using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tempAYAPPForman")]
    public partial class TempAyappforman
    {
        [Column("forId")]
        public int? ForId { get; set; }
        [Column("forName")]
        [StringLength(50)]
        public string ForName { get; set; }
        [Column("forSiteEng")]
        public int? ForSiteEng { get; set; }
        [Column("forProjID")]
        [StringLength(15)]
        public string ForProjId { get; set; }
        [Column("forUsername")]
        [StringLength(50)]
        public string ForUsername { get; set; }
    }
}
