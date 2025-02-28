using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Repositories.Core;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class DeleteBookUseCase : IDeleteBookUseCase
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookUseCase(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task ExecuteAsync(int bookId, CancellationToken ct = default)
        {
            var book = await _bookRepository.GetBookByIdAsync(bookId, ct);
            if (book == null)
                throw new ArgumentException("Livre non trouvé");

            await _bookRepository.DeleteBookAsync(bookId, ct);
        }
    }
}
