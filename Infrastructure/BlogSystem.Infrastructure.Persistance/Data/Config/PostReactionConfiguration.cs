using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogSystem.Domain.Entities.Reaction;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BlogSystem.Infrastructure.Persistence.Data.Config
{
    public class PostReactionConfiguration : IEntityTypeConfiguration<PostReactions>
    {
        public void Configure(EntityTypeBuilder<PostReactions> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.BlogPost)
                   .WithMany(p => p.Reactions)
                   .HasForeignKey(r => r.PostId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(r => r.User)
                   .WithMany()
                   .HasForeignKey(r => r.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            // Unique constraint: user can react only once per post
            builder.HasIndex(r => new { r.PostId, r.UserId }).IsUnique();
        }
    }

}
