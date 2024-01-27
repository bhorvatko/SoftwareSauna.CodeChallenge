using FluentAssertions;
using SoftwareSauna.CodeChallenge.Maps;

namespace SoftwareSauna.CodeChallenge.UnitTests.Maps;

public class VectorMapTests
{
    [Fact]
    public void Creating_a_vector_map_should_correctly_map_vectors()
    {
        string input =
            "@-+" + Environment.NewLine +
            "  |" + Environment.NewLine +
            "  x" + Environment.NewLine;

        MatrixMap matrixMap = MatrixMap.FromString(input);

        VectorMap vectorMap = VectorMap.FromMatrixMap(matrixMap);

        vectorMap.Vectors.Should().SatisfyRespectively(
            firstVector => firstVector.ToString().Should().Be("@-+"),
            secondVector => secondVector.ToString().Should().Be("+|x"));
    }
}
