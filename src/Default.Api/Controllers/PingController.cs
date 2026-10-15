using Microsoft.AspNetCore.Mvc;

namespace Default.Api.Controllers;

/// <summary>Minimal host smoke endpoint — replace with domain modules.</summary>
[ApiController]
[Route("[controller]")]
public class PingController : ControllerBase
{
    [HttpGet]
    public ActionResult<object> Get() => Ok(new { status = "ok" });
}
