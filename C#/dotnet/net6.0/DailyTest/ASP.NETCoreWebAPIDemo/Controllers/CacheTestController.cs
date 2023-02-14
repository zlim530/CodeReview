using Microsoft.AspNetCore.Mvc;

namespace ASP.NETCoreWebAPIDemo;

[Route("api/[controller]/[action]")]
[ApiController]
public class CacheTestController : ControllerBase
{

    [HttpGet]
    [ResponseCache(Duration=20)]
    public DateTime GetNow() => DateTime.Now;
}