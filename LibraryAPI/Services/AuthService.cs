
using IdentityServer.Data;
using IdentityServer.Models;
using LibraryAPI.Data.Dtos;
using LibraryAPI.Services.Contracts;
using LibraryAPI.Utilities;
using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Services
{
    public class AuthService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IdentityServerDbContext _identityDbContext) : IAuthService
    {
        public async Task<LoginResponseDto> LoginAsync(LoginRequestDTO request)
        {
            var user = await userManager.FindByNameAsync(request.Username) ?? throw new Exception("Invalid login attempt.");
            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                throw new Exception("Invalid login attempt.");
            }
            var roles = await userManager.GetRolesAsync(user);
            var token = Utility.GenerateJwtToken(user, roles, configuration);
            return new LoginResponseDto
            {
                Token = token,
                Username = user.UserName!,
                Email = user.Email!,
                Roles = roles
            };
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto request)
        {
            var existingUser = await userManager.FindByNameAsync(request.Username);
            if (existingUser != null)
            {
                throw new Exception("Username already exists.");
            }

            using var transaction = await _identityDbContext.Database.BeginTransactionAsync();

            try
            {
                var user = new ApplicationUser
                {
                    UserName = request.Username,
                    Email = request.Email,
                };

                var result = await userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    var message = string.Join("; ", result.Errors.Select(e => e.Description));
                    throw new Exception($"User creation failed: {message}");
                }

                var roleResult = await userManager.AddToRoleAsync(user, request.Role);
                if (!roleResult.Succeeded)
                {
                    throw new Exception("User role assignment failed.");
                }

                await transaction.CommitAsync();

                return new RegisterResponseDto
                {
                    Success = true,
                };
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

       

    }
}
