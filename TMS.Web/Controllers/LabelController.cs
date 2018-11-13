using Microsoft.AspNetCore.Mvc;
using TMS.EntitiesDTO;
using TMS.Business.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using TMS.Web.Models;
using System.Diagnostics;

namespace TMS.Web.Controllers
{
    [Authorize]
    public class LabelController : Controller
    {
        private string _userId { get { return User.Identity.GetUserId(); } }
        private readonly LabelService _labelService;
        public LabelController(LabelService labelService)
        {
            _labelService = labelService;
        }

        [HttpGet]
        public IActionResult List()
        {
            var labels = _labelService.GetAll(_userId);
            return View(labels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            if(_userId==null)
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            var label = new LabelDTO()
            {
               UserId= _userId
            };
            return View(label);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([FromForm]LabelDTO label)
        {
            _labelService.Create(label);
            return RedirectToAction(nameof(List));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var label = _labelService.GetById(id);
            if(label.UserId !=_userId) // when u try to get access to other people label
            {
                return View(new ErrorViewModel { RequestId = "U can`t get access to this label" });
            }
            return View(label);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromForm]LabelDTO label)
        {
            _labelService.Update(label);
            return RedirectToAction(nameof(List));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var label = _labelService.GetById(id);
            if (label.UserId != _userId) // when u try to delete other people label
            {
                return View(new ErrorViewModel { RequestId = "U can`t get access to this label" });
            }
            _labelService.Delete(id);
            return RedirectToAction(nameof(List));
        }
    }
}