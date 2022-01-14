using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblWbsAreas")]
    public partial class TblWbsArea
    {
        [Key]
        [Column("waWBS")]
        [StringLength(20)]
        public string WaWbs { get; set; }
        [Key]
        [Column("waArea")]
        [StringLength(12)]
        public string WaArea { get; set; }
        [Key]
        [Column("waProj")]
        public int WaProj { get; set; }
        [Column("waMaxExecQty")]
        public double? WaMaxExecQty { get; set; }
    }
}
