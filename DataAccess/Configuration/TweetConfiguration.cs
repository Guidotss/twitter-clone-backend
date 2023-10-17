using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging.Configuration;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configuration
{
    public class TweetConfiguration : IEntityTypeConfiguration<Tweet>
    {
        public void Configure(EntityTypeBuilder<Tweet> builder)
        {
            builder.Property(tweet => tweet.Id).IsRequired().HasDefaultValueSql("gen_random_uuid()");
            builder.Property(tweet => tweet.ImageUrl).IsRequired(false).HasMaxLength(280);
            builder.Property(tweet => tweet.GifUrl).IsRequired(false).HasMaxLength(280);
            builder.Property(tweet => tweet.Content).IsRequired().HasMaxLength(280);
            builder.Property(tweet => tweet.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            builder.Property(Tweet => Tweet.UpdatedAt).IsRequired().HasDefaultValueSql("now()");
            builder.Property(tweet => tweet.UserId).IsRequired();   
        }
    }
}
