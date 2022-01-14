using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEproTsLocation")]
    public partial class TblEproTsLocation
    {
        [Key]
        [Column("EproWorkPlaceID")]
        [StringLength(7)]
        public string EproWorkPlaceId { get; set; }
        [StringLength(70)]
        public string EproWorkPlaceDesc { get; set; }
        [Column("SAPWorkPlaceCode")]
        [StringLength(5)]
        public string SapworkPlaceCode { get; set; }
        [Column("TSProjID")]
        public int? TsprojId { get; set; }
    }
}
