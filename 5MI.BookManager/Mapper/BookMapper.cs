using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.DTO;

namespace _5MI.BookManager.Mapper
{
    public class BookMapper
    {
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
