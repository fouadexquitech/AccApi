using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    public partial class ViewBoqitemLastRevisionActive
    {
        [Required]
        [Column("burItem")]
        [StringLength(25)]
        public string BurItem { get; set; }
        [Column("burBackUpDate", TypeName = "datetime")]
        public DateTime BurBackUpDate { get; set; }
        public short? LastRev { get; set; }
    }
}
