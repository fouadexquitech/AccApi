using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tblLabAddress")]
    public partial class TblLabAddress
    {
        [Required]
        [Column("ladLab")]
        [StringLength(14)]
        public string LadLab { get; set; }
        [Column("ladmouhafazaid")]
        public int? Ladmouhafazaid { get; set; }
        [Column("ladKadaID")]
        public int? LadKadaId { get; set; }
        [Column("ladAreaID")]
        public int? LadAreaId { get; set; }
        [Column("ladHay")]
        [StringLength(50)]
        public string LadHay { get; set; }
        [Column("ladStr")]
        [StringLength(50)]
        public string LadStr { get; set; }
        [Column("ladBldg")]
        [StringLength(50)]
        public string LadBldg { get; set; }
        [Column("ladFloor")]
        public int? LadFloor { get; set; }
        [Column("ladPostBox")]
        [StringLength(50)]
        public string LadPostBox { get; set; }
        [Column("ladRegion")]
        [StringLength(8)]
        public string LadRegion { get; set; }
        [Column("ladFax")]
        [StringLength(50)]
        public string LadFax { get; set; }
        [Column("ladEmail")]
        [StringLength(50)]
        public string LadEmail { get; set; }
        [Column("ladRecNo")]
        public int? LadRecNo { get; set; }
        [Column("ladRecPlace")]
        public int? LadRecPlace { get; set; }
        [Column("ladAddress")]
        [StringLength(250)]
        public string LadAddress { get; set; }
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
