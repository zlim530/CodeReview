using HelloDotnetCoreThree.Models;
using HelloDotnetCoreThree.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HelloDotnetCoreThree.Controllers {
    public class DepartmentController:Controller {

        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService) {
            _departmentService = departmentService;
        }

        public async Task<IActionResult> Index() {
            ViewBag.Title = "Department Index";
            var departments = _departmentService.GetAll();
            return View(departments);
        }


        public IActionResult Add() {
            ViewBag.Title = "Add Department";
            return View(new Department());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Department model) {
            // 判断传入的 model 参数是否合法
            if (ModelState.IsValid) {
                await _departmentService.Add(model);
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
