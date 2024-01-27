namespace SoftwareSauna.CodeChallenge.Exceptions;

public class NoStartingFieldException
    : Exception
{
    internal NoStartingFieldException()
        : base("No starting field has been found in the provided map.")
    {

    }
}
