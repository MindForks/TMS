using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TMS.EntitiesDTO
{
    public class UserAppDTO:IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
    }
}
