using IAM.Application.DTOs.Responses;
using IAM.Application.DTOs.Requests;
using IAM.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IAM.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IAM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController(IAuthService authService) : ControllerBase
    {
        // POST api/register
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDto>> Register([FromBody] RegisterRequestDto request)
        {
            return await authService.RegisterUser(request);
        }

        // POST api/Login
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            return await authService.Authenticate(request);
        }
        
        // POST api/RefreshToken
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<LoginResponseDto>> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            return await authService.RefreshToken(request);
        }
    }
}
