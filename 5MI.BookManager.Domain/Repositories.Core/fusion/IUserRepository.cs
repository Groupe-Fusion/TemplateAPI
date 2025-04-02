using _5MI.BookManager.Domain.Models.fusion;

namespace _5MI.BookManager.Domain.Repositories.Core.fusion;

public interface IUserRepository
{
    Task<List<User>> GetAllUsersAsync(CancellationToken ct = default);
    Task<User?> GetUserByIdAsync(int id, CancellationToken ct = default);
    Task<User> AddUserAsync(User user, CancellationToken ct = default);
    Task<User> UpdateUserAsync(User user, CancellationToken ct = default);
    Task<User> DeleteUserAsync(int id, CancellationToken ct = default);
}