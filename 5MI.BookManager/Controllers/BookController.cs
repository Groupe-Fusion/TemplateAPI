using Microsoft.AspNetCore.Mvc;
using _5MI.BookManager.Applicatif.Core;

namespace _5MI.BookManager.Presentation.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class BorrowBookController : ControllerBase
	{
		private readonly IBorrowBookUseCase _borrowBookUseCase;
		private readonly IReturnBookUseCase _returnBookUseCase;

		public BorrowBookController(IBorrowBookUseCase borrowBookUseCase, IReturnBookUseCase returnBookUseCase)
		{
			_borrowBookUseCase = borrowBookUseCase;
			_returnBookUseCase = returnBookUseCase;
		}

		/// <summary>
		/// Emprunter un livre.
		/// </summary>
		[HttpPost("borrow/{bookId}")]
		public async Task<IActionResult> BorrowBook(int bookId, CancellationToken ct)
		{
			try
			{
				var book = await _borrowBookUseCase.ExecuteAsync(bookId, ct);
				return Ok(book);
			}
			catch (KeyNotFoundException)
			{
				return NotFound($"{bookId}Not found");
			}
		}

		/// <summary>
		/// Retourner un livre.
		/// </summary>
		[HttpPost("return/{bookId}")]
		public async Task<IActionResult> ReturnBook(int bookId, CancellationToken ct)
		{
			try
			{
				var book = await _returnBookUseCase.ExecuteAsync(bookId, ct);
				return Ok(book);
			}
			catch (KeyNotFoundException)
			{
				return NotFound($"{bookId}Not found");
			}
		}
	}
}
