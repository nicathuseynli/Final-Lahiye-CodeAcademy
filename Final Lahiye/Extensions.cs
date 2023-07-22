using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Final_Lahiye
{
    public static class Extensions
    {
        //email RegexExtation
        public static bool IsEmail(this string value)
        {
            return Regex.IsMatch(value, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static string GetPrincipalName(this ClaimsPrincipal principal)
        {
            string name = principal.Claims.FirstOrDefault(c => c.Type.Equals("name"))?.Value;

            if (!string.IsNullOrWhiteSpace(name))
            {
                return $"{name}";
            }
            return principal.Claims.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value;
        }
    }
}
