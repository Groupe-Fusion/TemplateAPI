using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.Domain.Repositories.Core
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetAllReservationsAsync(CancellationToken ct = default);
    }
}
