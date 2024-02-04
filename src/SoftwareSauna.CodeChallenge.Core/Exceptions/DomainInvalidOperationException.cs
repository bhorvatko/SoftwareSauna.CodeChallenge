namespace SoftwareSauna.CodeChallenge.Core.Exceptions;

public class DomainInvalidOperationException
    : InvalidOperationException
{
    public DomainInvalidOperationException(string message)
        : base(message)
    {
    }
}
