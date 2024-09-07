using AutoMapper;
using EventManagementAPI.DTOs;
using EventManagementAPI.Models;
using EventManagementAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagementAPI.Services
{
    public class TicketTypeService
    {
        private readonly ITicketTypeRepository _ticketTypeRepository;
        private readonly IMapper _mapper;

        public TicketTypeService(ITicketTypeRepository ticketTypeRepository, IMapper mapper)
        {
            _ticketTypeRepository = ticketTypeRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TicketTypeDTO>> GetAllTicketTypesAsync()
        {
            var ticketTypes = await _ticketTypeRepository.GetAllTicketTypesAsync();
            return _mapper.Map<IEnumerable<TicketTypeDTO>>(ticketTypes);
        }

        public async Task<TicketTypeDTO> GetTicketTypeByIdAsync(Guid ticketTypeId)
        {
            var ticketType = await _ticketTypeRepository.GetTicketTypeByIdAsync(ticketTypeId);
            return _mapper.Map<TicketTypeDTO>(ticketType);
        }

        public async Task<TicketTypeDTO> AddTicketTypeAsync(TicketTypeDTO ticketTypeDto)
        {
            var ticketType = _mapper.Map<TicketType>(ticketTypeDto);
            var createdTicketType = await _ticketTypeRepository.AddTicketTypeAsync(ticketType);
            return _mapper.Map<TicketTypeDTO>(createdTicketType);
        }

        public async Task<TicketTypeDTO> UpdateTicketTypeAsync(TicketTypeDTO ticketTypeDto)
        {
            var ticketType = _mapper.Map<TicketType>(ticketTypeDto);
            var updatedTicketType = await _ticketTypeRepository.UpdateTicketTypeAsync(ticketType);
            return _mapper.Map<TicketTypeDTO>(updatedTicketType);
        }

        public async Task<bool> DeleteTicketTypeAsync(Guid ticketTypeId)
        {
            return await _ticketTypeRepository.DeleteTicketTypeAsync(ticketTypeId);
        }
    }
}
