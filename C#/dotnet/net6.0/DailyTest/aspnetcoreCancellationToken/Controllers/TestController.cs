using aspnetcoreCancellationToken.Models;
using Microsoft.AspNetCore.Mvc;

namespace aspnetcoreCancellationToken.Controllers;

public class TestController : Controller
{
    public IActionResult Demo1()
    {
        var model = new Person("Tim", true, DateTime.Now);
        return View(model);
    }
}