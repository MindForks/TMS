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
    public class DashboardController : Controller
    {
        private readonly TaskService _taskService;
        private readonly UserService _userService;
        private readonly TaskStatusService _taskStatusService;

        public DashboardController(TaskService taskService, UserService userService, TaskStatusService taskStatusService)
        {
            _taskService = taskService;
            _userService = userService;
            _taskStatusService = taskStatusService;
        }

        public IActionResult Index()
        {
            var tasksTodo = new List<TaskDTO>();
            var tasksInProgress = new List<TaskDTO>();
            var tasksDone = new List<TaskDTO>();

            var statuses = _taskStatusService.GetAll();

            var tasks = _taskService.GetAll();

            foreach (var task in tasks)
            {
                if (task.StatusId == 1)
                {
                    tasksTodo.Add(task);
                }
                else if(task.StatusId == 2)
                {
                    tasksInProgress.Add(task);
                }
                else
                {
                    tasksDone.Add(task);
                }
            }
            /*var tasksInProgress = _taskService.GetAll().Select(task => task.StatusId == 2);
            var tasksDone = _taskService.GetAll().Select(task => task.StatusId == 3);*/

            TasksDTO tasksModel = new TasksDTO
            {
                TasksToDo = tasksTodo,
                TasksInProgress = tasksInProgress,
                TasksDone = tasksDone
            };

            return View(tasksModel);
        }
    }
}