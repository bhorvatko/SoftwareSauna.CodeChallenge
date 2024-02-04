using SoftwareSauna.CodeChallenge.Exceptions;
using SoftwareSauna.CodeChallenge.MapFields;

namespace SoftwareSauna.CodeChallenge.Maps;

public class MatrixMap
{
    private MatrixMap(
        StartingMapField startingField,
        MapField[,] mapFields)
    {
        StartingField = startingField;
        MapFields = mapFields;
    }

    public StartingMapField StartingField { get; private set; }
    public MapField[,] MapFields { get; private set; }

    public static MatrixMap FromString(string input)
    {
        MapField[,] mapFields = GetMapFields(input);

        StartingMapField startingMapField = GetStartField(mapFields);

        CheckEndFieldExists(mapFields);

        return new MatrixMap(startingMapField, mapFields);
    }

    private static MapField[,] GetMapFields(string input)
    {
        string[] rows = input.Split(new[] { "\n", "\r\n" }, StringSplitOptions.None);

        int mapHeight = rows.Count();
        int mapWidth = rows.Max(x => x.Length);

        MapField[,] mapFields = new MapField[mapWidth, mapHeight];

        for (uint lineIndex = 0; lineIndex < mapHeight; lineIndex++)
        {
            string line = rows[lineIndex];

            for (uint columnIndex = 0; columnIndex < mapWidth; columnIndex++)
            {
                char fieldValue = line.ElementAtOrDefault((int)columnIndex);

                MapField field = MapField.Create(
                    fieldValue is not default(char) ? fieldValue : EmptyMapField.Identifiers.First(),
                    columnIndex,
                    lineIndex,
                    GetFieldOrDefault(mapFields, columnIndex - 1, lineIndex),
                    GetFieldOrDefault(mapFields, columnIndex, lineIndex - 1));

                mapFields[columnIndex, lineIndex] = field;
            }
        }

        return mapFields;
    }

    private static MapField? GetFieldOrDefault(
        MapField[,] mapFields,
        uint xCoordinate,
        uint yCoordinate) =>
        xCoordinate >= 0
            && xCoordinate < mapFields.GetLength(0)
            && yCoordinate >= 0
            && yCoordinate < mapFields.GetLength(1)
                ? mapFields[xCoordinate, yCoordinate]
                : null;

    private static IEnumerable<MapField> GetAllFields(MapField[,] mapFields)
    {
        foreach (MapField field in mapFields)
        {
            yield return field;
        }
    }

    private static StartingMapField GetStartField(MapField[,] mapFields)
    {
        IEnumerable<StartingMapField> startMapFields =
            GetAllFields(mapFields)
                .Where(x => x is StartingMapField)
                .Select(x => (StartingMapField)x);

        if (!startMapFields.Any())
        {
            throw new NoStartingFieldException();
        }

        if (startMapFields.Count() > 1)
        {
            throw new MultipleStartFieldsException(startMapFields);
        }

        return startMapFields.Single();
    }

    private static void CheckEndFieldExists(MapField[,] mapFields)
    {
        EndMapField? endMapField =
            GetAllFields(mapFields).FirstOrDefault(x => x is EndMapField) as EndMapField;

        if (endMapField is null)
        {
            throw new NoEndFieldException();
        }
    }
}
