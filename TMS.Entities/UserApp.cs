using Microsoft.AspNetCore.Identity;

namespace TMS.Entities
{
    public class UserApp : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
    }
}
