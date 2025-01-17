using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Repositories.Core;

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
            if (string.IsNullOrEmpty(newBook.Title))
                throw new ArgumentException("Entrer un titre valide");

            await _bookRepository.AddBookAsync(newBook, ct);
            return newBook;
        }
    }
}