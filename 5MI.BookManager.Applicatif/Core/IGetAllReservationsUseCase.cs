using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.Applicatif.Core
{
    public interface IGetAllReservationsUseCase
    {
        Task<IList<Reservation>> ExecuteAsync(CancellationToken ct = default);
    }
}
