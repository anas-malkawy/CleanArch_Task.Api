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
            try
            {
                var events = service.GetAllEvent();
                return Ok(events);
            }

            catch (Exception ex)
            {
                throw new Exception("not created", ex);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetEventById(int id)
        {
            try
            {
                var eventDto = service.GetEventById(id);
                if (eventDto == null) return NotFound($"Event with ID {id} not found.");
                return Ok(eventDto);
            }

            catch (Exception ex)
            {
                throw new Exception("not created", ex);
            }

        }

        [HttpPost]
        public IActionResult CreateEvent([FromForm] EventDTO eventDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var created = service.CreateEvent(eventDto);
                return Created();
            }

            catch (Exception ex)
            {
                throw new Exception("not created", ex);
            }


        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvent(int id, [FromForm] EventDTO eventDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updated = service.UpdateEvent(id, eventDto);
                if (updated == null) return NotFound($"Event with ID {id} not found.");
                return Ok(updated);
            }

            catch (Exception ex)
            {
                throw new Exception("not created", ex);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            try
            {
                var deleted = service.DeleteEvent(id);
                if (!deleted) return NotFound($"Event with ID {id} not found.");
                return NoContent();
            }

            catch (Exception ex)
            {
                throw new Exception("not created", ex);
            }

        }
    }
}