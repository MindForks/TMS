using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TMS.EntitiesDTO
{
    public class UserAppDTO : IdentityUser
    {
        public UserAppDTO()
        {
            TaskUsers = new List<TaskUserDTO>();
        }
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }

        public ICollection<TaskUserDTO> TaskUsers { get; set; }

    }
}
