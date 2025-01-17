using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.DTO.Requests;
using _5MI.BookManager.DTO.Responses;

namespace _5MI.BookManager.Mapper
{
    public class BookMapper
    {
        public static Book ToBookEntity(BookRequest req)
        {
            return new Book()
            {
                Title = req.Title,
                IsBorrowed = false,
            };
        }

        public static BookResponse ToBookResponse(Book book)
        {
            return new BookResponse
            {
                Id = book.Id,
                Title = book.Title,
                IsBorrowed = book.IsBorrowed
            };
        }
    }
}
