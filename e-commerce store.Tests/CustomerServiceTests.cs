using AutoMapper;
using e_commerce_store.Dto;
using e_commerce_store.Exceptions;
using e_commerce_store.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

public class CustomerServiceTests
{
    private readonly IMapper _mapper;
    private readonly Mock<ICustomerRepository> _mockRepository;
    private readonly CustomerService _customerService;

    public CustomerServiceTests()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Customer, CustomerDto>();
            cfg.CreateMap<CustomerCreateDto, Customer>();
            cfg.CreateMap<CustomerUpdateDto, Customer>();
        });
        _mapper = mapperConfig.CreateMapper();

        _mockRepository = new Mock<ICustomerRepository>();

        _customerService = new CustomerService(_mockRepository.Object, _mapper);
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
        _mockRepository.Setup(repo => repo.GetCustomers()).ReturnsAsync(customers);

        // Act
        var result = await _customerService.GetAllCustomers();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customerDtos.Count, result.Count());
        Assert.Equal(customerDtos.ElementAt(0).Id, result.ElementAt(0).Id);
        Assert.Equal(customerDtos.ElementAt(1).Name, result.ElementAt(1).Name);
        Assert.Equal(customerDtos.ElementAt(2).Email, result.ElementAt(2).Email);
    }

    [Fact]
    public async Task GetCustomerById_ShouldReturnCustomerWithGivenId()
    {
        // Arrange
        var customer = new Customer { Id = 1, Name = "John Doe", Email = "johndoe@example.com" };
        var customerDto = new CustomerDto { Id = 1, Name = "John Doe", Email = "johndoe@example.com" };
        _mockRepository.Setup(repo => repo.GetCustomerById(customer.Id)).ReturnsAsync(customer);

        // Act
        var result = await _customerService.GetCustomerById(customer.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(customerDto.Id, result.Id);
        Assert.Equal(customerDto.Name, result.Name);
        Assert.Equal(customerDto.Email, result.Email);
    }

    [Fact]
    public async Task UpdateCustomer_ShouldUpdateExistingCustomer()
    {
        // Arrange
        var id = 1;
        var customerUpdateDto = new CustomerUpdateDto { Name = "John Smith", Email = "johnsmith@example.com" };
        var existingCustomer = new Customer { Id = 1, Name = "John Doe", Email = "johndoe@example.com" };
        var updatedCustomer = _mapper.Map<Customer>(customerUpdateDto);
        _mockRepository.Setup(repo => repo.GetCustomerById(id)).ReturnsAsync(existingCustomer);
        _mockRepository.Setup(repo => repo.UpdateCustomer(updatedCustomer)).Returns(Task.CompletedTask);

        // Act
        await _customerService.UpdateCustomer(id, customerUpdateDto);

        // Assert
        _mockRepository.Verify(repo => repo.UpdateCustomer(It.Is<Customer>(c =>
            c.Id == existingCustomer.Id &&
            c.Name == updatedCustomer.Name &&
            c.Email == updatedCustomer.Email)), Times.Once);
    }

    [Fact]
    public async Task UpdateCustomer_ShouldThrowCustomExceptionWhenCustomerNotFound()
    {
        // Arrange
        var id = 1;
        var customerUpdateDto = new CustomerUpdateDto { Name = "John Smith", Email = "johnsmith@example.com" };
        _mockRepository.Setup(repo => repo.GetCustomerById(id)).ReturnsAsync((Customer)null);

        // Act & Assert
        await Assert.ThrowsAsync<CustomException>(() => _customerService.UpdateCustomer(id, customerUpdateDto));
    }

    [Fact]
    public async Task DeleteCustomer_ShouldDeleteExistingCustomer()
    {
        // Arrange
        var id = 1;
        var existingCustomer = new Customer { Id = 1, Name = "John Doe", Email = "johndoe@example.com" };
        _mockRepository.Setup(repo => repo.GetCustomerById(id)).ReturnsAsync(existingCustomer);
        _mockRepository.Setup(repo => repo.DeleteCustomer(existingCustomer)).Returns(Task.CompletedTask);

        // Act
        await _customerService.DeleteCustomer(id);

        // Assert
        _mockRepository.Verify(repo => repo.DeleteCustomer(existingCustomer), Times.Once);
    }

    [Fact]
    public async Task DeleteCustomer_ShouldThrowCustomExceptionWhenCustomerNotFound()
    {
        // Arrange
        var id = 1;
        _mockRepository.Setup(repo => repo.GetCustomerById(id)).ReturnsAsync((Customer)null);

        // Act & Assert
        await Assert.ThrowsAsync<CustomException>(() => _customerService.DeleteCustomer(id));
    }
}
