using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Applicatif.Core.fusion;
using _5MI.BookManager.Domain.Models.fusion;
using _5MI.BookManager.Domain.Repositories.Core.fusion;

namespace _5MI.BookManager.Applicatif.UseCases.fusion;

public class AddUserUseCase : IAddUserUseCase
{
    private readonly IUserRepository _userRepository;
    public AddUserUseCase(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<User> ExecuteAsync(User newUser, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(newUser.Email))
            throw new ApplicationException("Email is required.");
        
        await _userRepository.AddUserAsync(newUser, cancellationToken);
        return newUser;
    }
}