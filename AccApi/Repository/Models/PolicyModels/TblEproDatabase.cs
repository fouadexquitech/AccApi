using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEproDatabases")]
    public partial class TblEproDatabase
    {
        [Key]
        [StringLength(20)]
        public string EproDatabase { get; set; }
        public int? Companies { get; set; }
    }
}
