using IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityServer.Data
{
    public class IdentityServerDbContext(DbContextOptions<IdentityServerDbContext> options) : IdentityDbContext<ApplicationUser, IdentityRole, string>(options)
    {
    }
}
