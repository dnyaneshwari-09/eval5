using System.ComponentModel.DataAnnotations;

namespace EventManagementAPI.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } // Unique identifier for the user

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters.")]
        public string FirstName { get; set; } // First name of the user

        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters.")]
        public string LastName { get; set; } // Last name of the user

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; } // Email address of the user

     

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must be between 8 and 100 characters.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; } // Password of the user (hashed and salted in practice)

        [Required(ErrorMessage = "confirm Password is required.")]
        [StringLength(100, ErrorMessage = "Password must be between 8 and 100 characters.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } // Password of the user (hashed and salted in practice)

    }

}
