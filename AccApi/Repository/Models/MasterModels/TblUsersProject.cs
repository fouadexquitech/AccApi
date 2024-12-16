using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblUsersProjects")]
    public partial class TblUsersProject
    {
        [Key]
        [Column("UsrProjID")]
        public int UsrProjId { get; set; }
        [Key]
        [Column("usrID")]
        [StringLength(20)]
        public string UsrId { get; set; }
        [Column("isActualEM")]
        public bool? IsActualEm { get; set; }
        [Column("EMSType")]
        [StringLength(50)]
        public string Emstype { get; set; }
        public float? Hrs { get; set; }
        [Column("insertDate", TypeName = "datetime")]
        public DateTime? InsertDate { get; set; }
    }
}
