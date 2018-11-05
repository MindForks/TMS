using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TMS.Business.Services;
using TMS.EntitiesDTO;

namespace TMS.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IdentityService _identityService;

        public AccountController(IdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDTO model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _identityService.Register(model);
                    return View(model); // we need to return mainPage 
                }
                catch (AggregateException e)
                {
                    foreach (Exception ex in e.InnerExceptions)
                        ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(model);
        }
    }
}