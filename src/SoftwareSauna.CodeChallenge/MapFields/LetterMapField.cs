using SoftwareSauna.CodeChallenge.Vectors;

namespace SoftwareSauna.CodeChallenge.MapFields;

public class LetterMapField
    : MapField
{
    public static readonly IEnumerable<char> Identifiers =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();

    internal LetterMapField(char value, FieldCoordinates coordinates, MapField? fieldToTheLeft, MapField? fieldAbove)
        : base(value, coordinates, fieldToTheLeft, fieldAbove)
    {
    }

    internal override bool CanBeTraversedThroughFromDirection(Direction direction) => true;
}
