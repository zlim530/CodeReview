using CoreDemo.Models;
using CoreDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using System.Threading.Tasks;

namespace CoreDemo.Controllers {
    public class HomeController :Controller{
        private readonly ICinemaService _cinemaService;
        // 构造函数注入
        public HomeController(ICinemaService cinemaService) {
            _cinemaService = cinemaService;
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

        public IActionResult Edit(int cinemaId) {
            return RedirectToAction("Index");
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

    }
}
