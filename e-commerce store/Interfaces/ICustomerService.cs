using e_commerce_store.Dto;

namespace e_commerce_store.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllCustomers();
        Task<CustomerDto> GetCustomerById(int id);
        Task<CustomerDto> AddCustomer(CustomerCreateDto customerCreateDto);
        Task UpdateCustomer(int id, CustomerUpdateDto customerUpdateDto);
        Task DeleteCustomer(int id);
    }

}
