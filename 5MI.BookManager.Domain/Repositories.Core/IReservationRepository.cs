using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.Domain.Repositories.Core
{
    public interface IReservationRepository
    {
        Task<List<Reservation>> GetAllReservationsAsync(CancellationToken ct = default);
        Task<Reservation?> GetReservationByIdAsync(int id, CancellationToken ct = default);
        Task<Reservation> AddReservationAsync(Reservation reservation, CancellationToken ct = default);
        Task<Reservation> UpdateReservationAsync(Reservation reservation, CancellationToken ct = default);
        Task<Reservation> DeleteReservationAsync(int id, CancellationToken ct = default);
    }
}
