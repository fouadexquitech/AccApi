using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblSkillPrice")]
    public partial class TblSkillPrice
    {
        [Key]
        [Column("spSkillID")]
        public int SpSkillId { get; set; }
        [Key]
        [Column("spProjectID")]
        public int SpProjectId { get; set; }
        [Column("spSkillPrice")]
        public double? SpSkillPrice { get; set; }
        [Key]
        [Column("spFDate", TypeName = "datetime")]
        public DateTime SpFdate { get; set; }
        [Column("spToDate", TypeName = "datetime")]
        public DateTime? SpToDate { get; set; }
        [StringLength(30)]
        public string InsertedBy { get; set; }
        [Column("LUser")]
        [StringLength(30)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
    }
}
