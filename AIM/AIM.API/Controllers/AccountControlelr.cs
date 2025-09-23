using AIM.API.Models;
using AIM.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AIM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountControlelr(JwtTokenService jwtTokenService) : ControllerBase
    {
        // POST api/Login
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            LoginResponseDto? result = await jwtTokenService.Authenticate(request);
            return result is null ? Unauthorized() : result;
        }

        // POST api/RefreshToken
        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDto>> RefreshToken([FromBody] RefreshTokenModel request)
        {
            if (string.IsNullOrWhiteSpace(request.Token))
                return BadRequest("Invalid Token");

            LoginResponseDto? result = await jwtTokenService.ValidateRefreshToken(request.Token);
            return result is null ? Unauthorized() : result;
        }
    }
}
