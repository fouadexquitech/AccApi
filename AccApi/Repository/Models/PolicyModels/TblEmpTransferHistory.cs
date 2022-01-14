using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEmpTransferHistory")]
    public partial class TblEmpTransferHistory
    {
        [Column("ethSeq")]
        public int EthSeq { get; set; }
        [Key]
        [Column("ethEmpID")]
        [StringLength(10)]
        public string EthEmpId { get; set; }
        [Key]
        [Column("ethDate", TypeName = "datetime")]
        public DateTime EthDate { get; set; }
        [Key]
        [Column("ethProjIDFrom")]
        public int EthProjIdfrom { get; set; }
        [Column("ethDateTo", TypeName = "datetime")]
        public DateTime? EthDateTo { get; set; }
        [Column("ethProjIDTo")]
        public int? EthProjIdto { get; set; }
        [Column("ethProjectDefFrom")]
        [StringLength(15)]
        public string EthProjectDefFrom { get; set; }
        [Column("ethProjectDefTo")]
        [StringLength(15)]
        public string EthProjectDefTo { get; set; }
    }
}
