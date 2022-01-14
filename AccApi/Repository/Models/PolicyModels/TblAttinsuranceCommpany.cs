using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblAttinsuranceCommpany")]
    public partial class TblAttinsuranceCommpany
    {
        [Key]
        [Column("seq")]
        public int Seq { get; set; }
        [Column("insProject")]
        [StringLength(50)]
        public string InsProject { get; set; }
        [Column("insInsurancecompany")]
        [StringLength(100)]
        public string InsInsurancecompany { get; set; }
        [Column("insPolicynumber")]
        [StringLength(255)]
        public string InsPolicynumber { get; set; }
        [Column("insFromdate", TypeName = "datetime")]
        public DateTime? InsFromdate { get; set; }
        [Column("insTodate", TypeName = "datetime")]
        public DateTime? InsTodate { get; set; }
        [Column("insTypeofinsurance")]
        [StringLength(50)]
        public string InsTypeofinsurance { get; set; }
        [Column("insInsertBy")]
        [StringLength(50)]
        public string InsInsertBy { get; set; }
        [Column("insInsertDate", TypeName = "datetime")]
        public DateTime? InsInsertDate { get; set; }
    }
}
