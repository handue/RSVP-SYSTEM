using Microsoft.AspNetCore.Mvc;
using RSVP.Core.DTOs;
using RSVP.Core.Interfaces.Services;

namespace RSVP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        // ActionResult is used to handle different HTTP response types (200 OK, 401 Unauthorized, etc.)
        // ActionResult는 다양한 HTTP 응답 타입(200 OK, 401 Unauthorized 등)을 처리하기 위해 사용됩니다
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Login([FromBody] LoginDto loginDto)
        {

            var response = await _authService.LoginAsync(loginDto);
            return Ok(ApiResponse<AuthResponseDto>.CreateSuccess(response));

        }

        [HttpPost("register")]
        public async Task<ActionResult<ApiResponse<AuthResponseDto>>> Register([FromBody] RegisterDto registerDto)
        {

            // var response = 
            await _authService.RegisterAsync(registerDto);

            // * Not giving any response to the client when register is successful. 

            return NoContent();


        }
    }
}