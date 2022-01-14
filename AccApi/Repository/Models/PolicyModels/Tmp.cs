using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    [Table("tmp")]
    public partial class Tmp
    {
        [Column("ID")]
        [StringLength(50)]
        public string Id { get; set; }
        [Column("ID1")]
        [StringLength(50)]
        public string Id1 { get; set; }
        [Column("ID3")]
        [StringLength(50)]
        public string Id3 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LastAtt { get; set; }
        [Column("value")]
        [StringLength(50)]
        public string Value { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
        [Column("DescripID")]
        [StringLength(50)]
        public string DescripId { get; set; }
        [Column("DescripID1")]
        [StringLength(50)]
        public string DescripId1 { get; set; }
        [Column("DescripID2")]
        [StringLength(50)]
        public string DescripId2 { get; set; }
        [Column("DescripID3")]
        [StringLength(50)]
        public string DescripId3 { get; set; }
    }
}
