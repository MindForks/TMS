using System.Collections.Generic;

namespace TMS.Entities
{
    public class Label
    {
        public Label()
        {
            Tasks = new List<Task_Label>();
        }

        public int Id { get; set; }

        public UserApp User{ get; set; }
        public string UserId { get; set; }

        public string Title { get; set; }
        public string Color { get; set; }

        public ICollection<Task_Label> Tasks { get; set; }
    }
}
