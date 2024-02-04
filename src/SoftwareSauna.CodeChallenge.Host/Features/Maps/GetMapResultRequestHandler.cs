using MediatR;
using SoftwareSauna.CodeChallenge.Maps;

namespace SoftwareSauna.CodeChallenge.Host.Features.Maps;

public class GetMapResultRequestHandler
    : IRequestHandler<GetMapResultRequest, GetMapResultResponse>
{
    public Task<GetMapResultResponse> Handle(
        GetMapResultRequest request, 
        CancellationToken cancellationToken)
    {
        MatrixMap matrixMap = MatrixMap.FromString(request.MapString);
        VectorMap vectorMap = VectorMap.FromMatrixMap(matrixMap);
        LinearMap linearMap = LinearMap.FromVectorMap(vectorMap);

        return Task.FromResult(
            new GetMapResultResponse(
                linearMap.GetLetters(), 
                linearMap.GetPathAsCharacters()));
    }
}
