using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.Applicatif.Core
{
    public interface IDeleteBookUseCase
    {
        Task ExecuteAsync(int bookId, CancellationToken ct = default);
    }
}
