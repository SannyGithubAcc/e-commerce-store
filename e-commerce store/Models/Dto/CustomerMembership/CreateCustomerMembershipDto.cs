using e_commerce_store.Dto;

namespace e_commerce_store.Models.Dto.CustomerMembership
{
    public class CreateCustomerMembershipDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public int CustomerId { get; set; }


    }
}
