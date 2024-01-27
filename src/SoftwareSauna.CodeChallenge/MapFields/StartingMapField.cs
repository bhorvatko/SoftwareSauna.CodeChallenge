using SoftwareSauna.CodeChallenge.Vectors;

namespace SoftwareSauna.CodeChallenge.MapFields;

public class StartingMapField
    : MapField
{
    public static readonly IEnumerable<char> Identifiers = new[] { '@' };

    internal StartingMapField(FieldCoordinates coordinates, MapField? fieldToTheLeft, MapField? fieldAbove)
        : base(Identifiers.Single(), coordinates, fieldToTheLeft, fieldAbove)
    {
    }

    internal override bool CanBeTraversedThroughFromDirection(Direction direction) => false;
}
