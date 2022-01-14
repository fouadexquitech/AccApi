using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblLabBlockHrs")]
    public partial class TblLabBlockHr
    {
        [Key]
        [Column("labId")]
        [StringLength(8)]
        public string LabId { get; set; }
        [Key]
        [Column(TypeName = "datetime")]
        public DateTime DateF { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateT { get; set; }
        [Column("BlockOT")]
        public byte? BlockOt { get; set; }
        public byte? BlockContra { get; set; }
    }
}
