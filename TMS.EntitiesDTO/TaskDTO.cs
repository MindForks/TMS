using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TMS.EntitiesDTO
{
    public class TaskDTO
    {
        public TaskDTO()
        {
            Moderators = new List<UserAppDTO>();
            Viewers = new List<UserAppDTO>();
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
        [Required(ErrorMessage = "Content must be defined")]
        public ICollection<UserAppDTO> Moderators { get; set; }

        [Display(Name = "Task viewers")]
        [Required(ErrorMessage = "Content must be defined")]
        public ICollection<UserAppDTO> Viewers { get; set; }

        public string Status { get; set; }
    }
}
