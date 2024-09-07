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
    public class RegistrationsController : ControllerBase
    {
        private readonly RegistrationService _registrationService;

        public RegistrationsController(RegistrationService registrationService)
        {
            _registrationService = registrationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RegistrationDTO>>> GetAllRegistrations()
        {
            var registrations = await _registrationService.GetAllRegistrationsAsync();
            return Ok(registrations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RegistrationDTO>> GetRegistrationById(Guid id)
        {
            var registration = await _registrationService.GetRegistrationByIdAsync(id);
            if (registration == null)
            {
                return NotFound();
            }
            return Ok(registration);
        }

        [HttpPost]
        public async Task<ActionResult<RegistrationDTO>> AddRegistration([FromBody] RegistrationDTO registrationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdRegistration = await _registrationService.AddRegistrationAsync(registrationDto);
            return CreatedAtAction(nameof(GetRegistrationById), new { id = createdRegistration.RegistrationId }, createdRegistration);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegistration(Guid id, [FromBody] RegistrationDTO registrationDto)
        {
            if (id != registrationDto.RegistrationId)
            {
                return BadRequest("Registration ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedRegistration = await _registrationService.UpdateRegistrationAsync(registrationDto);
            if (updatedRegistration == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegistration(Guid id)
        {
            var deleted = await _registrationService.DeleteRegistrationAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}

