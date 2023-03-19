using AutoMapper;
using e_commerce_store.Dto;
using e_commerce_store.Exceptions;
using e_commerce_store.Models;


    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(ICustomerRepository customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllCustomers()
        {
            var customers = await _customerRepository.GetCustomers();
            return _mapper.Map<IEnumerable<CustomerDto>>(customers);
        }

        public async Task<CustomerDto> GetCustomerById(int id)
        {
            var customer = await _customerRepository.GetCustomerById(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public async Task<CustomerDto> AddCustomer(CustomerCreateDto customerCreateDto)
        {
            var customer = _mapper.Map<Customer>(customerCreateDto);
            var addedCustomer = await _customerRepository.AddCustomer(customer);
            return _mapper.Map<CustomerDto>(addedCustomer);
        }

        public async Task UpdateCustomer(int id, CustomerUpdateDto customerUpdateDto)
        {
            var existingCustomer = await _customerRepository.GetCustomerById(id);
            if (existingCustomer == null)
            {
                throw new CustomException("Customer not found", 404);
            }
            _mapper.Map(customerUpdateDto, existingCustomer);
            await _customerRepository.UpdateCustomer(existingCustomer);
        }
        public async Task DeleteCustomer(int id)
        {
            var existingCustomer = await _customerRepository.GetCustomerById(id);
            if (existingCustomer == null)
            {
                throw new CustomException("Customer not found", 404);
            }
            await _customerRepository.DeleteCustomer(existingCustomer);
        }
    }

