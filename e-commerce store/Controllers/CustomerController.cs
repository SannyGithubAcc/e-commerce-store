using AutoMapper;
using e_commerce_store.Dto;
using e_commerce_store.Exceptions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace e_commerce_store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerService customerService, IMapper mapper, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(Summary = "Get all customers")]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all customers");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting all customers");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Get a customer by id")]
        public async Task<ActionResult<CustomerDto>> GetCustomerById(int id)
        {
            try
            {
                var customer = await _customerService.GetCustomerById(id);
                return Ok(customer);
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, $"Error getting customer with id {id}");
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting customer with id {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting customer");
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Create a customer")]
        public async Task<ActionResult<CustomerDto>> AddCustomer(CustomerCreateDto customerCreateDto)
        {
            try
            {
                var addedCustomer = await _customerService.AddCustomer(customerCreateDto);
                return CreatedAtAction(nameof(GetCustomerById), new { id = addedCustomer.Id }, addedCustomer);
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, "Error adding customer");
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding customer");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error adding customer");
            }
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Update a customer by id")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerUpdateDto customerUpdateDto)
        {
            try
            {
                await _customerService.UpdateCustomer(id, customerUpdateDto);
                return NoContent();
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, $"Error updating customer with id {id}");
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating customer with id {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating customer");
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Delete a customer by id")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                await _customerService.DeleteCustomer(id);
                return NoContent();
            }
            catch (CustomException ex)
            {
                _logger.LogError(ex, $"Error deleting customer with id {id}");
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting customer with id {id}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting customer");
            }
        }
    }
}
