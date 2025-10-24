using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Domain.Entities.Post;
using BlogSystem.Domain.Entities.User;

namespace BlogSystem.Domain.Entities.Reaction
{
    public class PostReactions : BaseAuditableEntity<int>
    {
        public int PostId { get; set; }
        public virtual BlogPost BlogPost { get; set; } = default!;
        public string UserId { get; set; } = string.Empty;
        public virtual ApplicationUser User { get; set; } = default!;
        public bool IsLike { get; set; }
    }
}
