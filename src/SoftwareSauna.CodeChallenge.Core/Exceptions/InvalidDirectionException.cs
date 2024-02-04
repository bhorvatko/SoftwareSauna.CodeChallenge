using SoftwareSauna.CodeChallenge.Core.Exceptions;
using SoftwareSauna.CodeChallenge.Vectors;

namespace SoftwareSauna.CodeChallenge.Exceptions;

public class InvalidDirectionException
    : DomainInvalidOperationException
{
    internal InvalidDirectionException(Direction direction)
        : base($"Unknown direction {direction}")
    {
    }
}
