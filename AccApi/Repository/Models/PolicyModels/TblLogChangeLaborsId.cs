using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblLogChangeLaborsID")]
    public partial class TblLogChangeLaborsId
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Column("Old File No")]
        [StringLength(8)]
        public string OldFileNo { get; set; }
        [Column("New File No")]
        [StringLength(8)]
        public string NewFileNo { get; set; }
        [StringLength(10)]
        public string Luser { get; set; }
        [Column("LDate", TypeName = "datetime")]
        public DateTime? Ldate { get; set; }
        [StringLength(150)]
        public string Descrip { get; set; }
    }
}
