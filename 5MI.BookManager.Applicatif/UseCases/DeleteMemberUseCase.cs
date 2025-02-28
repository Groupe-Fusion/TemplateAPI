using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Repositories.Core;
using _5MI.BookManager.Applicatif.Exceptions;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class DeleteMemberUseCase : IDeleteMemberUseCase
    {
        private readonly IMemberRepository _memberRepository;

        public DeleteMemberUseCase(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public async Task<Member> ExecuteAsync(int id, CancellationToken ct = default)
        {
            var old = await _memberRepository.GetMemberByIdAsync(id, ct)
                ?? throw new ItemNotFoundException<Member>("No member found with that id.");

            var ok = await _memberRepository.DeleteMemberAsync(old, ct);

            return ok ? old : throw new Exception("Delete member faield");
        }
    }
}