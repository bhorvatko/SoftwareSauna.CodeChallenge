using FluentAssertions;
using SoftwareSauna.CodeChallenge.MapFields;
using SoftwareSauna.CodeChallenge.Maps;

namespace SoftwareSauna.CodeChallenge.UnitTests.Maps;

public class LinearMapTests
{
    [Fact]
    public void Creating_a_linear_map_should_correctly_map_fields()
    {
        string input =
            "@-+" + Environment.NewLine +
            "  |" + Environment.NewLine +
            "  x" + Environment.NewLine;

        MatrixMap matrixMap = MatrixMap.FromString(input);
        VectorMap vectorMap = VectorMap.FromMatrixMap(matrixMap);
        LinearMap linearMap = LinearMap.FromVectorMap(vectorMap);

        linearMap
            .MapFields
            .Select(x => x.GetType())
            .Should()
            .BeEquivalentTo(new[]
            {
                typeof(StartingMapField),
                typeof(HorizontalPathMapField),
                typeof(IntersectionMapField),
                typeof(VerticalPathMapField),
                typeof(EndMapField)
            }, opt => opt.WithStrictOrdering());
    }

    [Fact]
    public void Getting_path_as_characters_should_return_the_correct_character_sequence()
    {
        string input =
            "@-+" + Environment.NewLine +
            "  |" + Environment.NewLine +
            "  x" + Environment.NewLine;

        MatrixMap matrixMap = MatrixMap.FromString(input);
        VectorMap vectorMap = VectorMap.FromMatrixMap(matrixMap);
        LinearMap linearMap = LinearMap.FromVectorMap(vectorMap);

        linearMap.GetPathAsCharacters().Should().Be("@-+|x");
    }

    [Fact]
    public void Getting_letters_should_return_the_correct_letter_sequence()
    {
        string input =
            "@A+" + Environment.NewLine +
            "  B" + Environment.NewLine +
            "  x" + Environment.NewLine;

        MatrixMap matrixMap = MatrixMap.FromString(input);
        VectorMap vectorMap = VectorMap.FromMatrixMap(matrixMap);
        LinearMap linearMap = LinearMap.FromVectorMap(vectorMap);

        linearMap.GetLetters().Should().Be("AB");
    }

    [Fact]
    public void Getting_letters_should_include_a_field_letter_only_once()
    {
        string input =
            " x"    + Environment.NewLine +
            "@A+"   + Environment.NewLine +
            " +B"   + Environment.NewLine;

        MatrixMap matrixMap = MatrixMap.FromString(input);
        VectorMap vectorMap = VectorMap.FromMatrixMap(matrixMap);
        LinearMap linearMap = LinearMap.FromVectorMap(vectorMap);

        linearMap.GetLetters().Should().Be("AB");
    }

    [Fact]
    public void Getting_letters_should_include_repeating_letters_if_they_are_found_on_different_fields()
    {
        string input =
            " x" + Environment.NewLine +
            "@A+" + Environment.NewLine +
            " +A" + Environment.NewLine;

        MatrixMap matrixMap = MatrixMap.FromString(input);
        VectorMap vectorMap = VectorMap.FromMatrixMap(matrixMap);
        LinearMap linearMap = LinearMap.FromVectorMap(vectorMap);

        linearMap.GetLetters().Should().Be("AA");
    }
}
