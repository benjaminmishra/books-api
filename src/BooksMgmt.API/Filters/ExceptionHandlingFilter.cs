using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BooksMgmt.API.Filters;

[AttributeUsage(AttributeTargets.Method)]
public class ExceptionHandlingFilterAttribute : Attribute, IExceptionFilter
{
    public ExceptionHandlingFilterAttribute()
    {

    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        //_logger.LogError(exception, "An unhandled exception occurred.");

        var response = new
        {
            message = "An error occurred.",
            details = exception.Message
        };

        context.Result = new ObjectResult(response)
        {
            StatusCode = StatusCodes.Status500InternalServerError
        };

        context.ExceptionHandled = true;
    }
}

