using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Domain.Repositories.Core;
using Microsoft.EntityFrameworkCore;

namespace _5MI.BookManager.Persistence.Repositories
{
    public class ReservationRepository(
        BookManagerContext _context)
        : IReservationRepository
    {
        public async Task<Reservation> AddReservationAsync(Reservation reservation, CancellationToken ct = default)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync(ct);
            return reservation;
        }

        public async Task<Reservation> DeleteReservationAsync(int id, CancellationToken ct = default)
        {
            var reservation = await GetReservationByIdAsync(id, ct);
            if (reservation is null)
                throw new ArgumentNullException("No reservation found with that id");

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync(ct);
            return reservation;
        }

        public Task<List<Reservation>> GetAllReservationsAsync(CancellationToken ct = default)
        {
            return _context.Reservations.ToListAsync(ct);
        }

        //TODO modifs
        public Task<Reservation?> GetReservationByIdAsync(int MemberId, CancellationToken ct = default)
        {
            return _context.Reservations
                .FirstOrDefaultAsync(x => x.MemberId == MemberId, ct);
        }

        public async Task<Reservation> UpdateReservationAsync(Reservation reservation, CancellationToken ct = default)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync(ct);
            return reservation;
        }
    }
}
