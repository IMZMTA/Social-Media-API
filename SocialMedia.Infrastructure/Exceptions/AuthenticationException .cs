namespace SocialMedia.Infrastructure.Exceptions;

public class AuthenticationException : Exception
{
    public string Field { get; }

    public AuthenticationException(string message, string field = "General") : base(message)
    {
        Field = field;
    }
}
