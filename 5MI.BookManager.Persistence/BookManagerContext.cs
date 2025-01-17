using _5MI.BookManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace _5MI.BookManager.Persistence
{
    public class BookManagerContext(
        DbContextOptions<BookManagerContext> options) 
        : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }
    }
}
