using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Domain.Repositories.Core;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class AddReservationUseCase : IAddReservationUseCase
    {
        private readonly IReservationRepository _reservationRepository;

        public AddReservationUseCase(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<Reservation> ExecuteAsync(Reservation newReservation, CancellationToken ct = default)
        {
            if (newReservation.BookId == 0)
                throw new ArgumentException("Entrer un id de livre valide");

            if (newReservation.MemberId == 0)
                throw new ArgumentException("Entrer un id de member valide");

            await _reservationRepository.AddReservationAsync(newReservation, ct);

            return newReservation;
        }
    }
}
