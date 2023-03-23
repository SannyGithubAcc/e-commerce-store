using e_commerce_store.Data;
using e_commerce_store.Exceptions;
using e_commerce_store.Models;
using Microsoft.EntityFrameworkCore;


    public class CustomerRepository : ICustomerRepository
    {
        private readonly EcommerceStoreDbContext _dbContext;

        public CustomerRepository(EcommerceStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<Customer> AddCustomer(Customer Customers)
        {
            try
            {
                await _dbContext.Customer.AddAsync(Customers);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new CustomException("Error adding Customers.", ex, 500);
            }
            return Customers;
        }


        public async Task DeleteCustomer(Customer? Customers)
        {
            try
            {
                var existingCustomers = await _dbContext.Customer.FindAsync(Customers.Id);
                if (existingCustomers == null)
                {
                    throw new CustomException($"Customers with ID {Customers.Id} not found.", 404);
                }

                _dbContext.Customer.Remove(existingCustomers);
                await _dbContext.SaveChangesAsync();
            }
            catch (CustomException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw new CustomException("Error deleting Customers.", ex, 500);
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
                throw new CustomException("Error getting Customers.", ex, 500);
            }
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {

            try
            {
                return await _dbContext.Customer.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new CustomException("Error getting Customerss.", ex, 500);
            }

        }


        public async Task UpdateCustomer(Customer Customers)
        {
            try
            {
                _dbContext.Entry(Customers).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new CustomException("Error updating Customers.", ex, 500);
            }
        }
    }


