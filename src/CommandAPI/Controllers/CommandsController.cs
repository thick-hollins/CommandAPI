using Microsoft.AspNetCore.Mvc;

namespace CommandApi.Contollers;

[Route("api/[controller]")]
[ApiController]
public class CommandsController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get()
        => new string[] { "this", "is", "hard", "coded" };
}