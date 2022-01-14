using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("kada")]
    public partial class Kadum
    {
        [Column("kadaid")]
        public int Kadaid { get; set; }
        [Column("kadaname")]
        [StringLength(255)]
        public string Kadaname { get; set; }
        [Column("mouhafazaid")]
        public int? Mouhafazaid { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
    }
}
