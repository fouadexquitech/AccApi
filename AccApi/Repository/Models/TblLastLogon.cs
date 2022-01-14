using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblLastLogon")]
    public partial class TblLastLogon
    {
        [Key]
        [Column("llLogonDate", TypeName = "datetime")]
        public DateTime LlLogonDate { get; set; }
    }
}
