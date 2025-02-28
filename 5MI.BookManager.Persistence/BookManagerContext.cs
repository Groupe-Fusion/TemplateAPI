using _5MI.BookManager.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace _5MI.BookManager.Persistence
{
    public class BookManagerContext(
        DbContextOptions<BookManagerContext> options) 
        : DbContext(options)
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reservation>()
                .HasKey(e => new { e.MemberId, e.BookId });

            modelBuilder.Entity<Member>()
                .HasMany(m => m.Reservations)
                .WithOne(r => r.Member)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Book>()
                .HasMany(b => b.Reservations)
                .WithOne(r => r.Book)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
