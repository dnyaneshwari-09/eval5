using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Models
{
    public class Category
    {
        [Key]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "Category Type is required.")]
        [StringLength(50, ErrorMessage = "Category Type cannot exceed 50 characters.")]
        public string CategoryType { get; set; }

        // Navigation property
        public ICollection<Event> Events { get; set; }
    }

}
