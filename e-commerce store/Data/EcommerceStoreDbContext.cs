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

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => new { op.Order_ID, op.Product_ID });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.Order_ID);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.Product_ID); 

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<CustomerMembership>()
                .HasKey(cm => cm.Id);
            modelBuilder.Entity<CustomerMembership>()
                .HasOne(cm => cm.Customer)
                .WithMany(c => c.CustomerMemberships)
                .HasForeignKey(cm => cm.CustomerId);
            modelBuilder.Entity<CustomerMembership>()
                .HasOne(cm => cm.Membership)
                .WithMany(m => m.CustomerMemberships);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.Customer_ID);

            modelBuilder.Entity<OrderProduct>()
                .HasKey(op => op.Id);
            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.Order_ID);
            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.Product_ID);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Membership>()
                .HasKey(m => m.Id);
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
