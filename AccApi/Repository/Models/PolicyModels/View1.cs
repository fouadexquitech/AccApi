using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models.PolicyModels
{
    [Keyless]
    public partial class View1
    {
        [Column("disDate", TypeName = "datetime")]
        public DateTime? DisDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? Expr1 { get; set; }
        [Column("disLab")]
        [StringLength(15)]
        public string DisLab { get; set; }
        [StringLength(15)]
        public string Expr2 { get; set; }
        [Column("disWBS")]
        [StringLength(15)]
        public string DisWbs { get; set; }
        [StringLength(15)]
        public string Expr3 { get; set; }
        [Column("disProject")]
        [StringLength(9)]
        public string DisProject { get; set; }
        [StringLength(9)]
        public string Expr4 { get; set; }
    }
}
