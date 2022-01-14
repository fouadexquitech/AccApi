using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    public partial class Uuuuuuuuuu
    {
        [Required]
        [Column("rinHdrSeq")]
        [StringLength(14)]
        public string RinHdrSeq { get; set; }
        [Column("rinFuel")]
        public float? RinFuel { get; set; }
    }
}
