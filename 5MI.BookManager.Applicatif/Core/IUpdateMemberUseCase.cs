using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.Applicatif.Core
{
    public interface IUpdateMemberUseCase
    {
        Task<Member> ExecuteAsync(int id, Member member, CancellationToken ct = default);
    }
}