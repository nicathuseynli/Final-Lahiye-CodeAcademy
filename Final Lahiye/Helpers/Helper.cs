using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Final_Lahiye.Helpers
{
    public static class Helper
    {
        public static int? GetUserId(this ControllerContext controllerContext)
        {
            var identity = controllerContext.HttpContext.User.Identity;

            if (!identity.IsAuthenticated)
                return null;

            return Convert.ToInt32(controllerContext.HttpContext.User.Claims.FirstOrDefault(m => m.Type.Equals(ClaimTypes.NameIdentifier))?.Value);
        }
    }
}
