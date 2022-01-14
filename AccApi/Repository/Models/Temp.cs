using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("TEMP")]
    public partial class Temp
    {
        [Key]
        [Column("boqItem")]
        [StringLength(25)]
        public string BoqItem { get; set; }
        [StringLength(25)]
        public string NewItem { get; set; }
        [StringLength(3)]
        public string Section { get; set; }
    }
}
