using System;
using System.Data.Entity;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() :
            base("NorthwindConnection")
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
