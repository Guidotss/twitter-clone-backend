﻿using Microsoft.EntityFrameworkCore;
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
            builder.Property(user => user.ImageUrl).IsRequired(false).HasDefaultValue("res.cloudinary.com/dqsqafh2n/image/upload/v1696429350/default_profile_400x400_vc3l5c.png");
            builder.Property(user => user.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            builder.Property(user => user.UpdatedAt).IsRequired().HasDefaultValueSql("now()");
    
            builder.HasMany(user => user.Followers)
                    .WithOne(follow => follow.Follower)
                    .HasForeignKey(follow => follow.FollowerId)
                    .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(user => user.Following)
                    .WithOne(follow => follow.Followee)
                    .HasForeignKey(follow => follow.FolloweeId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
