using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Applicatif.Core;
using System.Threading;
using System.Threading.Tasks;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class ReturnBookUseCase : IReturnBookUseCase
    {
        private readonly IBookRepository _bookRepository;

        public ReturnBookUseCase(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> ExecuteAsync(int bookId, CancellationToken ct = default)
        {
            var book = await _bookRepository.GetByIdAsync(bookId, ct);
            if (book == null || book.IsAvailable)
                throw new InvalidOperationException("Le livre est déjà en stock.");

            book.IsAvailable = true;
            await _bookRepository.UpdateAsync(book, ct);

            return book;
        }
    }
}