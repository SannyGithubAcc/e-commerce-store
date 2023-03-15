using e_commerce_store.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store.Data
{
    public class EcommerceStoreDbContext : DbContext
    {
        public EcommerceStoreDbContext(DbContextOptions<EcommerceStoreDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customer { get; set; }

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
