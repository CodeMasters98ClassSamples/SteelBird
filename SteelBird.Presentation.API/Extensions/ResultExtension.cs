using Microsoft.AspNetCore.Mvc;
using SteelBird.Presentation.API.Wrappers;
using System.Net;

namespace SteelBird.Presentation.API.Extensions;

public static class ResultExtension
{
    public static ObjectResult ToObjectResult(this Result result, bool withEnglishMessage)
    {
        //if (result.Status is not HttpStatusCode.OK)
        //    //return new ObjectResult(result.ToProblemDetails()) { StatusCode = (int)result.Status };
        //    return null;

        if (result.IsSuccess)
        {
            if (withEnglishMessage)
                result.WithMessage("Success");

            return new OkObjectResult(result);
        }

        Result problemResult = Result.Failure(result.Error);

        if (withEnglishMessage)
            problemResult.WithMessage("Failure");

        return new ObjectResult(problemResult) { StatusCode = (int)result.Status };
    }
}
