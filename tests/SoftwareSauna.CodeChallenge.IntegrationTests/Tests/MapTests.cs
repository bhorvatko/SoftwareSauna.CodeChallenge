using FluentAssertions;
using SoftwareSauna.CodeChallenge.Host.Features.Maps;
using SoftwareSauna.CodeChallenge.IntegrationTests.Helpers;
using System.Net.Http.Json;
using Xunit.Abstractions;

namespace SoftwareSauna.CodeChallenge.IntegrationTests.Tests;

public class MapTests
    : IntegrationTestBase
{
    public const string MapsEndpoint = "Maps";

    public MapTests(ITestOutputHelper testOutputHelper) 
        : base(testOutputHelper)
    {
    }

    [Theory]
    [InlineData("Map_001", "ACB", "@---A---+|C|+---+|+-B-x")]
    public async Task Requesting_a_result_for_a_valid_map_should_return_OK_with_expected_response(
        string mapFileName,
        string collectedLetters,
        string pathAsCharacters)
    {
        GetMapResultRequest request = new(MapHelper.ReadMap(mapFileName));

        HttpResponseMessage response = 
            await _client.PostAsJsonAsync(MapsEndpoint, request);

        response
            .Should().BeSuccessful()
            .And.Satisfy<GetMapResultResponse>(response =>
            {
                response.CollectedLetters.Should().Be(collectedLetters);
                response.PathAsCharacters.Should().Be(pathAsCharacters);
            });
    }

    [Theory]
    [InlineData("Map_007")]
    public async Task Requesting_a_result_for_an_invalid_map_should_return_bad_request(string mapFileName)
    {
        GetMapResultRequest request = new(MapHelper.ReadMap(mapFileName));

        HttpResponseMessage response =
            await _client.PostAsJsonAsync(MapsEndpoint, request);

        response.Should().Be400BadRequest();
    }
}
