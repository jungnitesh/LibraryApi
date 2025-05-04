using LibraryAPI.Application.Contracts.ServiceContracts;
using LibraryAPI.Application.Dtos;
using LibraryAPI.Common.Exceptions;
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
            var response = await authService.LoginAsync(request);
            return Ok(response);
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
                // Assuming Logout is a client-side operation (e.g., token removal)
                return Ok("Logout successful.");
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDto request)
        {
            
                var response = await authService.RegisterAsync(request);
                if (!response.Success)
                {
                   throw new AuthException("Registration failed.", null);
                }
                return Ok("Registration successful.");
              // Log the exception (optional)
             
        }

        [AllowAnonymous]
        [HttpPost("PasswordReset")]
        public IActionResult PasswordReset()
        {
                // Placeholder for password reset logic
                return Ok("Password reset functionality is not implemented yet.");
          
        }
    }
}