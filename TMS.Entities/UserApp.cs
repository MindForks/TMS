using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TMS.Entities
{
    public class UserApp : IdentityUser<int>
    {
        public UserApp()
        {
            TaskUsers = new List<TaskUser>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<TaskUser> TaskUsers { get; set; }

        
    }
}