using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tblBankSal")]
    public partial class TblBankSal
    {
        [Column("labId")]
        [StringLength(14)]
        public string LabId { get; set; }
        [Column("labname")]
        [StringLength(75)]
        public string Labname { get; set; }
        [Column("codAbrv")]
        [StringLength(2)]
        public string CodAbrv { get; set; }
        [Column("labFAccNew")]
        [StringLength(50)]
        public string LabFaccNew { get; set; }
        [Column("salary")]
        [StringLength(255)]
        public string Salary { get; set; }
    }
}
