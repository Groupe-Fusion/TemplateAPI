using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Repositories.Core;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class GetBookByIdUseCase : IGetBookByIdUseCase
    {
        private readonly IBookRepository _bookRepository;

        public GetBookByIdUseCase(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> ExecuteAsync(int bookId, CancellationToken ct = default)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId, ct);
            if (book == null)
                throw new InvalidOperationException("Livre introuvable avec cet ID.");

            return book;
        }
    }
}