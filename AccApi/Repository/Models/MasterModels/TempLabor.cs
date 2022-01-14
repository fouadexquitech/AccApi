using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    public partial class TempLabor
    {
        [Key]
        [Column("labFileNo")]
        [StringLength(10)]
        public string LabFileNo { get; set; }
        [Key]
        [StringLength(10)]
        public string LegacyNo { get; set; }
    }
}
