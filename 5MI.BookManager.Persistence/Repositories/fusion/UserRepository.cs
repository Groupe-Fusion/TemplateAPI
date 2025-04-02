using _5MI.BookManager.Domain.Models.fusion;
using _5MI.BookManager.Domain.Repositories.Core.fusion;
using Microsoft.EntityFrameworkCore;

namespace _5MI.BookManager.Persistence.Repositories.fusion
{
    public class UserRepository(UserManagerContext _context) : IUserRepository
    {
        public async Task<User> AddUserAsync(User user, CancellationToken ct = default)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(ct);
            return user;
        }

        public async Task<User> DeleteUserAsync(int id, CancellationToken ct = default)
        {
            var user = await GetUserByIdAsync(id, ct);
            if (user is null)
                throw new ArgumentNullException("No user found with that id");
            _context.Users.Remove(user);
            await _context.SaveChangesAsync(ct);
            return user;
        }

        public Task<List<User>> GetAllUsersAsync(CancellationToken ct = default)
        {
            return _context.Users.ToListAsync(ct);
        }

        public Task<User?> GetUserByIdAsync(int id, CancellationToken ct = default)
        {
            return _context.Users
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<User> UpdateUserAsync(User user, CancellationToken ct = default)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync(ct);
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email, CancellationToken ct = default)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email, ct);
        }
    }
}
