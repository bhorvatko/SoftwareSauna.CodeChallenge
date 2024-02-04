using Microsoft.AspNetCore.Http;
using SoftwareSauna.CodeChallenge.Core.Exceptions;

namespace SoftwareSauna.CodeChallenge.Infrastructure.ErrorHandling;

internal class DomainInvalidOperationExceptionHandler
    : ExceptionHandlerBase<DomainInvalidOperationException>
{
    protected override int StatusCode => StatusCodes.Status500InternalServerError;

    protected override string Title => "Internal Server Error";
}
