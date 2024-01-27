using SoftwareSauna.CodeChallenge.MapFields;

namespace SoftwareSauna.CodeChallenge.UnitTests.MapFields;

internal static class MapFieldHelper
{
    public static MapField Create(
        char value = '-',
        uint xCoordinate = 0,
        uint yCoordinate = 0,
        MapField? fieldToTheLeft = null,
        MapField? fieldAbove = null) =>
        MapField.Create(
            value,
            xCoordinate,
            yCoordinate,
            fieldToTheLeft,
            fieldAbove);
}
