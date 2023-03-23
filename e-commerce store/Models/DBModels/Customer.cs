using e_commerce_store.Models.DBModels;

namespace e_commerce_store.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Phone { get; set; }

        public List<CustomerMembership> CustomerMemberships { get; set; }
        public List<Order> Orders { get; set; }
    }
}
