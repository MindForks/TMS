using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Entities
{
   public class Task
    {
        public Task()
        {
            Moderators = new List<TaskUser>();
            Viewers = new List<TaskUser>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Weight { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ClosingTime { get; set; }

        public ICollection<TaskUser> Moderators { get; set; }
        public ICollection<TaskUser> Viewers { get; set; }

        public TaskStatus Status { get; set; }
        public int StatusId { get; set; }
    }
}
