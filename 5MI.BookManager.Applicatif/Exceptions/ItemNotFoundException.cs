namespace _5MI.BookManager.Applicatif.Exceptions
{
    public class ItemNotFoundException<Ttem>(
        string? message = null)
        : Exception(message)
    {
    }
}
