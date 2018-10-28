using Microsoft.AspNetCore.Identity;

namespace TMS.Web.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the User class
    public class UserApp : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
        [PersonalData]
        public string LastName { get; set; }
    }
}
