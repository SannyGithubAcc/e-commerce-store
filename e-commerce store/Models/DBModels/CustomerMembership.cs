namespace e_commerce_store.Models.DBModels
{
    public class CustomerMembership
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public Membership Membership { get; set; }
    }
}
