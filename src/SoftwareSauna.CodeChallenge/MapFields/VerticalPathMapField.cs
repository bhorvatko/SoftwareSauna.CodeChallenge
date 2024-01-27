using SoftwareSauna.CodeChallenge.Vectors;

namespace SoftwareSauna.CodeChallenge.MapFields;

public class VerticalPathMapField
    : MapField
{
    public static readonly IEnumerable<char> Identifiers = new[] { '|' };

    internal VerticalPathMapField(FieldCoordinates coordinates, MapField? fieldToTheLeft, MapField? fieldAbove)
        : base(Identifiers.Single(), coordinates, fieldToTheLeft, fieldAbove)
    {
    }

    internal override bool CanBeTraversedThroughFromDirection(Direction direction) =>
        direction is Direction.Up or Direction.Down
            || GetNextField(direction)?.CanBeTraversedThroughFromDirection(direction) == true;
}
