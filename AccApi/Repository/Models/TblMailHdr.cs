using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblMailHdr")]
    public partial class TblMailHdr
    {
        [Key]
        [Column("mlhSeq")]
        public int MlhSeq { get; set; }
        [Key]
        [Column("mlhProjCode")]
        [StringLength(20)]
        public string MlhProjCode { get; set; }
        [Key]
        [Column("mlhMailType")]
        [StringLength(20)]
        public string MlhMailType { get; set; }
        [Column("mlhMailTypeDesc")]
        [StringLength(50)]
        public string MlhMailTypeDesc { get; set; }
        [Column("mlhMailSubject")]
        [StringLength(200)]
        public string MlhMailSubject { get; set; }
        [Column("mlhMailBody", TypeName = "ntext")]
        public string MlhMailBody { get; set; }
    }
}
