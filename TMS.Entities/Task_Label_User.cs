using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Entities
{
    public class Task_Label_User
    {
        public int LabelId { get; set; }
        public Label Label { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }

        public string UserId { get; set; }
        public UserApp User { get; set; }
    }
}
