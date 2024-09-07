using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Models
{
    public class Organizer
    {
        [Key]
        public Guid OrganizerId { get; set; }

        [Required(ErrorMessage = "Organizer Name is required.")]
        [StringLength(100, ErrorMessage = "Organizer Name cannot exceed 100 characters.")]
        public string OrganizerName { get; set; }

        [Required(ErrorMessage = "Contact is required.")]
        [Phone(ErrorMessage = "Invalid Contact Number format.")]
        [StringLength(15, ErrorMessage = "Contact cannot exceed 15 characters.")]
        public string Contact { get; set; }

        //[Required(ErrorMessage = "Event ID is required.")]
        //public Guid EventId { get; set; }

        // Navigation property
        public Event Event { get; set; }
    }

}
