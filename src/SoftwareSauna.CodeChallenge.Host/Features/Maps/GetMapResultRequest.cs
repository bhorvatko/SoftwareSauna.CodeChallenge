using MediatR;

namespace SoftwareSauna.CodeChallenge.Host.Features.Maps;

public record GetMapResultRequest(string MapString)
    : IRequest<GetMapResultResponse>
{
}
