using Microsoft.AspNetCore.Mvc;
using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.DTO;
using _5MI.BookManager.Mapper;

namespace _5MI.BookManager.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BorrowBookController : ControllerBase
    {
        private readonly IBorrowBookUseCase _borrowBookUseCase;
        private readonly IReturnBookUseCase _returnBookUseCase;
        private readonly IAddBookUseCase _addBookUseCase;
        private readonly IGetAllBooksUseCase _getAllBooksUseCase;
        private readonly IGetBookByIdUseCase _getBookByIdUseCase;

        public BorrowBookController(
            IBorrowBookUseCase borrowBookUseCase,
            IReturnBookUseCase returnBookUseCase,
            IAddBookUseCase addBookUseCase,
            IGetAllBooksUseCase getAllBooksUseCase,
            IGetBookByIdUseCase getBookByIdUseCase)
        {
            _borrowBookUseCase = borrowBookUseCase;
            _returnBookUseCase = returnBookUseCase;
            _addBookUseCase = addBookUseCase;
            _getAllBooksUseCase = getAllBooksUseCase;
            _getBookByIdUseCase = getBookByIdUseCase;
        }

        /// <summary>
        /// Emprunter un livre par son ID.
        /// </summary>
        [HttpPut("borrow/{bookId}")]
        public async Task<IActionResult> BorrowBook(int bookId, CancellationToken ct)
        {
            try
            {
                var book = await _borrowBookUseCase.ExecuteAsync(bookId, ct);
                var bookResponse = BookMapper.ToBookResponse(book);
                return Ok(bookResponse);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"{bookId}Not found");
            }
        }

        /// <summary>
        /// Retourner un livre par son ID.
        /// </summary>
        [HttpPut("return/{bookId}")]
        public async Task<IActionResult> ReturnBook(int bookId, CancellationToken ct)
        {
            try
            {
                var book = await _returnBookUseCase.ExecuteAsync(bookId, ct);
                var bookResponse = BookMapper.ToBookResponse(book);
                return Ok(bookResponse);
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"{bookId}Not found");
            }
        }

        /// <summary>
        /// Ajouter un nouveau livre.
        /// </summary>
        [HttpPost("add")]
        public async Task<IActionResult> AddBook([FromBody] Book newBook, CancellationToken ct)
        {
            try
            {
                var book = await _addBookUseCase.ExecuteAsync(newBook, ct);
                return CreatedAtAction(nameof(GetBookById), new { bookId = book.Id }, book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"errror {ex.Message}");
            }
        }

        /// <summary>
        /// Récupérer tous les livres.
        /// </summary>
        [HttpGet("all")]
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
