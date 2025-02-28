using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.Applicatif.Core
{
    public interface IAddMemberUseCase
    {
        Task<Member> ExecuteAsync(Member member, CancellationToken ct = default);
    }
}