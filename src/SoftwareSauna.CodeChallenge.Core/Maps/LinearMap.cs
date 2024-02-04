using SoftwareSauna.CodeChallenge.MapFields;
using SoftwareSauna.CodeChallenge.Vectors;

namespace SoftwareSauna.CodeChallenge.Maps;

public class LinearMap
{
    private LinearMap(IEnumerable<MapField> mapFields)
    {
        MapFields = mapFields;
    }

    public IEnumerable<MapField> MapFields { get; private set; }

    public static LinearMap FromVectorMap(VectorMap vectorMap)
    {
        List<MapField> mapFields = new List<MapField>();

        foreach (Vector vector in vectorMap.Vectors)
        {
            IEnumerable<MapField> vectorFields =
                vector.IsStartVector
                    ? vector.Fields
                    : vector.Fields.Skip(1);

            mapFields.AddRange(vectorFields);
        }

        return new LinearMap(mapFields);
    }

    public string GetPathAsCharacters() =>
        new string(MapFields.Select(x => x.Value).ToArray());

    public string GetLetters() =>
        new string(
            MapFields
                .Where(x => x is LetterMapField)
                .Distinct()
                .Select(x => x.Value)
                .ToArray());
}
