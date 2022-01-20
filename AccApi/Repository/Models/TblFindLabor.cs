using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Keyless]
    [Table("tblFindLabor")]
    public partial class TblFindLabor
    {
        [StringLength(14)]
        public string Seq { get; set; }
        [Column("labname")]
        [StringLength(75)]
        public string Labname { get; set; }
        [Column("labnameE")]
        [StringLength(75)]
        public string LabnameE { get; set; }
        [Column("prjName")]
        [StringLength(50)]
        public string PrjName { get; set; }
        [Column("codDescE")]
        [StringLength(50)]
        public string CodDescE { get; set; }
        [Column("labId")]
        [StringLength(10)]
        public string LabId { get; set; }
        [Column("labFileNo")]
        [StringLength(15)]
        public string LabFileNo { get; set; }
        [Column("labWDate", TypeName = "datetime")]
        public DateTime? LabWdate { get; set; }
        [Column("labWork")]
        public byte? LabWork { get; set; }
    }
}
