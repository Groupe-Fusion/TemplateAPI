using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.Domain.Repositories.Core
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllBooksAsync(CancellationToken ct = default);
        Task<Book?> GetBookByIdAsync(int id,  CancellationToken ct = default);
        Task<Book> AddBookAsync(Book book, CancellationToken ct = default);
        Task<Book> UpdateBookAsync(Book book, CancellationToken ct = default);
        Task<Book> DeleteBookAsync(int id, CancellationToken ct = default);
    }
}
