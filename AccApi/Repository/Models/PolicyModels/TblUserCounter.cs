using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tblUserCounter")]
    public partial class TblUserCounter
    {
        [Required]
        [Column("ucProj")]
        [StringLength(3)]
        public string UcProj { get; set; }
        [Required]
        [Column("ucStID")]
        [StringLength(3)]
        public string UcStId { get; set; }
        [Required]
        [Column("ucUserKey")]
        [StringLength(3)]
        public string UcUserKey { get; set; }
        [Column("ucType")]
        public short UcType { get; set; }
        [Column("ucLast")]
        [StringLength(19)]
        public string UcLast { get; set; }
    }
}
