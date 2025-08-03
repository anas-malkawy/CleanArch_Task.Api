using CleanArch_Task.Application.IService;
using CleanArch_Task.Application.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch_Task.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IService service;

        public EventController(IService _service)
        {
            service = _service;
        }

        [HttpGet]
        public IActionResult GetAllEvent()
        {
            var events = service.GetAllEvent();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public IActionResult GetEventById(int id)
        {
            var eventDto = service.GetEventById(id);
            if (eventDto == null) return NotFound($"Event with ID {id} not found.");
            return Ok(eventDto);
        }

        [HttpPost]
        public IActionResult CreateEvent([FromForm] EventDTO eventDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var created = service.CreateEvent(eventDto);
            return CreatedAtAction(nameof(GetEventById), new { id = created }, created);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, [FromForm] EventDTO eventDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = service.UpdateEvent(id, eventDto);
            if (updated == null) return NotFound($"Event with ID {id} not found.");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var deleted = service.DeleteEvent(id);
            if (!deleted) return NotFound($"Event with ID {id} not found.");
            return NoContent();
        }
    }
}