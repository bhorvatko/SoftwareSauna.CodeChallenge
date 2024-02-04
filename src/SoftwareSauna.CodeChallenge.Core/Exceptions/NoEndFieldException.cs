using SoftwareSauna.CodeChallenge.Core.Exceptions;

namespace SoftwareSauna.CodeChallenge.Exceptions;

public class NoEndFieldException
    : DomainArgumentException
{
    internal NoEndFieldException()
        : base("No end field was found in the provided map.")
    {
    }
}
