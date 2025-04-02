using _5MI.BookManager.Domain.Models.fusion;

namespace _5MI.BookManager.Applicatif.Core.fusion
{
    public interface IGetAllUsersUseCase
    {
        Task<IList<User>> ExecuteAsync(CancellationToken ct = default);
    }
}
