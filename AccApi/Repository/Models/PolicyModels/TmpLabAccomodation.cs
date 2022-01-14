using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tmpLabAccomodation")]
    public partial class TmpLabAccomodation
    {
        [Column("labID_Subc")]
        [StringLength(10)]
        public string LabIdSubc { get; set; }
        [Column("Date_IN", TypeName = "datetime")]
        public DateTime? DateIn { get; set; }
        [Column("Date_Out", TypeName = "datetime")]
        public DateTime? DateOut { get; set; }
        [Column("Lab_Name")]
        [StringLength(100)]
        public string LabName { get; set; }
        [Column("Lab_Company")]
        [StringLength(100)]
        public string LabCompany { get; set; }
        [StringLength(100)]
        public string Camp { get; set; }
        [StringLength(100)]
        public string Room { get; set; }
    }
}
