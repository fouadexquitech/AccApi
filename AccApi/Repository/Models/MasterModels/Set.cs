﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("Set", Schema = "HangFire")]
    [Index(nameof(Key), nameof(Score), Name = "IX_HangFire_Set_Score")]
    public partial class Set
    {
        [Key]
        [StringLength(100)]
        public string Key { get; set; }
        public double Score { get; set; }
        [Key]
        [StringLength(256)]
        public string Value { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ExpireAt { get; set; }
    }
}
