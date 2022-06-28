using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tblModify")]
    public partial class TblModify
    {
        [Column("modProj")]
        [StringLength(50)]
        public string ModProj { get; set; }
        [Column("modID")]
        public int ModId { get; set; }
        [Column("modUsrID")]
        [StringLength(10)]
        public string ModUsrId { get; set; }
        [Column("modDate", TypeName = "datetime")]
        public DateTime? ModDate { get; set; }
        [Column("modTime", TypeName = "datetime")]
        public DateTime? ModTime { get; set; }
        [Column("modMemo", TypeName = "text")]
        public string ModMemo { get; set; }
        [Column("LUser")]
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        public short? Export { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastUpdate { get; set; }
    }
}
