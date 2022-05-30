using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblTechCondGroup")]
    public partial class TblTechCondGroup
    {
        [Key]
        [Column("groupId")]
        public int GroupId { get; set; }
        [Key]
        [Column("techCondId")]
        public int TechCondId { get; set; }
    }
}
