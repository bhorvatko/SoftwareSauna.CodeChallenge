using SoftwareSauna.CodeChallenge.MapFields;
using SoftwareSauna.CodeChallenge.Vectors;

namespace SoftwareSauna.CodeChallenge.Exceptions;

public class NoVectorEndFieldException
    : Exception
{
    internal NoVectorEndFieldException(
        FieldCoordinates vectorOriginCoordinates,
        Direction vectorOrientation)
        : base($"Cannot determine vector end field for vector originating at {vectorOriginCoordinates} with orientation {vectorOrientation}")
    {

    }
}
