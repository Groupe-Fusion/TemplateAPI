using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Domain.Repositories.Core;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace _5MI.BookManager.Persistence.Repositories
{
    public class ReservationRepository(
        BookManagerContext _context)
        : IReservationRepository
    {
        public async Task<Reservation> AddReservationAsync(Reservation reservation, CancellationToken ct = default)
        {
            //bizarre ça TODO
            var book = await _context.Books.FindAsync(new object[] { reservation.BookId }, ct);
            if (book is null)
                throw new ArgumentException("Book not found");

            var member = await _context.Members.FindAsync(new object[] { reservation.MemberId }, ct);
            if (member is null)
                throw new ArgumentException("Member not found");

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync(ct);
            return reservation;
        }

        public async Task<Reservation> DeleteReservationAsync(int bookId, int memberId, CancellationToken ct = default)
        {
            var reservation = await GetReservationByIdAsync(bookId, memberId, ct);
            if (reservation is null)
                throw new ArgumentNullException("No reservation found with these id");

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync(ct);
            return reservation;
        }

        public Task<List<Reservation>> GetAllReservationsAsync(CancellationToken ct = default)
        {
            return _context.Reservations.ToListAsync(ct);
        }

        //TODO modifs
        public Task<Reservation?> GetReservationByIdAsync(int BookId, int MemberId, CancellationToken ct = default)
        {
            return _context.Reservations
                .FirstOrDefaultAsync(x => x.BookId == BookId && x.MemberId == MemberId, ct);
        }

        public async Task<Reservation> UpdateReservationAsync(Reservation reservation, CancellationToken ct = default)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync(ct);
            return reservation;
        }
    }
}
