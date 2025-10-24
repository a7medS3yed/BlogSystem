using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Domain.Entities.Comments;
using BlogSystem.Domain.Entities.Post;
using Microsoft.AspNetCore.Identity;



namespace BlogSystem.Domain.Entities.User
{
    public class ApplicationUser : IdentityUser
    {
        public ICollection<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
