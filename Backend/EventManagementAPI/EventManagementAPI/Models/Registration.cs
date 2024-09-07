using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManagementAPI.Models
{
    public class Registration
    {
        [Key]
        public Guid RegistrationId { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        [ForeignKey("User")]
        public Guid UserId { get; set; } // Foreign key to User table

        [Required(ErrorMessage = "Event ID is required.")]
        [ForeignKey("Event")]
        public Guid EventId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Mobile Number is required.")]
        [Phone(ErrorMessage = "Invalid Mobile Number format.")]
        [StringLength(15, ErrorMessage = "Mobile Number cannot exceed 15 characters.")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Age is required.")]
        [Range(1, 120, ErrorMessage = "Age must be between 1 and 120.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [StringLength(10, ErrorMessage = "Gender cannot exceed 10 characters.")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Ticket Type is required.")]
        [ForeignKey("TicketType")]
        public Guid TicketTypeId { get; set; } // Foreign key to TicketType

        // Navigation properties
        public User User { get; set; } // Navigation property for the User table
        public Event Event { get; set; } // Navigation property for the Event table
        public TicketType TicketType { get; set; } // Navigation property for TicketType
        public ICollection<Payment> Payments { get; set; } // Navigation property for Payments
    }
}
