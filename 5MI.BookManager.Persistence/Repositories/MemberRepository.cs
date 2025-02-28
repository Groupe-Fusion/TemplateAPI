using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Domain.Repositories.Core;
using _5MI.BookManager.Persistence;
using Microsoft.EntityFrameworkCore;

namespace _5MI.BookManager.Persistence.Repositories
{
    public class MemberRepository(
        BookManagerContext _context)
        : IMemberRepository
    {
        public async Task<Member> AddMemberAsync(Member member, CancellationToken ct = default)
        {
            _context.Members.Add(member);
            await _context.SaveChangesAsync(ct);
            return member;
        }

        public async Task<Member> DeleteMemberAsync(int id, CancellationToken ct = default)
        {
            var member = await GetMemberByIdAsync(id, ct);
            if (member is null)
                throw new ArgumentNullException("No member found with that id");

            _context.Members.Remove(member);
            await _context.SaveChangesAsync(ct);
            return member;
        }

        public Task<List<Member>> GetAllMembersAsync(CancellationToken ct = default)
        {
            return _context.Members.ToListAsync(ct);
        }

        public Task<Member?> GetMemberByIdAsync(int id, CancellationToken ct = default)
        {
            return _context.Members
                .FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task<Member> UpdateMemberAsync(Member member, CancellationToken ct = default)
        {
            _context.Members.Update(member);
            await _context.SaveChangesAsync(ct);
            return member;
        }
    }
}
