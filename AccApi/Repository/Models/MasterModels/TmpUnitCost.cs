using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Keyless]
    [Table("tmpUnitCost")]
    public partial class TmpUnitCost
    {
        [StringLength(50)]
        public string Office { get; set; }
        public int? Cost { get; set; }
        [StringLength(50)]
        public string Unit { get; set; }
    }
}
