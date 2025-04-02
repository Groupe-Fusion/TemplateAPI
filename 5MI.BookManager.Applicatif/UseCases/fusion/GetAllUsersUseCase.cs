using _5MI.BookManager.Applicatif.Core.fusion;
using _5MI.BookManager.Domain.Models.fusion;
using _5MI.BookManager.Domain.Repositories.Core.fusion;

namespace _5MI.BookManager.Applicatif.UseCases.fusion
{
    public class GetAllUsersUseCase : IGetAllUsersUseCase
    {
        private readonly IUserRepository _userRepository;
        public GetAllUsersUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IList<User>> ExecuteAsync(CancellationToken ct = default)
        {
            return await _userRepository.GetAllUsersAsync(ct);
        }
    }
}
