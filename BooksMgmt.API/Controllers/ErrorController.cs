using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;

namespace BooksMgmt.API.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet("/error")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Get()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var stacktrace = context!.Error.StackTrace;
            var errorMessage = context.Error.Message;

            // do logging

            return Problem(errorMessage);
        }
    }
}
