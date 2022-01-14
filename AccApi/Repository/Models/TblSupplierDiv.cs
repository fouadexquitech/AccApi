using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblSupplierDiv")]
    public partial class TblSupplierDiv
    {
        [Key]
        [Column("supCode")]
        public int SupCode { get; set; }
        [Key]
        [Column("supDiv")]
        [StringLength(50)]
        public string SupDiv { get; set; }
    }
}
