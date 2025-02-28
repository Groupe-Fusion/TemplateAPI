namespace _5MI.BookManager.Domain.Models
{
    public class Member
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public int Age { get; set; }

        // FK
        public virtual ICollection<Reservation> Reservations { get; set; } = [];
    }
}
