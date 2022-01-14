using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEntryMedical")]
    public partial class TblEntryMedical
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Key]
        [StringLength(50)]
        public string EntryNoHdr { get; set; }
        [StringLength(10)]
        public string FileNo { get; set; }
        [Column("medIssued")]
        public byte? MedIssued { get; set; }
        [Column("medTestDate", TypeName = "datetime")]
        public DateTime? MedTestDate { get; set; }
        [Column("medTestResult")]
        public byte? MedTestResult { get; set; }
        [Column("medNote")]
        [StringLength(200)]
        public string MedNote { get; set; }
        [Column("medAttach")]
        [StringLength(150)]
        public string MedAttach { get; set; }
        [StringLength(20)]
        public string Luser { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [StringLength(20)]
        public string LuserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LdateUpdate { get; set; }
        [StringLength(50)]
        public string EntryNoHdr1 { get; set; }
    }
}
