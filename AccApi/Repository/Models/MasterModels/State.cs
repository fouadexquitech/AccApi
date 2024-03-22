using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Table("State", Schema = "HangFire")]
    [Index(nameof(CreatedAt), Name = "IX_HangFire_State_CreatedAt")]
    public partial class State
    {
        [Key]
        public long Id { get; set; }
        [Key]
        public long JobId { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; }
        [StringLength(100)]
        public string Reason { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        public string Data { get; set; }

        //[ForeignKey(nameof(JobId))]
        //[InverseProperty("States")]
        //public virtual Job Job { get; set; }
    }
}
