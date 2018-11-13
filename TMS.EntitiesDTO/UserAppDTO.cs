using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TMS.EntitiesDTO
{
    public class UserAppDTO : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
    }
}
