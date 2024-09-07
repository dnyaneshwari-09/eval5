using System;
using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.DTOs
{
    public class PaymentDTO
    {
        public Guid PaymentId { get; set; }

        [Required(ErrorMessage = "Registration ID is required.")]
        public Guid RegistrationId { get; set; }

        [Required(ErrorMessage = "Number of Tickets is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Number of Tickets must be at least 1.")]
        public int NoOfTickets { get; set; }

        [Required(ErrorMessage = "Total Amount is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Total Amount must be a non-negative value.")]
        public decimal TotalAmount { get; set; }

        [Required(ErrorMessage = "Payment Status is required.")]
        [StringLength(20, ErrorMessage = "Status cannot exceed 20 characters.")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Payment Method is required.")]
        [StringLength(50, ErrorMessage = "Payment Method cannot exceed 50 characters.")]
        public string PaymentMethod { get; set; }
    }
}
