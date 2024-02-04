using SoftwareSauna.CodeChallenge.Core.Exceptions;

namespace SoftwareSauna.CodeChallenge.Exceptions;

public class NoStartingFieldException
    : DomainArgumentException
{
    internal NoStartingFieldException()
        : base("No starting field has been found in the provided map.")
    {

    }
}
