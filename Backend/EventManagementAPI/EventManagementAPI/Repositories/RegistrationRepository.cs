using EventManagementAPI.Data;
using EventManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventManagementAPI.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly EventManagementContext _context;

        public RegistrationRepository(EventManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Registration>> GetAllRegistrationsAsync()
        {
            return await _context.Registrations.Include(r => r.User).Include(r => r.Event).ToListAsync();
        }

        public async Task<Registration> GetRegistrationByIdAsync(Guid registrationId)
        {
            return await _context.Registrations.Include(r => r.User)
                                               .Include(r => r.Event)
                                               .FirstOrDefaultAsync(r => r.RegistrationId == registrationId);
        }

        public async Task<Registration> AddRegistrationAsync(Registration registration)
        {
            _context.Registrations.Add(registration);
            await _context.SaveChangesAsync();
            return registration;
        }

        public async Task<Registration> UpdateRegistrationAsync(Registration registration)
        {
            _context.Registrations.Update(registration);
            await _context.SaveChangesAsync();
            return registration;
        }

        public async Task<bool> DeleteRegistrationAsync(Guid registrationId)
        {
            var registration = await _context.Registrations.FindAsync(registrationId);
            if (registration != null)
            {
                _context.Registrations.Remove(registration);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
