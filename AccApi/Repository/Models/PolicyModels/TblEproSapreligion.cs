using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Table("tblEproSAPReligion")]
    public partial class TblEproSapreligion
    {
        [Key]
        [Column("EproReligionID")]
        [StringLength(2)]
        public string EproReligionId { get; set; }
        [StringLength(20)]
        public string EproReligionDesc { get; set; }
        [StringLength(10)]
        public string SapEproReligionDesc { get; set; }
    }
}
