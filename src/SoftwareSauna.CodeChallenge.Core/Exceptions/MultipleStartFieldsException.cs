using SoftwareSauna.CodeChallenge.Core.Exceptions;
using SoftwareSauna.CodeChallenge.MapFields;

namespace SoftwareSauna.CodeChallenge.Exceptions;

public class MultipleStartFieldsException
    : DomainArgumentException
{
    internal MultipleStartFieldsException(IEnumerable<MapField> startFields)
        : base($"Multiple start fields have been defined at coordinates {string.Join(',', startFields.Select(x => x.Coordinates))}.")
    {

    }
}
