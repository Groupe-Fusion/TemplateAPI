using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Domain.Repositories.Core;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class DeleteReservationUseCase : IDeleteReservationUseCase
    {
        private readonly IReservationRepository _reservationRepository;

        public DeleteReservationUseCase(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<Reservation?> ExecuteAsync(int bookId, int memberId, CancellationToken ct = default)
        {
            return await _reservationRepository.DeleteReservationAsync(bookId, memberId, ct);
        }
    }

}
