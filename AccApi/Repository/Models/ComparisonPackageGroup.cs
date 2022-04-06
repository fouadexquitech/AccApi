using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccApi.Repository.Models
{
    public class ComparisonPackageGroup
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? PackageId { get; set; }
        public PackagesNetwork? Package { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string CreationUserId { get; set; }
        public string UpdateUserId { get; set; }
        public List<TblBoq>? Boqs { get; set; }

    }
}
