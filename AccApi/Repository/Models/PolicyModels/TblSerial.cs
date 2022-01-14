using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblSerial")]
    public partial class TblSerial
    {
        [Key]
        public int SerSeq { get; set; }
        public int? SerNumber { get; set; }
        [Column("StationGUID")]
        public int? StationGuid { get; set; }
    }
}
