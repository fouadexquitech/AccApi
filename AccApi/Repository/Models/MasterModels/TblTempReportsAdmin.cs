using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.MasterModels
{
    [Keyless]
    [Table("tblTempReportsAdmin")]
    public partial class TblTempReportsAdmin
    {
        [StringLength(50)]
        public string Project { get; set; }
        [Column("coddescE")]
        [StringLength(100)]
        public string CoddescE { get; set; }
        public float? D1 { get; set; }
        public float? D2 { get; set; }
        public float? D3 { get; set; }
        public float? D4 { get; set; }
        public float? D5 { get; set; }
        public float? D6 { get; set; }
        public float? D7 { get; set; }
        public float? D8 { get; set; }
        public float? D9 { get; set; }
        public float? D10 { get; set; }
        public float? D11 { get; set; }
        public float? D12 { get; set; }
        public float? D13 { get; set; }
        public float? D14 { get; set; }
        public float? D15 { get; set; }
        public float? D16 { get; set; }
        public float? D17 { get; set; }
        public float? D18 { get; set; }
        public float? D19 { get; set; }
        public float? D20 { get; set; }
        public float? D21 { get; set; }
        public float? D22 { get; set; }
        public float? D23 { get; set; }
        public float? D24 { get; set; }
        public float? D25 { get; set; }
        public float? D26 { get; set; }
        public float? D27 { get; set; }
        public float? D28 { get; set; }
        public float? D29 { get; set; }
        public float? D30 { get; set; }
        public float? D31 { get; set; }
        public float? Total { get; set; }
    }
}
