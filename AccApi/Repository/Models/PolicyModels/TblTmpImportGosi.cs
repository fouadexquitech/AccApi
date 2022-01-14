using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tblTmpImportGOSI")]
    public partial class TblTmpImportGosi
    {
        [Column("حالة مدة الإشتراك")]
        [StringLength(255)]
        public string حالةمدةالإشتراك { get; set; }
        public double? الاجر { get; set; }
        [Column("رقم الجواز")]
        [StringLength(50)]
        public string رقمالجواز { get; set; }
        [Column("رقم الاقامة")]
        [StringLength(50)]
        public string رقمالاقامة { get; set; }
        [Column("رقم الحفيظة")]
        [StringLength(50)]
        public string رقمالحفيظة { get; set; }
        [Column("رقم الهوية الوطنية")]
        [StringLength(50)]
        public string رقمالهويةالوطنية { get; set; }
        [Column("تاريح الإلتحاق")]
        [StringLength(50)]
        public string تاريحالإلتحاق { get; set; }
        [Column("تاريخ الميلاد")]
        [StringLength(50)]
        public string تاريخالميلاد { get; set; }
        [StringLength(255)]
        public string الجنسية { get; set; }
        [StringLength(100)]
        public string الاسم { get; set; }
        [Column("رقم المشترك")]
        [StringLength(20)]
        public string رقمالمشترك { get; set; }
        [StringLength(50)]
        public string Sponsor { get; set; }
        public bool? Staff { get; set; }
        [StringLength(10)]
        public string FileNo { get; set; }
        [StringLength(20)]
        public string ProjectCode { get; set; }
        [Column("SponsorID")]
        public int? SponsorId { get; set; }
        public byte? Status { get; set; }
    }
}
