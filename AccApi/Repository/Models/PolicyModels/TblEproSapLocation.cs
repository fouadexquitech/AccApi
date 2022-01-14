using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEproSapLocation")]
    public partial class TblEproSapLocation
    {
        [Key]
        [StringLength(7)]
        public string SapLocationCode { get; set; }
        [StringLength(70)]
        public string SapLocationDesc { get; set; }
    }
}
