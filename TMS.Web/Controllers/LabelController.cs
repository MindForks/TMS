using Microsoft.AspNetCore.Mvc;
using TMS.EntitiesDTO;
using TMS.Business.Services;
using Microsoft.AspNet.Identity;

namespace TMS.Web.Controllers
{
    public class LabelController : Controller
    {
        private readonly LabelService _labelService;
        public LabelController(LabelService labelService)
        {
            _labelService = labelService;
        }

        [HttpGet]
        public IActionResult List()
        {
            var labels = _labelService.GetAll();
            return View(labels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var label = new LabelDTO()
            {
               UserId= User.Identity.GetUserId()
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
            _labelService.Delete(id);
            var labels = _labelService.GetAll();
            return View("List", labels);
        }
    }
}