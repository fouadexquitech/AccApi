using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblActivitiesSubWBS")]
    public partial class TblActivitiesSubWb
    {
        [Key]
        [Column("swID")]
        public int SwId { get; set; }
        [Column("sgrpID")]
        public int? SgrpId { get; set; }
        [Column("swDesc")]
        [StringLength(100)]
        public string SwDesc { get; set; }
        [Column("swProjectCode")]
        [StringLength(50)]
        public string SwProjectCode { get; set; }
    }
}
