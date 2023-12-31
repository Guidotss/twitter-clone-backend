﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models; 
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> User { get; set; } = default!;
        public DbSet<Tweet> Tweet { get; set; } = default!;
        public DbSet<Follow> Follows { get; set; } = default!;
        public DbSet<Retweet> Retweets { get; set; } = default!;
        public DbSet<Like> Likes { get; set; } = default!;
        public DbSet<Comment> Comments { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());   
        }
    }
}
