using e_commerce_store.Data;
using e_commerce_store.Exceptions;
using e_commerce_store.Interfaces;
using e_commerce_store.Models;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_store.Repositories
{

    public class CustomerRepository : ICustomerRepository
    {
        private readonly EcommerceStoreDbContext _dbContext;

        public CustomerRepository(EcommerceStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Customer> AddCustomer(Customer customer)
        {
            try
            {
                await _dbContext.Customer.AddAsync(customer);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new CustomException("Error adding customer.", ex, 500);
            }
            return customer;
        }


        public async Task DeleteCustomer(Customer? customer)
        {
            try
            {
                var existingCustomer = await _dbContext.Customer.FindAsync(customer.Id);
                if (existingCustomer == null)
                {
                    throw new CustomException($"Customer with ID {customer.Id} not found.", 404);
                }

                _dbContext.Customer.Remove(existingCustomer);
                await _dbContext.SaveChangesAsync();
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomException("Error deleting customer.", ex, 500);
            }
        }



        public async Task<Customer> GetCustomerById(int id)
        {
            try
            {
                return await _dbContext.Customer.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new CustomException("Error getting customer.", ex, 500);
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {

            try
            {
                if (_dbContext.IsConnected()) { 
                
                }
                return await _dbContext.Customer.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new CustomException("Error getting customers.", ex, 500);
            }

        }


        public async Task UpdateCustomer(Customer customer)
        {
            try
            {
                _dbContext.Entry(customer).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new CustomException("Error updating customer.", ex, 500);
            }
        }
    }

}
