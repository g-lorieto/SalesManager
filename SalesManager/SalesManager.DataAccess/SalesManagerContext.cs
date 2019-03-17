using Microsoft.EntityFrameworkCore;
using SalesManager.Core.Interfaces;
using SalesManager.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SalesManager.DataAccess
{
    public class SalesManagerContext : DbContext
    {
        public SalesManagerContext(DbContextOptions<SalesManagerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>()
                .HasIndex(p => new { p.Name })
                .IsUnique(true);

            builder.Entity<Sale>()
                .HasMany(s => s.Items)
                .WithOne(i => i.Sale)
                .IsRequired();

            builder.Entity<Client>()
                .HasIndex(p => new { p.Name })
                .IsUnique(true);
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
}
