namespace _5MI.BookManager.DTO.Responses
{
    public class ReservationResponse
    {
        public required BookResponse Book { get; set; }
        public required MemberResponse Member { get; set; }
    }
}
