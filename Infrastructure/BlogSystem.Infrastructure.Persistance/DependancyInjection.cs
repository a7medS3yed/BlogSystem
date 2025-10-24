using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Application.Abstraction.Contracts.Repository;
using BlogSystem.Application.Abstraction.Contracts.Repository.Comment;
using BlogSystem.Application.Abstraction.Contracts.Repository.Posts;
using BlogSystem.Application.Abstraction.Contracts.Repository.Reactions;
using BlogSystem.Application.Abstraction.Contracts.UnitOfWork;
using BlogSystem.Infrastructure.Persistence.Data;
using BlogSystem.Infrastructure.Persistence.Repository;
using BlogSystem.Infrastructure.Persistence.Repository.Comments;
using BlogSystem.Infrastructure.Persistence.Repository.Posts;
using BlogSystem.Infrastructure.Persistence.Repository.Reactions;
using BlogSystem.Infrastructure.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogSystem.Infrastructure.Persistence
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IPostReactionRepository, PostReactionRepository>();
            


            return services;
        }
    }
}
