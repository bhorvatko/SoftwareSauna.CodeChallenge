using SoftwareSauna.CodeChallenge.MapFields;

namespace SoftwareSauna.CodeChallenge.Exceptions;

internal class NoAvailablePathsException
    : Exception
{
    public NoAvailablePathsException(FieldCoordinates fieldCoordinates)
        : base($"No available paths for field at coordinats {fieldCoordinates}.")
    {
    }
}

