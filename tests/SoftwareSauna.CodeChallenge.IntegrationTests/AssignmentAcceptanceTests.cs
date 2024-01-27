using FluentAssertions;
using SoftwareSauna.CodeChallenge.Exceptions;
using SoftwareSauna.CodeChallenge.IntegrationTests.Helpers;
using SoftwareSauna.CodeChallenge.Maps;

namespace SoftwareSauna.CodeChallenge.IntegrationTests;

public class AssignmentAcceptanceTests
{
    [Theory]
    [InlineData("Map_001", "@---A---+|C|+---+|+-B-x")]
    [InlineData("Map_002", "@|A+---B--+|+--C-+|-||+---D--+|x")]
    [InlineData("Map_003", "@---A---+|||C---+|+-B-x")]
    [InlineData("Map_004", "@-G-O-+|+-+|O||+-O-N-+|I|+-+|+-I-+|ES|x")]
    [InlineData("Map_005", "@B+++B|+-L-+A+++A-+Hx")]
    [InlineData("Map_006", "@-A--+|+-B--x")]
    public void Valid_maps_should_return_the_correct_path_as_characters(
        string fileName,
        string expectedOutput)
    {
        string input = MapHelper.ReadMap(fileName);

        MatrixMap matrixMap = MatrixMap.FromString(input);
        VectorMap vectorMap = VectorMap.FromMatrixMap(matrixMap);
        LinearMap linearMap = LinearMap.FromVectorMap(vectorMap);

        linearMap.GetPathAsCharacters().Should().Be(expectedOutput);
    }

    [Theory]
    [InlineData("Map_001", "ACB")]
    [InlineData("Map_002", "ABCD")]
    [InlineData("Map_003", "ACB")]
    [InlineData("Map_004", "GOONIES")]
    [InlineData("Map_005", "BLAH")]
    [InlineData("Map_006", "AB")]
    public void Valid_maps_should_return_the_correct_letters(
        string fileName,
        string expectedOutput)
    {
        string input = MapHelper.ReadMap(fileName);

        MatrixMap matrixMap = MatrixMap.FromString(input);
        VectorMap vectorMap = VectorMap.FromMatrixMap(matrixMap);
        LinearMap linearMap = LinearMap.FromVectorMap(vectorMap);

        linearMap.GetLetters().Should().Be(expectedOutput);
    }

    [Theory]
    [InlineData("Map_007")]
    public void Creating_a_map_without_a_start_character_should_fail(string fileName)
    {
        string input = MapHelper.ReadMap(fileName);

        Action act = () => MatrixMap.FromString(input);

        act.Should().Throw<NoStartingFieldException>();
    }

    [Theory]
    [InlineData("Map_008")]
    public void Creating_a_map_without_an_end_character_should_fail(string fileName)
    {
        string input = MapHelper.ReadMap(fileName);

        Action act = () => MatrixMap.FromString(input);

        act.Should().Throw<NoEndFieldException>();
    }

    [Theory]
    [InlineData("Map_009")]
    [InlineData("Map_010")]
    [InlineData("Map_011")]
    public void Creating_a_map_with_multiple_start_characters_should_fail(string fileName)
    {
        string input = MapHelper.ReadMap(fileName);

        Action act = () => MatrixMap.FromString(input);

        act.Should().Throw<MultipleStartFieldsException>();
    }

    [Theory]
    [InlineData("Map_012")]
    public void Creating_a_map_with_a_fork_in_the_path_should_fail(string fileName)
    {
        string input = MapHelper.ReadMap(fileName);

        Action act = () => 
            VectorMap.FromMatrixMap(MatrixMap.FromString(input));

        act.Should().Throw<MultiplePathsException>();
    }

    [Theory]
    [InlineData("Map_013")]
    public void Creating_a_map_with_a_broken_path_should_fail(string fileName)
    {
        string input = MapHelper.ReadMap(fileName);

        Action act = () =>
            VectorMap.FromMatrixMap(MatrixMap.FromString(input));

        act.Should().Throw<NoVectorEndFieldException>();
    }

    [Theory]
    [InlineData("Map_014")]
    public void Creating_a_map_with_multiple_starting_paths_should_fail(string fileName)
    {
        string input = MapHelper.ReadMap(fileName);

        Action act = () =>
            VectorMap.FromMatrixMap(MatrixMap.FromString(input));

        act.Should().Throw<MultiplePathsException>();
    }

    [Theory]
    [InlineData("Map_015")]
    public void Creating_a_map_with_a_fake_turn_should_fail(string fileName)
    {
        string input = MapHelper.ReadMap(fileName);

        Action act = () =>
            VectorMap.FromMatrixMap(MatrixMap.FromString(input));
        
        act.Should().Throw<FakeTurnException>();
    }
}