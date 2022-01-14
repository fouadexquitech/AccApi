using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblForemanProjects")]
    public partial class TblForemanProject
    {
        [Key]
        [Column("ForemanID")]
        public int ForemanId { get; set; }
        [Key]
        [Column("ProjectID")]
        public int ProjectId { get; set; }
        public int? OccupationGroup { get; set; }
    }
}
