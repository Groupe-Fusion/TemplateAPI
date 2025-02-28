using _5MI.BookManager.Applicatif.Core;
using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.Domain.Repositories.Core;
using System.Net;
using System.Net.Http;

namespace _5MI.BookManager.Applicatif.UseCases
{
    public class AddReservationUseCase : IAddReservationUseCase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly HttpClient _httpClientBook;
        private readonly HttpClient _httpClientMember;

        public AddReservationUseCase(IReservationRepository reservationRepository, IHttpClientFactory httpClientFactory)
        {
            _reservationRepository = reservationRepository;
            _httpClientBook = httpClientFactory.CreateClient("BookService");
            _httpClientMember = httpClientFactory.CreateClient("MemberService");
        }

        public async Task<Reservation> ExecuteAsync(Reservation newReservation, CancellationToken ct = default)
        {
            var response = await _httpClientBook.GetAsync($"api/books/{newReservation.BookId}", ct);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException("Book not found");

            var member = await _httpClientMember.GetAsync($"api/members/{newReservation.MemberId}", ct);
            if (member.StatusCode != HttpStatusCode.OK)
                throw new ArgumentException("Member not found");

            await _reservationRepository.AddReservationAsync(newReservation, ct);

            return newReservation;
        }
    }
}
