using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Domain.Repositories.Core;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class GetReservationByIdUseCase : IGetReservationByIdUseCase
    {
        private readonly IReservationRepository _reservationRepository;
        public GetReservationByIdUseCase(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }
        public async Task<Reservation> ExecuteAsync(int bookId, int memberId, CancellationToken ct = default)
        {
            var reservation = await _reservationRepository.GetReservationByIdAsync(bookId, memberId, ct);
            if (reservation == null)
                throw new InvalidOperationException("Réservation introuvable avec ces ID.");
            return reservation;
        }
    }
}
