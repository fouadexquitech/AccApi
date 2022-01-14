using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("mouhafaza")]
    public partial class Mouhafaza
    {
        [Column("mouhafazaid")]
        public int Mouhafazaid { get; set; }
        [Column("mouhafazaname")]
        [StringLength(255)]
        public string Mouhafazaname { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
    }
}
