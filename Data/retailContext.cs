using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using retail.Models;

namespace retail.Data
{
    public class retailContext : DbContext
    {
        public retailContext (DbContextOptions<retailContext> options)
            : base(options)
        {
        }

        public DbSet<retail.Models.Customer> Customer { get; set; } = default!;
        public DbSet<retail.Models.Product> Product { get; set; } = default!;
        public DbSet<retail.Models.OrderDetail> OrderDetail { get; set; } = default!;
        public DbSet<retail.Models.Order> Order{ get; set; } = default!;
        public DbSet<retail.Models.Cart> Cart { get; set; } = default!;
    }
}
