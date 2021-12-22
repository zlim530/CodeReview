using helloapi.Model;
using helloapi.Service;
using Microsoft.AspNetCore.Mvc;

namespace helloapi.Controllers
{
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        public IActionResult Create()
        {
            _studentService.Create(new Student
            {
                Name = "zlim",
                Age = 23
            });
            return Ok("ok");
        }

        public IActionResult GetName()
        {
            return Ok(_studentService.GetFirstName());
        }

    }
}