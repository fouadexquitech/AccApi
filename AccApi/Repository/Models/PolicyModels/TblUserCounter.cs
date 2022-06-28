using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblUserCounter")]
    public partial class TblUserCounter
    {
        [Key]
        [Column("ucProj")]
        [StringLength(3)]
        public string UcProj { get; set; }
        [Key]
        [Column("ucStID")]
        [StringLength(3)]
        public string UcStId { get; set; }
        [Key]
        [Column("ucUserKey")]
        [StringLength(3)]
        public string UcUserKey { get; set; }
        [Key]
        [Column("ucType")]
        public short UcType { get; set; }
        [Column("ucLast")]
        [StringLength(19)]
        public string UcLast { get; set; }
    }
}
