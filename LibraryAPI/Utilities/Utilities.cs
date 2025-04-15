using IdentityServer.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace LibraryAPI.Utilities
{
    public static class Utility
    {
        public static string GenerateJwtToken(ApplicationUser user, IList<string> roles, IConfiguration configuration)
        {
            var claims = new List<Claim>
               {
                   new(JwtRegisteredClaimNames.Sub, user.Id),
                   new(JwtRegisteredClaimNames.UniqueName, user.UserName!),
                   new(JwtRegisteredClaimNames.Email, user.Email!)
               };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: configuration["JwtSettings:Issuer"],
                audience: configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
