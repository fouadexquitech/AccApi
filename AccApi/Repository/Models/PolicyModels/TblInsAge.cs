using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblInsAge")]
    public partial class TblInsAge
    {
        [Key]
        [StringLength(10)]
        public string AgeBand { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
    }
}
