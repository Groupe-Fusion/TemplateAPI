using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.Applicatif.Core
{
    public interface IGetAllBooksUseCase
    {
        Task<IList<Book>> ExecuteAsync(CancellationToken ct = default);
    }
}