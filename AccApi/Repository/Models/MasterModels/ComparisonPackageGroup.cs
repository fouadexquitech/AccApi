using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    public partial class ComparisonPackageGroup
    {
        [Key]
        public int Id { get; set; }
        [StringLength(150)]
        public string Name { get; set; }
        public int? PackageId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreationDate { get; set; }
        [StringLength(50)]
        public string CreationUserId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdateDate { get; set; }
        [StringLength(50)]
        public string UpdateUserId { get; set; }
    }
}
