using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblReportsColumns")]
    public partial class TblReportsColumn
    {
        [Key]
        [Column("rcRpt")]
        public byte RcRpt { get; set; }
        [Key]
        [Column("rcCol")]
        public byte RcCol { get; set; }
        [Key]
        [Column("rcJob")]
        public int RcJob { get; set; }
    }
}
