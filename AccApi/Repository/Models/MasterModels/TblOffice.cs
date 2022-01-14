using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblOffices")]
    public partial class TblOffice
    {
        [Key]
        public int Seq { get; set; }
        [StringLength(50)]
        public string Office { get; set; }
    }
}
