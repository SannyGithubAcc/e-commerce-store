using AutoMapper;
using e_commerce_store.Exceptions;
using e_commerce_store.Models.Dto.Order;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace e_commerce_store.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {


        private readonly IOrderService _OrderService;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;

        public OrderController(  IOrderService OrderService, IMapper mapper, ILogger<CustomerController> logger)
        {
            _OrderService = OrderService;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<OrderDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            var orders = _OrderService.GetAllAsync();
            try
            {
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    


        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Get a Order by id")]
        public async Task<ActionResult<OrderDto>> GetOrderById(int id)
        {
            try
            {
                var order = await _OrderService.GetByIdAsync(id);
                if (order == null) return NotFound();
                return Ok(order);
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
        [SwaggerOperation(Summary = "Create a Order")]
        public async Task<ActionResult<OrderDto>> Create(CreateOrderDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _OrderService.CreateAsync(createDto);
           

            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OrderDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrderDto>> Update(int id, [FromBody] UpdateOrderDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != updateDto.Id)
            {
                return BadRequest("Id mismatch between route parameter and body data.");
            }

            try
            {
                var order = await _OrderService.UpdateAsync(id, updateDto);

                return Ok(order);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public IActionResult Delete(int id)
        {
            try
            {
                _OrderService.DeleteAsync(id);

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }


    }

}
