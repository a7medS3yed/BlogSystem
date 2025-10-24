using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Contracts.ServiceManager;
using BlogSystem.Application.Services.Auth;
using BlogSystem.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using BlogSystem.Application.Abstraction.Contracts.Services.Auth;
using BlogSystem.Application.Abstraction.Contracts.Services.Post;
using BlogSystem.Application.Services.Post;
using BlogSystem.Application.Abstraction.Contracts.Services.Comment;
using BlogSystem.Application.Services.Comments;
using BlogSystem.Application.Abstraction.Contracts.Services.Categories;
using BlogSystem.Application.Services.Categories;
using BlogSystem.Application.Abstraction.Contracts.Services.Tags;
using BlogSystem.Application.Services.Tags;
using BlogSystem.Application.Abstraction.Contracts.Services.Reactions;
using BlogSystem.Application.Services.Reactions;

namespace BlogSystem.Application
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register application services here
            services.AddScoped<IServiceMangar, ServiceManager>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<Func<IAuthService>>(provider => () => provider.GetRequiredService<IAuthService>());

            services.AddScoped<IBlogPostService, BlogPostService>();
            services.AddScoped<Func<IBlogPostService>>(provider => () => provider.GetRequiredService<IBlogPostService>());

            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<Func<ICommentService>>(provider => () => provider.GetRequiredService<ICommentService>());

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<Func<ICategoryService>>(provider => () => provider.GetRequiredService<ICategoryService>());

            services.AddScoped<ITagService, TagService>();
            services.AddScoped<Func<ITagService>>(provider => () => provider.GetRequiredService<ITagService>());

            services.AddScoped<IPostReactionService, PostReactionService>();
            services.AddScoped<Func<IPostReactionService>>(provider => () => provider.GetRequiredService<IPostReactionService>());

            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<Func<IProfileService>>(provider => () => provider.GetRequiredService<IProfileService>());

            services.AddAutoMapper(typeof(DependancyInjection).Assembly);

            return services;
        }
    }
}
