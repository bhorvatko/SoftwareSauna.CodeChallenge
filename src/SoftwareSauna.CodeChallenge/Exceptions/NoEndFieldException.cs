namespace SoftwareSauna.CodeChallenge.Exceptions;

public class NoEndFieldException
    : Exception
{
    internal NoEndFieldException()
        : base("No end field was found in the provided map.")
    {
    }
}
