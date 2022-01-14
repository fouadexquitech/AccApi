using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    public partial class ViewBoqunitPrice
    {
        [Column("boqItem")]
        [StringLength(25)]
        public string BoqItem { get; set; }
        [Column("boqRivision")]
        public short? BoqRivision { get; set; }
        [Column("boqBackUpDate", TypeName = "datetime")]
        public DateTime? BoqBackUpDate { get; set; }
        [Column("BOQUnitPrice")]
        public double? BoqunitPrice { get; set; }
    }
}
