using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DataAccess.Configuration
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(comment => comment.Id)
                .IsRequired()
                .HasDefaultValueSql("gen_random_uuid()"); 
            builder.Property(comment => comment.Content)
                .HasMaxLength(280)
                .IsRequired();
            builder.Property(comment => comment.UserId)
                    .HasConversion<Guid>()
                    .IsRequired(); 
            
            builder.Property(comment => comment.User)
                .HasConversion<Guid>()
                .HasField("userId")
                .IsRequired();

            builder.Property(comment => comment.TweetId)
                .HasConversion<Guid>()
                .IsRequired();
            builder.Property(comment => comment.Tweet)
                .HasConversion<Guid>()
                .HasField("tweetId")
                .IsRequired();
        }
    }
}
