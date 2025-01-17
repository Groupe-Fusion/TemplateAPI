using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Applicatif.Core;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class AddBookUseCase : IAddBookUseCase
    {
        private readonly IBookRepository _bookRepository;

        public AddBookUseCase(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> ExecuteAsync(Book newBook, CancellationToken ct = default)
        {
            await _bookRepository.AddBookAsync(newBook, ct);
            return newBook;
        }
    }
}