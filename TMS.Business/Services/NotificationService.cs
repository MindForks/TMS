using System;
using System.Net.Mail;
using TMS.Entities;
using TMS.EntitiesDTO;
using System.Threading.Tasks;
namespace TMS.Business.Services
{
    public class NotificationService
    {
        private readonly UserService _userService;
        private readonly MailCredentionals _mailCred;
        public NotificationService(UserService userService, MailCredentionals mailCred)
        {
            _userService = userService;
            _mailCred = mailCred;
        }

        public async System.Threading.Tasks.Task CreateMailsAndSend(TaskDTO task)
        {
            var notificationViewer = CreateNotification(task, "viewer");
            var notificationModerator = CreateNotification(task, "moderator");

            foreach (var viewerId in task.ViewerIDs)
            {
                var user = _userService.GetItemAsync(viewerId).Result.Email;
                await SendMail(user, notificationViewer);
            }

            foreach (var moderatorId in task.ModeratorIDs)
            {
                var user = _userService.GetItemAsync(moderatorId).Result.Email;
                await SendMail(user, notificationModerator);
            }
        }
        public async System.Threading.Tasks.Task SendMail(string email, NotificationTypeDTO notification)
        {

            using (var message = new MailMessage(_mailCred.Login, email))
            {
                message.To.Add(new MailAddress(_mailCred.Login));
                message.From = new MailAddress(_mailCred.Login);

                message.Subject = notification.Title;
                message.Body = notification.Message;

                using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.Credentials = new System.Net.NetworkCredential(_mailCred.Login, _mailCred.Password);
                    smtpClient.EnableSsl = true;
                    smtpClient.Port = 587;
                    await smtpClient.SendMailAsync(message);
                }
            }
        }

        private NotificationTypeDTO CreateNotification(TaskDTO task, string role)
        {
            NotificationTypeDTO notification = new NotificationTypeDTO
            {
                Title = task.Title,
                Message =
                $"You are {role} on a new task" + "\n" +
                $"Title: {task.Title}" + "\n" +
                $"Description: {task.Description}" + "\n" +
                "\n" +
                $"Start Date: {task.CreationTime}" + "\n" +
                $"Deadline: {task.EndDate}" + "\n" +
                "\n" +
                $"Best wishes," + "\n" +
                $"Task Manager System",
            };

            return notification;
        }

    }
}
