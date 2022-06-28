using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblLabDocuments")]
    public partial class TblLabDocument
    {
        [Key]
        [Column("docLabID")]
        [StringLength(8)]
        public string DocLabId { get; set; }
        [Key]
        [Column("docType")]
        public int DocType { get; set; }
        [Column("docIssDat", TypeName = "datetime")]
        public DateTime? DocIssDat { get; set; }
        [Column("docPath")]
        [StringLength(255)]
        public string DocPath { get; set; }
        [Column("docRemark", TypeName = "ntext")]
        public string DocRemark { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        public byte? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("docTypeNum")]
        [StringLength(50)]
        public string DocTypeNum { get; set; }
        [Column("docEndDate", TypeName = "datetime")]
        public DateTime? DocEndDate { get; set; }

        [ForeignKey(nameof(DocLabId))]
        [InverseProperty(nameof(TblLab.TblLabDocuments))]
        public virtual TblLab DocLab { get; set; }
    }
}
