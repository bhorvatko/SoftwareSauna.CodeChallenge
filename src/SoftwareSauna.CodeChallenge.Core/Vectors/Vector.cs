using SoftwareSauna.CodeChallenge.Exceptions;
using SoftwareSauna.CodeChallenge.MapFields;

namespace SoftwareSauna.CodeChallenge.Vectors;

public class Vector
{
    private Vector(
        Direction orientation,
        IEnumerable<MapField> fields)
    {
        Orientation = orientation;
        Fields = fields;
    }

    public Direction Orientation { get; private set; }
    public IEnumerable<MapField> Fields { get; private set; }
    public MapField OriginField => Fields.First();
    public MapField EndField => Fields.Last();
    public bool IsStartVector => OriginField is StartingMapField;
    public bool IsEndVector => EndField is EndMapField;

    public static Vector CreateStartingVector(StartingMapField startingField)
    {
        Direction orientation =
            GetVectorOrientation(startingField);

        IEnumerable<MapField> fields =
            GetVectorFields(startingField, orientation);

        return new Vector(orientation, fields);
    }

    public Vector CreateAppendedVector()
    {
        MapField vectorOrigin = EndField;

        Direction orientation = GetVectorOrientation(vectorOrigin, this);

        if (orientation == Orientation)
        {
            throw new FakeTurnException(vectorOrigin);
        }

        IEnumerable<MapField> fields =
            GetVectorFields(vectorOrigin, orientation);

        return new Vector(orientation, fields);
    }

    public override string ToString() =>
        new string(Fields.Select(x => x.Value).ToArray());

    private static Direction GetVectorOrientation(
        MapField vectorOrigin,
        Vector? previousVector = null)
    {
        IEnumerable<MapField> potentialNextFields =
            vectorOrigin.GetSurroundingFields()
                .Where(x => CanTraverseThroughField(vectorOrigin, x));

        if (previousVector is not null)
        {
            potentialNextFields = potentialNextFields
                .Where(x => !previousVector.Fields.Contains(x));
        }

        if (!potentialNextFields.Any())
        {
            throw new NoAvailablePathsException(vectorOrigin.Coordinates);
        }

        if (potentialNextFields.Count() > 1)
        {
            throw new MultiplePathsException(
                vectorOrigin,
                potentialNextFields.Select(x => x.Coordinates));
        }

        MapField nextField = potentialNextFields.Single();

        return GetOrientationRelativeToField(vectorOrigin, nextField);
    }

    private static IEnumerable<MapField> GetVectorFields(
        MapField vectorOrigin,
        Direction vectorOrientation)
    {
        if (TryGetVectorFields<IntersectionMapField>(vectorOrigin, vectorOrientation, out IEnumerable<MapField> fields))
        {
            return fields;
        }
        else if (TryGetVectorFields<EndMapField>(vectorOrigin, vectorOrientation, out fields))
        {
            return fields;
        }
        else if (TryGetVectorFields<LetterMapField>(vectorOrigin, vectorOrientation, out fields))
        {
            return fields;
        }

        throw new NoVectorEndFieldException(vectorOrigin.Coordinates, vectorOrientation);
    }

    private static bool TryGetVectorFields<TVectorTerminator>(
        MapField vectorOrigin,
        Direction vectorOrientation,
        out IEnumerable<MapField> fields)
        where TVectorTerminator : MapField
    {
        List<MapField> mapFields = [vectorOrigin];

        MapField? nextMapField = vectorOrigin.GetNextField(vectorOrientation);

        while (nextMapField?.CanBeTraversedThroughFromDirection(vectorOrientation) == true)
        {
            mapFields.Add(nextMapField);

            if (nextMapField is TVectorTerminator && nextMapField != vectorOrigin)
            {
                fields = mapFields;

                return true;
            }

            nextMapField = nextMapField.GetNextField(vectorOrientation);
        }

        fields = Enumerable.Empty<MapField>();

        return false;
    }

    private static Direction GetOrientationRelativeToField(MapField fromField, MapField toField)
    {
        if (toField.Coordinates.Y < fromField.Coordinates.Y
            && toField.Coordinates.X == fromField.Coordinates.X)
        {
            return Direction.Up;
        }
        else if (toField.Coordinates.Y > fromField.Coordinates.Y
            && toField.Coordinates.X == fromField.Coordinates.X)
        {
            return Direction.Down;
        }
        else if (toField.Coordinates.X > fromField.Coordinates.X
            && toField.Coordinates.Y == fromField.Coordinates.Y)
        {
            return Direction.Right;
        }
        else if (toField.Coordinates.X < fromField.Coordinates.X
            && toField.Coordinates.Y == fromField.Coordinates.Y)
        {
            return Direction.Left;
        }
        else
        {
            throw new InvalidOperationException(
                $"Cannot determine orientation of vector originating from {fromField.Coordinates}.");
        }
    }

    private static bool CanTraverseThroughField(MapField vectorOrigin, MapField nextField) =>
        nextField.CanBeTraversedThroughFromDirection(GetOrientationRelativeToField(vectorOrigin, nextField));
}
