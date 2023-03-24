using e_commerce_store.Models;
using e_commerce_store.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store.Data
{
    public class EcommerceStoreDbContext : DbContext
    {
        public EcommerceStoreDbContext(DbContextOptions<EcommerceStoreDbContext> options) : base(options)
        {
        }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerMembership> CustomerMembership { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderProduct> OrderProduct { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Membership> Membership { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>()
           .HasKey(c => c.Id);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => op.Id);

            modelBuilder.Entity<CustomerMembership>()
                .HasKey(cm => cm.Id);

            modelBuilder.Entity<Membership>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);




            modelBuilder.Entity<OrderProduct>()
        .HasOne<Product>()
        .WithMany()
        .HasForeignKey(op => op.Product_ID);

        }

        public bool IsConnected()
        {
            try
            {
                Database.CanConnect();
                return true;
            }
            catch
            {
                return false;
            }
        }

    }

}
