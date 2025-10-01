using IAM.Application.DTOs.Responses;
using IAM.Application.DTOs.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IAM.Infrastructure.Data;
using IAM.Application.Services;
using IAM.Domain.Entities;
using IAM.Application.Interfaces;

namespace IAM.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    //// ADD ROLES
    public class UsersController(IUserService userService) : ControllerBase
    {
        // GET: /Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAllUsers()
        {
            return Ok(await userService.GetAllUsers());
        }

        // GET: /Users/{param}
        [HttpGet("param")]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers(GetUsersRequestDto request)
        {
            return Ok(await userService.GetUsers(request));
        }

        // GET: /Users/{login}
        [HttpGet("login")]
        public async Task<ActionResult<UserResponseDto>> GetUser(GetUserRequestDto request)
        {
            return Ok(await userService.GetUser(request));
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
