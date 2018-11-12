using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TMS.EntitiesDTO
{
    public class TaskDTO
    {
        public TaskDTO()
        {
            ModeratorIDs = new List<string>();
            ViewerIDs = new List<string>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Title must be defined")]
        public int Title { get; set; }

        [Required(ErrorMessage = "Description must be defined")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Weight must be defined")]
        public string Weight { get; set; }

        [Required(ErrorMessage = "EndDate must be defined")]
        public DateTimeOffset EndDate { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime ClosingTime { get; set; }

        [Display(Name = "Task moderators")]
        public ICollection<string> ModeratorIDs { get; set; }

        [Display(Name = "Task viewers")]
        public ICollection<string> ViewerIDs { get; set; }

        public TaskStatusDTO Status { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }
    }
}
