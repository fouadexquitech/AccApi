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
        [StringLength(255)]
        public string FileNo { get; set; }
        [StringLength(255)]
        public string Area { get; set; }
        [StringLength(255)]
        public string Foreman { get; set; }
        [StringLength(255)]
        public string FromDate { get; set; }
        [StringLength(255)]
        public string ToDate { get; set; }
        [Column("WrongWBS")]
        [StringLength(255)]
        public string WrongWbs { get; set; }
        [Column("CorrectWBS")]
        [StringLength(255)]
        public string CorrectWbs { get; set; }
        [StringLength(255)]
        public string ProjectDef { get; set; }
        [StringLength(255)]
        public string InsertedBy { get; set; }
        [StringLength(255)]
        public string InsertedDate { get; set; }
        [StringLength(255)]
        public string NewProjectDef { get; set; }
        [StringLength(255)]
        public string NewArea { get; set; }
    }
}
