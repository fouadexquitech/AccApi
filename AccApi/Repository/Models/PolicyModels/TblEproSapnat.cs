using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEproSAPNat")]
    public partial class TblEproSapnat
    {
        [Key]
        [StringLength(7)]
        public string SapNatCode { get; set; }
        [StringLength(70)]
        public string SapNatDesc { get; set; }
    }
}
