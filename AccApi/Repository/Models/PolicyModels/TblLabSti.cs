using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tblLabSti")]
    public partial class TblLabSti
    {
        [Column("txtName")]
        [StringLength(200)]
        public string TxtName { get; set; }
        [Required]
        [Column("labId")]
        [StringLength(10)]
        public string LabId { get; set; }
        [Column("labPhoto")]
        [StringLength(255)]
        public string LabPhoto { get; set; }
        [Column("labjob")]
        public int? Labjob { get; set; }
        [Column("labHasPhoto")]
        public bool? LabHasPhoto { get; set; }
        [Required]
        [Column("labSort")]
        [StringLength(1)]
        public string LabSort { get; set; }
    }
}
