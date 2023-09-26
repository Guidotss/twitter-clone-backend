using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Configuration
{
    public class FollowConfiguration : IEntityTypeConfiguration<Follow>
    {
        public void Configure(EntityTypeBuilder<Follow> builder)
        {
            builder.Property(follow => follow.Id).IsRequired().HasDefaultValueSql("gen_random_uuid()");
            builder.Property(follow => follow.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            builder.Property(follow => follow.FolloweeId);
            builder.Property(follow => follow.FollowerId);

            builder.HasOne(follow => follow.Follower)
                    .WithMany(user => user.Followers)
                    .HasForeignKey(follow => follow.FollowerId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(follow => follow.Followee)
                    .WithMany(user => user.Following)
                    .HasForeignKey(follow => follow.FolloweeId)
                    .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
