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
        public DbSet<Membership> Membership { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Order> Order { get; set; }

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
