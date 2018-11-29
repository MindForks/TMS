using System.Net.Mail;
using TMS.EntitiesDTO;

namespace TMS.Business.Services
{
    public class NotificationService
    {
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
                    smtpClient.Send(message);
                }
            }
        }

        public NotificationTypeDTO CreateNotification(TaskDTO task, string role)
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
                $"Cheers," + "\n" +
                $"Task Manager System",
            };

            return notification;
        }

    }
}
