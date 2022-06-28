using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    public partial class ViewLaborsAttDay
    {
        [Column("disProject")]
        [StringLength(9)]
        public string DisProject { get; set; }
        [Column("disProjectDef")]
        [StringLength(9)]
        public string DisProjectDef { get; set; }
        [Column("disLab")]
        [StringLength(8)]
        public string DisLab { get; set; }
        [Column("disDate", TypeName = "datetime")]
        public DateTime? DisDate { get; set; }
    }
}
