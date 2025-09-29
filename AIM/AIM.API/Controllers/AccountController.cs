using AIM.Application.DTOs.Responses;
using AIM.Application.DTOs.Requests;
using AIM.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AIM.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AIM.API.Controllers
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
            try
            {
                return await authService.RegisterUser(request);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        // POST api/Login
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginRequestDto request)
        {
            try
            {
                return await authService.Authenticate(request);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        
        // POST api/RefreshToken
        [HttpPost("RefreshToken")]
        public async Task<ActionResult<LoginResponseDto>> RefreshToken([FromBody] RefreshTokenRequest request)
        {
            try
            {
                return await authService.RefreshToken(request);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
