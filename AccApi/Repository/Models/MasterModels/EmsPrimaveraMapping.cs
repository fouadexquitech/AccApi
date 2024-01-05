using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("emsPrimaveraMapping")]
    public partial class EmsPrimaveraMapping
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string St1 { get; set; }
        [StringLength(50)]
        public string St2 { get; set; }
        public int? SubmittedPer { get; set; }
        public int? ApprovedPer { get; set; }
    }
}
