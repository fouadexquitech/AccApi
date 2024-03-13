using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Keyless]
    [Table("tmpComparisonConditionsReply")]
    public partial class TmpComparisonConditionsReply
    {
        [Column("condId")]
        public int? CondId { get; set; }
        [Column("condDesc")]
        public string CondDesc { get; set; }
        [Column("condReply")]
        public string CondReply { get; set; }
        [Column("supId")]
        public int? SupId { get; set; }
        [Column("supName")]
        [StringLength(200)]
        public string SupName { get; set; }
    }
}
