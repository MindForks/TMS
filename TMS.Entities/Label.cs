using System;
using System.Collections.Generic;
using System.Text;

namespace TMS.Entities
{
    public class Label
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }
    }
}
