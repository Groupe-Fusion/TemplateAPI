using _5MI.BookManager.Domain.Models;

namespace _5MI.BookManager.DTO.Responses
{
    public class MemberResponse
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int Age { get; set; }
    }
}
