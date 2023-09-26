using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
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
            builder.Property(tweet => tweet.Content).IsRequired().HasMaxLength(280);
            builder.Property(tweet => tweet.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            builder.Property(Tweet => Tweet.UpdatedAt).IsRequired().HasDefaultValueSql("now()");

            builder.HasOne(tweet => tweet.User)
                    .WithMany(user => user.Tweets)
                    .HasForeignKey(tweet => tweet.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(tweet => tweet.Likes)
                    .WithOne(like => like.Tweet)
                    .HasForeignKey(like => like.TweetId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(tweet => tweet.Retweets)
                    .WithOne(retweet => retweet.Tweet)
                    .HasForeignKey(retweet => retweet.TweetId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(tweet => tweet.Comments)
                    .WithOne(comment => comment.Tweet)
                    .HasForeignKey(comment => comment.TweetId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
