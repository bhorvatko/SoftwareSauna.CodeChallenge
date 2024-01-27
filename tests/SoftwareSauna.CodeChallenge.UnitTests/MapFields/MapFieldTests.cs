using FluentAssertions;
using SoftwareSauna.CodeChallenge.Exceptions;
using SoftwareSauna.CodeChallenge.MapFields;
using SoftwareSauna.CodeChallenge.Vectors;

namespace SoftwareSauna.CodeChallenge.UnitTests.MapFields;

public class MapFieldTests
{
    [Theory]
    [InlineData('@', typeof(StartingMapField))]
    [InlineData('-', typeof(HorizontalPathMapField))]
    [InlineData('|', typeof(VerticalPathMapField))]
    [InlineData('+', typeof(IntersectionMapField))]
    [InlineData('A', typeof(LetterMapField))]
    [InlineData('Z', typeof(LetterMapField))]
    [InlineData('x', typeof(EndMapField))]
    [InlineData(' ', typeof(EmptyMapField))]
    public void Creating_a_map_field_returns_a_field_with_the_correct_type(
        char fieldValue,
        Type expectedType)
    {
        MapField mapField = MapField.Create(fieldValue, 0, 0, null, null);

        mapField.Should().BeOfType(expectedType);
    }

    [Fact]
    public void Creating_a_map_field_with_an_invalid_value_should_fail()
    {
        Action act = () => MapField.Create('$', 0, 0, null, null);

        act.Should().Throw<InvalidFieldValueException>();
    }

    [Fact]
    public void Getting_the_next_field_should_return_the_correct_adjacent_field()
    {
        MapField upperField = MapFieldHelper.Create(
            xCoordinate: 1,
            yCoordinate: 0);

        MapField leftField = MapFieldHelper.Create(
            xCoordinate: 0,
            yCoordinate: 1);

        MapField centerField = MapFieldHelper.Create(
            xCoordinate: 1,
            yCoordinate: 1,
            fieldToTheLeft: leftField,
            fieldAbove: upperField);

        MapField rightField = MapFieldHelper.Create(
            xCoordinate: 2,
            yCoordinate: 1,
            fieldToTheLeft: centerField);

        MapField lowerField = MapFieldHelper.Create(
            xCoordinate: 1,
            yCoordinate: 2,
            fieldAbove: centerField);

        centerField.GetNextField(Direction.Up).Should().Be(upperField);
        centerField.GetNextField(Direction.Right).Should().Be(rightField);
        centerField.GetNextField(Direction.Down).Should().Be(lowerField);
        centerField.GetNextField(Direction.Left).Should().Be(leftField);
    }

    [Fact]
    public void Getting_surrounding_fields_for_a_corner_field_should_return_only_two_fields()
    {

        MapField cornerField = MapFieldHelper.Create(
            xCoordinate: 0,
            yCoordinate: 0);

        MapField rightField = MapFieldHelper.Create(
            xCoordinate: 1,
            yCoordinate: 0,
            fieldToTheLeft: cornerField);

        MapField lowerField = MapFieldHelper.Create(
            xCoordinate: 0,
            yCoordinate: 1,
            fieldAbove: cornerField);

        IEnumerable<MapField> surroundingFields = cornerField.GetSurroundingFields();

        surroundingFields.Should().Contain(new[] { rightField, lowerField });
    }
}
