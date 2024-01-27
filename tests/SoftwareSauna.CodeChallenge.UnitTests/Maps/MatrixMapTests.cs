using FluentAssertions;
using SoftwareSauna.CodeChallenge.Exceptions;
using SoftwareSauna.CodeChallenge.MapFields;
using SoftwareSauna.CodeChallenge.Maps;
using SoftwareSauna.CodeChallenge.Vectors;

namespace SoftwareSauna.CodeChallenge.UnitTests.Maps;

public class MatrixMapTests
{
    [Fact]
    public void Creating_a_matrix_map_should_set_the_correct_dimensions()
    {
        string input =
            "@+" + Environment.NewLine +
            "x+";

        MatrixMap matrixMap = MatrixMap.FromString(input);

        matrixMap.MapFields.GetLength(0).Should().Be(2);
        matrixMap.MapFields.GetLength(1).Should().Be(2);
    }

    [Fact]
    public void Creating_a_matrix_map_with_jagged_input_should_set_the_correct_dimensions()
    {
        string input =
            "@-+" + Environment.NewLine +
            " ++" + Environment.NewLine +
            " x";

        MatrixMap matrixMap = MatrixMap.FromString(input);

        matrixMap.MapFields.GetLength(0).Should().Be(3);
        matrixMap.MapFields.GetLength(1).Should().Be(3);
    }

    [Fact]
    public void Creating_a_matrix_map_should_map_the_characters_to_the_correct_fields()
    {
        string input =
            "@+" + Environment.NewLine +
            "x+";

        MatrixMap matrixMap = MatrixMap.FromString(input);

        matrixMap.MapFields[0, 0].Should().BeOfType<StartingMapField>();
        matrixMap.MapFields[0, 1].Should().BeOfType<EndMapField>();
        matrixMap.MapFields[1, 0].Should().BeOfType<IntersectionMapField>();
        matrixMap.MapFields[1, 1].Should().BeOfType<IntersectionMapField>();
    }

    [Fact]
    public void Creating_a_map_should_create_correct_relations_between_fields()
    {
        string input = 
            "@+" + Environment.NewLine + 
            "x+";

        MatrixMap matrixMap = MatrixMap.FromString(input);

        MapField upperLeftField = matrixMap.StartingField;
        MapField lowerLeftField = matrixMap.MapFields[0, 1];
        MapField upperRightField = matrixMap.MapFields[1, 0];
        MapField lowerRightField = matrixMap.MapFields[1, 1];

        upperLeftField.GetNextField(Direction.Right).Should().Be(upperRightField);
        upperLeftField.GetNextField(Direction.Down).Should().Be(lowerLeftField);
        lowerLeftField.GetNextField(Direction.Right).Should().Be(lowerRightField);
        lowerLeftField.GetNextField(Direction.Up).Should().Be(upperLeftField);
        upperRightField.GetNextField(Direction.Left).Should().Be(upperLeftField);
        upperRightField.GetNextField(Direction.Down).Should().Be(lowerRightField);
        lowerRightField.GetNextField(Direction.Left).Should().Be(lowerLeftField);
        lowerRightField.GetNextField(Direction.Up).Should().Be(upperRightField);
    }

    [Fact]
    public void Creating_a_map_should_set_the_correct_starting_field()
    {
        string input =
            "@+" + Environment.NewLine +
            "x+";

        MatrixMap matrixMap = MatrixMap.FromString(input);

        matrixMap.StartingField.Should().BeOfType<StartingMapField>();
    }

    [Fact]
    public void Creating_a_map_without_a_start_field_should_fail()
    {
        string input =
            "-+" + Environment.NewLine +
            "x+";

        Action act = () => MatrixMap.FromString(input);

        act.Should().Throw<NoStartingFieldException>();
    }

    [Fact]
    public void Creating_a_map_with_multiple_start_fields_should_fail()
    {
        string input =
            "@+" + Environment.NewLine +
            "@+";

        Action act = () => MatrixMap.FromString(input);

        act.Should().Throw<MultipleStartFieldsException>();
    }

    [Fact]
    public void Creating_a_map_without_an_end_field_should_fail()
    {
        string input =
            "@+" + Environment.NewLine +
            "-+";

        Action act = () => MatrixMap.FromString(input);

        act.Should().Throw<NoEndFieldException>();
    }
}
