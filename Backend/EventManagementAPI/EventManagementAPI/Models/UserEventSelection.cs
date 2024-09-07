using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Models
{
    public class UserEventSelection
    {
        [Key]
        public Guid UserEventSelectionId { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Event ID is required.")]
        [ForeignKey("Event")]
        public Guid EventId { get; set; }

        // Navigation properties
        public User User { get; set; }
        public Event Event { get; set; }
        public ICollection<Registration> Registrations { get; set; }
    }

}
