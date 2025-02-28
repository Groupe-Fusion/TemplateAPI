namespace _5MI.BookManager.Domain.Models
{
    public class Reservation
    {
        public int BookId { get; set; }
        public required virtual Book Book { get; set; }

        public int MemberId { get; set; }
        public required virtual Member Member { get; set; }
    }
}
