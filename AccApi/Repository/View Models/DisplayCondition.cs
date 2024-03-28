using System.Collections.Generic;

namespace AccApi.Repository.View_Models
{
    public class DisplayCondition
    {
        public int Id { get; set; }
        
        public string Description { get; set; }
        public string? AccCondition { get; set; }

        public List<DisplayCondReply>? Replies { get; set; }
    }
}
