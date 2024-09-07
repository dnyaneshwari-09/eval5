using EventManagementAPI.DTOs;
using EventManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketTypesController : ControllerBase
    {
        private readonly TicketTypeService _ticketTypeService;

        public TicketTypesController(TicketTypeService ticketTypeService)
        {
            _ticketTypeService = ticketTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketTypeDTO>>> GetAllTicketTypes()
        {
            var ticketTypes = await _ticketTypeService.GetAllTicketTypesAsync();
            return Ok(ticketTypes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TicketTypeDTO>> GetTicketTypeById(Guid id)
        {
            var ticketType = await _ticketTypeService.GetTicketTypeByIdAsync(id);
            if (ticketType == null)
            {
                return NotFound();
            }
            return Ok(ticketType);
        }

        [HttpPost]
        public async Task<ActionResult<TicketTypeDTO>> AddTicketType([FromBody] TicketTypeDTO ticketTypeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTicketType = await _ticketTypeService.AddTicketTypeAsync(ticketTypeDto);
            return CreatedAtAction(nameof(GetTicketTypeById), new { id = createdTicketType.TicketTypeId }, createdTicketType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTicketType(Guid id, [FromBody] TicketTypeDTO ticketTypeDto)
        {
            if (id != ticketTypeDto.TicketTypeId)
            {
                return BadRequest("Ticket Type ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedTicketType = await _ticketTypeService.UpdateTicketTypeAsync(ticketTypeDto);
            if (updatedTicketType == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicketType(Guid id)
        {
            var deleted = await _ticketTypeService.DeleteTicketTypeAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
