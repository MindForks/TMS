using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TMS.Entities
{
    public class UserApp : IdentityUser
    {
        public UserApp()
        {
            TaskUsers = new List<TaskUser>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<TaskUser> TaskUsers { get; set; }

        
    }
}