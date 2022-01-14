using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tempCorrectWBS")]
    public partial class TempCorrectWb
    {
        [StringLength(15)]
        public string FileNo { get; set; }
        [StringLength(50)]
        public string Area { get; set; }
        [StringLength(50)]
        public string Foreman { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? FromDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ToDate { get; set; }
        [Column("WrongWBS")]
        [StringLength(15)]
        public string WrongWbs { get; set; }
        [Column("CorrectWBS")]
        [StringLength(15)]
        public string CorrectWbs { get; set; }
        [StringLength(20)]
        public string ProjectDef { get; set; }
        [StringLength(15)]
        public string InsertedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(25)]
        public string NewProjectDef { get; set; }
        [StringLength(50)]
        public string NewArea { get; set; }
    }
}
