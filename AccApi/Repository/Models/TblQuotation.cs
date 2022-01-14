using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("tblQuotations")]
    public partial class TblQuotation
    {
        [Column("kind")]
        [StringLength(50)]
        public string Kind { get; set; }
        [StringLength(50)]
        public string Category { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [StringLength(50)]
        public string Units { get; set; }
        [Column("UP")]
        public double? Up { get; set; }
        [StringLength(6)]
        public string Cur { get; set; }
        [StringLength(50)]
        public string Supplier { get; set; }
        [Column("Notes-q", TypeName = "ntext")]
        public string NotesQ { get; set; }
        [Column("Inquiry Ref")]
        [StringLength(50)]
        public string InquiryRef { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Date { get; set; }
        [StringLength(10)]
        public string Project { get; set; }
        [StringLength(50)]
        public string OurRef { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? OurDate { get; set; }
    }
}
