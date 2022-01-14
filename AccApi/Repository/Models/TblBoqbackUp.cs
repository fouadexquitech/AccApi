using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    [Table("tblBOQBackUp")]
    public partial class TblBoqbackUp
    {
        public TblBoqbackUp()
        {
            TblBoqunitRates = new HashSet<TblBoqunitRate>();
        }

        [Key]
        [Column("bbuDate", TypeName = "datetime")]
        public DateTime BbuDate { get; set; }
        [Column("bbuActive")]
        public byte? BbuActive { get; set; }
        [Column("bbuProj")]
        [StringLength(10)]
        public string BbuProj { get; set; }

        [InverseProperty(nameof(TblBoqunitRate.BurBackUpDateNavigation))]
        public virtual ICollection<TblBoqunitRate> TblBoqunitRates { get; set; }
    }
}
