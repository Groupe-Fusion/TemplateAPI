using _5MI.BookManager.Domain.Models.fusion;

namespace _5MI.BookManager.Applicatif.Core.fusion;

public interface IAddUserUseCase
{
    Task<User> ExecuteAsync(User newUser, CancellationToken cancellationToken);
}