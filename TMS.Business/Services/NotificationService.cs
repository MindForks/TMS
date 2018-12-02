using System;
using System.Net.Mail;
using TMS.EntitiesDTO;

namespace TMS.Business.Services
{
    public class NotificationService
    {
        private readonly UserService _userService;
        public NotificationService(UserService userService)
        {
            _userService = userService;
        }

        public void CreateMailsAndSend(TaskDTO task)
        {
            var notificationViewer = CreateNotification(task, "viewer");
            var notificationModerator = CreateNotification(task, "moderator");

            foreach (var viewerId in task.ViewerIDs)
            {
                var user = _userService.GetItemAsync(viewerId).Result.Email;
                SendMail(user, notificationViewer);
            }

            foreach (var moderatorId in task.ModeratorIDs)
            {
                var user = _userService.GetItemAsync(moderatorId).Result.Email;
                SendMail(user, notificationModerator);
            }
        }
        public void SendMail(string email, NotificationTypeDTO notification)
        {
            using (var message = new MailMessage("TMSnewtask@gmail.com", email))
            {
                message.To.Add(new MailAddress(email));
                message.From = new MailAddress("TMSnewtask@gmail.com");

                message.Subject = notification.Title;
                message.Body = notification.Message;

                using (var smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Credentials = new System.Net.NetworkCredential("tmsnewtask@gmail.com", "Kakoka911");
                    smtpClient.EnableSsl = true;
                    smtpClient.Port = 2525;
                    smtpClient.Send(message);
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
