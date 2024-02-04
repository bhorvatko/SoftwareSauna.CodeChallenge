namespace SoftwareSauna.CodeChallenge.Core.Exceptions;

public class DomainArgumentException
    : ArgumentException
{
    public DomainArgumentException(string message)
        : base(message)
    {
    }
}
