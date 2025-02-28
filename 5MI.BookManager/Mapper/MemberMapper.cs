using _5MI.BookManager.Domain.Models;
using _5MI.BookManager.DTO.Requests;
using _5MI.BookManager.DTO.Responses;

namespace _5MI.BookManager.Mapper
{
    public class MemberMapper
    {
        public static Member ToEntity(MemberRequest req)
        {
            return new Member()
            {
                FirstName = req.FirstName,
                LastName = req.LastName,
                Age = req.Age,
            };
        }

        public static MemberResponse ToResponse(Member member)
        {
            return new MemberResponse
            {
                Id = member.Id,
                FirstName = member.FirstName,
                LastName = member.LastName,
                Age = member.Age,
            };
        }
    }
}
