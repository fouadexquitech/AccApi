using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblLabAccomodation")]
    public partial class TblLabAccomodation
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Key]
        [Column("acclabID")]
        [StringLength(10)]
        public string AcclabId { get; set; }
        [Key]
        [Column("accDate", TypeName = "datetime")]
        public DateTime AccDate { get; set; }
        [Column("accDateOut", TypeName = "datetime")]
        public DateTime? AccDateOut { get; set; }
        [Column("accLabName")]
        [StringLength(100)]
        public string AccLabName { get; set; }
        [Column("accLabCompany")]
        public int? AccLabCompany { get; set; }
        [Column("accCamp")]
        public int? AccCamp { get; set; }
        [Column("accRoom")]
        public int? AccRoom { get; set; }
        [Column("accNotes")]
        [StringLength(150)]
        public string AccNotes { get; set; }
        [Column("accDeleted")]
        public byte? AccDeleted { get; set; }
        [StringLength(30)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
    }
}
