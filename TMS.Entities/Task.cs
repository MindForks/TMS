using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Entities
{
    public class Task
    {
        public Task()
        {
            Moderators = new List<TaskModerator_User>();
            Viewers = new List<TaskViewer_User>();
            Labels = new List<Task_Label_User>();
        }

        public int Id { get; set; }
        public string CurrentUserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Weight { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public DateTimeOffset CreationTime { get; set; }
        public DateTimeOffset ClosingTime { get; set; }

        public ICollection<TaskModerator_User> Moderators { get; set; }
        public ICollection<TaskViewer_User> Viewers { get; set; }
        public ICollection<Task_Label_User> Labels { get; set; }

        public TaskStatus Status { get; set; }
        public int StatusId { get; set; }
    }
}
