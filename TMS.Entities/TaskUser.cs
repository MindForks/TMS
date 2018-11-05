using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Entities
{
   public class TaskUser
    {
        public int UserId { get; set; }
        public UserApp User { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }
    }
}
