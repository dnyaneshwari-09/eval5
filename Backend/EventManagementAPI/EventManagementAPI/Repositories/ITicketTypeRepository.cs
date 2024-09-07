using EventManagementAPI.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagementAPI.Repositories
{
    public interface ITicketTypeRepository
    {
        Task<IEnumerable<TicketType>> GetAllTicketTypesAsync();
        Task<TicketType> GetTicketTypeByIdAsync(Guid ticketTypeId);
        Task<TicketType> AddTicketTypeAsync(TicketType ticketType);
        Task<TicketType> UpdateTicketTypeAsync(TicketType ticketType);
        Task<bool> DeleteTicketTypeAsync(Guid ticketTypeId);
    }
}
