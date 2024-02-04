using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareSauna.CodeChallenge.Host.Features.Common;

[ApiController]
[Route("[controller]s")]
public abstract class ApiControllerBase
    : ControllerBase
{
    private IMediator? _mediator;

    protected IMediator Mediator =>
        _mediator ??= HttpContext.RequestServices.GetRequiredService<IMediator>();
}
