using EventManagementAPI.Configuration.TokenGenerator;
using EventManagementAPI.Data;
using EventManagementAPI.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Web;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly EventManagementContext _context;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtTokenGenerator jwtTokenGenerator,
            EventManagementContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenerator;
            _context = context;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> SignUp(RegisterDTO registerDTO)
        {
            // Check if the user is already registered
            var exist = await _userManager.FindByEmailAsync(registerDTO.Email);

            if (exist != null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = $"{exist.Email} has already been registered!" });
            }

            // Create a new ApplicationUser object
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = registerDTO.Email,
                Email = registerDTO.Email,
                EmailConfirmed = false,
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName
            };

            // Create a new user with the provided email and password
            var result = await _userManager.CreateAsync(user, registerDTO.Password);

            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = "Registration failed", errors = result.Errors });
            }

            // Assign the "User" role
            if (!await _userManager.IsInRoleAsync(user, "User"))
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            // Create and save the additional User record
            var customUser = new EventManagementAPI.Models.User
            {
                UserId = Guid.Parse(user.Id), // Make sure to convert the Id to Guid if needed
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                Password = registerDTO.Password ,// Be cautious with storing plain text passwords
               ConfirmPassword = registerDTO.ConfirmPassword // Be cautious with storing plain text passwords

            };

            _context.Users.Add(customUser);
            await _context.SaveChangesAsync();


            return StatusCode(StatusCodes.Status201Created, new { message = "User registered successfully!" });
        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null || string.IsNullOrEmpty(token))
            {
                return NotFound();
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                // Assuming "User" role for simplicity
                var jwtToken = _jwtTokenGenerator.GenerateToken(user, "User");
                return StatusCode(StatusCodes.Status200OK, new { email = user.Email, token = jwtToken });
            }

            return StatusCode(StatusCodes.Status403Forbidden);
        }

        [HttpPost("signin")]
        public async Task<IActionResult> SignIn(LoginDTO loginDTO)
        {
            // Find the user by email
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = "User not found." });
            }

            // Attempt to sign in the user with the provided email and password
            var signInResult = await _signInManager.PasswordSignInAsync(user.UserName, loginDTO.Password, false, lockoutOnFailure: false);

            if (signInResult.Succeeded)
            {
                // Retrieve the user's role
                var roles = await _userManager.GetRolesAsync(user);
                var role = roles.FirstOrDefault(); // Assuming a user has only one role, adjust as necessary

                if (role == null)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, new { message = "User does not have a role assigned." });
                }

                // Generate JWT token
                var token = _jwtTokenGenerator.GenerateToken(user, role);

                // Return the token and email in the response
                return StatusCode(StatusCodes.Status200OK, new { email = loginDTO.Email, token = token });
            }
            else if (signInResult.IsLockedOut)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = "Account is locked out." });
            }
            else if (signInResult.IsNotAllowed)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = "Sign-in not allowed." });
            }
            else
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { message = "Invalid email or password." });
            }
        }


        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            // Find the user by ID
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }

            // Get the user's roles
            var roles = await _userManager.GetRolesAsync(user);

            // Map user data to UserProfileDTO
            var userProfile = new UserProfileDTO
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Roles = roles
            };


            return Ok(userProfile);
        }


        [HttpGet("ResendConfirmationLink")]
        public async Task<IActionResult> ResendConfirmationLink(string email)
        {
            // Check email exists in the database
            var user = await _userManager.FindByEmailAsync(email);
            if (user is not null)
            {
                // Generate token
                var token = HttpUtility.UrlEncode(await _userManager.GenerateEmailConfirmationTokenAsync(user));

                // Generate Confirmation link
                var confirmationLink = new StringBuilder($"https://yourdomain.com/api/account/confirmEmail?token={token}&userId={user.Id}");

                // Return the confirmation link for manual use (email sending functionality skipped)
                return StatusCode(StatusCodes.Status200OK, new { message = "Confirmation link generated", confirmationLink = confirmationLink.ToString() });
            }

            // Otherwise, return a BadRequest as a result
            return StatusCode(StatusCodes.Status400BadRequest);
        }


    }
}
