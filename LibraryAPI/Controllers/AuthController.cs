using LibraryAPI.Data.Dtos;
using LibraryAPI.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDTO request)
        {
            try
            {
                var response = await authService.LoginAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(501, ex.Message);
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            try
            {
                // Assuming Logout is a client-side operation (e.g., token removal)
                return Ok("Logout successful.");
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(501, "An error occurred while processing the logout request.");
            }
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto request)
        {
            try
            {
                var response = await authService.RegisterAsync(request);
                if (!response.Success)
                {
                    return BadRequest(response.Errors);
                }
                return Ok("Registration successful.");
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(501, "An error occurred while processing the registration request.");
            }
        }

        [AllowAnonymous]
        [HttpPost("PasswordReset")]
        public IActionResult PasswordReset()
        {
            try
            {
                // Placeholder for password reset logic
                return Ok("Password reset functionality is not implemented yet.");
            }
            catch (Exception ex)
            {
                // Log the exception (optional)
                return StatusCode(501, "An error occurred while processing the password reset request.");
            }
        }
    }
}