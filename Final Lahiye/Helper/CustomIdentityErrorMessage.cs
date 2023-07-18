using Microsoft.AspNetCore.Identity;

namespace Final_Lahiye.Helper
{
    public class CustomIdentityErrorMessage:IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresNonAlphanumeric()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description ="You don't use nonalphanumeric symbol.Please use it !"
            };
        }
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = nameof(PasswordTooShort),
                Description = $"Your password length must be minimum {length}!"
            };
        }
    }
}
