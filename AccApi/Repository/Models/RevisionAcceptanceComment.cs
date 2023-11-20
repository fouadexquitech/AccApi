using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AccApi.Repository.Models
{
    public partial class RevisionAcceptanceComment
    {
        [Key]
        public int Id { get; set; }
        public int? RevisionId { get; set; }
        public int? AcceptanceCommentId { get; set; }
    }
}
