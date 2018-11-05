using Microsoft.AspNetCore.Identity;

namespace TMS.EntitiesDTO
{
    public class UserAppDTO : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
