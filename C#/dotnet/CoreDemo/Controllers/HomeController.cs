using CoreDemo.Models;
using CoreDemo.Services;
using CoreDemo.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace CoreDemo.Controllers {
    public class HomeController :Controller{
        private readonly ICinemaService _cinemaService;
        private readonly IOptions<ConnectionOptions> _options;
        // 构造函数注入
        public HomeController(ICinemaService cinemaService,IOptions<ConnectionOptions> options) {
            _cinemaService = cinemaService;
            _options = options;
        }

        public async Task<IActionResult> Index() {
            ViewBag.Title = "电影院列表";
            // 在 View() 是 Controller 父类中定义的方法
            return View(await _cinemaService.GetAllAsync());
        }

        public IActionResult Add() {
            ViewBag.Title = "添加电影院";
            return View(new Cinema());
        }


        // 如果不写 [HttpPost] 则为 http get
        [HttpPost]
        public async Task<IActionResult> Add(Cinema model) {
            if (ModelState.IsValid) {
                await _cinemaService.AddAsync(model);
            }
            // 跳转回本 controller 下面的 action，即 Index action
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int cinemaId) {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,Cinema model) {
            if (ModelState.IsValid) {
                var exist = await _cinemaService.GetByIdAsync(id);
                if (exist == null) {
                    return NotFound();
                }

                exist.Name = model.Name;
                exist.Location = model.Location;
                exist.Capacity = model.Capacity;
            }

            return RedirectToAction("Index");
        }

    }
}
