namespace _5MI.BookManager.DTO.Responses
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public bool IsBorrowed { get; set; }
    }
}
