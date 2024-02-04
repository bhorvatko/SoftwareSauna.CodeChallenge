using SoftwareSauna.CodeChallenge.Core.Exceptions;
using SoftwareSauna.CodeChallenge.MapFields;

namespace SoftwareSauna.CodeChallenge.Exceptions;

internal class NoAvailablePathsException
    : DomainArgumentException
{
    public NoAvailablePathsException(FieldCoordinates fieldCoordinates)
        : base($"No available paths for field at coordinats {fieldCoordinates}.")
    {
    }
}

