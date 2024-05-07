using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace IntersectionFinder.API.Extensions;

public static class ResultExtensions
{
    public static IActionResult ToActionResult<T>(this Result<T> result)
    {
        if (result.IsSuccess)
        {
            return new OkObjectResult(result.Value);
        }

        var errors = string.Join(", ", result.Errors.Select(e => e.Message));
        var problemDetails = new ProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
            Title = "Bad request",
            Detail = string.Join(", ", result.Errors.Select(e => e.Message))
        };

        return new ObjectResult(problemDetails);
    }
}
