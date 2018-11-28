using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TMS.Business.Services;
using TMS.EntitiesDTO;

namespace TMS.Web.Controllers
{
    public class DashboardController : Controller
    {
        private readonly TaskService _taskService;
        private readonly UserService _userService;
        private readonly TaskStatusService _taskStatusService;
        private string _userId { get { return User.Identity.GetUserId(); } }

        public DashboardController(TaskService taskService, UserService userService, TaskStatusService taskStatusService)
        {
            _taskService = taskService;
            _userService = userService;
            _taskStatusService = taskStatusService;
        }

        public IActionResult Index()
        {      
            var statuses = _taskStatusService.GetAll();
            var tasks = _taskService.GetAll(_userId);

            var tasksTodo = tasks.Where( i=> i.StatusId == (int)TaskStatusesDTO.ToDo);
            var tasksInProgress = tasks.Where(i => i.StatusId == (int)TaskStatusesDTO.InProgress);
            var tasksDone = tasks.Where(i => i.StatusId == (int)TaskStatusesDTO.Done);

            TasksDTO tasksModel = new TasksDTO
            {
                TasksToDo = tasksTodo,
                TasksInProgress = tasksInProgress,
                TasksDone = tasksDone
            };

            return View(tasksModel);
        }

        public JsonResult GetEvents()
        {
            var events = _taskService.GetAll(_userId);

            return Json(events);
        }

        public IActionResult Calendar()
        {
            return View();
        }
    }
}