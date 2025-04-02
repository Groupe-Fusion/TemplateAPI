using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Repositories.Core;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class GetAllBooksUseCase : IGetAllBooksUseCase
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksUseCase(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IList<Book>> ExecuteAsync(CancellationToken ct = default)
        {
            return await _bookRepository.GetAllBooksAsync(ct);
        }
    }
}