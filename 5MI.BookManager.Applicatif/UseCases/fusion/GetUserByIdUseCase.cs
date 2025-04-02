using _5MI.BookManager.Applicatif.Core.fusion;
using _5MI.BookManager.Domain.Models.fusion;
using _5MI.BookManager.Domain.Repositories.Core.fusion;

namespace _5MI.BookManager.Applicatif.UseCases.fusion
{
    public class GetUserByIdUseCase : IGetUserByIdUseCase
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> ExecuteAsync(int userId, CancellationToken ct = default)
        {
            var user = await _userRepository.GetUserByIdAsync(userId, ct);
            if (user == null)
                throw new InvalidOperationException("Utilisateur introuvable avec cet ID.");
            return user;
        }
    }
}
