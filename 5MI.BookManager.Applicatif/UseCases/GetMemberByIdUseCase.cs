using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Repositories.Core;
using _5MI.BookManager.Applicatif.Exceptions;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class GetMemberByIdUseCase : IGetMemberByIdUseCase
    {
        private readonly IMemberRepository _memberRepository;

        public GetMemberByIdUseCase(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Member> ExecuteAsync(int id, CancellationToken ct = default)
        {
            return await _memberRepository.GetMemberByIdAsync(id, ct)
                ?? throw new ItemNotFoundException<Member>("No member found with that id.");
        }
    }
}