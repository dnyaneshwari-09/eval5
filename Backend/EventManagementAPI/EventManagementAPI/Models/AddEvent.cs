using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace EventManagementAPI.Models
{
    public class Event
    {
        [Key]
        public Guid EventId { get; set; }

        [Required(ErrorMessage = "Event Name is required.")]
        [StringLength(100, ErrorMessage = "Event Name cannot exceed 100 characters.")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "age limit is required.")]
        public int AgeLimit { get; set; }

        [Required(ErrorMessage = "Event Date and Time is required.")]
        public DateTime EventDateTime { get; set; }

        [Required(ErrorMessage = "Event Price is required.")]
        public decimal EventPrice { get; set; }

        [Required(ErrorMessage = "Event city is required.")]
        public string EventCity { get; set; }

        [Required(ErrorMessage = "Event address is required.")]
        public string EventAddress { get; set; }

        [Required(ErrorMessage = "Event duration is required.")]
        public decimal EventDuration { get; set; }

        [StringLength(500, ErrorMessage = "Event Details cannot exceed 500 characters.")]
        public string EventDetails { get; set; }

        [Required(ErrorMessage = "Max Capacity is required.")]
        public int MaxCapacity { get; set; }

        // Foreign keys
        public Guid CategoryId { get; set; }
      

        // Navigation properties
        public Category Category { get; set; }
        public ICollection<Organizer> Organizers { get; set; }
        public ICollection<UserEventSelection> UserEventSelections { get; set; }
    }

}
