using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.Applicatif.Core
{
    public interface IDeleteBookUseCase
    {
        Task ExecuteAsync(Guid bookId, CancellationToken ct = default);
    }
}
