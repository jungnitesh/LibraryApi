using Microsoft.EntityFrameworkCore;
using LibraryAPI.Domain.Models;

namespace LibraryAPI.Infrastructure
{
    public class LibraryDbContext(DbContextOptions<LibraryDbContext> options) : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<BookLend> BookLends { get; set; }
    }
}
