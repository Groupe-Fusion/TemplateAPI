using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.DTO.Requests;
using _5MI.BookManager.DTO.Responses;

namespace _5MI.BookManager.Mapper
{
    public class ReservationMapper
    {
        public static Reservation ToEntity(ReservationRequest req)
        {
            return new Reservation()
            {
                MemberId = req.MemberId,
                BookId = req.BookId,
            };
        }

        public static ReservationResponse ToResponse(Reservation reservation)
        {
            return new ReservationResponse
            {
                Book = new() { 
                    Id = reservation.BookId,
                    Title = reservation.Book.Title,
                },
                Member = new() { 
                    Id = reservation.MemberId,  
                    FirstName = reservation.Member.FirstName,
                    LastName = reservation.Member.LastName,
                    Age = reservation.Member.Age,
                }
            };
        }
    }
}
