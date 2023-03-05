using Microsoft.AspNetCore.Mvc.Filters;

namespace BooksMgmt.API.Filters;
public class LoggingFilter : IActionFilter
{
    private readonly ILogger _logger;

    public LoggingFilter(ILogger<LoggingFilter> logger)
    {
        _logger = logger;
    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _logger.LogInformation($"Executing action {context.ActionDescriptor.DisplayName}");
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        _logger.LogInformation($"Executed action {context.ActionDescriptor.DisplayName}");
    }
}