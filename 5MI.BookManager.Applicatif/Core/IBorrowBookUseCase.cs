using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Applicatif.Core;
using System.Threading;
using System.Threading.Tasks;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class BorrowBookUseCase : IBorrowBookUseCase
    {
        private readonly IBookRepository _bookRepository;

        public BorrowBookUseCase(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> ExecuteAsync(int bookId, CancellationToken ct = default)
        {
            var book = await _bookRepository.GetByIdAsync(bookId, ct);
            if (book == null || !book.IsAvailable)
                throw new InvalidOperationException("Le livre n'est pas disponible pour l'emprunt.");

            book.IsAvailable = false;
            await _bookRepository.UpdateAsync(book, ct);

            return book;
        }
    }
}