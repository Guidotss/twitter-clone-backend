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
    public class UserConfiguaration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(user => user.Id).IsRequired().HasDefaultValueSql("gen_random_uuid()");
            builder.Property(user => user.Name).IsRequired().HasMaxLength(50);
            builder.Property(user => user.Email).IsRequired().HasMaxLength(50);
            builder.Property(user => user.Password).IsRequired(); 
            builder.Property(user => user.Bio).IsRequired(false).HasMaxLength(120);
            builder.Property(user => user.ImageUrl).IsRequired(false);
            builder.Property(user => user.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            builder.Property(user => user.UpdatedAt).IsRequired().HasDefaultValueSql("now()");

            builder.HasMany(user => user.Tweets)
                    .WithOne(tweet => tweet.User)
                    .HasForeignKey(tweet => tweet.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Followers)
                    .WithOne(follow => follow.Follower)
                    .HasForeignKey(follow => follow.FollowerId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Following)
                    .WithOne(follow => follow.Followee)
                    .HasForeignKey(follow => follow.FolloweeId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Comments)
                    .WithOne(comment => comment.User)
                    .HasForeignKey(comment => comment.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Likes)
                    .WithOne(like => like.User)
                    .HasForeignKey(like => like.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Retweets)
                    .WithOne(retweet => retweet.User)
                    .HasForeignKey(retweet => retweet.UserId)
                    .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
