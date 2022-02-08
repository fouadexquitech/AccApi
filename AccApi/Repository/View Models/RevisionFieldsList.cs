using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccApi.Repository.View_Models
{
    public class RevisionFieldsList
    {
        public int Id { get; set; }      
        public int RevisionId { get; set; }
        public string Label { get; set; }
        public int Value { get; set; }
        public int? Type { get; set; }
    }
}
