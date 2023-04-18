using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblMapOccup")]
    public partial class TblMapOccup
    {
        [Key]
        public int Seq { get; set; }
        [StringLength(100)]
        public string EstOccup { get; set; }
        [Column("EstOccupID")]
        [StringLength(100)]
        public string EstOccupId { get; set; }
        [StringLength(100)]
        public string MapOccup { get; set; }
        [Column("MapOccupID")]
        [StringLength(100)]
        public string MapOccupId { get; set; }
        public byte? Staff { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsrertedDate { get; set; }
    }
}
