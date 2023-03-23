using AutoMapper;
using e_commerce_store.Controllers;
using e_commerce_store.Dto;
using e_commerce_store.Exceptions;
using e_commerce_store.Mappings;
using e_commerce_store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
public class CustomerControllerTests
{
    private Mock<ICustomerService> _mockService;
    private IMapper _mapper;
    private ILogger<CustomerController> _logger;
    private CustomerController _controller;

    public CustomerControllerTests()
    {
        // Set up AutoMapper
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MappingProfile());
        });
        _mapper = config.CreateMapper();

        // Set up logger
        _logger = Mock.Of<ILogger<CustomerController>>();

        // Set up service
        _mockService = new Mock<ICustomerService>();

        // Set up controller
        _controller = new CustomerController(_mockService.Object, _mapper, _logger);

    }

    [Fact]
    public async Task GetAllCustomers_ShouldReturnAllCustomers()
    {
        // Arrange
        var customers = new List<CustomerDto>
    {
        new CustomerDto { Id = 1, Name = "John Doe", Email = "johndoe@example.com", Phone = "222", IsActive = true },
        new CustomerDto { Id = 2, Name = "Jane Doe", Email = "janedoe@example.com", Phone = "333", IsActive = false },
    };
        _mockService.Setup(service => service.GetAllCustomers()).ReturnsAsync(customers);

        // Act
        var result = await _controller.GetAllCustomers();

        // Assert
        var okObjectResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedCustomers = Assert.IsAssignableFrom<IEnumerable<CustomerDto>>(okObjectResult.Value);
        Assert.Equal(customers.Count, returnedCustomers.Count());
        foreach (var expectedCustomer in customers)
        {
            var actualCustomer = returnedCustomers.FirstOrDefault(c => c.Id == expectedCustomer.Id);
            Assert.NotNull(actualCustomer);
            Assert.Equal(expectedCustomer.Name, actualCustomer.Name);
            Assert.Equal(expectedCustomer.Email, actualCustomer.Email);
            Assert.Equal(expectedCustomer.Phone, actualCustomer.Phone);
            Assert.Equal(expectedCustomer.IsActive, actualCustomer.IsActive);
        }
    }

    [Fact]
    public async Task GetCustomerById_WithValidId_ShouldReturnCustomer()
    {
        // Arrange
        var id = 1;
        var customer = new CustomerDto { Id = 1, Name = "John Doe", Email = "johndoe@example.com", Phone = "222", IsActive = true };
        _mockService.Setup(service => service.GetCustomerById(id)).ReturnsAsync(customer);

        // Act
        var result = await _controller.GetCustomerById(id);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedCustomer = Assert.IsType<CustomerDto>(okResult.Value);
        Assert.Equal(customer.Id, returnedCustomer.Id);
        Assert.Equal(customer.Name, returnedCustomer.Name);
        Assert.Equal(customer.Email, returnedCustomer.Email);
        Assert.Equal(customer.Phone, returnedCustomer.Phone);
        Assert.Equal(customer.IsActive, returnedCustomer.IsActive);
    }

    [Fact]
    public async Task GetCustomerById_WithInvalidId_ShouldReturnNotFound()
    {
        // Arrange
        var id = 1;
        CustomerDto customer = null;
        _mockService.Setup(service => service.GetCustomerById(id)).ReturnsAsync(customer);

    

        // Act
        var result = await _controller.GetCustomerById(id);
                          
        // Assert
        Assert.True(result.Value == null);
    }

    [Fact]
    public async Task AddCustomer_WithValidDto_ShouldReturnCreated()
    {
        // Arrange
        var customerCreateDto = new CustomerCreateDto { Name = "John Doe", Email = "johndoe@example.com", Phone = "222", IsActive = true };
        var addedCustomerDto = new CustomerDto { Id = 1, Name = "John Doe", Email = "johndoe@example.com", Phone = "222", IsActive = true };
        _mockService.Setup(service => service.AddCustomer(customerCreateDto)).ReturnsAsync(addedCustomerDto);

        // Act
        var controller = new CustomerController(_mockService.Object, _mapper, null);
        var result = await controller.AddCustomer(customerCreateDto);

        // Assert
        var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var customerDto = Assert.IsType<CustomerDto>(createdAtActionResult.Value);
        Assert.Equal(addedCustomerDto.Id, customerDto.Id);
        Assert.Equal(addedCustomerDto.Name, customerDto.Name);
        Assert.Equal(addedCustomerDto.Email, customerDto.Email);
        Assert.Equal(addedCustomerDto.Phone, customerDto.Phone);
        Assert.Equal(addedCustomerDto.IsActive, customerDto.IsActive);
        Assert.Equal(nameof(CustomerController.GetCustomerById), createdAtActionResult.ActionName);
        Assert.Equal(customerDto.Id, createdAtActionResult.RouteValues["id"]);
    }
    [Fact]
    public async Task UpdateCustomer_ShouldReturnNoContent()
    {
        // Arrange
        var controller = new CustomerController(_mockService.Object, _mapper, null);
        var customerId = 1;
        var customerUpdateDto = new CustomerUpdateDto { Name = "John Smith", Email = "johnsmith@example.com", Phone = "1234567890" };

        // Act
        var result = await controller.UpdateCustomer(customerId, customerUpdateDto);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task UpdateCustomer_WithInvalidData_ShouldReturnBadRequest()
    {
        // Arrange
        var controller = new CustomerController(_mockService.Object, _mapper, null);
        var customerId = 1;
        var customerUpdateDto = new CustomerUpdateDto { Name = "", Email = "invalid-email" };
        controller.ModelState.AddModelError("Name", "The Name field is required.");
        controller.ModelState.AddModelError("Email", "The Email field is not a valid e-mail address.");

        // Act
        var result = await controller.UpdateCustomer(customerId, customerUpdateDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.IsType<SerializableError>(badRequestResult.Value);
        Assert.True(controller.ModelState.ErrorCount > 0);
    }

    [Fact]
        public async Task UpdateCustomer_WithNonExistingCustomer_ShouldReturnNotFound()
        {
            // Arrange
            _mockService.Setup(x => x.UpdateCustomer(It.IsAny<int>(), It.IsAny<CustomerUpdateDto>()))
                .ThrowsAsync(new CustomException("Customer not found", 404));
            var controller = new CustomerController(_mockService.Object, _mapper, null);
            var customerId = 1;
            var customerUpdateDto = new CustomerUpdateDto { Name = "John Smith", Email = "johnsmith@example.com", Phone = "1234567890" };

            // Act
            var result = await controller.UpdateCustomer(customerId, customerUpdateDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("Customer not found", notFoundResult.Value);
        }

        [Fact]
        public async Task UpdateCustomer_WithException_ShouldReturnInternalServerError()
        {
            // Arrange
            _mockService.Setup(x => x.UpdateCustomer(It.IsAny<int>(), It.IsAny<CustomerUpdateDto>()))
                .ThrowsAsync(new Exception("Something went wrong"));
            var customerId = 1;
            var customerUpdateDto = new CustomerUpdateDto { Name = "John Smith", Email = "johnsmith@example.com", Phone = "1234567890" };

            // Act
            var result = await _controller.UpdateCustomer(customerId, customerUpdateDto);

            // Assert
            var internalServerErrorResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(500, internalServerErrorResult.StatusCode);
        }
    }


