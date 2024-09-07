using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.DTOs
{
    public class EventDTO
    {
        public Guid EventId { get; set; }

        [Required(ErrorMessage = "Event Name is required.")]
        [StringLength(100, ErrorMessage = "Event Name cannot exceed 100 characters.")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "Age Limit is required.")]
        public int AgeLimit { get; set; }

        [Required(ErrorMessage = "Event Date and Time is required.")]
        public DateTime EventDateTime { get; set; }

        [Required(ErrorMessage = "Event Price is required.")]
        public decimal EventPrice { get; set; }

        [Required(ErrorMessage = "Event City is required.")]
        public string EventCity { get; set; }

        [Required(ErrorMessage = "Event Address is required.")]
        public string EventAddress { get; set; }

        [Required(ErrorMessage = "Event Duration is required.")]
        public decimal EventDuration { get; set; }

        [StringLength(500, ErrorMessage = "Event Details cannot exceed 500 characters.")]
        public string EventDetails { get; set; }

        [Required(ErrorMessage = "Max Capacity is required.")]
        public int MaxCapacity { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        public Guid CategoryId { get; set; }
    }
}
