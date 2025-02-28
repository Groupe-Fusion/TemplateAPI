using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Repositories.Core;
using _5MI.BookManager.Applicatif.Exceptions;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class UpdateMemberUseCase : IUpdateMemberUseCase
    {
        private readonly IMemberRepository _memberRepository;

        public UpdateMemberUseCase(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Member> ExecuteAsync(int id, Member member, CancellationToken ct = default)
        {
            var old = await _memberRepository.GetMemberByIdAsync(id, ct)
                ?? throw new ItemNotFoundException<Member>("No member found with that id.");

            old.FirstName = member.FirstName;
            old.LastName = member.LastName;
            old.Age = member.Age;

            return await _memberRepository.UpdateMemberAsync(old, ct);
        }
    }
}