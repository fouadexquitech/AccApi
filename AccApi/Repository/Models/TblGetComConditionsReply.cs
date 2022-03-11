using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("tblGetComConditionsReply")]
    public partial class TblGetComConditionsReply
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
