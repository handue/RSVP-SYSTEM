using RSVP.Core.DTOs;
using RSVP.Core.Models;

namespace RSVP.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        
        // * It's only for backend testing.
        // * Not Implemented into frontend 
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<User?> GetUserByEmailAsync(string email);
        string GenerateJwtToken(User user);
    }
}