using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksMgmt.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = "Basic", Roles = "User")]
public class NameController : ControllerBase
{

    [HttpGet]
    public string GetName([FromQuery] string Name)
    {
        return $"Hello {Name} you are not authenticated";
    }
}

