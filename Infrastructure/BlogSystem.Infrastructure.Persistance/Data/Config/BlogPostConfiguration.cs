using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogSystem.Infrastructure.Persistence.Data.Config
{
    public class BlogPostConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(EntityTypeBuilder<BlogPost> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Content)
                .IsRequired();

            // BlogPost → Category (M - 1)
            builder.HasOne(p => p.Category)
                   .WithMany(c => c.BlogPosts)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.NoAction);

            // BlogPost → Author (M - 1)
            builder.HasOne(p => p.Author)
                   .WithMany(u => u.BlogPosts)
                   .HasForeignKey(p => p.AuthorId)
                   .OnDelete(DeleteBehavior.NoAction);

            // BlogPost → Comments (1 - M)
            builder.HasMany(p => p.Comments)
                   .WithOne(c => c.BlogPost)
                   .HasForeignKey(c => c.BlogPostId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
