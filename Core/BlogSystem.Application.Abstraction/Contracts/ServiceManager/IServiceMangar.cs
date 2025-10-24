using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Contracts.Services.Auth;
using BlogSystem.Application.Abstraction.Contracts.Services.Categories;
using BlogSystem.Application.Abstraction.Contracts.Services.Comment;
using BlogSystem.Application.Abstraction.Contracts.Services.Post;
using BlogSystem.Application.Abstraction.Contracts.Services.Reactions;
using BlogSystem.Application.Abstraction.Contracts.Services.Tags;

namespace BlogSystem.Application.Abstraction.Contracts.ServiceManager
{
    public interface IServiceMangar
    {
        public IAuthService AuthService { get;}
        public IBlogPostService blogPostService { get; }
        public ICommentService CommentService { get; }
        public ICategoryService categoryService { get; }
        public ITagService TagService { get; }
        public IPostReactionService PostReactionService { get; }
        public IProfileService ProfileService { get; }
    }
}
