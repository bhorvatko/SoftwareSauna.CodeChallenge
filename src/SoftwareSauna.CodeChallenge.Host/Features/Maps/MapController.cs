using Microsoft.AspNetCore.Mvc;
using SoftwareSauna.CodeChallenge.Host.Features.Common;

namespace SoftwareSauna.CodeChallenge.Host.Features.Maps;

public class MapController
    : ApiControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(GetMapResultResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMapResult(
        [FromBody] GetMapResultRequest request,
        CancellationToken cancellationToken)
    {
        var response = await Mediator.Send(request, cancellationToken);

        return Ok(response);
    }
}
