using Microsoft.AspNetCore.Identity;

namespace Final_Lahiye.Membership
{
    public class MUser : IdentityUser<int>
    {
        public string Name { get;set; }
    }
}
