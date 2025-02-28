using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.Applicatif.Core
{
    public interface IGetAllMembersUseCase
    {
        Task<IList<Member>> ExecuteAsync(CancellationToken ct = default);
    }
}