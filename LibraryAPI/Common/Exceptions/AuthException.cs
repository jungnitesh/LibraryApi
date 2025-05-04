namespace LibraryAPI.Common.Exceptions
{
    public class AuthException(string message,Exception? ex):Exception(message, ex?.InnerException)
    {
    }
}
