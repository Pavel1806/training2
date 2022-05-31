using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;

namespace DALayer
{
    public partial class NorthwindContext : DbContext
    {
        public NorthwindContext() : base("NorthwindConnection")
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }

        public virtual DbSet<OrderDetails> OrderDetails { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<Product> Products { get; set; }


    }
}
