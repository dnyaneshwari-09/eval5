using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventManagementAPI.DTOs;
using EventManagementAPI.Models;
using EventManagementAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventsController(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        // GET: api/Events
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EventDTO>>> GetEvents()
        {
            var events = await _eventRepository.GetAllEventsAsync();
            return Ok(_mapper.Map<IEnumerable<EventDTO>>(events));
        }

        // GET: api/Events/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<EventDTO>> GetEvent(Guid id)
        {
            var eventEntity = await _eventRepository.GetEventByIdAsync(id);
            if (eventEntity == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<EventDTO>(eventEntity));
        }

        // POST: api/Events
        [HttpPost]
        public async Task<ActionResult<EventDTO>> PostEvent(EventDTO eventDTO)
        {
            var eventEntity = _mapper.Map<Event>(eventDTO);
            await _eventRepository.AddEventAsync(eventEntity);
            return CreatedAtAction(nameof(GetEvent), new { id = eventEntity.EventId }, _mapper.Map<EventDTO>(eventEntity));
        }

        // PUT: api/Events/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(Guid id, EventDTO eventDTO)
        {
            if (id != eventDTO.EventId)
            {
                return BadRequest();
            }

            var eventEntity = _mapper.Map<Event>(eventDTO);
            await _eventRepository.UpdateEventAsync(eventEntity);

            return NoContent();
        }

        // DELETE: api/Events/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(Guid id)
        {
            var eventExists = await _eventRepository.EventExistsAsync(id);
            if (!eventExists)
            {
                return NotFound();
            }

            await _eventRepository.DeleteEventAsync(id);
            return NoContent();
        }
    }
}
