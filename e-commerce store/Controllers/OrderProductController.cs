using e_commerce_store.Models.Dto.Order;
using e_commerce_store.Models.Dto.OrderProduct;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace e_commerce_store.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderProductController : ControllerBase
    {
        private readonly IOrderProductService _orderProductService;

        public OrderProductController(IOrderProductService orderService)
        {
            _orderProductService = orderService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<OrderProductDto>), (int)HttpStatusCode.OK)]
        public IActionResult GetAll()
        {
            var orders = _orderProductService.GetAllAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(OrderProductDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public IActionResult GetById(int id)
        {
            var order = _orderProductService.GetByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderProductDto), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ModelStateDictionary), (int)HttpStatusCode.BadRequest)]

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(Summary = "Create a OrderProduct")]
        public async Task<ActionResult<OrderProductDto>> Create(CreateOrderProductDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _orderProductService.CreateAsync(createDto);

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(OrderProductDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<OrderProductDto>> Update(int id, [FromBody] UpdateOrderProductDto updateDto)
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
                var order = await _orderProductService.UpdateAsync(updateDto);

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
                _orderProductService.DeleteAsync(id);

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }

}
