namespace SoftwareSauna.CodeChallenge.Host.Features.Maps;

public record GetMapResultResponse(
    string CollectedLetters,
    string PathAsCharacters)
{
}
