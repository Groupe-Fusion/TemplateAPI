using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.Applicatif.Core
{
    public interface IAddReservationUseCase
    {
        Task<Reservation> ExecuteAsync(Reservation newReservation, CancellationToken ct = default);
    }
}
