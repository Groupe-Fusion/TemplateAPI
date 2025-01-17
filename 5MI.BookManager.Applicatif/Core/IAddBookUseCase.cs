using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.Applicatif.Core
{
    public interface IAddBookUseCase
    {
        Task<Book> ExecuteAsync(Book newBook, CancellationToken ct = default);
    }
}