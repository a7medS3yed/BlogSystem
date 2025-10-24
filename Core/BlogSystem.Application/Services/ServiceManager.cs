using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Contracts.ServiceManager;
using BlogSystem.Application.Abstraction.Contracts.Services.Auth;
using BlogSystem.Application.Abstraction.Contracts.Services.Categories;
using BlogSystem.Application.Abstraction.Contracts.Services.Comment;
using BlogSystem.Application.Abstraction.Contracts.Services.Post;
using BlogSystem.Application.Abstraction.Contracts.Services.Reactions;
using BlogSystem.Application.Abstraction.Contracts.Services.Tags;

namespace BlogSystem.Application.Services
{
    internal class ServiceManager(Func<IAuthService> AuthFactory,
        Func<IBlogPostService> PostFactory,
        Func<ICommentService> CommentFactory,
        Func<ICategoryService> CategoryFactory,
        Func<ITagService> TagFactory,
        Func<IPostReactionService> PostReactionFactory,
        Func<IProfileService> ProfileFactory) : IServiceMangar
    {
        public IAuthService AuthService => AuthFactory.Invoke();

        public IBlogPostService blogPostService => PostFactory.Invoke();

        public ICommentService CommentService => CommentFactory.Invoke();
        public ICategoryService categoryService => CategoryFactory.Invoke();
        public ITagService TagService => TagFactory.Invoke();

        public IPostReactionService PostReactionService => PostReactionFactory.Invoke();

        public IProfileService ProfileService => ProfileFactory.Invoke();
    }
}
