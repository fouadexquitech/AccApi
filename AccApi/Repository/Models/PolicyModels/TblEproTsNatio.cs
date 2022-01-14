using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEproTsNatio")]
    public partial class TblEproTsNatio
    {
        [Key]
        [Column("EproNatioID")]
        [StringLength(7)]
        public string EproNatioId { get; set; }
        [Key]
        [Column("TSNatioID")]
        public int TsnatioId { get; set; }
        [StringLength(70)]
        public string EproNatioDesc { get; set; }
        [StringLength(10)]
        public string SapCode { get; set; }
    }
}
