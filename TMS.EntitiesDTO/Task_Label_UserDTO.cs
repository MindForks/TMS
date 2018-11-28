using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.EntitiesDTO
{
    public class Task_Label_UserDTO
    {
        public int LabelId { get; set; }

        public int TaskId { get; set; }

        public string UserId { get; set; }
    }
}
