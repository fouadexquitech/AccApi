using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    public partial class ViewLaborMaxDate
    {
        [Required]
        [StringLength(8)]
        public string LabSeq { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastDate { get; set; }
    }
}
