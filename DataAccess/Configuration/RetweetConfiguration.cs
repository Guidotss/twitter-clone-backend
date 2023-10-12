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
    public class RetweetConfiguration : IEntityTypeConfiguration<Retweet>
    {
        public void Configure(EntityTypeBuilder<Retweet> builder)
        {
            builder.Property(retweet => retweet.Id).IsRequired().HasDefaultValueSql("gen_random_uuid()");
            builder.Property(retweet => retweet.CreatedAt).IsRequired().HasDefaultValueSql("now()");
            builder.Property(retweet => retweet.UserId).IsRequired();
            builder.Property(retweet => retweet.TweetId).IsRequired();
        }
    }
}
