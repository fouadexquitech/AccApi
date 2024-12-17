using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblQuantitysubcontractor")]
    public partial class TblQuantitysubcontractor
    {
        [Key]
        [Column("subconID")]
        public int SubconId { get; set; }
        [Key]
        [Column("qtydate", TypeName = "datetime")]
        public DateTime Qtydate { get; set; }
        [Key]
        [Column("qtywbs")]
        [StringLength(50)]
        public string Qtywbs { get; set; }
        [Column("qtyproject")]
        [StringLength(50)]
        public string Qtyproject { get; set; }
        [Column("quantity")]
        public int? Quantity { get; set; }
        [StringLength(50)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [Column("qtynuForman")]
        public int? QtynuForman { get; set; }
        [Column("qtynuqlader")]
        public int? Qtynuqlader { get; set; }
    }
}
