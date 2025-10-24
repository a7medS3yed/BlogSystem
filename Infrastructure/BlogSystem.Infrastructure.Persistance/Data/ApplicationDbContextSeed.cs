using System.Text.Json;
using BlogSystem.Domain.Entities.Comments;
using BlogSystem.Domain.Entities.Post;
using BlogSystem.Domain.Entities.User;
using BlogSystem.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Identity;

namespace BlogSystem.Infrastructure.Persistence.Seed
{
    public static class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            // ✅ Seed Roles
            var roles = new[] { "Admin", "Editor", "Reader" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }

            // ✅ Seed Admin User
            var adminEmail = "admin@blog.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = "admin",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123"); // Default password
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // ✅ Seed Editor User
            var editorEmail = "editor@blog.com";
            if (await userManager.FindByEmailAsync(editorEmail) == null)
            {
                var editorUser = new ApplicationUser
                {
                    UserName = "editor",
                    Email = editorEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(editorUser, "Editor@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(editorUser, "Editor");
                }
            }


            // ✅ Seed Categories
            if (!context.Categories.Any())
            {
                var categoriesData = File.ReadAllText(@"D:\Career\Projects\Backend\BlogSystem\Infrastructure\BlogSystem.Infrastructure.Persistance\Data\Seeds\categories.json");
                var categories = JsonSerializer.Deserialize<List<Category>>(categoriesData);
                if (categories != null && categories.Any())
                {
                    context.Categories.AddRange(categories);
                    await context.SaveChangesAsync();
                }
            }

            // ✅ Seed Tags
            if (!context.Tags.Any())
            {
                var tagsData = File.ReadAllText(@"D:\Career\Projects\Backend\BlogSystem\Infrastructure\BlogSystem.Infrastructure.Persistance\Data\Seeds\tags.json");
                var tags = JsonSerializer.Deserialize<List<Tag>>(tagsData);
                if (tags != null && tags.Any())
                {
                    context.Tags.AddRange(tags);
                    await context.SaveChangesAsync();
                }
            }

            // ✅ Seed Posts
            if (!context.BlogPosts.Any())
            {
                var PostsData = File.ReadAllText(@"D:\Career\Projects\Backend\BlogSystem\Infrastructure\BlogSystem.Infrastructure.Persistance\Data\Seeds\Posts.json");
                var Posts = JsonSerializer.Deserialize<List<BlogPost>>(PostsData);
                if (Posts != null && Posts.Any())
                {
                    context.BlogPosts.AddRange(Posts);
                    await context.SaveChangesAsync();
                }
            }

            // ✅ Seed Comments
            if (!context.Comments.Any())
            {
                var CommentsData = File.ReadAllText(@"D:\Career\Projects\Backend\BlogSystem\Infrastructure\BlogSystem.Infrastructure.Persistance\Data\Seeds\comments.json");
                var Comments = JsonSerializer.Deserialize<List<Comment>>(CommentsData);
                if (Comments != null && Comments.Any())
                {
                    context.Comments.AddRange(Comments);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
