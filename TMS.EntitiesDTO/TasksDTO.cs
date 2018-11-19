using System.Collections.Generic;

namespace TMS.EntitiesDTO
{
    public class TasksDTO
    {
        public IEnumerable<TaskDTO> TasksToDo { get; set; }
        public IEnumerable<TaskDTO> TasksInProgress { get; set; }
        public IEnumerable<TaskDTO> TasksDone { get; set; }
    }
}
