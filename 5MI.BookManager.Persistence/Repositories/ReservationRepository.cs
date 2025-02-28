using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Domain.Repositories.Core;
using _5MI.BookManager.Persistence;
using Microsoft.EntityFrameworkCore;

namespace _5MI.BookManager.Persistence.Repositories
{
    public class ReservationRepository(
        BookManagerContext _context)
        : IReservationRepository
    {
        public Task<List<Reservation>> GetAllReservationsAsync(CancellationToken ct = default)
        {
            return _context.Reservations.ToListAsync(ct);
        }
    }
}
