using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RSVP.Core.DTOs;
using RSVP.Core.Interfaces.Services;
using RSVP.Core.Models;
using RSVP.Infrastructure.Data;



namespace RSVP.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;

        // *  IConfiguration is an interface provided by ASP.NET Core for managing application configuration values
        // * IConfiguration은 ASP.NET Core에서 애플리케이션의 설정값들을 관리하는 인터페이스입니다.

        // private readonly IConfiguration _configuration;

        public AuthService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            // _configuration = configuration;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await GetUserByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            if (!VerifyPassword(loginDto.Password, user.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            user.LastLoginAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = user.Role.ToLower()
                }
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingUser = await GetUserByEmailAsync(registerDto.Email);
            if (existingUser != null)
            {
                throw new InvalidOperationException("Email already registered");
            }

            var user = new User
            {
                Email = registerDto.Email,
                Password = HashPassword(registerDto.Password),
                FullName = registerDto.FullName,
                Role = "User"
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = GenerateJwtToken(user);
            return new AuthResponseDto
            {
                Token = token,
                User = new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = user.Role
                }
            };
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new InvalidOperationException("JWT Key not found")));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // claims는 JWT 토큰에 포함될 사용자 정보를 담는 배열.
            // claims is an array that contains user information to be included in the JWT token.
            var claims = new[]
            {
                // ClaimTypes.NameIdentifier: 사용자의 고유 ID
                // ClaimTypes.NameIdentifier: User's unique identifier
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                
                // ClaimTypes.Email: 사용자의 이메일 주소
                // ClaimTypes.Email: User's email address
                new Claim(ClaimTypes.Email, user.Email),
                
                // ClaimTypes.Role: 사용자의 역할(권한)
                // ClaimTypes.Role: User's role (permission)
                new Claim(ClaimTypes.Role, user.Role)
            };

            var token = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
                audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}