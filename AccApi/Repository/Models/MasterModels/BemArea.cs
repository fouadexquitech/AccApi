using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Keyless]
    public partial class BemArea
    {
        [Column("old")]
        [StringLength(50)]
        public string Old { get; set; }
        [Column("new")]
        [StringLength(50)]
        public string New { get; set; }
    }
}
