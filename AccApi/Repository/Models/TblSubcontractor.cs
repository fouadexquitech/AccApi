using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblSubcontractor")]
    [Index(nameof(SubName), Name = "IX_tblSubcontractor", IsUnique = true)]
    public partial class TblSubcontractor
    {
        [Key]
        [Column("ID")]
        [StringLength(12)]
        public string Id { get; set; }
        [StringLength(50)]
        public string SubName { get; set; }
        [StringLength(10)]
        public string Project { get; set; }
        [StringLength(255)]
        public string Note { get; set; }
        [StringLength(12)]
        public string Forman { get; set; }
        [Column("insertdate", TypeName = "datetime")]
        public DateTime? Insertdate { get; set; }
        [Column("insertBy")]
        [StringLength(50)]
        public string InsertBy { get; set; }
        [Column("updatedBy")]
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        [Column("updatedDate", TypeName = "datetime")]
        public DateTime? UpdatedDate { get; set; }
        [Column("VendorSAPID")]
        [StringLength(50)]
        public string VendorSapid { get; set; }
    }
}
