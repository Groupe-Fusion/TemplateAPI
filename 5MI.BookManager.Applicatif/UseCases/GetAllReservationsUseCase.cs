using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Domain.Repositories.Core;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class GetAllReservationsUseCase : IGetAllReservationsUseCase
    {
        private readonly IReservationRepository _reservationRepository;

        public GetAllReservationsUseCase(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<IList<Reservation>> ExecuteAsync(CancellationToken ct = default)
        {
            return await _reservationRepository.GetAllReservationsAsync(ct);
        }

        Task<IList<Reservation>> IGetAllReservationsUseCase.ExecuteAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
