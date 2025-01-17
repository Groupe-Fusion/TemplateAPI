using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Domain.Repositories.Core;
using Microsoft.EntityFrameworkCore;

namespace _5MI.BookManager.Persistence.Repositories
{
    public class BookRepository(
        BookManagerContext _context) 
        : IBookRepository
    {
        public async Task<Book> AddBookAsync(Book book, CancellationToken ct = default)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync(ct);
            return book;
        }

        public async Task<Book> DeleteBookAsync(int id, CancellationToken ct = default)
        {
            var book = await GetBookByIdAsync(id, ct);
            if (book is null)
                throw new ArgumentNullException("No book found with that id");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync(ct);
            return book;
        }

        public Task<List<Book>> GetAllBooksAsync(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Book?> GetBookByIdAsync(int id, CancellationToken ct = default)
        {
            return _context.Books
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<Book> UpdateBookAsync(Book book, CancellationToken ct = default)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync(ct);
            return book;
        }
    }
}
