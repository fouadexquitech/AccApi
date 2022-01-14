using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tmpResignedLab")]
    public partial class TmpResignedLab
    {
        [Column("File no")]
        [StringLength(15)]
        public string FileNo { get; set; }
        [Column("Resignation Date", TypeName = "datetime")]
        public DateTime? ResignationDate { get; set; }
    }
}
