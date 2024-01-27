using SoftwareSauna.CodeChallenge.Exceptions;
using SoftwareSauna.CodeChallenge.Vectors;
using System.Data;

namespace SoftwareSauna.CodeChallenge.MapFields;

public abstract class MapField
{
    protected MapField(
        char value,
        FieldCoordinates coordinates,
        MapField? fieldToTheLeft,
        MapField? fieldAbove)
    {
        Value = value;
        Coordinates = coordinates;

        FieldAbove = fieldAbove;
        FieldToTheLeft = fieldToTheLeft;

        if (FieldAbove is not null)
        {
            FieldAbove.FieldBelow = this;
        }

        if (FieldToTheLeft is not null)
        {
            FieldToTheLeft.FieldToTheRight = this;
        }
    }

    internal char Value { get; private set; }
    internal FieldCoordinates Coordinates { get; private set; }
    internal MapField? FieldAbove { get; private set; }
    internal MapField? FieldToTheRight { get; private set; }
    internal MapField? FieldBelow { get; private set; }
    internal MapField? FieldToTheLeft { get; private set; }

    public static MapField Create(
        char value,
        uint xCoordinate,
        uint yCoordinate,
        MapField? fieldToTheLeft,
        MapField? fieldAbove)
    {
        FieldCoordinates coordinates = new FieldCoordinates() { X = xCoordinate, Y = yCoordinate };

        Dictionary<IEnumerable<char>, Func<MapField>> factories = new Dictionary<IEnumerable<char>, Func<MapField>>()
        {
            [StartingMapField.Identifiers] = () => new StartingMapField(coordinates, fieldToTheLeft, fieldAbove),
            [HorizontalPathMapField.Identifiers] = () => new HorizontalPathMapField(coordinates, fieldToTheLeft, fieldAbove),
            [VerticalPathMapField.Identifiers] = () => new VerticalPathMapField(coordinates, fieldToTheLeft, fieldAbove),
            [IntersectionMapField.Identifiers] = () => new IntersectionMapField(coordinates, fieldToTheLeft, fieldAbove),
            [LetterMapField.Identifiers] = () => new LetterMapField(value, coordinates, fieldToTheLeft, fieldAbove),
            [EndMapField.Identifiers] = () => new EndMapField(coordinates, fieldToTheLeft, fieldAbove),
            [EmptyMapField.Identifiers] = () => new EmptyMapField(coordinates, fieldToTheLeft, fieldAbove)
        };

        Func<MapField>? factory = factories.SingleOrDefault(x => x.Key.Contains(value)).Value;

        if (factory is null)
        {
            throw new InvalidFieldValueException(value, coordinates);
        }

        return factory();
    }

    public MapField? GetNextField(Direction direction) =>
        direction switch
        {
            Direction.Up => FieldAbove,
            Direction.Right => FieldToTheRight,
            Direction.Down => FieldBelow,
            Direction.Left => FieldToTheLeft,
            _ => throw new InvalidDirectionException(direction)
        };

    public IEnumerable<MapField> GetSurroundingFields() =>
        new[] { FieldAbove, FieldBelow, FieldToTheRight, FieldToTheLeft }.Where(x => x is not null)!;

    internal abstract bool CanBeTraversedThroughFromDirection(Direction direction);
}
