using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tblPermGrpUsr")]
    public partial class TblPermGrpUsr
    {
        [Column("prmUser")]
        [StringLength(30)]
        public string PrmUser { get; set; }
        [Column("prmFuncID")]
        [StringLength(30)]
        public string PrmFuncId { get; set; }
        public byte? MinOfprmRead { get; set; }
        public byte? MinOfprmWrite { get; set; }
        public byte? MinOfprmUpdate { get; set; }
        public byte? MinOfprmDelete { get; set; }
        public short? MinOfprmUpdPeriod { get; set; }
    }
}
