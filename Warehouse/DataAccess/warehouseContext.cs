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
        public warehouseContext()
        {
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new WarehouseDBInitializer());
        }

       /* protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Handle cycles by not traversing relations
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Default the database to use datetime2 type rather than the default SqlDateTime
            // due to the fact that the oldest date SqlDateTime can hold is 1/1-1753, and
            // William Shakespeare was born in 1564!
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            // Control how the mapping between Authors and Books are handled
            modelBuilder.Entity<Category>()
                .HasMany(a => a.hasDiscount)
                .WithMany(b => b.discounts)
                .Map(m =>
                {
                    m.ToTable("MemberCategory");
                    m.MapLeftKey("membershipId ");
                    m.MapRightKey("categoryId");
                });
        }*/
    }
}
