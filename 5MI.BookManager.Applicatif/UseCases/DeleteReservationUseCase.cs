using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Domain.Repositories.Core;
using System.Net;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class DeleteReservationUseCase : IDeleteReservationUseCase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly HttpClient _httpClientBook;
        private readonly HttpClient _httpClientMember;

        public DeleteReservationUseCase(IReservationRepository reservationRepository, IHttpClientFactory httpClientFactory)
        {
            _reservationRepository = reservationRepository;
            _httpClientBook = httpClientFactory.CreateClient("BookService");
            _httpClientMember = httpClientFactory.CreateClient("MemberService");
        }

        public async Task<Reservation?> ExecuteAsync(int bookId, int memberId, CancellationToken ct = default)
        {
            var response = await _httpClientBook.GetAsync($"api/book/{bookId}", ct);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException("Book not found");

            var member = await _httpClientMember.GetAsync($"api/member/{memberId}", ct);
            if (member.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException("Member not found");

            return await _reservationRepository.DeleteReservationAsync(bookId, memberId, ct);
        }
    }

}
