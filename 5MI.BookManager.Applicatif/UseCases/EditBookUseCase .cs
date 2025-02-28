using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Repositories.Core;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class EditBookUseCase : IEditBookUseCase
    {
        private readonly IBookRepository _bookRepository;

        public EditBookUseCase(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> ExecuteAsync(Book updatedBook, CancellationToken ct = default)
        {
            if (string.IsNullOrEmpty(updatedBook.Title))
                throw new ArgumentException("Entrer un titre valide");

            var existingBook = await _bookRepository.GetBookByIdAsync(updatedBook.Id, ct);
            if (existingBook == null)
                throw new ArgumentException("Livre non trouvé");

            existingBook.Title = updatedBook.Title;

            await _bookRepository.UpdateBookAsync(existingBook, ct);
            return existingBook;
        }
    }
}
