using System;
using System.Collections.Generic;
using System.Text;
using MessengerUI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MessengerUI.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Text> Texts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Text>()
                .HasOne(x => x.Sender)
                .WithMany(m => m.TextsSent)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Text>()
                .HasOne(x => x.Recipient)
                .WithMany(x => x.TextsRecieved)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
