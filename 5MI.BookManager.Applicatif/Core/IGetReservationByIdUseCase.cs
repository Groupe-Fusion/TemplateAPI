using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.Applicatif.Core
{
    public interface IGetReservationByIdUseCase
    {
        Task<Reservation> ExecuteAsync(int bookId, int memberId, CancellationToken ct = default);
    }
}
