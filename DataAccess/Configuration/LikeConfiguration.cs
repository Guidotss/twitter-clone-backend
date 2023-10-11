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
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.Property(like => like.Id).IsRequired().HasDefaultValueSql("gen_random_uuid()");
            builder.Property(like => like.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            builder.Property(like => like.UserId).IsRequired();
            builder.Property(like => like.TweetId).IsRequired();

        }
    }
}
