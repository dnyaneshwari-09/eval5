using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.DTOs
{
    public class CategoryDTO
    {
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Category Type is required.")]
        [StringLength(50, ErrorMessage = "Category Type cannot exceed 50 characters.")]
        public string CategoryType { get; set; }

        // Navigation property
        public ICollection<Guid> EventIds { get; set; } // Only include Event IDs for simplicity
    }
}
