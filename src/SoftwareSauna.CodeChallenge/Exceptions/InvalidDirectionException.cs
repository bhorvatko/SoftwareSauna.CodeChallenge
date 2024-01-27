using SoftwareSauna.CodeChallenge.Vectors;

namespace SoftwareSauna.CodeChallenge.Exceptions;

public class InvalidDirectionException
    : Exception
{
    internal InvalidDirectionException(Direction direction)
        : base($"Unknown direction {direction}")
    {
    }
}
