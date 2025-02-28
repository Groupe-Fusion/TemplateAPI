using Microsoft.AspNetCore.Mvc;
using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.DTO;
using _5MI.BookManager.Mapper;
using _5MI.BookManager.DTO.Requests;
using _5MI.BookManager.Applicatif.UseCases;

namespace _5MI.BookManager.Presentation.Controllers
{
    [ApiController]
    [Route("api/books")]
    public class BorrowBookController : ControllerBase
    {
        private readonly IAddBookUseCase _addBookUseCase;
        private readonly IGetAllBooksUseCase _getAllBooksUseCase;
        private readonly IGetBookByIdUseCase _getBookByIdUseCase;
        private readonly IEditBookUseCase _editBookUseCase;
        private readonly IDeleteBookUseCase _deleteBookUseCase;

        public BorrowBookController(
            IAddBookUseCase addBookUseCase,
            IGetAllBooksUseCase getAllBooksUseCase,
            IGetBookByIdUseCase getBookByIdUseCase,
            IEditBookUseCase editBookUseCase,
            IDeleteBookUseCase deleteBookUseCase)
        {
            _addBookUseCase = addBookUseCase;
            _getAllBooksUseCase = getAllBooksUseCase;
            _getBookByIdUseCase = getBookByIdUseCase;
            _editBookUseCase = editBookUseCase;
            _deleteBookUseCase = deleteBookUseCase;
        }

        /// <summary>
        /// Ajouter un nouveau livre.
        /// </summary>
        [HttpPost("")]
        public async Task<IActionResult> AddBook([FromBody] BookRequest bookRequest, CancellationToken ct)
        {
            try
            {
                var newBook = BookMapper.ToBookEntity(bookRequest);
                var book = await _addBookUseCase.ExecuteAsync(newBook, ct);
                return CreatedAtAction(nameof(GetBookById), new { bookId = book.Id }, book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"errror {ex.Message}");
            }
        }

        /// <summary>
        /// Modifier un livre existant.
        /// </summary>
        [HttpPut("{bookId}")]
        public async Task<IActionResult> UpdateBook(int bookId, [FromBody] BookRequest bookRequest, CancellationToken ct)
        {
            try
            {
                var updatedBook = BookMapper.ToBookEntity(bookRequest);
                updatedBook.Id = bookId;

                var book = await _editBookUseCase.ExecuteAsync(updatedBook, ct);

                return Ok(book);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Book with ID {bookId} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error {ex.Message}");
            }
        }


        /// <summary>
        /// Supprimer un livre par son ID.
        /// </summary>
        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBook(int bookId, CancellationToken ct)
        {
            try
            {
                await _deleteBookUseCase.ExecuteAsync(bookId, ct);

                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Book with ID {bookId} not found");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error {ex.Message}");
            }
        }


        /// <summary>
        /// Récupérer tous les livres.
        /// </summary>
        [HttpGet("")]
        public async Task<IActionResult> GetAllBooks(CancellationToken ct)
        {
            try
            {
                var books = await _getAllBooksUseCase.ExecuteAsync(ct);
                var bookResponses = books.Select(BookMapper.ToBookResponse).ToList();
                return Ok(bookResponses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"error {ex.Message}");
            }
        }

        /// <summary>
        /// Récupérer un livre par son ID.
        /// </summary>
        [HttpGet("{bookId}")]
        public async Task<IActionResult> GetBookById(int bookId, CancellationToken ct)
        {
            try
            {
                var book = await _getBookByIdUseCase.ExecuteAsync(bookId, ct);
                var bookResponse = BookMapper.ToBookResponse(book);
                return Ok(bookResponse);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"{bookId}Not found");
            }
        }
    }
}
