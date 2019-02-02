using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLibrary;

namespace DataAccess
{
    public class warehouseContext : DbContext
    {
        public virtual DbSet<Category> Categories{ get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<ProductToOrder> ProdToOrders { get; set; }
        public warehouseContext()
        {
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new WarehouseDBInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Handle cycles by not traversing relations
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Control how the mapping between Authors and Books are handled


            modelBuilder.Entity<ProductToOrder>()
                .HasKey(um => um.prodToOrderId)
                .ToTable("ProdToOrder");

            modelBuilder.Entity<ProductToOrder>()
                .HasRequired(um => um.product).WithMany(g => g.ProductsToOrders)
                .HasForeignKey(um => um.prodDescription);

            modelBuilder.Entity<ProductToOrder>()
                .HasRequired(um => um.order).WithMany(g => g.ProductsToOrders)
                .HasForeignKey(um => um.orderId);

            modelBuilder.Entity<Customer>()
                .HasMany(a => a.Orders)
                .WithMany(b => b.Customers)
                .Map(m =>
                {
                    m.ToTable("OrderCustomer");
                    m.MapLeftKey("customerId");
                    m.MapRightKey("orderId");
                });
            
        }
    }
}
