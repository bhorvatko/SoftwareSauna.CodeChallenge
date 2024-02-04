using Microsoft.AspNetCore.Http;
using SoftwareSauna.CodeChallenge.Core.Exceptions;

namespace SoftwareSauna.CodeChallenge.Infrastructure.ErrorHandling;

internal class DomainArgumentExceptionHandler
    : ExceptionHandlerBase<DomainArgumentException>
{
    protected override int StatusCode => StatusCodes.Status400BadRequest;

    protected override string Title => "Bad Request";
}
