using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TMS.Business.Services;
using TMS.EntitiesDTO;

namespace TMS.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskService _taskService;
        private readonly UserService _userService;
        private readonly TaskStatusService _taskStatusService;

        public TaskController(TaskService taskService, UserService userService, TaskStatusService taskStatusService)
        {
            _taskService = taskService;
            _userService = userService;
            _taskStatusService = taskStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tasks = _taskService.GetAll();
            ViewData["Users"] = (await _userService.GetAllAsync())
           .Select(user => new SelectListItem
           {
               Value = user.Id,
               Text = user.UserName
           });
            ViewData["Statuses"] = _taskStatusService.GetAll()
                .Select(taskStatus => new SelectListItem
                {
                    Value = taskStatus.Id.ToString(),
                    Text = taskStatus.Title
                });
            return View(tasks);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var task = new TaskDTO()
            {
                CreationTime= DateTimeOffset.Now,
                EndDate = DateTimeOffset.Now
            };
            ViewData["Users"] = (await _userService.GetAllAsync())
              .Select(user => new SelectListItem
              {
                  Value = user.Id,
                  Text = user.UserName
              });
            ViewData["Statuses"] = _taskStatusService.GetAll()
                .Select(taskStatus => new SelectListItem
                {
                    Value = taskStatus.Id.ToString(),
                    Text = taskStatus.Title
                });

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm]TaskDTO task)
        {
            _taskService.Create(task);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = _taskService.GetById(id);
            ViewData["Users"] = (await _userService.GetAllAsync())
              .Select(user => new SelectListItem
              {
                  Value = user.Id,
                  Text = user.UserName
              });
            ViewData["Statuses"] = _taskStatusService.GetAll()
                .Select(taskStatus => new SelectListItem
                {
                    Value = taskStatus.Id.ToString(),
                    Text = taskStatus.Title
                });

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm]TaskDTO task)
        {
            _taskService.Update(task);
            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _taskService.Delete(id);
            var tasks = _taskService.GetAll();
            return View("List", tasks);
        }
    }
}