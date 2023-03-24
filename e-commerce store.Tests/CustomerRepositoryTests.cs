using AutoMapper;
using e_commerce_store.Data;
using e_commerce_store.Dto;
using e_commerce_store.Exceptions;
using e_commerce_store.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

public class CustomerRepositoryTests
{
    private readonly DbContextOptions<EcommerceStoreDbContext> _options;
    private readonly EcommerceStoreDbContext _dbContext;

    public CustomerRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<EcommerceStoreDbContext>()
            .UseInMemoryDatabase(databaseName: "Ecommerance")
            .Options;
        _dbContext = new EcommerceStoreDbContext(_options);
    }

    [Fact]
    public async Task AddCustomer_ShouldAddCustomerToDatabase()
    {
        // Arrange
        var repository = new CustomerRepository(_dbContext);
        var customer = new Customer { Name = "John Doe", Email = "johndoe@example.com",Phone = "22222",IsActive=true };

        // Act
        var result = await repository.AddCustomer(customer);

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(0, result.Id);
        Assert.Equal(customer.Name, result.Name);
        Assert.Equal(customer.Email, result.Email);
        Assert.Equal(1, _dbContext.Customer.Count());
    }

    [Fact]
    public async Task DeleteCustomer_ShouldDeleteCustomerFromDatabase()
    {
        // Arrange
        var repository = new CustomerRepository(_dbContext);
        var customer = new Customer { Name = "John Doe", Email = "johndoe@example.com", Phone = "22222", IsActive = true };
        _dbContext.Customer.Add(customer);
        await _dbContext.SaveChangesAsync();

        // Act
        await repository.DeleteCustomer(customer);

        // Assert
        Assert.Equal(0, _dbContext.Customer.Count());
    }

    [Fact]
    public async Task DeleteCustomer_ShouldThrowCustomException_WhenCustomerNotFound()
    {
        // Arrange
        var repository = new CustomerRepository(_dbContext);
        var customer = new Customer { Id = 1 };

        // Act and Assert
        await Assert.ThrowsAsync<CustomException>(() => repository.DeleteCustomer(customer));
    }

    [Fact]
    public async Task GetCustomerById_ShouldReturnCustomerFromDatabase()
    {
        // Arrange
        var repository = new CustomerRepository(_dbContext);
        var customer = new Customer { Name = "John Doe", Email = "johndoe@example.com", Phone = "22222", IsActive = true };
        _dbContext.Customer.Add(customer);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetCustomerById(customer.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customer.Name, result.Name);
        Assert.Equal(customer.Email, result.Email);
    }

    [Fact]
    public async Task GetCustomerById_ShouldReturnNull_WhenCustomerNotFound()
    {
        // Arrange
        var repository = new CustomerRepository(_dbContext);
        var customerId = 1;

        // Act
        var result = await repository.GetCustomerById(customerId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetCustomers_ShouldReturnAllCustomersFromDatabase()
    {
        // Arrange
        var repository = new CustomerRepository(_dbContext);
        var customers = new List<Customer>
        {
            new Customer { Name = "John Doe", Email = "johndoe@example.com",Phone = "444",IsActive = true },
            new Customer { Name = "Jane Doe", Email = "janedoe@example.com" ,Phone = "22222",IsActive = true},
            new Customer { Name = "Bob Smith", Email = "bobsmith@example.com",Phone = "44554",IsActive = true }
        };
        _dbContext.Customer.AddRange(customers);
        await _dbContext.SaveChangesAsync();

        // Act
        var result = await repository.GetCustomers();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customers.Count, result.Count());

    }
    [Fact]
    public async Task UpdateCustomer_ShouldUpdateCustomerInDatabase()
    {
        // Arrange
        var repository = new CustomerRepository(_dbContext);
        var customer = new Customer { Name = "John Doe", Email = "johndoe@example.com" , Phone = "22222",IsActive=true };
        _dbContext.Customer.Add(customer);
        await _dbContext.SaveChangesAsync();
        var updatedCustomer = new Customer { Id = customer.Id, Name = "John Smith", Email = "johnsmith@example.com", Phone = "444", IsActive = true };
        _dbContext.Entry(customer).State = EntityState.Detached; // Detach the original customer entity

        // Act
        await repository.UpdateCustomer(updatedCustomer);

        // Assert
        var result = await repository.GetCustomerById(updatedCustomer.Id);
        Assert.NotNull(result);
        Assert.Equal(updatedCustomer.Name, result.Name);
        Assert.Equal(updatedCustomer.Email, result.Email);
    }

    [Fact]
    public async Task UpdateCustomer_ShouldThrowCustomException_WhenCustomerNotFound()
    {
        // Arrange
        var repository = new CustomerRepository(_dbContext);
        var customer = new Customer { Id = 1 };

        // Act and Assert
        await Assert.ThrowsAsync<CustomException>(() => repository.UpdateCustomer(customer));
    }

    [Fact]
    public async Task GetAllCustomers_ShouldReturnAllCustomers()
    {
        // Arrange
        var customers = new List<Customer>
    {
        new Customer { Id = 1, Name = "John Doe", Email = "johndoe@example.com" },
        new Customer { Id = 2, Name = "Jane Doe", Email = "janedoe@example.com" },
        new Customer { Id = 3, Name = "Bob Smith", Email = "bobsmith@example.com" }
    };
        var customerDtos = new List<CustomerDto>
    {
        new CustomerDto { Id = 1, Name = "John Doe", Email = "johndoe@example.com" },
        new CustomerDto { Id = 2, Name = "Jane Doe", Email = "janedoe@example.com" },
        new CustomerDto { Id = 3, Name = "Bob Smith", Email = "bobsmith@example.com" }
    };
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Customer, CustomerDto>();
        });
        var mapper = mapperConfig.CreateMapper();
        var mockRepo = new Mock<ICustomerRepository>();
        mockRepo.Setup(repo => repo.GetCustomers()).ReturnsAsync(customers);
        var service = new CustomerService(mockRepo.Object, mapper);

        // Act
        var result = await service.GetAllCustomers();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customerDtos.Count, result.Count());
        Assert.Equal(customerDtos.ElementAt(0).Id, result.ElementAt(0).Id);
        Assert.Equal(customerDtos.ElementAt(1).Name, result.ElementAt(1).Name);
        Assert.Equal(customerDtos.ElementAt(2).Email, result.ElementAt(2).Email);
    }


}
