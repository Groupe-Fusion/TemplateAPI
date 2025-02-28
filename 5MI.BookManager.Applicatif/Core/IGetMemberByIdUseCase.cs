using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.Applicatif.Core
{
    public interface IGetMemberByIdUseCase
    {
        Task<Member> ExecuteAsync(int id, CancellationToken ct = default);
    }
}