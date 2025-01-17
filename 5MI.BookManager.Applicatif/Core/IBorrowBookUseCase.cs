using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public interface IBorrowBookUseCase
    {
        Task<Book> ExecuteAsync(int bookId, CancellationToken ct = default);
    }
}