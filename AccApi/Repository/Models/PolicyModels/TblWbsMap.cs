using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblWbsMap")]
    public partial class TblWbsMap
    {
        [Key]
        [Column("WBS_TK")]
        [StringLength(50)]
        public string WbsTk { get; set; }
        [Key]
        [Column("WBS_SAP")]
        [StringLength(50)]
        public string WbsSap { get; set; }
    }
}
