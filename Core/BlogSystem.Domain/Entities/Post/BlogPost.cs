using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Domain.Entities.Comments;
using BlogSystem.Domain.Entities.Reaction;
using BlogSystem.Domain.Entities.User;

namespace BlogSystem.Domain.Entities.Post
{
    public class BlogPost : BaseAuditableEntity<int>
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public string AuthorId { get; set; } = string.Empty;
        public ApplicationUser Author { get; set; } = default!;

        public int CategoryId { get; set; }
        public Category Category { get; set; } = default!;

        public PostStatus Status { get; set; } = PostStatus.Published;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<BlogPostTag> BlogPostTags { get; set; } = new List<BlogPostTag>();

        public virtual ICollection<PostReactions> Reactions { get; set; } = new List<PostReactions>();
    }
}
