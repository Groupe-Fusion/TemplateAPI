using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Repositories.Core;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class AddMemberUseCase : IAddMemberUseCase
    {
        private readonly IMemberRepository _memberRepository;

        public AddMemberUseCase(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Member> ExecuteAsync(Member member, CancellationToken ct = default)
        {
            return await _memberRepository.AddMemberAsync(member, ct);
        }
    }
}