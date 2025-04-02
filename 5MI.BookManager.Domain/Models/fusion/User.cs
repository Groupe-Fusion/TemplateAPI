namespace _5MI.BookManager.Domain.Models.fusion;

public class User
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}