using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblLogInDateChange")]
    public partial class TblLogInDateChange
    {
        [Key]
        [Column("ldcSeq")]
        public int LdcSeq { get; set; }
        [Column("ldcUser")]
        [StringLength(15)]
        public string LdcUser { get; set; }
        [Column("ldcDate", TypeName = "datetime")]
        public DateTime? LdcDate { get; set; }
        [Column("ldcChangeDate", TypeName = "datetime")]
        public DateTime? LdcChangeDate { get; set; }
    }
}
