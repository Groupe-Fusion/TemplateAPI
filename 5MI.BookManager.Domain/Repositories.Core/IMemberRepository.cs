using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.Domain.Repositories.Core
{
    public interface IMemberRepository
    {
        Task<List<Member>> GetAllMembersAsync(CancellationToken ct = default);
        Task<Member?> GetMemberByIdAsync(int id, CancellationToken ct = default);
        Task<Member> AddMemberAsync(Member member, CancellationToken ct = default);
        Task<Member> UpdateMemberAsync(Member member, CancellationToken ct = default);
        Task<Member> DeleteMemberAsync(int id, CancellationToken ct = default);
    }
}
