using CleanArch_Task.Application.IService;
using CleanArch_Task.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch_Task.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IService service;

        public OrderController(IService _service)
        {
            service = _service;
        }

        [HttpGet]
        public IActionResult GetAllOrder()
        {
            var orders = service.GetAllOrder();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var orderDto = service.GetOrderById(id);
            if (orderDto == null) return NotFound($"Order with ID {id} not found.");
            return Ok(orderDto);
        }

        [HttpPost]
        public IActionResult CreateOrder([FromForm] OrderDTO orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = service.CreateOrder(orderDto);
            return CreatedAtAction(nameof(GetOrderById), new { id = created }, created);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(int id, [FromForm] OrderDTO orderDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = service.UpdateOrder(id, orderDto);
            if (updated == null) return NotFound($"Order with ID {id} not found.");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            var deleted = service.DeleteOrder(id);
            if (!deleted) return NotFound($"Order with ID {id} not found.");
            return NoContent();
        }
    }
}