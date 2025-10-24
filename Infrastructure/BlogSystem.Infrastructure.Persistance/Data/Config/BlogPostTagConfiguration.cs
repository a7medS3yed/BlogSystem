using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Domain.Entities.Post;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Infrastructure.Persistence.Data.Config
{
    public class BlogPostTagConfiguration : IEntityTypeConfiguration<BlogPostTag>
    {
        public void Configure(EntityTypeBuilder<BlogPostTag> builder)
        {
            builder.HasKey(pt => new { pt.BlogPostId, pt.TagId });

            // BlogPostTag → BlogPost
            builder.HasOne(pt => pt.BlogPost)
                   .WithMany(p => p.BlogPostTags)
                   .HasForeignKey(pt => pt.BlogPostId)
                   .OnDelete(DeleteBehavior.NoAction);

            // BlogPostTag → Tag
            builder.HasOne(pt => pt.Tag)
                   .WithMany(t => t.BlogPostTags)
                   .HasForeignKey(pt => pt.TagId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }

}
