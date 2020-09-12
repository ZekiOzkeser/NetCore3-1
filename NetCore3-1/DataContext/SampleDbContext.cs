using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NetCore3_1.Entities;

namespace NetCore3_1.DataContext
{
    public class SampleDbContext:DbContext
    {
        public SampleDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItems> OrderItemses { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItems>()
                .HasOne(p => p.Order)
                .WithMany(b => b.OrderItems)
                .HasForeignKey(c => c.OrderId);
        }
    }
}
