using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TMS.EntitiesDTO
{
    public class TaskDetailsDTO
    {
        public TaskDetailsDTO()
        {
            ModeratorIDs = new List<string>();
            ViewerIDs = new List<string>();
            LabelIDs = new List<int>();
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int Weight { get; set; }

        [Display(Name = "Task deadline")]
        public DateTimeOffset EndDate { get; set; }

        [Display(Name = "Task start")]
        public DateTimeOffset CreationTime { get; set; }

        [Display(Name = "Task moderators")]
        public ICollection<string> ModeratorIDs { get; set; }

        [Display(Name = "Task viewers")]
        public ICollection<string> ViewerIDs { get; set; }

        [Display(Name = "Label")]
        public ICollection<int> LabelIDs { get; set; }

        public TaskStatusDTO Status { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }
        
    }
}
