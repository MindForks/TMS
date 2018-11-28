﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TMS.Business.Services;
using TMS.EntitiesDTO;

namespace TMS.Web.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly TaskService _taskService;
        private readonly UserService _userService;
        private readonly TaskStatusService _taskStatusService;
        private readonly LabelService _labelService;
        private string _userId { get { return User.Identity.GetUserId(); } }

        public TaskController(TaskService taskService, UserService userService, TaskStatusService taskStatusService, LabelService labelService)
        {
            _taskService = taskService;
            _userService = userService;
            _taskStatusService = taskStatusService;
            _labelService = labelService;
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var tasks = _taskService.GetAll(_userId);
  
            ViewData["Moderators"] = (await _userService.GetAllAsync())
           .Select(user => new SelectListItem
           {
               Value = user.Id,
               Text = String.Format("{0} {1} ({2})", user.LastName, user.FirstName, user.Email)
           }
           );
            ViewData["Statuses"] = _taskStatusService.GetAll()
                .Select(taskStatus => new SelectListItem
                {
                    Value = taskStatus.Id.ToString(),
                    Text = taskStatus.Title
                });
            ViewData["Labels"] = (_labelService.GetAll(_userId))
             .Select(label => new SelectListItem
             {
                 Value = label.Id.ToString(),
                 Text = label.Title,
             });
            ViewData["LabelColors"] = (_labelService.GetAll(_userId))
             .Select(label => new SelectListItem
             {
                 Value = label.Id.ToString(),
                 Text = label.Color,
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
                  Text = String.Format("{0} {1} ({2})", user.LastName, user.FirstName, user.Email)
              });
            ViewData["Statuses"] = _taskStatusService.GetAll()
                .Select(taskStatus => new SelectListItem
                {
                    Value = taskStatus.Id.ToString(),
                    Text = taskStatus.Title
                });
            ViewData["Labels"] = new[] { new SelectListItem{
                Value = "-1",
                Text = "-",
            }}
          .Concat((_labelService.GetAll(_userId))
               .Select(label => new SelectListItem
               {
                   Value = label.Id.ToString(),
                   Text = label.Title,
               }));

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm]TaskDTO task)
        {
            _taskService.Create(task, _userId);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = _taskService.GetById(id, _userId);
            ViewData["Users"] = (await _userService.GetAllAsync())
              .Select(user => new SelectListItem
              {
                  Value = user.Id,
                  Text = String.Format("{0} {1} ({2})", user.LastName, user.FirstName, user.Email)
              });
            ViewData["Statuses"] = _taskStatusService.GetAll()
                .Select(taskStatus => new SelectListItem
                {
                    Value = taskStatus.Id.ToString(),
                    Text = taskStatus.Title
                });
            ViewData["Labels"] = new[] { new SelectListItem{
                Value = "-1",
                Text = "-",
            }}
           .Concat((_labelService.GetAll(_userId))
                .Select(label => new SelectListItem
            {
                    Value = label.Id.ToString(),
                    Text = label.Title,
            }));


            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm]TaskDTO task)
        {
            _taskService.Update(task, _userId);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ViewData["Users"] = (await _userService.GetAllAsync())
             .Select(user => new SelectListItem
             {
                 Value = user.Id,
                 Text = String.Format("{0} {1} ({2})", user.LastName, user.FirstName, user.Email)
             });
            ViewData["Statuses"] = _taskStatusService.GetAll()
                .Select(taskStatus => new SelectListItem
                {
                    Value = taskStatus.Id.ToString(),
                    Text = taskStatus.Title
                });

            var task = _taskService.GetById(id, _userId);

            var currentLabel = _labelService.GetAll(_userId)
                .FirstOrDefault(i => i.Id == task.CurrentLabelID);

            if (currentLabel != null)
            {
                ViewData["LabelColor"] = currentLabel.Color;
                ViewData["LabelData"] = currentLabel.Title;
            }

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _taskService.Delete(id);
            return RedirectToAction(nameof(List));
        }


    }

}