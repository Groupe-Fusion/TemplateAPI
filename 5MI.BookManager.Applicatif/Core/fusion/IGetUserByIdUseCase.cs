using _5MI.BookManager.Domain.Models.fusion;

namespace _5MI.BookManager.Applicatif.Core.fusion
{
    public interface IGetUserByIdUseCase
    {
        Task<User> ExecuteAsync(int userId, CancellationToken ct = default);
    }
}
