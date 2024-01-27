using FluentAssertions;
using SoftwareSauna.CodeChallenge.Exceptions;
using SoftwareSauna.CodeChallenge.MapFields;
using SoftwareSauna.CodeChallenge.Maps;
using SoftwareSauna.CodeChallenge.Vectors;

namespace SoftwareSauna.CodeChallenge.UnitTests.Vectors;

public class VectorTests
{
    [Theory]
    [InlineData("@-x", Direction.Right)]
    [InlineData("x-@", Direction.Left)]
    [InlineData("x\n|\n@", Direction.Up)]
    [InlineData("@\n|\nx", Direction.Down)]
    public void Creating_a_starting_vector_should_set_the_correct_orientation(
        string map,
        Direction expectedOrientation)
    {
        MatrixMap matrixMap = MatrixMap.FromString(map);

        Vector vector = Vector.CreateStartingVector(matrixMap.StartingField);

        vector.Orientation.Should().Be(expectedOrientation);
    }

    [Theory]
    [InlineData("@\n+x", Direction.Right)]
    [InlineData(" @\nx+", Direction.Left)]
    [InlineData(" x\n@+", Direction.Up)]
    [InlineData("@+\n x", Direction.Down)]
    public void Creating_an_appended_vector_should_set_the_correct_orientation(
        string map,
        Direction expectedOrientation)
    {
        MatrixMap matrixMap = MatrixMap.FromString(map);
        Vector startingVector = Vector.CreateStartingVector(matrixMap.StartingField);

        Vector appendedVector = startingVector.CreateAppendedVector();

        appendedVector.Orientation.Should().Be(expectedOrientation);
    }

    [Fact]
    public void Creating_a_vector_should_set_the_correct_vector_fields()
    {
        MatrixMap matrixMap = MatrixMap.FromString("@-x");

        Vector vector = Vector.CreateStartingVector(matrixMap.StartingField);

        vector.Fields.Should().SatisfyRespectively(
            firstField => firstField.Should().BeOfType<StartingMapField>(),
            secondField => secondField.Should().BeOfType<HorizontalPathMapField>(),
            thirdField => thirdField.Should().BeOfType<EndMapField>());
    }

    [Fact]
    public void Creating_a_vector_that_passes_through_a_perpendicular_path_field()
    {
        string input =
            "  x"   + Environment.NewLine +
            "@-|-+" + Environment.NewLine + 
            "  +-+";

        MatrixMap matrixMap = MatrixMap.FromString(input);

        Vector vector = Vector.CreateStartingVector(matrixMap.StartingField);

        vector.ToString().Should().Be("@-|-+");   
    }

    [Fact]
    public void Creating_a_vector_with_multiple_possible_orientations_should_fail()
    {
        string input =
            "  +-x" + Environment.NewLine +
            "@-+ |" + Environment.NewLine +
            "  +-+";
                
        MatrixMap matrixMap = MatrixMap.FromString(input);
        Vector startingVector = Vector.CreateStartingVector(matrixMap.StartingField);

        Action act = () => startingVector.CreateAppendedVector();

        act.Should().Throw<MultiplePathsException>();
    }
}
