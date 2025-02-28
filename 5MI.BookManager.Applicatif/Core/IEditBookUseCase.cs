using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.Applicatif.Core
{
    public interface IEditBookUseCase
    {
        Task<Book> ExecuteAsync(Book updatedBook, CancellationToken ct = default);
    }
}
