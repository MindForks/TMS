using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using TMS.Business.Services;
using TMS.Entities;
using TMS.EntitiesDTO;

namespace TMS.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly NotificationService _notificationService;

        public ForgotPasswordModel(UserManager<UserApp> userManager, IEmailSender emailSender, NotificationService notificationService)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _notificationService = notificationService;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(Input.Email);

                // For more information on how to enable account confirmation and password reset please 
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { code },
                    protocol: Request.Scheme);

                NotificationTypeDTO notification = new NotificationTypeDTO
                {
                    Title = "Reset Password",
                    Message = $"Please reset your password by link: {HtmlEncoder.Default.Encode(callbackUrl)}"
                };

                _notificationService.SendMail(Input.Email, notification);

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
        }
    }
}
