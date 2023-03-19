using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using e_commerce_store.Controllers;
using e_commerce_store.Dto;
using e_commerce_store.Exceptions;
using e_commerce_store.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace e_commerce_store.Tests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        private CustomerController _controller;
        private Mock<ICustomerService> _customerServiceMock;
        private Mock<IMapper> _mapperMock;
        private Mock<ILogger<CustomerController>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _customerServiceMock = new Mock<ICustomerService>();
            _mapperMock = new Mock<IMapper>();
            _loggerMock = new Mock<ILogger<CustomerController>>();
            _controller = new CustomerController(_customerServiceMock.Object, _mapperMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetAllCustomers_ReturnsCustomers()
        {
            // Arrange
            var customers = new List<CustomerDto> { new CustomerDto { Id = 1, Name = "John Doe" }, new CustomerDto { Id = 2, Name = "Jane Doe" } };
            _customerServiceMock.Setup(s => s.GetAllCustomers()).ReturnsAsync(customers);
            var customerDtos = new List<CustomerDto> { new CustomerDto { Id = 1, Name = "John Doe" }, new CustomerDto { Id = 2, Name = "Jane Doe" } };
            _mapperMock.Setup(m => m.Map<IEnumerable<CustomerDto>>(customers)).Returns(customerDtos);

            // Act
            var result = await _controller.GetAllCustomers();

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okObjectResult = (OkObjectResult)result.Result;
            var resultValue = (IEnumerable<CustomerDto>)okObjectResult.Value;
            Assert.AreEqual(customerDtos, resultValue);
        }

        [Test]
        public async Task GetCustomerById_ReturnsCustomer()
        {
            // Arrange
            var customerId = 1;
            var customer = new CustomerDto { Id = customerId, Name = "John Doe" };
            _customerServiceMock.Setup(s => s.GetCustomerById(customerId)).ReturnsAsync(customer);
            var customerDto = new CustomerDto { Id = customerId, Name = "John Doe" };
            _mapperMock.Setup(m => m.Map<CustomerDto>(customer)).Returns(customerDto);

            // Act
            var result = await _controller.GetCustomerById(customerId);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result.Result);
            var okObjectResult = (OkObjectResult)result.Result;
            var resultValue = (CustomerDto)okObjectResult.Value;
            Assert.AreEqual(customerDto, resultValue);
        }

        [Test]
        public async Task GetCustomerById_ReturnsNotFound_ForInvalidCustomerId()
        {
            // Arrange
            var customerId = 1;
            _customerServiceMock.Setup(s => s.GetCustomerById(customerId)).Throws(new Exception("Invalid"));

            // Act
            var result = await _controller.GetCustomerById(customerId);

            // Assert
            Assert.IsInstanceOf<StatusCodeResult>(result.Result);
            var statusCodeResult = (StatusCodeResult)result.Result;
            Assert.AreEqual(StatusCodes.Status404NotFound, statusCodeResult.StatusCode);
        }

        [Test]
        public async Task AddCustomer_ReturnsCreated()
        {
            // Arrange
            var customerCreateDto = new CustomerCreateDto { Name = "John Doe" };
            var customer = new CustomerDto { Id = 1, Name = "John Doe" };
            _customerServiceMock.Setup(s => s.AddCustomer(customerCreateDto)).ReturnsAsync(customer);
            var customerDto = new CustomerDto { Id = 1, Name = "John Doe" };
            _mapperMock.Setup(m => m.Map<CustomerDto>(customer)).Returns(customerDto);

            // Act
            var result = await _controller.AddCustomer(customerCreateDto);

            // Assert
            Assert.IsInstanceOf<CreatedAtActionResult>(result.Result);
            var createdAtActionResult = (CreatedAtActionResult)result.Result;
            Assert.AreEqual(nameof(CustomerController.GetCustomerById), createdAtActionResult.ActionName);
  
        }

        [Test]
        public async Task UpdateCustomer_ReturnsNoContent()
        {
            // Arrange
            var customerId = 1;
            var customerUpdateDto = new CustomerUpdateDto { Name = "John Smith" };

            // Act
            var result = await _controller.UpdateCustomer(customerId, customerUpdateDto);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task UpdateCustomer_ReturnsNotFound_ForInvalidCustomerId()
        {
            // Arrange
            var customerId = 1;
            var customerUpdateDto = new CustomerUpdateDto { Name = "John Smith" };
            _customerServiceMock.Setup(s => s.UpdateCustomer(customerId, customerUpdateDto)).Throws(new Exception("Customer not found"));

            // Act
            var result = await _controller.UpdateCustomer(customerId, customerUpdateDto);

            // Assert
            Assert.IsInstanceOf<StatusCodeResult>(result);
            var statusCodeResult = (StatusCodeResult)result;
            Assert.AreEqual(StatusCodes.Status404NotFound, statusCodeResult.StatusCode);
        }

        [Test]
        public async Task DeleteCustomer_ReturnsNoContent()
        {
            // Arrange
            var customerId = 1;

            // Act
            var result = await _controller.DeleteCustomer(customerId);

            // Assert
            Assert.IsInstanceOf<NoContentResult>(result);
        }

        [Test]
        public async Task DeleteCustomer_ReturnsNotFound_ForInvalidCustomerId()
        {
            // Arrange
            var customerId = 1;
            _customerServiceMock.Setup(s => s.DeleteCustomer(customerId)).Throws(new Exception("Customer not found"));

            // Act
            var result = await _controller.DeleteCustomer(customerId);

            // Assert
            var statusCodeResult = (StatusCodeResult)result;
            Assert.AreEqual(StatusCodes.Status500InternalServerError, statusCodeResult.StatusCode);
        }
    }
}

