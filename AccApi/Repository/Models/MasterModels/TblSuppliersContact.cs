using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("tblSuppliersContact")]
    public partial class TblSuppliersContact
    {
        [Key]
        [Column("supId")]
        public int SupId { get; set; }
        [Key]
        [Column("supEmail")]
        [StringLength(50)]
        public string SupEmail { get; set; }
        [Column("supContactName")]
        [StringLength(50)]
        public string SupContactName { get; set; }
        [Column("supDepartment")]
        [StringLength(200)]
        public string SupDepartment { get; set; }
        public bool? PortalAccountCreated { get; set; }
    }
}
