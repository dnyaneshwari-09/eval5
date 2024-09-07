using EventManagementAPI.Data;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagementAPI.Repositories
{
    public class TicketTypeRepository : ITicketTypeRepository
    {
        private readonly EventManagementContext _context;

        public TicketTypeRepository(EventManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TicketType>> GetAllTicketTypesAsync()
        {
            return await _context.TicketTypes.ToListAsync();
        }

        public async Task<TicketType> GetTicketTypeByIdAsync(Guid ticketTypeId)
        {
            return await _context.TicketTypes.FindAsync(ticketTypeId);
        }

        public async Task<TicketType> AddTicketTypeAsync(TicketType ticketType)
        {
            _context.TicketTypes.Add(ticketType);
            await _context.SaveChangesAsync();
            return ticketType;
        }

        public async Task<TicketType> UpdateTicketTypeAsync(TicketType ticketType)
        {
            _context.TicketTypes.Update(ticketType);
            await _context.SaveChangesAsync();
            return ticketType;
        }

        public async Task<bool> DeleteTicketTypeAsync(Guid ticketTypeId)
        {
            var ticketType = await _context.TicketTypes.FindAsync(ticketTypeId);
            if (ticketType != null)
            {
                _context.TicketTypes.Remove(ticketType);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
