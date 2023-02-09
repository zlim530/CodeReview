using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP.NETCoreWebAPIDemo.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class LoginController : ControllerBase
{

    [HttpPost]
    public ActionResult<LoginResponse> Login(LoginRequest req)
    {
        if (req.UserName == "admin" && req.Password == "123456")
        {
            var processes = Process.GetProcesses().Select(p => new ProcessInfo(p.Id, p.ProcessName, p.WorkingSet64)).ToArray();
            return new LoginResponse(true, processes);
        }
        else
        {
            return new LoginResponse(false, null);
        }
    }
}

public record LoginRequest (string UserName, string Password);

public record ProcessInfo (long Id, string Name, long WorkingSet);

public record LoginResponse(bool Ok, ProcessInfo[]? ProcessInfos);