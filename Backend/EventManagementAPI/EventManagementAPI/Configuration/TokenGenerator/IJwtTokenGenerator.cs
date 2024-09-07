using Microsoft.AspNetCore.Identity;

namespace EventManagementAPI.Configuration.TokenGenerator
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IdentityUser user, string role); // Updated to match the new method signature
    }
}
