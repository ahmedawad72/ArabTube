﻿using ArabTube.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArabTube.Services.DataServices.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> option) : base(option)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppUser>().HasMany(ap => ap.Videos).WithOne(c => c.AppUser).HasForeignKey(c => c.UserId);


            builder.Entity<AppUser>().HasMany(ap => ap.WatchedVideos).WithMany(v => v.Viewers).UsingEntity<WatchedVideo>();
            builder.Entity<WatchedVideo>().HasOne(wv => wv.User).WithMany(ap => ap.History);
            builder.Entity<WatchedVideo>().HasOne(wv => wv.Video).WithMany(v => v.History);

        }

        public DbSet<Video> Videos { get; set; }

        public DbSet<WatchedVideo> WatchedVideos { get; set; }
    }
}
