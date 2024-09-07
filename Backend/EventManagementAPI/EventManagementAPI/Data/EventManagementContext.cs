using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using EventManagementAPI.Models;
namespace EventManagementAPI.Data
{
    public class EventManagementContext : IdentityDbContext<ApplicationUser>
    {
        public EventManagementContext(DbContextOptions<EventManagementContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserEventSelection> UserEventSelections { get; set; }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Organizer> Organizers { get; set; }
        public DbSet<TicketType> TicketTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Register default user and roles
            SeedDefaultUser.RegisterDefaultUser(builder);

            // Call base method to ensure Identity configurations are applied
            base.OnModelCreating(builder);
            // Configure relationships if needed
            builder.Entity<Event>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.Entity<Organizer>()
            //    .HasOne(o => o.Event)
            //    .WithMany(e => e.Organizers)
            //    .HasForeignKey(o => o.EventId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
