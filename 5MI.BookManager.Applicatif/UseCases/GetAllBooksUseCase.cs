using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Applicatif.Core;
using System.Collections.Generic;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class GetAllBooksUseCase : IGetAllBooksUseCase
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksUseCase(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> ExecuteAsync(CancellationToken ct = default)
        {
            return await _bookRepository.GetAllBooksAsync(ct);
        }
    }
}