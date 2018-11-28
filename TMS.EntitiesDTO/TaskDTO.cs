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
            Labels = new List<Task_Label_UserDTO>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Title must be defined")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description must be defined")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Weight must be defined")]
        public string Weight { get; set; }

        [Display(Name = "Task deadline")]
        [Required(ErrorMessage = "Deadline must be defined")]
        public DateTimeOffset EndDate { get; set; }

        [Display(Name = "Task start")]
        public DateTimeOffset CreationTime { get; set; }

        [Display(Name = "Task moderators")]
        public ICollection<string> ModeratorIDs { get; set; }

        [Display(Name = "Task viewers")]
        public ICollection<string> ViewerIDs { get; set; }

        public TaskStatusDTO Status { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Display(Name = "Your own label")]
        public ICollection<Task_Label_UserDTO> Labels { get; set; }

        [Display(Name = "Your own label")]
        public int CurrentLabelID { get; set; }

    }
}
