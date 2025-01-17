using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Domain.Repositories.Core;

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
            var book = await _bookRepository.GetBookByIdAsync(bookId, ct);
            if (book == null || book.IsBorrowed)
                throw new InvalidOperationException("Le livre n'est pas disponible pour l'emprunt.");

            book.IsBorrowed = true;
            return await _bookRepository.UpdateBookAsync(book, ct); ;
        }
    }
}
