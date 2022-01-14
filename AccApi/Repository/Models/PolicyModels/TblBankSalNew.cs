using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tblBankSalNew")]
    public partial class TblBankSalNew
    {
        [Column("labFAccNew")]
        [StringLength(50)]
        public string LabFaccNew { get; set; }
        [Column("labname")]
        [StringLength(100)]
        public string Labname { get; set; }
        [StringLength(10)]
        public string Cur { get; set; }
        [Column("salary")]
        [StringLength(10)]
        public string Salary { get; set; }
        [StringLength(50)]
        public string EmplyerData { get; set; }
        [StringLength(30)]
        public string UserName { get; set; }
    }
}
