using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblJobSkills")]
    public partial class TblJobSkill
    {
        [Key]
        [Column("jsJobSeq")]
        public int JsJobSeq { get; set; }
        [Key]
        [Column("jsProjID")]
        public int JsProjId { get; set; }
        [Key]
        [Column("jsSponsor")]
        public int JsSponsor { get; set; }
        [Key]
        [Column("jsSkill")]
        [StringLength(100)]
        public string JsSkill { get; set; }
        [Column("jsSkillGroup")]
        public short? JsSkillGroup { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? InsertedDate { get; set; }
        [StringLength(25)]
        public string InsertedBy { get; set; }
        [StringLength(25)]
        public string LastUserUpdate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
        [Column("jsSkillPrice")]
        public double? JsSkillPrice { get; set; }
        [Column("jsBasicRate")]
        public double? JsBasicRate { get; set; }
        [Column("jsFDate", TypeName = "datetime")]
        public DateTime? JsFdate { get; set; }
        [Column("jsToDate", TypeName = "datetime")]
        public DateTime? JsToDate { get; set; }
        [Column("jsSponsorType")]
        public int? JsSponsorType { get; set; }
    }
}
