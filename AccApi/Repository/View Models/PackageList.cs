
using System.ComponentModel.DataAnnotations;

namespace AccApi.Repository.View_Models
{
    public class Package
    {
        public int IDPkge { get; set; }
        public string PkgeName { get; set; }
        public string Division { get; set; }
        public bool? Standard { get; set; }
        public short? Trade { get; set; }
        public string FilePath { get; set; }
    }
}
