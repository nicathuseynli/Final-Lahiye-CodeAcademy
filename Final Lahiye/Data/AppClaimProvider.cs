using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace Final_Lahiye.Data
{
    public class AppClaimProvider:IClaimsTransformation
    {
        private readonly AppDbContext _context;

        public AppClaimProvider(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity.IsAuthenticated && principal.Identity is ClaimsIdentity currentIdentity)
            {
                var userId = Convert.ToInt32(currentIdentity.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value);

                var user = await _context.Users.FirstOrDefaultAsync(c => c.Id == userId);
                if (user != null)
                {
                    currentIdentity.AddClaim(new Claim("Name",user.Name));
                }

                var role = currentIdentity.Claims.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Role));
                while (role != null)
                {
                    currentIdentity.RemoveClaim(role);
                    role = currentIdentity.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Role));
                }

                var currentRoles = (from ur in _context.UserRoles
                                    join r in _context.Roles on ur.RoleId equals r.Id
                                    where ur.UserId == userId
                                    select r.Name).ToArray();

                foreach (var roleName in currentRoles)
                {
                    currentIdentity.AddClaim(new Claim(ClaimTypes.Role, roleName));
                }
         
            }
            return principal;
        }
    }
}
