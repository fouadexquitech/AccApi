using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("mstatus")]
    public partial class Mstatus
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("mst")]
        [StringLength(50)]
        public string Mst { get; set; }
        [Column("rowguid")]
        public Guid Rowguid { get; set; }
    }
}
