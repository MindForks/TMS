using System.ComponentModel.DataAnnotations;

namespace TMS.EntitiesDTO
{
    public class LabelDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage ="Name must be defined")]
        public string Title { get; set; }

        [Display(Name = "Color")]
        public string Color { get; set; }
    }
}
