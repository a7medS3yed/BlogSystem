using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Domain.Entities.Post;
using BlogSystem.Domain.Entities.User;

namespace BlogSystem.Domain.Entities.Comments
{
    public class Comment : BaseAuditableEntity<int>
    {
        public string Content { get; set; } = string.Empty;

        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; } = default!;

        public string AuthorId { get; set; } = string.Empty;
        public ApplicationUser Author { get; set; } = default!;

    }
}
